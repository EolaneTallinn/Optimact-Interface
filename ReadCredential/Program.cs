using System.Net;
using System.Security.Cryptography.X509Certificates;
using ReadCredential.Classes;

namespace ReadCredential
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            Console.WriteLine(Globals.UserName);
            Console.WriteLine(Globals.Password);
            Console.WriteLine(new NetworkCredential("", Globals.Password).Password);

            using (StreamWriter writer = new StreamWriter("C:\\Users\\tn_merqe\\Documents\\ReadCredentials.txt"))
            {
                writer.WriteLine(Globals.UserName);
                writer.WriteLine(Globals.Password);
                writer.WriteLine(new NetworkCredential("", Globals.Password).Password);
            }

        }
    }
}