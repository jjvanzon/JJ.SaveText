using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using JJ.Apps.SetText.ViewModels;
using System.Text;
using JJ.Framework.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using JJ.Framework.Common;
using System.Reflection;

namespace JJ.Apps.SetText.AppService.Tests
{
    [TestClass]
    public class SetTextAppService_OverHttp_Tests
    {
        private const string HTTP_METHOD_POST = "POST";

        [TestMethod]
        public void Test_SetTextAppService_OverHttp_Save()
        {
            string url = "http://localhost:6371/settextappservice.svc";
            string soapAction = "http://tempuri.org/ISetTextAppService/Save";
            byte[] dataToSend = EmbeddedResourceHelper.GetEmbeddedResourceBytes(Assembly.GetExecutingAssembly(), "TestResources", "Save.xml");

            HttpWebRequest request = CreateSoapPost(url, soapAction, dataToSend);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string dataReceived = reader.ReadToEnd();
                        SetTextViewModel viewModel = ParseReceivedData(dataReceived);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_SetTextAppService_OverHttp_Save_WithValidationMessages()
        {
            string url = "http://localhost:6371/settextappservice.svc";
            string soapAction = "http://tempuri.org/ISetTextAppService/Save";
            byte[] dataToSend = EmbeddedResourceHelper.GetEmbeddedResourceBytes(Assembly.GetExecutingAssembly(), "TestResources", "Save_WithValidationMessages.xml");

            HttpWebRequest request = CreateSoapPost(url, soapAction, dataToSend);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string dataReceived = reader.ReadToEnd();
                        SetTextViewModel viewModel = ParseReceivedData(dataReceived);
                    }
                }
            }
        }

        private HttpWebRequest CreateSoapPost(string url, string soapAction, byte[] content)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_METHOD_POST;
            request.ContentLength = content.Length;
            request.ContentType = @"text/xml;charset=""utf-8""";
            request.Accept = "text/xml";
            request.Headers.Add("SOAPAction", soapAction);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(content, 0, content.Length);

            return request;
        }

        private SetTextViewModel ParseReceivedData(string data)
        {
            XNamespace def = "http://tempuri.org/";
            //XNamespace vm = "http://schemas.datacontract.org/2004/07/JJ.Apps.SetText.ViewModels";

            XElement root = XElement.Parse(data);
            XElement saveResult = root.Descendants(def + "SaveResult").Single();

            var converter = new XmlToObjectConverter<SetTextViewModel>(XmlCasingEnum.UnmodifiedCase);
            SetTextViewModel viewModel = converter.Convert(saveResult);
            return viewModel;
        }
    }
}
