using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Diagnostics;
using JJ.Framework.IO;

public class AsyncSoapMessageSender
{
	private string _url;
	private string _soapAction;
	private string _stringToSend;
	private int _timeoutInMilliseconds;
	
	// These fields are assigned when sending the message.
	
	private Stopwatch _sw;
	private WWW _www;
	
	public AsyncSoapMessageSender(string url, string soapAction, string stringToSend, int timeoutInMilliseconds)
	{
		if (url == null) throw new ArgumentNullException ("url");
		if (soapAction == null) throw new ArgumentNullException ("soapAction");
		if (stringToSend == null) throw new ArgumentNullException ("stringToSend");
		
		_url = url;
		_soapAction = soapAction;
		_stringToSend = stringToSend;
		_timeoutInMilliseconds = timeoutInMilliseconds;
		
		BeginSendMessage ();
	}
	
	private void BeginSendMessage()
	{
		byte[] bytesToSend = StreamHelper.StringToBytes(_stringToSend, Encoding.UTF8);
		
		Hashtable header = new Hashtable();
		header["Content-Length"] = bytesToSend.Length;
		header["Content-Type"] = @"text/xml;charset=""utf-8""";
		header["Accept"] = "text/xml";
		header["SOAPAction"] = _soapAction;
		
		_www = new WWW(_url, bytesToSend, header);
		
		_sw = Stopwatch.StartNew();
	}
	
	public string TryGetResponse()
	{
		bool isTimedOut = _sw.ElapsedMilliseconds > _timeoutInMilliseconds;
		if (isTimedOut)
		{
			throw new Exception(String.Format ("Timeout after {0} milliseconds.", _timeoutInMilliseconds));
		}
		
		if (!String.IsNullOrEmpty(_www.error))
		{
			throw new Exception("www.error: " + _www.error);
		}
		
		if (_www.isDone) 
		{
			string responseString = _www.text;
			return responseString;
		}
		else
		{
			return null;
		}
	}
}