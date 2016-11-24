using UnityEngine;

using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

using SoapTree = System.Collections.Generic.SimpleTree<System.Collections.DictionaryEntry>;
using SoapTreeNode = System.Collections.Generic.SimpleTreeNode<System.Collections.DictionaryEntry>;
using SoapTreeNodeList = System.Collections.Generic.SimpleTreeNodeList<System.Collections.DictionaryEntry>;

//13-06-2009 - veg - added support for plain text responses (see whois sample).  
//                   For this the Details parameter should not end in a '/'.
//           - veg - removed url from functions and now use SoapUrl property.
//12-06-2009 - veg - separated Soap Object from GUI.
//           - veg - made the sync version return the hashtable.
//15-06-2009 - veg - swapped Hashtable for ListDictionary because it retains order.
//           - veg - started on Bing Search API.
//           - veg - added parmwrapper for Bing to simulate hierachical data.
//16-06-2009 - veg - added SoapTree class to enable hieachical data.
//           - veg - added overloaded methods for various type of parameters.
//           - veg - added enum sinblings support to SoapTree.
//           - veg - return data as a SoapTree too instead of a Hashtable.
//17-06-2009 - veg - added clear() to SoapTreeNode class.
//           - veg - fixed double debugging output in async mode.
//           - veg - added code for parsing the soapfault envelope.

/// <summary>
/// Test script for the SoapObject 
///
/// Author: 	G.W. van der vegt
/// Version: 	1.0
/// Date:		17-06-2009
///
/// Usage: 
/// Add a SoapPackage to the project and an Empty GameObject that contains the GuiScript used for demoing purposes.
/// Either have the script search for the SoapPackage and the SoapObject script inside it at RunTime or
/// assign the SoapPackage to the Soapobject property of the GuiScript at designtime by dropping the SoapPackage object onto the inspector.
///
/// SoapObject uses SimpleTree from: 
/// 	This collection of non-binary tree data structures created by Dan Vanderboom.
/// 	Critical Development blog: http://dvanderboom.wordpress.com
/// 	Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
/// 	Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro
/// </summary>
public partial class SoapObject : MonoBehaviour
{
    /// <summary>
    /// The URL of the Soap Service.
    /// </summary>
    public string SoapUrl = "";

    /// <summary>
    /// The XPath Expression where the Soap Results can be found. 
    /// </summary>
    public string Details = "";
    
    /// <summary>
    /// The Name of the Service.
    /// </summary>
    public string Service = "";

    /// <summary>
    /// The URL of Namespace of the Soap Service.
    /// </summary>
    public string ServiceNS = "";

    /// <summary>
    /// The XPath Expression for Soap Faults.
    /// </summary>
    private const string SoapFault = "/soapenv:Envelope/soapenv:Body/soapenv:Fault";

    /// <summary>
    /// The XPath Expression for Soap Errors.
    /// </summary>
    private const string SoapErrors = "/soapenv:Envelope/soapenv:Body/soapenv:Fault/detail/tns:Errors/tns:Error";

    /// <summary>
    /// A Callback function that is the bridge between the Asynchronous call and the Application that passes the WWW results.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="error"></param>
    public delegate void Callback(string data, string error);

    /// <summary>
    /// Creates a Soap v1.0 Envelope by embedding the Service, ServiceNS and Parameters into a XML template.
    /// </summary>
    /// <param name="parameters">The Parameters to be used in the Soap Request</param>
    /// <returns>The Soap v1.0 Envelope</returns>
    private string CreateRequestEnvelop(string parameters)
    {

        //TODO Support Soap v1.2 also (see http://www.ecubicle.net/whois_service.asmx?op=Whois)

        string envelope_v10 =
            "<?xml version=" + '\"' + "1.0" + '\"' + " encoding=" + '\"' + "utf-8" + '\"' + "?>\r\n" +
            "<soap:Envelope xmlns:soap=" + '\"' + "http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>\r\n" +
                "<soap:Body>\r\n" +
                "<" + Service + " xmlns='" + ServiceNS + "'>\r\n" +
                    parameters +
                "</" + Service + ">\r\n" +
                "</soap:Body>\r\n" +
            "</soap:Envelope>\r\n";

        Debug.Log("Envelope:\r\n" + envelope_v10);

        return envelope_v10;
    }

    /// <summary>
    /// Helper function to PrettyPrint the Parameters into an idented string that can be used in the Soap Envelope.
    /// </summary>
    /// <param name="node">The SoapTreeNode to recurs.</param>
    /// <param name="msg">The formatted output string.</param>
    /// <summary>
    private string EnumTree(SoapTreeNode node)
    {
        string xml = "";

        //Enum & Count Children so we can generate <tag /> when there is no child or content.	
        int cnt = 0;
        foreach (SoapTreeNode child in node.Children)
        {
            xml = xml + EnumTree(child) + "\r\n";
            cnt++;
        }

        //Wrap Children if any in a suitable xml tag.
        if (node.Value.Key.ToString() != "")
        {

            //NOTE: If there are no Children we generate an empty (self closed) tag.
            if (cnt == 0 && (node.Value.Value == null || node.Value.Value.ToString() == ""))
            {
                xml = "<" + node.Value.Key + " />";
            }
            else
            {
                if (node.Value.Value == null || node.Value.Value.ToString() == "")
                {
                    xml = string.Format("<{0}>\r\n", node.Value.Key) + xml;
                }
                else
                {
                    xml = string.Format("<{0}>{1}", node.Value.Key, node.Value.Value) + xml;
                }
                xml = xml + "</" + node.Value.Key + ">";
            }
        }
        else
        {
            //NOTE: An empty Key singals that we skip this node in rendering. Used multi-rooted trees where the root's children are the actual roots.
        }

        return xml;
    }

    /// <summary>
    /// Creates a Soap Envelop from a SoapTree datastructure.
    /// </summary>
    /// <param name="parameters">The SoapTree to create the envelope from</param>
    /// <returns>The Soap Envelope</returns>
    private string CreateRequestEnvelop(SoapTree parameters)
    {
        return CreateRequestEnvelop(EnumTree(parameters));
    }

    /// <summary>
    /// Starts the actual WWW request by POSTing the Soap Request Envelope.
    /// </summary>
    /// <param name="envelope">The Soap Request Envelope to POST</param>
    /// <returns>a WWW Object</returns>
    private WWW StartRequest(string envelope)
    {
        //TODO Add Prefilled Hashtable for namespaces (like: ns=url).

        Hashtable headers = new Hashtable();

        //Official Content-Type "application/soap+xml;" might give a 415 Unsupported Media Type error !
        //headers["Content-Type"] = "application/soap+xml;"; 
        headers["Content-Type"] = "text/xml;";

        //Use the following line to 'fake' IE7 on WinXP SP2.
        //headers["User-Agent"] = "/4.0 (compatible; MSIE 7.0; Windows NT 5.1) ";

        // send the data via web 
        return new WWW(SoapUrl, System.Text.Encoding.UTF8.GetBytes(envelope), headers);
    }

    /// <summary>
    /// Performs an Synchronous Soap Request.
    /// </summary>
    /// <param name="parameters">The paramaters to build the Soap Request Envelope from</param>
    /// <param name="stop">The Timeout in seconds</param>
    /// <returns></returns>
    public SoapTree RequestSync(SoapTree parameters, float timeout)
    {
        return RequestSync(CreateRequestEnvelop(parameters), timeout);
    }

    /// <summary>
    /// Performs an Synchronous Soap Request.
    /// </summary>
    /// <param name="envelope">The Soap Request Envelope to POST</param>
    /// <param name="stop">The Timeout in seconds</param>
    /// <returns></returns>
    private SoapTree RequestSync(string envelope, float timeout)
    {
        Debug.Log("Start Sync URL request: " + Time.time.ToString() + " seconds");

        WWW www = StartRequest(envelope);

        float stop = Time.time + timeout;

        while ((!www.isDone) && (stop > Time.time))
        {
            StartCoroutine(IEWaitaSec(www, stop));
        }

        Debug.Log("Finished Sync URL request: " + Time.time.ToString() + " seconds");
        if (www.isDone)
        {
            if (www.error != "")
            {
                Debug.Log("[Status]");
                Debug.Log("Error=Ok");
            }
            else
            {
                Debug.Log("[Status]");
                Debug.Log("Error=" + www.error);
            }

            return ParseResponseEnvelope(www.data, www.error); // Executes Callback, passes in retrieved data		
        }
        else
        {
            Debug.Log("[Status]");
            Debug.Log("Timeout=" + stop);

            return new SoapTree();
        }
    }

    /// <summary>
    /// Performs an Asynchronous Soap Request.
    /// </summary>
    /// <param name="parameters">The paramaters to build the Soap Request Envelope from</param>
    /// <param name="results">The SoapTree in which to return the data.</param>
    /// <param name="stop">The Timeout in seconds</param>
    public void RequestAsync(SoapTree parameters, ref SoapTree results, float timeout)
    {
        RequestAsync(CreateRequestEnvelop(parameters), ref results, timeout);
    }

    /// <summary>
    /// Performs an Asynchronous Soap Request.
    /// </summary>
    /// <param name="envelope">The Soap Request Envelope to POST</param>
    /// <param name="results">The SoapTree in which to return the data.</param>
    /// <param name="stop">The Timeout in seconds</param>
    private void RequestAsync(string envelope, ref SoapTree results, float timeout)
    {
        Debug.Log("Start Async URL request: " + Time.time.ToString() + " seconds");

        //Does not wait for the request we need (so data is returned to early)!!
        StartCoroutine(IEWaitForData(StartRequest(envelope), results, Time.time + timeout));
    }

    /// <summary>
    /// An IEnumerator to facilitate Synchronous Http Calls.
    /// Note: this way of calling seems to be impossible in a browser application as it hangs the Browser.
    /// Note: Without the Debug.Log statement, the Synchronous call will also hang and crash the Unity3D development environment.
    /// </summary>
    /// <param name="www">The WWW object to wait for</param>
    /// <param name="stop">The Timeout in seconds</param>
    /// <returns>Nothing</returns>
    private IEnumerator IEWaitaSec(WWW www, float stop)
    {
        //NOTE: We need the Debug log or the program will hang... why?
        Debug.Log("I'm waiting... ");

        while ((!www.isDone) && (stop > Time.time))
        {
            //Yield simply returns values for the iterator!
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// An IEnumerator to facilitate Asynchronous Http Calls.
    /// </summary>
    /// <param name="www">The WWW object to wait for</param>
    /// <param name="results">The SoapTree structure to return the results into</param>
    /// <param name="stop">The Timeout in seconds</param>
    /// <returns>Nothing</returns>
    private IEnumerator IEWaitForData(WWW www, SoapTree results, float stop)
    {
        while ((!www.isDone) && (stop > Time.time))
        {
            //Yield simply returns values for the iterator!
            yield return new WaitForEndOfFrame();
        }

        if (!www.isDone)
        {
            // print message to the debug log 
            Debug.Log("[Status]");
            Debug.Log("Timeout=" + stop);
        }
        else if (www.error != null)
        {
            // print the server answer on the debug log 
            Debug.Log("[Status]");
            Debug.Log("Error=" + www.error);
        }
        else
        {
            Debug.Log("[Status]");
            Debug.Log("Error=Ok");
        }

        Debug.Log("Finished Async URL request: " + Time.time.ToString() + " seconds");

        //Use the following code to retrieve data only stored in the bytes property and not in the data property (due to encoding issues).
        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
        //enc.GetString(www.bytes)+		

        //An out/ref modifier for the ListDictionary is not allowed. So we add stuff to it silently.	
        SoapTree local = ParseResponseEnvelope(www.data, www.error);

        results.Root.Clear();
        results.Root.Value = new DictionaryEntry(local.Value.Key, local.Value.Value);

        foreach (SoapTreeNode result in local.Children)
        {
            results.Children.Add(new DictionaryEntry(result.Value.Key, result.Value.Value));
        }
    }

    /// <summary>
    /// Put global code here (whats put outside of functions in JScript. 
    /// </summary>
    public void Awake()
    {
        //Placeholder
    }

    /// <summary>
    /// Use this for initialization. 
    /// </summary>
    void Start()
    {
        //DebugConsole.IsOpen = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        //Placeholder
    }

    /// <summary>
    /// Walk an XmlNode recursively and copy the data into the SoapTree to be returned to the caller.
    /// </summary>
    /// <param name="node">The XmlNode to be recursed</param>
    /// <param name="results">The resulting SoapTree</param>
    private void EnumXml(XmlNode node, SoapTreeNode results)
    {
        SoapTreeNode treenode;

        string text = "";

        //XmlNodeList xmltext = node.SelectNodes("@text");
        foreach (XmlNode xmltxt in node.ChildNodes)
        {
            if (xmltxt.NodeType == XmlNodeType.Text)
            {
                text = text + xmltxt.Value;
            }
        }

        if (text == "")
        {
            treenode = results.Children.Add(new DictionaryEntry(node.Name, null));
        }
        else
        {
            treenode = results.Children.Add(new DictionaryEntry(node.Name, text));
        }
        string indent = "";
        for (int i = 0; i < treenode.Depth; i++)
        {
            indent = indent + "    ";
        }

        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                EnumXml(child, treenode);
            }
        }
    }

    /// <summary>
    /// Helper function to PrettyPrint the results into an idented string.
    /// </summary>
    /// <param name="results">The SoapTreeNode to recurs.</param>
    /// <param name="msg">The formatted output string.</param>
    private void EnumParmTree(SoapTreeNode results, ref string msg)
    {
        string indent = "";

        for (int i = 0; i < results.Depth; i++)
        {
            indent = indent + "    ";
        }

        if (results.Value.Key != null && results.Value.Key.ToString() != "")
        {
            msg = msg + indent + results.Value.Key + " = " + results.Value.Value + "\r\n";
        }

        foreach (SoapTreeNode result in results.Children)
        {
            EnumParmTree(result, ref msg);
        }
    }

    /// <summary>
    /// Parse the Soap Envelope for either SoapErrors or valid data.
    /// Return the data found at the Details XPATH expression as a SoapTree.
    /// </summary>
    /// <param name="data">The Xml in string format to parse.</param>
    /// <param name="error">The WWW errors of the http request.</param>
    /// <returns>The data found at the Details XPATH expression as a SoapTree.</returns>
    private SoapTree ParseResponseEnvelope(string data, string error)
    {
        //this.error = error;	
        SoapTree results = new SoapTree();

        Debug.Log("Response:\r\n" + data);

        //http://www.mono-project.com/XML#XmlReader_and_XmlWrier
        XmlDocument oDoc = new XmlDocument();
        oDoc.LoadXml(data);

        //Fails to work properly...
        XmlNamespaceManager nsMgr = new XmlNamespaceManager(oDoc.NameTable);
        nsMgr.AddNamespace("diffgr", "urn:schemas-microsoft-com:xml-diffgram-v1");
        nsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
        nsMgr.AddNamespace("tns", ServiceNS); 			//veg: was "", needed for whois sample, allows tns: prefix in xpath.
        nsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        nsMgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
        nsMgr.AddNamespace("msdata", "urn:schemas-microsoft-com:xml-msdata");
        nsMgr.AddNamespace("", ServiceNS);

        results.Clear();
        results.Value = new DictionaryEntry("", null);

        //TODO CHeck for Errors...
        XmlNode oFault = oDoc.SelectSingleNode(SoapFault, nsMgr);

        if (oFault != null)
        {
            XmlNodeList oErrors = oDoc.SelectNodes(SoapErrors, nsMgr);

            results.Root.Value = new DictionaryEntry("Errors", oErrors.Count);

            foreach (XmlNode oErrorNode in oErrors)
            {
                //dict.Children.Add(new DictionaryEntry(oListNode.Name, oListNode.InnerText));
                if (oErrorNode.NodeType == XmlNodeType.Element)
                {
                    EnumXml(oErrorNode, results);
                }
            }
        }
        else
        {
            XmlNodeList oList = oDoc.SelectNodes(Details + "/*", nsMgr);

            if (oList != null && oList.Count == 0)
            {
                XmlNode oNode = oDoc.SelectSingleNode(Details, nsMgr);
                //XmlNode text = oNode.SelectSingleNode("@text");
                string text = "";
                foreach (XmlNode txt in oNode.ChildNodes)
                {
                    if (txt.NodeType == XmlNodeType.Text || txt.NodeType == XmlNodeType.EntityReference)
                    {
                        text = text + txt.Value;
                    }
                }

                if (text != "")
                {
                    results.Root.Value = new DictionaryEntry("ChildCount", "1");
                    results.Children.Add(new DictionaryEntry("SoapResult", text));
                }
                else
                {
                    results.Root.Value = new DictionaryEntry("ChildCount", oList.Count.ToString());
                }
            }
            else
            {
                results.Root.Value = new DictionaryEntry("ChildCount", oList.Count.ToString());
            }

            foreach (XmlNode oListNode in oList)
            {
                EnumXml(oListNode, results);
            }
        }


        Debug.Log("[Results]");

        string msg = "";
        EnumParmTree(results.Root, ref msg);

        Debug.Log(msg);

        return results;
    }
}
