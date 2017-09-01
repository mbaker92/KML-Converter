/* Author: Matthew Baker
 * Purpose: Popup box to get new filename
 * Date Created: 8/23/2017
 * Date Modified: 8/23/2017
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KML_Converter
{
    public partial class NewFilename : Form
    {
        public NewFilename()
        {
            InitializeComponent();
        }

        // OK button on form
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the last for characters of the input box to check if the user put the extension
            if(UpdatedKML.Text == "")
            {
                Form1.NewFile = "UpdatedKML.kml";
            }
            else if (UpdatedKML.Text.Length < 4)
            {
                Form1.NewFile = UpdatedKML.Text + ".kml";
            }
            else
            {
                string temp = UpdatedKML.Text.Substring(UpdatedKML.Text.Length - 4);

                // The extension the program is looking for
                string extension = ".kml";

                // If the last four characters equal .kml ignoring the case, then NewFile variable is updated with textbox value.
                if (string.Equals(temp, extension, StringComparison.OrdinalIgnoreCase))
                {
                    Form1.NewFile = UpdatedKML.Text;
                }
                // If the Characters dont match, then NewFile variable will be the user input + .kml
                else
                {
                    Form1.NewFile = UpdatedKML.Text + ".kml";
                }
            }
            // Close this form to continue on with the program.
            this.Close();
        }


    }
}
