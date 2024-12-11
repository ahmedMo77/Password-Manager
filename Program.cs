using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PassManger.Csharp
{
    internal class Program
    {
        private static readonly Dictionary<string, string> _PasswordEntries = new();
        static void Main(string[] args)
        {
            ReadPassword();
            while (true)
            {
                Console.WriteLine("Please select opton: ");
                Console.WriteLine("1. List All Passwords");
                Console.WriteLine("2. Add or Change Password");
                Console.WriteLine("3. Get Password");
                Console.WriteLine("4. Delete Password");
                var choose = int.Parse(Console.ReadLine());
                switch(choose)
                {
                    case 1:
                        ListAllPasswords();
                        break;
                    case 2:
                        Add_or_ChangePassword();
                        break;
                    case 3:
                        GetPassword();
                        break;
                    case 4:
                        DeletePassword();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                Console.WriteLine("=============================================================");
            }
        }

        private static void ListAllPasswords()
        {
            foreach (var item in _PasswordEntries)
            {
                Console.WriteLine($"{item.Key}={item.Value}");
            }
        }

        private static void Add_or_ChangePassword()
        {
            Console.Write("Enter the site name: ");
            var appName = Console.ReadLine();
            Console.Write("Enter the password: ");
            var password = Console.ReadLine();

            if(_PasswordEntries.ContainsKey(appName))
            {
                _PasswordEntries[appName] = password;
            }
            else
            {
                _PasswordEntries.Add(appName, password);
            }
            SavePassword();
        }

        private static void GetPassword()
        {
            Console.Write("Enter the site name: ");
            var appName = Console.ReadLine();

            if (_PasswordEntries.ContainsKey(appName))
            {
                Console.WriteLine($"Yuor password is : '{_PasswordEntries[appName]}'");
            }
            else
            {
                Console.WriteLine("Password not found");
            }
        }

        private static void DeletePassword()
        {
            Console.Write("Enter the Application name : ");
            var appName = Console.ReadLine();

            if (_PasswordEntries.ContainsKey(appName))
            {
                _PasswordEntries.Remove(appName);
                SavePassword();
            }
            else
            {
                Console.WriteLine("Password not found");
            }
        }
        private static void ReadPassword()
        {
            if(File.Exists("passwords.txt"))
            {
                var PasswordLines = File.ReadAllText("passwords.txt");
                foreach (var line in PasswordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('=');
                        var appName = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);
                        _PasswordEntries.Add(appName, EncryptionUtility.Decrypt(password));
                    }
                }
            }
        }

        private static void SavePassword()
        {
            StringBuilder PasswordLines = new StringBuilder();
            foreach (var item in _PasswordEntries)
            {
                PasswordLines.AppendLine($"{item.Key}={EncryptionUtility.Encrypt(item.Value)}");
            }
            File.WriteAllText("passwords.txt", PasswordLines.ToString());
        }
    }
}
