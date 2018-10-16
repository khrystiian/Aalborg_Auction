using AuctionProject.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionProject
{
    public partial class 
        LoginForm : Form
    {
        public string loginstatus { get; set; }

        ServiceReference1.IAuctionProjectService serv = new ServiceReference1.AuctionProjectServiceClient("binary");

        Form1 form1 = new Form1();
        public LoginForm()
        {
            InitializeComponent();
            PasswordTextField.PasswordChar = '*';
            PasswordTextField.MaxLength = 10;
            Size size = new Size(220, 42);
            PasswordTextField.Size = size;
            EmailTextField.Size = size;
            EmailTextField.Text = "diliev1996@gmail.com";
            PasswordTextField.Text = "test1";
        }

        public void HideThisFormAndShowNewOne(Form form)
        {
            this.Hide();
            form.Show();
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }

        public static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Account acc = serv.GetAccountByEmail(EmailTextField.Text);
            if (acc != null)
            {
                if (Verify(acc.Salt, acc.Password, PasswordTextField.Text))
                {
                    form1.Account = acc;
                    form1.Show();
                    this.Visible = false;
                }
                else
                    MessageBox.Show("Wrong password");
                }
            else
            {
                MessageBox.Show("Username does not exist");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            new RegistrationForm();
        }

        private void PasswordTextField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void EmailTextField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void EmailTextField_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HideThisFormAndShowNewOne(new RegistrationForm());
        }
    }
}