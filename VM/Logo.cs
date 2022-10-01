using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace VM
{
    internal class Logo
    {
        public static void logo()
        {
            Colorful.Console.WriteLine("");                                            
            Colorful.Console.WriteLine("                                       ╔═════════════════════════════╗                      ", Color.Orange);
            Colorful.Console.WriteLine("                                       ║       Coded By Pawan        ║                      ", Color.Orange);
            Colorful.Console.WriteLine("                                       ╚═════════════════════════════╝                      ", Color.Orange);
            Colorful.Console.WriteLine("* Use Only Yahoo Domain Combos For Best Performance", Color.Red);
            Colorful.Console.WriteLine("");
            Colorful.Console.WriteLine("");
        }

        public static void AutoLogin()
        {
            /*Yahoi.Connect();
            if (new FileInfo("..\\YAY\\User.txt").Length > 0L)
            {
                string[] strArray = File.ReadAllLines("..\\YAY\\User.txt");
                if (Yahoi.Login(strArray[0], strArray[1]))
                {
                    Colorful.Console.WriteLine("Success Login", Color.Green);
                    Thread.Sleep(1000);
                    Colorful.Console.Clear();
                    Logo.logo();
                    YAY_V2.IO.Start();
                }
                else
                {
                    Colorful.Console.WriteLine("Wrong Info", Color.Red);
                    Thread.Sleep(1000);
                    File.Delete("user.txt");
                    Colorful.Console.WriteLine("[1] Register");
                    Colorful.Console.WriteLine("[2] Login");
                    Colorful.Console.WriteLine("[3] Extend Subscription");
                    int int32 = Convert.ToInt32(Colorful.Console.ReadLine());
                    if (int32 == 2)
                        Logo.EnterInfo();
                    if (int32 == 1)
                        Logo.Register();
                    if (int32 != 3)
                        return;
                    Logo.Extend();
                }
            }
            else
            {
                Colorful.Console.WriteLine("No Info detected, Please login", Color.Red);
                Colorful.Console.Clear();
                Logo.logo();
                Colorful.Console.WriteLine("[1] Register");
                Colorful.Console.WriteLine("[2] Login");
                Colorful.Console.WriteLine("[3] Extend Subscription");
                int int32 = Convert.ToInt32(Colorful.Console.ReadLine());
                if (int32 == 2)
                    Logo.EnterInfo();
                if (int32 == 1)
                    Logo.Register();
                if (int32 != 3)
                    return;
                Logo.Extend();
            }
        }

        public static void EnterInfo()
        {
            Colorful.Console.Clear();
            Logo.logo();
            Colorful.Console.WriteLine("");
            Colorful.Console.Write("Username >> ", Color.LightGoldenrodYellow);
            string username = Colorful.Console.ReadLine();
            Colorful.Console.Write("\n");
            Colorful.Console.Write("Password >> ", Color.LightGoldenrodYellow);
            string password = Colorful.Console.ReadLine();
            Colorful.Console.Write("\n");
            if (Yahoi.Login(username, password))
            {
                File.WriteAllText("..\\YAY\\User.txt", username + "\n" + password);
                Colorful.Console.WriteLine("Login Success", Color.Green);
                Colorful.Console.Clear();
                Logo.logo();
                YAY_V2.IO.Start();
            }
            else
            {
                Colorful.Console.WriteLine("Wrong Info, Please re-login", Color.Red);
                Console.ReadKey();
                File.Delete("user.txt");
            }
        }

        public static void Register()
        {
            Colorful.Console.Clear();
            Logo.logo();
            Colorful.Console.WriteLine();
            Colorful.Console.Write("Username:");
            string username = Colorful.Console.ReadLine();
            Colorful.Console.Write("Password:");
            string str1 = Colorful.Console.ReadLine();
            Colorful.Console.Write("Email:");
            string str2 = Colorful.Console.ReadLine();
            Colorful.Console.Write("License:");
            string str3 = Colorful.Console.ReadLine();
            string password = str1;
            string email = str2;
            string license = str3;
            if (!Yahoi.Register(username, password, email, license))
                return;
            int num = (int)MessageBox.Show("You have successfully registered! Relaunch App", "Yahoi", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Environment.Exit(0);
        }

        public static void Extend()
        {
            Colorful.Console.Clear();
            Logo.logo();
            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Username:");
            string username = Colorful.Console.ReadLine();
            Colorful.Console.WriteLine("Password:");
            string str1 = Colorful.Console.ReadLine();
            Colorful.Console.WriteLine("Token:");
            string str2 = Colorful.Console.ReadLine();
            string password = str1;
            string license = str2;
            if (!Yahoi.ExtendSubscription(username, password, license))
                return;
            int num = (int)MessageBox.Show("You have successfully extended your subscription!", "Yahoi", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        public static void MethodLoggedIn()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();

                Console.WriteLine("1. Get User Info");
                Console.WriteLine("2. Get Variable");

                Console.Write("> ");

                switch (Console.ReadLine())
                {
                    case "1":

                        Console.WriteLine();

                        Console.WriteLine($"Username: {Yahoi.UserInfo.Username}");
                        Console.WriteLine($"Email: {Yahoi.UserInfo.Email}");
                        Console.WriteLine($"HWID: {Yahoi.UserInfo.HWID}");
                        Console.WriteLine($"Level: {Yahoi.UserInfo.Level}");
                        Console.WriteLine($"Expire-Date: {Yahoi.UserInfo.ExpireDate}");

                        break;


                    case "2":

                        Console.Write("\nVariable Secret-Code: ");

                        string secretKey = Console.ReadLine();

                        Console.WriteLine($"Variable Value: {Yahoi.GetVariable(secretKey)}");

                        break;
                }

                Console.WriteLine("\nEnter to return to the menu");
                Console.ReadLine();
            }*/
        }
    }
}
