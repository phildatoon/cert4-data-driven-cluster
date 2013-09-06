// Datoon, Philip Bryan B.
// 131311399
// 2 September 2013
// Using LINQ queries (pp 262-266)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ_Queries
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] countries = new string[17] {"Antarctica",
                "Akrotiri", "Algeria", "Albania", "Australia", "Andorra",
                "Angola", "Anguilla", "Antigua and Barbuda",
                "Ashmore and Cartier Islands", "Argentina", "Azerbaijan",
                "Armenia", "American Samoa", "Afghanistan", "Austria", "Aruba"};

            // sets the query
            var query = from country in countries       // gets each array element
                        where country.EndsWith("ia")    // sets limitation for display
            //          where country.StartsWith("An")
            //          wherecountry.Length >= 7 && country.Length <= 10
                        orderby country                 // sorts array alphabetically
            //          orderby country.Length, country // sorts array by character length
                        select country;                 // selects from the array

            // displays array in list box
            foreach (var value in query)
            {
                listBox1.Items.Add(value);
            }
        }
    }
}
