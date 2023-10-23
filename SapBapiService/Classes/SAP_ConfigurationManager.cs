using SAP.Middleware.Connector;
using System;
using ReadCredentialNET462;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace SapBapiService.Classes
{
    public class SAP_ConfigurationManager : IDestinationConfiguration
    {

        // This should not be changed - Specification for function and parameter names are defined in the SAP connector documentation
        public bool ChangeEventsSupported()
        {
            return true;
        }
        // This should not be changed - Specification for function and parameter names are defined in the SAP connector documentation
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public int SelectedFileNum
        {
            get; set;
        }

        // This should not be changed - Specification for function and parameter names are defined in the SAP connector documentation
        public RfcConfigParameters GetParameters(string DestinationName)
        {
            RfcConfigParameters parameters_ = new RfcConfigParameters
            {
                { RfcConfigParameters.AppServerHost, "10.48.72.10" },
                { RfcConfigParameters.SystemNumber, "00" },
                { RfcConfigParameters.Client, "600" }
            };

            var documnetsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var credentialPath = Path.Combine(documnetsPath, "Credentials");

            var UserNamePath = Path.Combine(credentialPath, "optisapuser.username");
            var PasswordPath = Path.Combine(credentialPath, "optisapuser.password");

            string SAPUserName;
            string SAPPassword;

            if (File.Exists(UserNamePath))
            {
                SAPUserName = File.ReadAllText(UserNamePath);
                SAPPassword = File.ReadAllText(PasswordPath);
            }
            else
            {
                Application.Run(new Form1());
                SAPUserName = Globals.UserName;
                SAPPassword = Globals.Password;

                using (StreamWriter writer = new StreamWriter(UserNamePath))
                {
                    writer.WriteLine(SAPUserName);
                }
                using (StreamWriter writer = new StreamWriter(PasswordPath))
                {
                    writer.WriteLine(SAPPassword);
                }
            }


            parameters_.Add(RfcConfigParameters.User, SAPUserName);
            parameters_.Add(RfcConfigParameters.Password, Encryption.Unprotect(SAPPassword, null, DataProtectionScope.CurrentUser));
            parameters_.Add(RfcConfigParameters.Name, "PEO");

            /*
            switch (SelectedFileNum)
            {
                case 1:

                    break;
                case 7:
                    parameters_.Add(RfcConfigParameters.User, SAPUserName);
                    parameters_.Add(RfcConfigParameters.Password, Encryption.Unprotect(SAPPassword, null, DataProtectionScope.CurrentUser));
                    parameters_.Add(RfcConfigParameters.Name, "PEO");

                    break;
                case 2:
                    parameters_.Add(RfcConfigParameters.User, SAPUserName);
                    parameters_.Add(RfcConfigParameters.Password, Encryption.Unprotect(SAPPassword, null, DataProtectionScope.CurrentUser));
                    parameters_.Add(RfcConfigParameters.Name, "PEO");

                    break;
            }
            */

            return parameters_;
        }
    }
}
