using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ReadCredentialNET462
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = buttonOK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Globals.UserName = textBoxUser.Text;
            Globals.Password = Encryption.Protect(textBoxPassword.Text, null, DataProtectionScope.CurrentUser);
            this.Close();
        }
    }
}
