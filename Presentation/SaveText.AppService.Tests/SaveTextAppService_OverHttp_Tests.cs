using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Xml.Linq;
using JJ.Presentation.SaveText.Interface.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable

namespace JJ.Presentation.SaveText.AppService.Tests
{
    [TestClass]
    public class SaveTextAppService_OverHttp_Tests
    {
        private const string HTTP_METHOD_POST = "POST";

        [TestMethod]
        public void Test_SaveTextAppService_OverHttp_Save()
        {
            try
            {
                string url = AppSettingsReader<IAppSettings>.Get(x => x.SaveTextAppServiceUrl);
                string soapAction = "http://tempuri.org/ISaveTextAppService/Save";
                byte[] dataToSend = GetBytesToSendFromEmbeddedResource();

                //byte[] dataToSend = GetBytesToSendFromViewModel();

                HttpWebRequest request = CreateSoapRequest(url, soapAction, dataToSend);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream == null)
                        {
                            throw new NullException(nameof(responseStream));
                        }

                        using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            string dataReceived = reader.ReadToEnd();
                            SaveTextViewModel viewModel = ParseReceivedData(dataReceived);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }

        [TestMethod]
        public void Test_SaveTextAppService_OverHttp_Save_WithValidationMessages()
        {
            try
            {
                string url = "http://localhost:6371/savetextappservice.svc";
                string soapAction = "http://tempuri.org/ISaveTextAppService/Save";
                byte[] dataToSend = EmbeddedResourceHelper.GetEmbeddedResourceBytes(
                    Assembly.GetExecutingAssembly(),
                    "TestResources",
                    "Save_WithValidationMessages.xml");

                HttpWebRequest request = CreateSoapRequest(url, soapAction, dataToSend);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream == null) throw new NullException(nameof(responseStream));

                        using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            string dataReceived = reader.ReadToEnd();
                            SaveTextViewModel viewModel = ParseReceivedData(dataReceived);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }

        private HttpWebRequest CreateSoapRequest(string url, string soapAction, byte[] content)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_METHOD_POST;
            request.ContentLength = content.Length;
            request.ContentType = @"text/xml;charset=""utf-8""";
            request.Accept = "text/xml";
            request.Headers.Add("SOAPAction", soapAction);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(content, 0, content.Length);

            return request;
        }

        private byte[] GetBytesToSendFromEmbeddedResource() => EmbeddedResourceHelper.GetEmbeddedResourceBytes(
            Assembly.GetExecutingAssembly(),
            "TestResources",
            "Save.xml");

        private SaveTextViewModel ParseReceivedData(string data)
        {
            XNamespace def = "http://tempuri.org/";
            //XNamespace vm = "http://schemas.datacontract.org/2004/07/JJ.Presentation.SaveText.ViewModels";

            XElement root = XElement.Parse(data);
            XElement saveResult = root.Descendants(def + "SaveResult").Single();

            var converter = new XmlToObjectConverter<SaveTextViewModel>(XmlCasingEnum.UnmodifiedCase);
            SaveTextViewModel viewModel = converter.Convert(saveResult);
            return viewModel;
        }
    }
}