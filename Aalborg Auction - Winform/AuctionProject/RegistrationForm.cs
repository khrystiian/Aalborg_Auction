using AuctionProject.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionProject
{
    public partial class RegistrationForm : Form
    {
       IAuctionProjectService service = new AuctionProjectServiceClient("binary");  
        
        public RegistrationForm()
        {
            InitializeComponent();
        }
        public void HideThisFormAndShowNewOne(Form form)
        {
            this.Hide();
            form.Show();
        }

        private void RegFormBack_Click(object sender, EventArgs e)
        {
            HideThisFormAndShowNewOne(new LoginForm());
        }

        private void RegFormRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (PasswordBox.Text.Equals("") || PasswordConfirm.Text.Equals("") || EmailBox.Text.Equals("") || FnameBox.Text.Equals("") || LnameBox.Text.Equals("") || AddressBox.Text.Equals("") || PhoneBox.Text.Equals(""))
                {
                    this.Show();
                    MessageBox.Show("Please do not leave empty fields !");

                }

               else if (!PasswordBox.Text.Equals(PasswordConfirm.Text))
                {
                    this.Show();
                    MessageBox.Show("Please insert correct password !");
                    PasswordBox.Text = "";
                    PasswordConfirm.Text = "";
                }
                else
                {
                    HideThisFormAndShowNewOne(new Form1());
                    Account account = new Account { Email = EmailBox.Text,
                    Fname = FnameBox.Text, Lname = LnameBox.Text,
                    Address = AddressBox.Text, PhoneNumber = PhoneBox.Text,
                    Password = PasswordBox.Text, Balance = 0 };
                account.UserName = account.Email;
                account = HashAndSaltPassword(account);
                service.AddAccount(account);
                }
            }
            catch (Exception ex)
            {
              // MessageBox.Show("An account with that email already exists.");
                MessageBox.Show(ex.Message);
            }
        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }



        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{
        //    checkBox1.Checked = false;
        //    string email = textBox3.Text;
        //    Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //    Match match = reg.Match(email);

        //    if (match.Success)
        //    {
        //        checkBox1.Checked = true;
        //    }
        //    else
        //    {
        //        checkBox1.Checked = false;
        //    }

        //}

        //private void textBox7_TextChanged(object sender, EventArgs e)
        //{
        //    checkBox5.Checked = false;
        //    string phonenumber = textBox7.Text;
        //    Regex reg2 = new Regex(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
        //    Match match = reg2.Match(phonenumber);

        //    if (match.Success)
        //    {
        //        checkBox5.Checked = true;
        //    }else
        //    {
        //        checkBox5.Checked = false;
        //    }
        //}

        //private void textBox4_TextChanged(object sender, EventArgs e)
        //{
        //    checkBox2.Checked = false;
        //    string firstname = textBox4.Text;
        //    Regex reg3 = new Regex("");
        //    Match match3 = reg3.Match(firstname);
        //    if (match3.Success)
        //    {
        //        checkBox2.Checked = true;
        //    }
        //    else
        //    {
        //        checkBox2.Checked = false;

        //    }
        //}

        //private void textBox5_TextChanged(object sender, EventArgs e)
        //{
        //    checkBox3.Checked = false;
        //    string lastname = textBox5.Text;
        //    Regex reg4 = new Regex("");
        //    Match match4 = reg4.Match(lastname);
        //    if (match4.Success)
        //    {
        //        checkBox3.Checked = true;
        //    }
        //    else
        //    {
        //        checkBox3.Checked = false;

        //    }
        //}

        //private void textBox6_TextChanged(object sender, EventArgs e)
        //{
        //    checkBox5.Checked = false;
        //    string address = textBox6.Text;
        //    Regex reg5 = new Regex("");
        //    Match match5 = reg5.Match(address);
        //    if (match5.Success)
        //    {
        //        checkBox4.Checked = true;
        //    }
        //    else
        //    {
        //        checkBox4.Checked = false;

        //    }
        //}

        //private void passwordField_TextChanged(object sender, EventArgs e)
        //{
        //    PasswordCheckb1.Checked = false;
        //    string pass = passwordField.Text;
        //    Regex reg6 = new Regex("");
        //    Match match6 = reg6.Match(pass);
        //    if (match6.Success)
        //    {
        //        PasswordCheckb1.Checked = true;

        //    }
        //    else
        //    {
        //        PasswordCheckb1.Checked = false;
        //    }
        //}
        public Account HashAndSaltPassword(Account acc)
        {
            // creates salt with random vales
            byte[] saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            acc.Salt = Convert.ToBase64String(saltBytes);
            // Create the Rfc2898DeriveBytes and get the hash value
            acc.Password = LoginForm.ComputeHash(acc.Salt, acc.Password);
            return acc;
        }
    }


}
