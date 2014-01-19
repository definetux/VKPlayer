using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKPlayer
{
    public partial class Authorization : Form
    {
        private string login;

        private string password;

        private DialogResult result;


        public Authorization()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            login = txtLogin.Text;
            password = txtPassword.Text;
            result = DialogResult.OK;
            this.Close();
        }

        public  DialogResult ShowDialog()
        {
            base.ShowDialog();
            return result;
        }

        public string Login
        {
            get
            {
                return login;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Close();
        }
    }
}
