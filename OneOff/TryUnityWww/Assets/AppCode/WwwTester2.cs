using UnityEngine;
using System;
using System.Collections;
using System.Text;
using JJ.Framework.IO;

public class WwwTester2 : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
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

			int timeoutInMilliseconds = 30000;

			Hashtable header = new Hashtable();
			header["Content-Length"] = bytesToSend.Length;
			header["Content-Type"] = @"text/xml;charset=""utf-8""";
			header["Accept"] = "text/xml";
			header["SOAPAction"] = soapAction;

			WWW www = new WWW(url, bytesToSend, header);

			System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

			while (!www.isDone)
			{ 
				bool isTimedOut = sw.ElapsedMilliseconds > timeoutInMilliseconds;
				if (isTimedOut)
				{
					Debug.Log(String.Format ("Timeout after {0} milliseconds.", timeoutInMilliseconds));
					return;
				}
			}
			
			Debug.Log("Finished Waiting for www.isDone == true: " + Time.time.ToString() + " seconds");
			if (www.error != "")
			{
				Debug.Log("Status = Ok");
			}
			else
			{
				Debug.Log("Error = " + www.error);
				return;
			}

			string responseString = www.text;
			Debug.Log (responseString);
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
}
