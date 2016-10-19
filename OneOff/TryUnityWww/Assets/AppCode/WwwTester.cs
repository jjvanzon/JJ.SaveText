using UnityEngine;
using System;
using System.Collections;
using System.Text;
using JJ.Framework.IO;


public class WwwTester : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		int WIDTH = 300;
		int LINE_HEIGHT = 24;
		int y = 0;
		if (GUI.Button(new Rect(0, y, WIDTH, LINE_HEIGHT), "Send Request"))
		{
			string url = "http://localhost:6371/SetTextAppService.svc";
			string operationName = "Show";
			string interfaceName = "ISetTextAppService";
			string soapAction = String.Format("http://tempuri.org/{0}/{1}", interfaceName, operationName);

			byte[] bytesToSend = GetBytesToSend ();

			Hashtable header = new Hashtable();
			header["Content-Length"] = bytesToSend.Length;
			header["Content-Type"] = @"text/xml;charset=""utf-8""";
			header["Accept"] = "text/xml";
			header["SOAPAction"] = soapAction;

			WWW www = new WWW(url, bytesToSend, header);

			int timeoutInSeconds = 30;
			
			float stopTime = Time.time + timeoutInSeconds;
			
			while (!www.isDone && Time.time <= stopTime)
			{
				StartCoroutine(IEWaitaSec(www, stopTime));
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
					return;
				}

				//byte[] responseBytes = www.data;
				//string responseString = Encoding.UTF8.GetString (responseBytes);

				string responseString = www.text;
				Debug.Log (responseString);
			}
			else
			{
				Debug.Log("[Status]");
				Debug.Log("Timeout=" + stopTime);
			}
		}
	}

	private byte[] GetBytesToSend()
	{
		string text = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
				<soapenv:Header/>
				<soapenv:Body>
					<tem:Show>
						<tem:cultureName>nl-NL</tem:cultureName>
					</tem:Show>
				</soapenv:Body>
			</soapenv:Envelope>";

		byte[] bytes = StreamHelper.StringToBytes (text, Encoding.UTF8);
		return bytes;
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
		// NOTE: We need the Debug log or the program will hang... why?
		Debug.Log ("I'm waiting... ");

		while ((!www.isDone) && (stop > Time.time)) {
				//Yield simply returns values for the iterator!
				yield return new WaitForFixedUpdate ();
		}
	}
}
