using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace VM
{
    internal class IO
    {
        [STAThread]
        private static void Main()
        {
            Logo.logo();
            Colorful.Console.Title = Colorful.Console.Title = "[ Yahoi Checker | Made by CrackerShadow#2814 | For Yahoi Enterprices ]";
            /*OnProgramStart.Initialize("Yahoo By @CrackerShadow", "839849", "xKT3AxkOc7wl2pfjOOmWAisbYtUoFEcD2Tn", "1.0");
            if (!ApplicationSettings.Status)
            {
                int num = (int)MessageBox.Show("Application is disabled!", OnProgramStart.Name, (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Hand);
                Colorful.Console.ReadKey();
                Environment.Exit(0);
            }*/
            try
            {
                Logo.AutoLogin();
            }
            catch (Exception ex)
            {
                Colorful.Console.WriteLine((object)ex);
                Colorful.Console.ReadKey();
            }
        }

        public static void Start()
        {
            Colorful.Console.Write("Do You Want to seen Checked Accounts on console?(Y/N): ", System.Drawing.Color.SkyBlue);
            Checker.show = Colorful.Console.ReadLine().ToUpper();
            Colorful.Console.WriteLine("Selct Your API's", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 1 ] Amazon_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 2 ] CoinBase_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 3 ] Discord_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 4 ] Apple_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 2 ] CoinBase_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 2 ] CoinBase_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 2 ] CoinBase_VM", System.Drawing.Color.SkyBlue);
            Colorful.Console.WriteLine("[ 2 ] CoinBase_VM", System.Drawing.Color.SkyBlue);
            Checker.api = Convert.ToInt32(Colorful.Console.ReadLine());
            Colorful.Console.Write("\nEnter ProxyTimeout in ms(10000 default If HQ proxy 5000): ", System.Drawing.Color.SkyBlue);
            try
            {
                Checker.timeout = Convert.ToInt32(Colorful.Console.ReadLine());
            }
            catch
            {
                Checker.timeout = 10000;
            }
            Colorful.Console.Write("\nHow many ", System.Drawing.Color.SkyBlue);
            Colorful.Console.Write("THREADS", System.Drawing.Color.SkyBlue);
            Colorful.Console.Write(" you want to use", System.Drawing.Color.SkyBlue);
            Colorful.Console.Write(": ", System.Drawing.Color.SkyBlue);
            try
            {
                Checker.threads = int.Parse(Colorful.Console.ReadLine());
            }
            catch
            {
                Checker.threads = 100;
            }
            while (true)
            {
                Colorful.Console.Write("What type of ", System.Drawing.Color.GreenYellow);
                Colorful.Console.Write("PROXIES ", System.Drawing.Color.IndianRed);
                Colorful.Console.Write("do you wanna use? [ HTTP | SOCKS4 | SOCKS5 ]", System.Drawing.Color.GreenYellow);
                Colorful.Console.WriteLine(": ", System.Drawing.Color.GreenYellow);
                Checker.proxytype = Colorful.Console.ReadLine().ToUpper();
                if (Checker.proxytype != "HTTP" && Checker.proxytype != "SOCKS4" & Checker.proxytype != "SOCKS5")
                {
                    Colorful.Console.Write("\n Dude...stop joking and select a Proxie type -.-' \n\n", System.Drawing.Color.Red);
                    Thread.Sleep(1200);
                }
                else
                    break;
            }
            Checker.StartTitle();
            Colorful.Console.WriteLine();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string fileName1;
            do
            {
                Colorful.Console.Write(" ~ Load Your", System.Drawing.Color.DarkGoldenrod);
                Colorful.Console.Write(" COMBOS ", System.Drawing.Color.Gold);
                Thread.Sleep(500);
                openFileDialog.Title = "Select Your Combolist";
                openFileDialog.DefaultExt = "txt";
                openFileDialog.Filter = "Text files|*.txt";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.ShowDialog();
                fileName1 = openFileDialog.FileName;
            }
            while (!File.Exists(fileName1));
            Checker.ComboQueue = new ConcurrentQueue<string>((IEnumerable<string>)File.ReadAllLines(fileName1));
            Checker.total = Checker.ComboQueue.Count;
            if (Checker.proxytype != "PROXYLESS")
            {
                string fileName2;
                do
                {
                    Colorful.Console.WriteLine();
                    Colorful.Console.WriteLine();
                    Colorful.Console.Write(" ~ Load Your", System.Drawing.Color.DarkGoldenrod);
                    Colorful.Console.Write(" PROXIES ", System.Drawing.Color.Gold);
                    Thread.Sleep(500);
                    openFileDialog.Title = "Select Your Proxies";
                    openFileDialog.DefaultExt = "txt";
                    openFileDialog.Filter = "Text files|*.txt";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.ShowDialog();
                    fileName2 = openFileDialog.FileName;
                }
                while (!File.Exists(fileName2));
                Checker.ProxyList = new List<string>((IEnumerable<string>)File.ReadAllLines(fileName2));
                Checker.proxytotal = Checker.ProxyList.Count;
            }
            Console.Clear();
            Logo.logo();
            Colorful.Console.Write("\n                                   [ ", System.Drawing.Color.Gainsboro);
            Colorful.Console.Write("Total Combos: ", System.Drawing.Color.Firebrick);
            Colorful.Console.Write(Checker.total.ToString() + " ", System.Drawing.Color.Firebrick);
            Colorful.Console.Write(" / ", System.Drawing.Color.Gainsboro);
            Colorful.Console.Write("Total Proxies: ", System.Drawing.Color.Firebrick);
            Colorful.Console.Write(Checker.proxytotal, System.Drawing.Color.Firebrick);
            Colorful.Console.Write(" ]\n ", System.Drawing.Color.Gainsboro);
            Checker.StartWorker(Checker.threads);
            int num1 = (int)MessageBox.Show("Work Done", "Completed", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Asterisk);
            Colorful.Console.ReadLine();
            Environment.Exit(0);
        }

        internal static void start() => throw new NotImplementedException();
    }
}
