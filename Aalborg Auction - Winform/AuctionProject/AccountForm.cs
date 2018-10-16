using AuctionProject.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionProject
{
    public partial class AccountForm : Form
    {
        public AccountForm(Account acc)
        {
            InitializeComponent();
            UsernameLabel.Text += acc.UserName;
            EmailLabel.Text += acc.Email;
            FirstNameLabel.Text += acc.Fname;
            LastNameLabel.Text += acc.Lname;
            AddressLabel.Text += acc.Address;
            PhoneLabel.Text += acc.PhoneNumber;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
