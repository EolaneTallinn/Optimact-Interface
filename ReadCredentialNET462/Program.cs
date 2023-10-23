using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ReadCredentialNET462
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



            using (StreamWriter writer = new StreamWriter("C:\\Users\\tn_merqe\\Documents\\ReadCredentials.txt"))
            {
                writer.WriteLine(Globals.UserName);
                writer.WriteLine(Globals.Password);
                writer.WriteLine(Encryption.Unprotect(Globals.Password, null, DataProtectionScope.CurrentUser));
            }
        }
    }
}
