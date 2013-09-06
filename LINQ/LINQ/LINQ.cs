// Datoon, Philip Bryan B.
// 131311399
// 2 September 2013
// Using LINQ methods and properties (pp 253-261)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ
{
    public partial class LINQ : Form
    {
        public LINQ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] numberList = new int[5] { 1, 2, 3, 4, 5 };

            listBox1.Items.Add("ARRAY ITEMS: 1, 2, 3, 4, 5");
            listBox1.Items.Add("==========================");

            // gets sum of all numbers in the array
            int listTotal = numberList.Sum();

            listBox1.Items.Add("The SUM of the numbers is: " + listTotal);

            // gets the minimum or lowest number in the array
            int lowestNumber = numberList.Min();
            listBox1.Items.Add("Lowest number is: " + lowestNumber);

            // gets maximum or highest number in the array
            int highestNumber = numberList.Max();
            listBox1.Items.Add("Highest number is: " + highestNumber);

            // gets the average of the numbers in the array
            double averageValue = numberList.Average();
            listBox1.Items.Add("Average of all values is " + averageValue);

            // checks if array containts a specific value
            bool doesContain = numberList.Contains(3);
            listBox1.Items.Add("Contains the number 3: " + doesContain);

            // gets value of an element in a specific position
            int elementValue = numberList.ElementAt(1);
            listBox1.Items.Add("The element at array position 1 is: " + elementValue);

            // gets value of the first element
            int firstElement = numberList.First();
            listBox1.Items.Add("First array value = " + firstElement);

            // gets value of the last element
            int lastElement = numberList.Last();
            listBox1.Items.Add("Last array value = " + lastElement);

            listBox1.Items.Add("==========================");
            listBox1.Items.Add("Array items: 1, 1, 2, 2, 3, 4, 5, 5");
            listBox1.Items.Add("Remove duplicates from array");
            listBox1.Items.Add("==========================");

            int[] aryNums = new int[8] { 1, 1, 2, 2, 3, 4, 5, 5 };
            
            // checks if array contains duplicates
            var distinctNums = aryNums.Distinct();

            foreach (var num in distinctNums)
            {
                listBox1.Items.Add(num.ToString());
            }

            int[] lotNums = new int[10] { 43, 31, 7, 22, 29, 16, 10, 4, 7, 41 };
            int[] chosen = new int[6] { 31, 9, 8, 43, 22, 1 };

            // gets only first six elements of array lotNums[]
            var winners = lotNums.Take(6);

            listBox1.Items.Add("==========================");

            foreach (var num in winners)
            {
                listBox1.Items.Add(num.ToString());
            }

            listBox1.Items.Add("==========================");

            // gets values which exist on both arrays chosen[] and winners[]
            var myNumbers = chosen.Intersect(winners);

            foreach (var numbers in myNumbers)
            {
                listBox1.Items.Add(numbers.ToString());
            }

            listBox1.Items.Add("==========================");

            // gets how many elements are the same on both arrays chosen[] and winners[]
            listBox1.Items.Add("Number of winners: " + myNumbers.Count());
        }
    }
}
