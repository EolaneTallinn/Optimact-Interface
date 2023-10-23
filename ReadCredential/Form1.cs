using ReadCredential.Classes;
using System.Net;

namespace ReadCredential
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Globals.UserName = textBoxUser.Text;
            Globals.Password = new NetworkCredential("", textBoxPassword.Text).SecurePassword;
            this.Close();
        }
    }
}