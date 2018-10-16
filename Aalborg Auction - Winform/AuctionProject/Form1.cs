using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using AuctionProject.ServiceReference1;

namespace AuctionProject
{
    public partial class Form1 : Form
    {
        ServiceReference1.IAuctionProjectService service = new ServiceReference1.AuctionProjectServiceClient("binary");
        public Account Account { get; set; }
        string currentValue = "";

        public Form1()
        {
            InitializeComponent();
            dataGridView2.DataSource = service.GetAllAuctionsWithObjects();
        }

        public void DisableColumns(DataGridView dataView)
        {
            for (int i = 0; i < dataView.Columns.Count; i++)
            {
                dataView.Columns[i].ReadOnly = true;
            }
        }


        private void fillPanel1()
        {

            DataGridViewTextBoxColumn dgvId = new DataGridViewTextBoxColumn();
            dgvId.HeaderText = "Id";

            DataGridViewImageColumn dgvimage = new DataGridViewImageColumn();
            dgvimage.HeaderText = "Image";
            dgvimage.ImageLayout = DataGridViewImageCellLayout.Stretch;

            DataGridViewTextBoxColumn dgvSP = new DataGridViewTextBoxColumn();
            dgvSP.HeaderText = "Starting Price";

            DataGridViewTextBoxColumn dgvDS = new DataGridViewTextBoxColumn();
            dgvDS.HeaderText = "Description";

            DataGridViewTextBoxColumn dgvC = new DataGridViewTextBoxColumn();
            dgvC.HeaderText = "Category";

            DataGridViewTextBoxColumn dgLB = new DataGridViewTextBoxColumn();
            dgLB.HeaderText = "Last Bid by";

            dataGridView4.Columns.Add(dgvimage);
            dataGridView4.Columns.Add(dgvId);
            dataGridView4.Columns.Add(dgvSP);
            dataGridView4.Columns.Add(dgvDS);
            dataGridView4.Columns.Add(dgvC);
            dataGridView4.Columns.Add(dgLB);


            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.RowTemplate.Height = 120;

            dataGridView4.AllowUserToAddRows = false;

            DataGridViewTextBoxColumn dgvId2 = new DataGridViewTextBoxColumn();
            dgvId2.HeaderText = "Id";

            DataGridViewImageColumn dgvimage2 = new DataGridViewImageColumn();
            dgvimage2.HeaderText = "Image";
            dgvimage2.ImageLayout = DataGridViewImageCellLayout.Stretch;

            DataGridViewTextBoxColumn dgvSP2 = new DataGridViewTextBoxColumn();
            dgvSP2.HeaderText = "Starting Price";

            DataGridViewTextBoxColumn dgvDS2 = new DataGridViewTextBoxColumn();
            dgvDS2.HeaderText = "Description";

            DataGridViewTextBoxColumn dgvC2 = new DataGridViewTextBoxColumn();
            dgvC2.HeaderText = "Category";

            DataGridViewTextBoxColumn dgLB2 = new DataGridViewTextBoxColumn();
            dgLB2.HeaderText = "Last Bid by";


            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 120;
            dataGridView2.AllowUserToAddRows = false;

            DataGridViewTextBoxColumn dgvDS3 = new DataGridViewTextBoxColumn();
            dgvDS3.HeaderText = "E-mail";

            DataGridViewTextBoxColumn dgvFN = new DataGridViewTextBoxColumn();
            dgvFN.HeaderText = "First Name";

            DataGridViewTextBoxColumn dgvLN = new DataGridViewTextBoxColumn();
            dgvLN.HeaderText = "Last Name";

            DataGridViewTextBoxColumn dgvAD = new DataGridViewTextBoxColumn();
            dgvAD.HeaderText = "Address";

            DataGridViewTextBoxColumn dgvPN = new DataGridViewTextBoxColumn();
            dgvPN.HeaderText = "Phone Number";


            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.RowTemplate.Height = 120;
            dataGridView3.AllowUserToAddRows = false;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm lf = new LoginForm();
            lf.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            RegistrationForm regform = new RegistrationForm();
            regform.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ItemTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:/Users/krist/Desktop/lenovo-laptop-ideapad-y410p-front-1");
        }

        private void ItemNameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            LoginForm lf = new LoginForm();
            lf.Show();


        }

        private void LinkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            this.Close();
            RegistrationForm regform = new RegistrationForm();
            regform.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose image (*.jpg; *.png; *.gif)| *.jpg; *.png; *.gif ";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
                byte[] img = ms.ToArray();
                dataGridView4.Rows.Add(img, itemnametext4.Text, StartingPriceTextbox.Text, RichtextboxDescr.Text, CategoryTextBox.Text);
                dataGridView2.Rows.Add(img, itemnametext4.Text, StartingPriceTextbox.Text, RichtextboxDescr.Text, CategoryTextBox.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void AccountLinkedLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccountForm af = new AccountForm(Account);
            af.Show();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            try
            {
            List<ServiceReference1.Auction> auction = new List<ServiceReference1.Auction>();
            auction.Add(service.GetAuctionById(int.Parse(textBox1.Text)));

                dataGridView2.DataSource = auction;
                textBox1.Text = "";
            }

            catch
            {
               
                textBox1.Text = "";
            }
        }

        private void itemnametext4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            int rowindex = dataGridView3.CurrentCell.RowIndex;
            int columnindex = dataGridView3.CurrentCell.ColumnIndex;

            string select = dataGridView3.Rows[rowindex].Cells[columnindex].Value.ToString();
            textBox2.Text = select;
            }
            catch
            {

            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadFromDB_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void LoadFromDB_Click_1(object sender, EventArgs e)
        {
            dataGridView3.DataSource = service.GetAllAccounts();
        }

        private void SearchB_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void LoadAllAuctions_Click(object sender, EventArgs e)
        {


        }


     

        private void productLoadbtn_Click(object sender, EventArgs e)
        {
            try { dataGridView5.DataSource = service.GetAllProducts(); }

            catch
            {

            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView2.DataSource = service.GetAllAuctions();
        }

        private void SearchB_Click_1(object sender, EventArgs e)
        {
            try
            {

            List<ServiceReference1.Account> account = new List<ServiceReference1.Account>();
            account.Add(service.GetAccountByEmail(textBox2.Text));

                dataGridView3.DataSource = account;
            }

            catch
            {
              
                textBox2.Text = "";
            }
        }

        private void productSearchbtn_Click(object sender, EventArgs e)
        {
            try
            {
            List<ServiceReference1.Product> product = new List<ServiceReference1.Product>();
            product.Add(service.GetProductById(int.Parse(textBox3.Text)));

                dataGridView5.DataSource = product;
                textBox3.Text = "";
            }

            catch
            {
                textBox3.Text = "";
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
            List<ServiceReference1.Account> account = new List<ServiceReference1.Account>();
            account.Add(service.GetAccountById(int.Parse(textBox2.Text)));

                dataGridView3.DataSource = account;
            }

            catch 
            {
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product ?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int i = service.RemoveProductById(int.Parse(textBox3.Text));
                    textBox3.Text = "";
                    dataGridView5.DataSource = service.GetAllProducts();
                }
            }
            catch 
            {
               
            }
            //try
            //{
            //    service.RemoveProductById(int.Parse(textBox2.Text));
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Invalid ID");
            //    textBox2.Text = "";
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this auction ?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int i = service.RemoveAuctionById(int.Parse(textBox1.Text));
                    textBox1.Text = "";
                    dataGridView2.DataSource = service.GetAllAuctions();
                }
            }
            catch
            {
               
            }
            //try
            //{
            //    service.RemoveAuctionById(int.Parse(textBox1.Text));
            //    textBox1.Text = "";
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Invalid ID");
            //    textBox1.Text = "";
            //}
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            dataGridView6.DataSource = service.GetAllCategories();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (CategoryNameBox.Text.Length > 0)
            {
                Category category = new Category() { Name = CategoryNameBox.Text };
                service.AddCategory(category);
                button6_Click_1(null, null);
                CategoryNameBox.Text = "";
            }
            else
            {
                MessageBox.Show("You have to insert a name !");
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = tabControl1.SelectedIndex;
            switch (i)
            {
                case 0:
                    dataGridView6.DataSource = service.GetAllAuctions();
                    DisableColumns(dataGridView6);
                    break;
                case 1:
                    dataGridView5.DataSource = service.GetAllProducts();
                    DisableColumns(dataGridView5);
                    break;
                case 2:
                    dataGridView3.DataSource = service.GetAllAccounts();
                    DisableColumns(dataGridView3);
                    break;
                case 3:
                    dataGridView6.DataSource = service.GetAllCategories();
                    DisableColumns(dataGridView6);
                    break;

                default:
                    break;
            }

        }

        private void CategoryNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
            Category cat = service.GetCategoryByName(currentValue);
            cat.Name = CategoryNameBox.Text;
                int i = service.UpdateCategory(cat);
                    dataGridView6.DataSource = service.GetAllCategories();
            }
            catch 
            {
                
            }
          
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this category ?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int i = service.RemoveCategoryByName(CategoryNameBox.Text);
                    CategoryNameBox.Text = "";
                    dataGridView6.DataSource = service.GetAllCategories();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The category has products in it so it can't be deleted.");
            }
        }
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnNumber = dataGridView6.SelectedCells[0].ColumnIndex;           
            if (!dataGridView6.Columns[columnNumber].HeaderText.Equals("Id")) {
               currentValue = CategoryNameBox.Text = dataGridView6.SelectedCells[0].Value.ToString();       
            }
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            int rowindex = dataGridView5.CurrentCell.RowIndex;
            int columnindex = dataGridView5.CurrentCell.ColumnIndex;

            string select = dataGridView5.Rows[rowindex].Cells[columnindex].Value.ToString();
            textBox3.Text = select;
            }
            catch
            {

            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            int rowindex = dataGridView2.CurrentCell.RowIndex;
            int columnindex = dataGridView2.CurrentCell.ColumnIndex;

            string select = dataGridView2.Rows[rowindex].Cells[columnindex].Value.ToString();
            textBox1.Text = select;
            }
            catch
            {
              
            }
            }

        private void dataGridView2_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}