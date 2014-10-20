using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Threading;
//using JJ.Framework.IO;
//using JJ.Framework.Logging;

public class WwwTester5 : MonoBehaviour 
{
	private int _width = 200;
	private int _lineHeight = 24;
	private int _spacing = 10;
	private int _textBoxHeight = 160;
	private GUIStyle _labelStyle;

	private AsyncSoapMessageSender _asyncMessageSender;
	private string _resultString = "";
	private Exception _lastException;

	// Use this for initialization
	void Start () 
	{
		_labelStyle = new GUIStyle ();
		_labelStyle.fontSize = 14;
		_labelStyle.normal.textColor = new Color (255, 255, 255);
		_labelStyle.wordWrap = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		try
		{
			if (_lastException != null)
			{
				// JJ.Framework.Logging does not workon Windows Phone 8, because it references something out of System.Diagnostics that is not supported
				//string exceptionMessage = ExceptionHelper.FormatExceptionWithInnerExceptions(_lastException, includeStackTrace: true);
				string exceptionMessage = _lastException.Message;

				GUI.Label (new Rect(0, 0, 580, 3000), exceptionMessage);
				if (GUI.Button (new Rect(580, 0, 100, _lineHeight), "Clear"))
				{
					_lastException = null;
				}
				return;
			}

			int y = _spacing;

			GUI.Label (new Rect(_spacing, y, _width, _lineHeight), "SOAP Test", _labelStyle);

			y += _lineHeight;
			y += _spacing;

			if (GUI.Button(new Rect(_spacing, y, _width, _lineHeight), "Send Request"))
			{
				SendMessage_Async_UsingGameLoop();
				//SendMessage_Async_UsingThread();
			}
			y += _lineHeight;
			y += _spacing;

			if (_asyncMessageSender != null)
			{
				_resultString = _asyncMessageSender.TryGetResponse();
				if (!String.IsNullOrEmpty(_resultString))
				{
					_asyncMessageSender = null;
				}
			}

			if (_resultString != null)
			{
				GUI.Label (new Rect(_spacing, y, 400, 600), _resultString, _labelStyle);
			}
		}
		catch (Exception ex)
		{
			_lastException = ex;
		}
	}

	private void SendMessage_Async_UsingGameLoop()
	{
		string url = "http://83.82.26.17:6371/SetTextAppService.svc";
		string operationName = "Show";
		string interfaceName = "ISetTextAppService";
		string soapAction = String.Format("http://tempuri.org/{0}/{1}", interfaceName, operationName);
		int timeoutInMilliseconds = 3000;
		
		string stringToSend = GetStringToSend();
		_asyncMessageSender = new AsyncSoapMessageSender(url, soapAction, stringToSend, timeoutInMilliseconds);
	}

	private void SendMessage_Async_UsingThread()
	{
		Thread thread = new Thread (SendMessage);
		thread.Start ();
	}

	private void SendMessage()
	{
		string url = "http://83.82.26.17:6371/SetTextAppService.svc";
		string operationName = "Show";
		string interfaceName = "ISetTextAppService";
		string soapAction = String.Format("http://tempuri.org/{0}/{1}", interfaceName, operationName);
		int timeoutInMilliseconds = 3000;
		
		string stringToSend = GetStringToSend();
		_resultString = AppServiceHelper.SendSoapMessage (url, soapAction, stringToSend, timeoutInMilliseconds);
	}

	private string GetStringToSend()
	{
		string text = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
				<soapenv:Header/>
				<soapenv:Body>
					<tem:Show>
						<tem:cultureName>nl-NL</tem:cultureName>
					</tem:Show>
				</soapenv:Body>
			</soapenv:Envelope>";

		return text;
	}

	private byte[] GetBytesToSend()
	{
		string text = GetStringToSend ();
		// JJ.Framework.IO does not work on Windows Phone 8, because it uses an overload not supported on Windows Phone 8.
		//byte[] bytes = StreamHelper.StringToBytes (text, Encoding.UTF8);
		byte[] bytes = Encoding.UTF8.GetBytes (text);
		return bytes;
	}
}
