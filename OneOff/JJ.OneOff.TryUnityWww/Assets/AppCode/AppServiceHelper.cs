using UnityEngine;
using System;
using System.Collections;
using JJ.Framework.IO;
using System.Text;

public static class AppServiceHelper
{
	public static string SendSoapMessage(string url, string soapAction, string stringToSend)
	{
		int timeoutInMilliseconds = 30000;

		byte[] bytesToSend = StreamHelper.StringToBytes(stringToSend, Encoding.UTF8);
		
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
				throw new Exception(String.Format ("Timeout after {0} milliseconds.", timeoutInMilliseconds));
			}
		}
		
	    if (String.IsNullOrEmpty(www.error))
		{
			string responseString = www.text;
			return responseString;
		}
		else
		{
			throw new Exception("www.error: " + www.error);
		}
	}
}
