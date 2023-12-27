using System;
using System.IO;
using System.Net;
using System.Text;

namespace Moka.Tool.Translate
{
    public class Google
    {
        public static string Translate(string text, IWebProxy webProxy = null)
        {
            try
            {
                var escapeDataString = Uri.EscapeDataString(text);
                string postString = $"f.req=%5B%5B%5B%22MkEWBc%22%2C%22%5B%5B%5C%22{escapeDataString}%5C%22%2C%5C%22en%5C%22%2C%5C%22ko%5C%22%2Ctrue%5D%2C%5Bnull%5D%5D%22%2Cnull%2C%22generic%22%5D%5D%5D&at=AO-hD9zzF83sx1ETo0DMdxsSSWTq%3A1677735594424&";

                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(new Cookie("SID", "TwhWm5gXXgC2WlK4bvdHLFeyEr7Y_ZeoKoYWSJ1tImlmqTpDSlyvL6viNtxe6a163tnbhw.", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("__Secure-1PSID", "TwhWm5gXXgC2WlK4bvdHLFeyEr7Y_ZeoKoYWSJ1tImlmqTpDb3eh4wIYFw5pSKrb-r6ckQ.", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("__Secure-3PSID", "TwhWm5gXXgC2WlK4bvdHLFeyEr7Y_ZeoKoYWSJ1tImlmqTpDZUCommoxYFqcz_qVtqMbeQ.", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("HSID", "AfZqOoYFL-kKrrEal", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("SSID", "AmviZcYCNEvj_fROg", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("APISID", "Jqjl_fcHhBAhCD05/AmqQYYNxN9G52PulX", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("SAPISID", "LieadaPaXkqcmFRX/AVgdiL3EEw_O97mTN", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("__Secure-1PAPISID", "LieadaPaXkqcmFRX/AVgdiL3EEw_O97mTN", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("__Secure-3PAPISID", "LieadaPaXkqcmFRX/AVgdiL3EEw_O97mTN", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("_ga", "GA1.4.213608567.1677723639", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("_gid", "GA1.4.1445649879.1677723639", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("OTZ", "6923661_20_20__20_", "/", "translate.google.co.kr"));
                cookieContainer.Add(new Cookie("NID", "511=aah0LX6j_V1o5cuziQe0XSH051fud42HjjWSKXuRETF9ptxVvFDSlzulxGHqxJBlD5UCrhn99ubV5wHxLDRNsluHHFzn2M-JlTPNkNBNODasDczxZR0N_YFjChg9ACrlGnrJonujcQr7yuNKw7TG7kU4RPaQsjJUIUolLqBRpTC82swJ", "/", "translate.google.co.kr"));

                var request = WebRequest.Create("https://translate.google.co.kr/_/TranslateWebserverUi/data/batchexecute?rpcids=AVdN8&source-path=%2F&f.sid=7654638798699141828&bl=boq_translate-webserver_20230228.08_p0&hl=ko&soc-app=1&soc-platform=1&soc-device=1&_reqid=1452796&rt=c") as HttpWebRequest;
                request.Method = "POST";
                request.CookieContainer = cookieContainer;
                request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                request.Host = "translate.google.co.kr";
                if (webProxy != null)
                {
                    request.Proxy = webProxy;
                }

                using (Stream stream = request.GetRequestStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(postString);
                    stream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            var result = reader.ReadToEnd();

                            string value = null;
                            string start = "null,null,null,[[\\\"";
                            var push = result.IndexOf(start);
                            value = result.Substring(push + start.Length);

                            string end = "\\\"";
                            var cut = value.IndexOf(end);
                            string translate = value.Substring(0, cut);

                            if (text.ToLower() == translate.ToLower())
                            {
                                return null;
                            }
                            return translate;
                        }
                    }
                }
            }
            catch (WebException exception)
            {
                using (WebResponse response = exception.Response)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
