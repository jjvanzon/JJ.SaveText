using UnityEngine;

using System.Collections;

using SoapTree = System.Collections.Generic.SimpleTree<System.Collections.DictionaryEntry>;
using SoapTreeNode = System.Collections.Generic.SimpleTreeNode<System.Collections.DictionaryEntry>;
using SoapTreeNodeList = System.Collections.Generic.SimpleTreeNodeList<System.Collections.DictionaryEntry>;

//NOTE: 	No need to clear the results as it's either overwritten in Sync Mode or Cleared just before 
//	        overwriting it in Async Mode. If you want to clear it manually, use the following code:
//
//			results.Clear();
//			results.Value = new DictionaryEntry("",null);
//
//NOTE: Single parameter (tree) can be build of the Root value. Code example:
//
//			SoapTree parameters = new SoapTree(new DictionaryEntry("ZipCode","20500"));
//
//NOTE: A Single rooted tree is build like: 
//
//			SoapTree parameters = new SoapTree(new DictionaryEntry("parameters",null));
//		
//			parameters.Children.Add(new DictionaryEntry("Query", "Opensock"));
//			parameters.Children.Add(new DictionaryEntry("AppId",BingAppId));
//				//NOTE: Key, null signals wrapper tag.
//				SoapTreeNode sources = parameters.Children.Add(new DictionaryEntry("Sources",null));
//				sources.Children.Add(new DictionaryEntry("SourceType","Web"));
//			parameters.Children.Add(new DictionaryEntry("Image", ""));
//			
//NOTE: Emulate MultiRoot Tree by using an empty Key value. Code:
//
//			SoapTree parameters = new SoapTree(new DictionaryEntry("",null));
//
//			parameters.Children.Add(new DictionaryEntry("servername","whois.tucows.com"));
//			parameters.Children.Add(new DictionaryEntry("port","43"));
//			parameters.Children.Add(new DictionaryEntry("domain","www.ecubicle.net"));
//

/// <summary>
/// Test script for the SoapObject 
///
/// Author: 	G.W. van der vegt
/// Version: 	1.0
/// Date:		17-06-2009
///
/// SoapObject uses SimpleTree from: 
/// 	This collection of non-binary tree data structures created by Dan Vanderboom.
/// 	Critical Development blog: http://dvanderboom.wordpress.com
/// 	Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
/// 	Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro
/// </summary>
public class GuiScript : MonoBehaviour
{

    /// <summary>
    /// The SoapTree to hold the Soap Results of a Soap Request.
    /// </summary>
    public SoapTree results = new SoapTree();

    /// <summary>
    /// TODO Make this public so we can attach a soap object too...
    /// </summary>
    public GameObject soapobject = null;

    /// <summary>
    /// TODO Make this public so we can attach a soap object too...
    /// </summary>
    private SoapObject soap = null;

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

		if (soapobject==null) {
			soapobject = GameObject.Find("SoapPackage");
		} else {
            Debug.Log("Designtime Soap Object!");
		}
		
        soap = (SoapObject)soapobject.GetComponent(typeof(SoapObject));
        if (soap != null)
        {
            Debug.Log("Found Soap Script in Soap Object!");

            soap.SoapUrl = "";
            soap.Details = "";
            soap.Service = "";
            soap.ServiceNS = "";

        }
        else
        {
            Debug.Log("Failed to locate Soap Script!");
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        //Placeholder
    }

    /// <summary>
    /// -
    /// </summary>		
    public void OnApplicationQuit()
    {
        //Placeholder
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
        } if (results.Value.Key != null && results.Value.Key.ToString() != "")

            foreach (SoapTreeNode result in results.Children)
            {
                EnumParmTree(result, ref msg);
            }
    }

    /// <summary>
    /// The OnGUI method generates four buttons featuring various types of Soap Services and calling methods.
    /// - Calling methods are either Sync or Async.
    /// - The services return either a single piece of data, a single record or an array of records. 
    /// - Parameters and Results can can be hierachical in case of a record or array of records.
    /// </summary>
    public void OnGUI()
    {
        int y = 10;
        int h = 30;

        if (GUI.Button(new Rect(10, y, 150, h), "Sync Zipcode Request"))
        {
            soap.SoapUrl = "http://www.jasongaylord.com/webservices/zipcodes.asmx";
            soap.Details = "//diffgr:diffgram/NewDataSet/Details";
            soap.Service = "ZipCodeToDetails";
            soap.ServiceNS = "http://www.jasongaylord.com/webservices/zipcodes";

            SoapTree parameters = new SoapTree(new DictionaryEntry("ZipCode", "20500"));

            results = soap.RequestSync(parameters, 10);
        }
        y += h + 5;

        if (GUI.Button(new Rect(10, y, 150, h), "Async Zipcode Request"))
        {
            soap.SoapUrl = "http://www.jasongaylord.com/webservices/zipcodes.asmx";
            soap.Details = "//diffgr:diffgram/NewDataSet/Details";
            soap.Service = "ZipCodeToDetails";
            soap.ServiceNS = "http://www.jasongaylord.com/webservices/zipcodes";

            //NOTE: Single parameter (tree) can be build of the Root value.
            SoapTree parameters = new SoapTree(new DictionaryEntry("ZipCode", "20500"));

            soap.RequestAsync(parameters, ref results, 10);
        }
        y += h + 5;

        if (GUI.Button(new Rect(10, y, 150, h), "Sync WhoIs Request"))
        {
            soap.SoapUrl = "http://www.ecubicle.net/whois_service.asmx?op=Whois";
            soap.Details = "//tns:WhoisResponse/tns:WhoisResult";
            soap.Service = "Whois";
            soap.ServiceNS = "http://www.ecubicle.net/webservices/";

            //NOTE: Emulate MultiRoot Tree by using an empty Key value.
            SoapTree parameters = new SoapTree(new DictionaryEntry("", null));

            parameters.Children.Add(new DictionaryEntry("servername", "whois.tucows.com"));
            parameters.Children.Add(new DictionaryEntry("port", "43"));
            parameters.Children.Add(new DictionaryEntry("domain", "www.ecubicle.net"));

            results = soap.RequestSync(parameters, 10);
        }
        y += h + 5;

        //Please substitue your own Bing Developers Key here...
        string BingAppId = "ENTER YOUR OWN";

        if (GUI.Button(new Rect(10, y, 150, h), "Sync Bing Request"))
        {
            soap.SoapUrl = "http://api.search.live.net/soap.asmx";
            soap.Details = "//tns:parameters/tns:Web/tns:Results";
            soap.Service = "SearchRequest";
            soap.ServiceNS = "http://schemas.microsoft.com/LiveSearch/2008/03/Search";

            SoapTree parameters = new SoapTree(new DictionaryEntry("parameters", null));

            parameters.Children.Add(new DictionaryEntry("Query", "Opensock"));
            parameters.Children.Add(new DictionaryEntry("AppId", BingAppId));
            //NOTE: Key, null signals wrapper tag.
            SoapTreeNode sources = parameters.Children.Add(new DictionaryEntry("Sources", null));
            sources.Children.Add(new DictionaryEntry("SourceType", "Web"));
            parameters.Children.Add(new DictionaryEntry("Image", ""));
            parameters.Children.Add(new DictionaryEntry("Phonebook", ""));
            parameters.Children.Add(new DictionaryEntry("Video", ""));
            parameters.Children.Add(new DictionaryEntry("News", ""));
            parameters.Children.Add(new DictionaryEntry("MobileWeb", ""));

            results = soap.RequestSync(parameters, 10);
        }
        y += h + 5;

        Rect clientrect = new Rect();

        clientrect.x = 10;
        clientrect.y = y;
        clientrect.width = 400;
        clientrect.height = 20;
        y += 20 + 5;

        GUI.Label(clientrect, soap.SoapUrl);

        clientrect.x = 10;
        clientrect.y = y;
        clientrect.width = 400;
        clientrect.height = 20;
        y += 20 + 5;

        if (results != null)
        {
            GUI.Label(clientrect, results.ToString());
        }

        clientrect.x = 10;
        clientrect.y = y;
        clientrect.width = 400;
        clientrect.height = 200;
        y += 50 + 5;

        if (results != null)
        {
            string msg = "";

            EnumParmTree(results, ref msg);

            GUI.Label(clientrect, msg);
        }
    }
}

