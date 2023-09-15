using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmCustomerMainMenu : Form
    {
        public frmCustomerMainMenu()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString();
        }

        private void frmCustomerMainMenu_Load(object sender, EventArgs e)
        {
          /**  string connectionString = "Data Source=VALDEMAR;Initial Catalog=POS;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ProductName, Data_Validade FROM Product WHERE DATEDIFF(day, GETDATE(), Data_Validade) <= 100";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nomeProduto = reader["ProductName"].ToString();
                            DateTime dataValidade = (DateTime)reader["Data_Validade"];
                            TimeSpan diff = dataValidade - DateTime.Today;
                            int diasRestantes = diff.Days;

                            MessageBox.Show($"O produto '{nomeProduto}' vai expirar daqui a  {diasRestantes} dias\n Por favor, tome alguma providência.", "Aviso de Prazo de Validade");
                        }
                    }
                }
            }  **/
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            frmUpdateCustomerDetails frm = new frmUpdateCustomerDetails();
            frm.textBox1.Text = lblUser.Text;
            frm.Getdata();
            frm.Show();
        }

        private void btnProfileEntry_Click(object sender, EventArgs e)
        {
            frmCustomerProfileEntry frm = new frmCustomerProfileEntry();
            frm.txtCustomerID.Text = lblUser.Text;
            frm.Show();
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            frmUpdateProfile frm = new frmUpdateProfile();
            frm.Getdata();
            frm.txtCustomerID.Text = lblUser.Text;
            frm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerProfileEntry o1 = new frmCustomerProfileEntry();
            o1.Hide();
            frmCustomerRegistration o2 = new frmCustomerRegistration();
            o2.Hide();
            frmUpdateCustomerDetails o3 = new frmUpdateCustomerDetails();
            o3.Hide();
            frmUpdateProfile o4 = new frmUpdateProfile();
            o4.Hide();
            frmCustomerOrders o5 = new frmCustomerOrders();
            o5.Hide();
            frmLogin frm = new frmLogin();
            frm.txtUserName.Text = "";
            frm.txtPassword.Text = "";
            frm.ProgressBar1.Visible = false;
            frm.txtUserName.Focus();
            frm.Show();
        }

        private void frmCustomerMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("De certeza que pretende sair?   ", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnTrackOrder_Click(object sender, EventArgs e)
        {
            frmCustomerOrders frm = new frmCustomerOrders();
            frm.txtCustomerID.Text = lblUser.Text;
            frm.Show();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            frmPlaceOrders frm = new frmPlaceOrders();
            frm.txtCustomerID.Text = lblUser.Text;
            frm.Getdata1();
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
