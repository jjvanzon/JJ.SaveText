using UnityEngine;
using System;
using System.Collections;
using System.Text;
using JJ.Framework.IO;

public class WwwTester3 : MonoBehaviour 
{
	private int _width = 200;
	private int _lineHeight = 24;
	private int _spacing = 10;
	private int _textBoxHeight = 160;
	private GUIStyle _labelStyle;

	private string _resultString = "";

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
		int y = _spacing;

		GUI.Label (new Rect(_spacing, y, _width, _lineHeight), "SOAP Test", _labelStyle);

		y += _lineHeight;
		y += _spacing;

		if (GUI.Button(new Rect(_spacing, y, _width, _lineHeight), "Send Request"))
		{
			//string url = "http://localhost:6371/SetTextAppService.svc";
			string url = "http://83.82.26.17:6371/SetTextAppService.svc";
			string operationName = "Show";
			string interfaceName = "ISetTextAppService";
			string soapAction = String.Format("http://tempuri.org/{0}/{1}", interfaceName, operationName);
			int timeoutInMillsecods = 10000;

			string stringToSend = GetStringToSend();
			try
			{
				_resultString = AppServiceHelper.SendSoapMessage(url, soapAction, stringToSend, timeoutInMillsecods);
				Debug.Log("Status = Ok");
				Debug.Log(_resultString);
			}
			catch (Exception ex)
			{
				_resultString =  ex.Message;
				Debug.Log (_resultString);
			}
		}
		y += _lineHeight;
		y += _spacing;

		GUI.Label (new Rect(_spacing, y, 400, 600), _resultString, _labelStyle);
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
		byte[] bytes = StreamHelper.StringToBytes (text, Encoding.UTF8);
		return bytes;
	}
}
