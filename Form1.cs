using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Telephone_Book
{
    public partial class TelephoneBook : Form
    {
 
        DataTable dgv = new DataTable() { TableName = "xmlTable" };
 
        bool editing = false;

        private void WriteXmlToFile(DataTable thisDataTable)
        {
            if (thisDataTable == null) { return; }

            // Create a file name to write to.
            string filename = "XmlDoc.xml";

            // Write to the file with the WriteXml method.
            thisDataTable.WriteXml(filename);
        }

        public TelephoneBook()
        {
            InitializeComponent();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            firstNameTextBox.Text = "";
            lastnameTextBox.Text = "";
            phoneNumberTextBox.Text = "";
            emailTextBox.Text = "";
        }

        private void TelephoneBook_Load(object sender, EventArgs e)
        {
            dgv.Columns.Add("First Name");
            dgv.Columns.Add("Last Name");
            dgv.Columns.Add("Phone number");
            dgv.Columns.Add("Email");
            dgv.ReadXml("XmlDoc.xml");
            dataGridView1.DataSource = dgv;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                firstNameTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[0].ToString();
                lastnameTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[1].ToString();
                phoneNumberTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[2].ToString();
                emailTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[3].ToString();
                editing = true;
            }
            catch { }
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            firstNameTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[0].ToString();
            lastnameTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[1].ToString();
            phoneNumberTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[2].ToString();
            emailTextBox.Text = dgv.Rows[dataGridView1.CurrentCell.RowIndex].ItemArray[3].ToString();
            editing = true;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {  
            try
            {
                if (editing)
                {
                    dgv.Rows[dataGridView1.CurrentCell.RowIndex]["First Name"] = firstNameTextBox.Text;
                    dgv.Rows[dataGridView1.CurrentCell.RowIndex]["Last Name"] = lastnameTextBox.Text;
                    dgv.Rows[dataGridView1.CurrentCell.RowIndex]["Phone number"] = phoneNumberTextBox.Text;
                     dgv.Rows[dataGridView1.CurrentCell.RowIndex]["Email"] = emailTextBox.Text;
                }
                else dgv.Rows.Add(firstNameTextBox.Text, lastnameTextBox.Text, phoneNumberTextBox.Text, emailTextBox.Text);
                editing = false;
            }
            catch {}
            WriteXmlToFile(dgv);
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Rows[dataGridView1.CurrentCell.RowIndex].Delete();
                WriteXmlToFile(dgv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cant delete current entry");
            }
        }
        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }
        private void TelephoneBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteXmlToFile(dgv);
        }
    }
}
