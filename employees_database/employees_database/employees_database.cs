// Datoon, Philip Bryan B.
// 131311399
// 04 September 2013
// Using SQL Server Express and database in Windows form (pp 310-340)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace employees_database
{
    public partial class employees_database : Form
    {
        public employees_database()
        {
            InitializeComponent();
        }

        // declaring connection variable
        System.Data.SqlServerCe.SqlCeConnection con;

        // fills dataset with records from database
        System.Data.SqlServerCe.SqlCeDataAdapter da;

        // holds all data when pulled from database table
        DataSet ds1 = new DataSet();

        int MaxRows = 0;    // holds number of rows in a DataSet
        int inc = 0;        // uses to increment or to change current row number

        private void Form1_Load(object sender, EventArgs e)
        {   // instantiating SqlCeConnection object
            con = new System.Data.SqlServerCe.SqlCeConnection();

            // holds location of the database
            con.ConnectionString = "Data Source=C:\\Users\\131311399\\Documents\\databases\\Employees.sdf";

            // opens connection
            con.Open();

            // holds SQL query
            String sql = "SELECT * from tbl_employees";

            // used to fill the DataSet with records from the database
            da = new System.Data.SqlServerCe.SqlCeDataAdapter(sql, con);

            MessageBox.Show("Connection open.");

            // fills DataSet ds1 with table named "Workers"
            da.Fill(ds1, "Workers");

            // calls NavigateRecords() method
            NavigateRecords();

            // counts number of rows in DataSet ds1
            MaxRows = ds1.Tables["Workers"].Rows.Count;

            // calls countRecord() to display record number currently displayed
            countRecord();

            // closes connection
            con.Close();
        }

        private void NavigateRecords()
        {
            try
            {   // accesses specific row from the DataSet
                DataRow dRow = ds1.Tables["Workers"].Rows[inc];

                // accesses specific column in a row and displays value in textboxes
                textBox1.Text = dRow.ItemArray.GetValue(1).ToString();  // first_name column
                textBox2.Text = dRow.ItemArray.GetValue(2).ToString();  // last_name column
                textBox3.Text = dRow.ItemArray.GetValue(3).ToString();  // job_title column
                textBox4.Text = dRow.ItemArray.GetValue(4).ToString();  // department column
            }
            catch (DeletedRowInaccessibleException) {}
        }

        private void btnNext_Click(object sender, EventArgs e)
        {   // displays next record
            if (inc != MaxRows - 1)
            {
                inc++;
                NavigateRecords();
            }
            // displays when program reaches last record
            else
            {
                MessageBox.Show("No more records.");
            }

            enableButton();

            // calls countRecord() to display record number currently displayed
            countRecord();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {   
            if (inc > 0)
            {   // displays previous record
                inc--;
                NavigateRecords();
            }
            else
            {   // displays when program reaches first record
                MessageBox.Show("This is the first record.");
            }

            enableButton();

            // calls countRecord() to display record number currently displayed
            countRecord();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {   // displays last record
            if (inc != MaxRows - 1)
            {
                inc = MaxRows - 1;
                NavigateRecords();
            }

            enableButton();

            // calls countRecord() to display record number currently displayed
            countRecord();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {   // displays first record
            if (inc != 0)
            {
                inc = 0;
                NavigateRecords();
            }

            enableButton();

            // calls countRecord() to display record number currently displayed
            countRecord();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {   // clears all text boxes
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAddNew.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   // creates new row for a new record to be saved
            DataRow dRow = ds1.Tables["Workers"].NewRow();

            if (textBox1.Text.Length == 0 && textBox2.Text.Length == 0 &&
                textBox3.Text.Length == 0 && textBox4.Text.Length == 0)
            {   // does not allow user to save null values for all text boxes
                MessageBox.Show("Text boxes empty. Please enter data.");
            }
            else
            {
                // stores textBox value of a specific column
                dRow[1] = textBox1.Text;    // first_name column
                dRow[2] = textBox2.Text;    // last_name column
                dRow[3] = textBox3.Text;    // job_title column
                dRow[4] = textBox4.Text;    // department column

                // adds new row to the DataSet ds1
                ds1.Tables["Workers"].Rows.Add(dRow);

                // calls UpdateDB() method to save new record to the database
                UpdateDB();

                MaxRows = MaxRows + 1;
                inc = MaxRows - 1;

                // calls countRecord() to display record number currently displayed
                countRecord();

                // displays message record has been successfully saved
                MessageBox.Show("New record saved.");

                enableButton();
            }
        }

        private void UpdateDB()
        {
            try
            {   // saves or updates record to the database
                System.Data.SqlServerCe.SqlCeCommandBuilder cb;
                cb = new System.Data.SqlServerCe.SqlCeCommandBuilder(da);
                cb.DataAdapter.Update(ds1.Tables["Workers"]);
            }
            catch (DBConcurrencyException) {}
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   // gets the current row being viewed at
            DataRow dRow2 = ds1.Tables["Workers"].Rows[inc];

            // stores textBox value of a specific column
            dRow2[1] = textBox1.Text;    // first_name column
            dRow2[2] = textBox2.Text;    // last_name column
            dRow2[3] = textBox3.Text;    // job_title column
            dRow2[4] = textBox4.Text;    // department column

            // calls UpdateDB() method to update record to the database
            UpdateDB();

            // displays message record has been successfully updated
            MessageBox.Show("Data updated.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {   // sets current row to be deleted
            ds1.Tables["Workers"].Rows[inc].Delete();

            // calls UpdateDB() method to update record to the database
            UpdateDB();

            // counts number of rows in database
            MaxRows = ds1.Tables["Workers"].Rows.Count;

            // displays previous record
            --inc;
            NavigateRecords();

            // calls countRecord() to display record number currently displayed
            countRecord();

            // displays message record has been successfully deleted
            MessageBox.Show("Record deleted.");
        }

        private void countRecord()
        {
            label5.Text = "Record " + (inc + 1) + " of " + ds1.Tables["Workers"].Rows.Count;
        }

        private void enableButton()
        {   // enables button while not saving new record
            btnAddNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnFind.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            String searchFor;           // holds texts of record to find
            int results = 0;            // checks if results are found or not
            DataRow[] returnedRows;     // holds record(s) to be found

            searchFor = txtSearch.Text;

            if (comboBox1.Text == "Search by" || comboBox1.Text == null)
            {   // forces user to select item from comboBox
                MessageBox.Show("Please select data to be searched in the drop-down menu.");
            }
            else
            {
                String colName = "";

                if (comboBox1.Text == "First name")
                    colName = "first_name";
                else if (comboBox1.Text == "Last name")
                    colName = "last_name";
                else if (comboBox1.Text == "Job title")
                    colName = "job_title";
                else if (comboBox1.Text == "Department")
                    colName = "department";

                // gets specific row that matches with searchFor variable corresponding to comboBox item selected
                returnedRows = ds1.Tables["Workers"].Select(colName + "='" + searchFor + "'");

                // gets number of rows
                results = returnedRows.Length;

                if (results > 0)
                {
                    int index = 0;

                    // does the loop in case there are two or more results
                    do
                    {   // displays search results through message box
                        DataRow dr1;
                        dr1 = returnedRows[index];
                        MessageBox.Show("\tViewing result " + (index + 1) +
                            " of " + results + "\n\n" +
                            "ID number:\t" + dr1[0].ToString() + "\n" +
                            "First name:\t" + dr1[1].ToString() + "\n" +
                            "Last name:\t" + dr1[2].ToString() + "\n" +
                            "Job title:\t\t" + dr1[3].ToString() + "\n" +
                            "Department:\t" + dr1[4].ToString());

                        index++;
                    } while (index < results);
                }
                else
                {
                    MessageBox.Show("No such record found.");
                }
            }            
        }
    }
}
