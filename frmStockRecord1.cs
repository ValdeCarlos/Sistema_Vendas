using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmStockRecord1 : Form
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmStockRecord1()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT RTRIM(StockID),RTRIM(StockDate),RTRIM(Product.ProductID),RTRIM(ProductName),RTRIM(Features),RTRIM(Supplier.SupplierID),RTRIM(SupplierName),RTRIM(Quantity) from Stock,Product,Supplier where Stock.ProductID=Product.ProductID and Stock.SupplierID=Supplier.SupplierID order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewwl.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewwl.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5],rdr[6],rdr[7]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmStockRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT RTRIM(StockID),RTRIM(StockDate),RTRIM(Product.ProductID),RTRIM(ProductName),RTRIM(Features),RTRIM(Supplier.SupplierID),RTRIM(SupplierName),RTRIM(Quantity) from Stock,Product,Supplier where Stock.ProductID=Product.ProductID and Stock.SupplierID=Supplier.SupplierID and productname like '" + txtProductname.Text + "%' order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridViewwl.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridViewwl.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6],rdr[7]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridViewwl_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string rowNumber = (e.RowIndex + 1).ToString();
            SizeF textSize = e.Graphics.MeasureString(rowNumber, dataGridViewwl.Font);

            if (dataGridViewwl.RowHeadersWidth < Convert.ToInt32(textSize.Width + 20))
            {
                dataGridViewwl.RowHeadersWidth = Convert.ToInt32(textSize.Width + 20);
            }

            using (Brush brush = new SolidBrush(dataGridViewwl.RowHeadersDefaultCellStyle.ForeColor))
            {
                float yPos = e.RowBounds.Location.Y + (e.RowBounds.Height - textSize.Height) / 2;
                e.Graphics.DrawString(rowNumber, dataGridViewwl.Font, brush, e.RowBounds.Location.X + 15, yPos);
            }
        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewwl.Rows.Count)
                {
                    DataGridViewRow selectedRow = dataGridViewwl.Rows[e.RowIndex];

                    frmStock frm = new frmStock();
                    frm.txtStockID.Text = selectedRow.Cells[0].Value.ToString();
                    frm.dtpStockDate.Value = Convert.ToDateTime(selectedRow.Cells[1].Value);
                    frm.txtProductID.Text = selectedRow.Cells[2].Value.ToString();
                    frm.txtProductName.Text = selectedRow.Cells[3].Value.ToString();
                    frm.txtFeatures.Text = selectedRow.Cells[4].Value.ToString();
                    frm.txtSupplierID.Text = selectedRow.Cells[5].Value.ToString();
                    frm.cmbSupplierName.Text = selectedRow.Cells[6].Value.ToString();
                    frm.txtQty.Text = selectedRow.Cells[7].Value.ToString();
                    frm.txtQty1.Text = selectedRow.Cells[7].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.btnDelete.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.label8.Text = label1.Text;

                    this.Hide();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmStockRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = label1.Text;
            frm.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = dataGridViewwl.RowCount - 1;
                colsTotal = dataGridViewwl.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridViewwl.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridViewwl.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtProductname.Text = "";
            dtpStockDateFrom.Text = System.DateTime.Today.ToString();
            dtpStockDateTo.Text = System.DateTime.Today.ToString();
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs.DBConn))
                {
                    con.Open();
                    string sql = "SELECT RTRIM(StockID), RTRIM(StockDate), RTRIM(Product.ProductID), RTRIM(ProductName), RTRIM(Features), RTRIM(Supplier.SupplierID), RTRIM(SupplierName), RTRIM(Quantity) FROM Stock, Product, Supplier WHERE Stock.ProductID = Product.ProductID AND Stock.SupplierID = Supplier.SupplierID AND StockDate BETWEEN @d1 AND @d2 ORDER BY ProductName";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@d1", SqlDbType.DateTime).Value = dtpStockDateFrom.Value.Date;
                        cmd.Parameters.Add("@d2", SqlDbType.DateTime).Value = dtpStockDateTo.Value.Date;

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            dataGridViewwl.Rows.Clear();
                            while (rdr.Read())
                            {
                                dataGridViewwl.Rows.Add(rdr["StockID"], rdr["StockDate"], rdr["ProductID"], rdr["ProductName"], rdr["Features"], rdr["SupplierID"], rdr["SupplierName"], rdr["Quantity"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
