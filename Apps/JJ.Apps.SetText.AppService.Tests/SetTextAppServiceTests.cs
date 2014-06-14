using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using JJ.Apps.SetText.ViewModels;
using System.Text;
using JJ.Framework.Xml;
using System.Xml;

namespace JJ.Apps.SetText.AppService.Tests
{
    [TestClass]
    public class SetTextAppServiceTests
    {
        private const string HTTP_METHOD_POST = "POST";

        [TestMethod]
        public void Test_SetTextAppService_OverHttp()
        {
            /*
            string url = "http://localhost:6371/settextappservice.svc";
           
            byte[] dataToSend = GetDataToSend();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_METHOD_POST;
            request.ContentLength = dataToSend.Length;
            request.ContentType = @"text/xml;charset=""utf-8""";
            request.Accept = "text/xml";
            request.Headers.Add("SOAPAction", "http://tempuri.org/ISetTextAppService/Save");

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(dataToSend, 0, dataToSend.Length);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string dataReceived = reader.ReadToEnd();
                        SetTextViewModel viewModel = ParseDataReceived(dataReceived);
                    }
                }
            }
            */
        }

        private byte[] GetDataToSend()
        {
            string text = @"
                <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" 
		                    xmlns:o=""http://tempuri.org/"" 
		                    xmlns:vm=""http://schemas.datacontract.org/2004/07/JJ.Apps.SetText.ViewModels"" 
		                    xmlns:c=""http://schemas.datacontract.org/2004/07/JJ.Models.Canonical"">
                   <s:Header/>
                   <s:Body>
                      <o:Save>
                         <o:viewModel>
                            <vm:SyncSuccessfulMessageVisible>false</vm:SyncSuccessfulMessageVisible>
                            <vm:Text>Hi</vm:Text>
                            <vm:TextWasSavedButNotYetSynchronized>false</vm:TextWasSavedButNotYetSynchronized>
                            <vm:TextWasSavedMessageVisible>false</vm:TextWasSavedMessageVisible>
                         </o:viewModel>
                      </o:Save>
                   </s:Body>
                </s:Envelope>";

            return Encoding.UTF8.GetBytes(text);
        }

        private SetTextViewModel ParseDataReceived(string data)
        {
            var converter = new XmlToObjectConverter<SetTextViewModel>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            return converter.Convert(doc);
        }
    }
}
