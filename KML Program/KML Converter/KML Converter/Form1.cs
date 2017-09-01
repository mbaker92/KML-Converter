/* Author: Matthew Baker
 * Purpose: Create a Gui to make it easy for the user to convert KMLs
 * Date Created: 8/21/2017
 * Date Last Modified : 8/24/2017
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KML_Converter
{

    public partial class Form1 : Form
    {
        public string InputFilePath1;
        public string InputFilePath2;
        public string InputFilePath3;
        public static string Path;
        public static string NewFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void Phase1_Click(object sender, EventArgs e)
        {
            if (InputFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the Full path of the input file
                InputFilePath1 = InputFileDialog1.FileName;

                // Get the path of the file without the filename
                Path = InputFilePath1.Replace(InputFileDialog1.SafeFileName, "");

                // Convert the filepath to a usable form to pass into the C++ code
                string ArgumentPath = "\"" + InputFilePath1 + "\"";

                // Copy Excel Macro to Folder
                System.IO.File.Copy(@"\RemoveNonMatches_Sonoma.xlsb",  @"C:\Users\Public\RemoveNonMatches_Sonoma.xlsb", true);

                // Copy the C++ code EXE to the public directory for use
                System.IO.File.Copy(@"\XML_Project.exe", @"C:\Users\Public\XML_Project.exe");

                // Create a new process
                Process phase1 = new Process();
               
                // Give the file of the C++ exe and the arguments that are passed to the process
                phase1.StartInfo.FileName = @"C:\Users\Public\XML_Project.exe";
                phase1.StartInfo.Arguments = "/k 0 " + ArgumentPath + " non";

                // Start the C++ process and wait for it to finish before continuing.
               phase1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                phase1.Start();
                phase1.WaitForExit();


                // Run Excel Macro
                RunMacro(@"C:\Users\Public\RemoveNonMatches_Sonoma.xlsb", "NonMatchMac");

                // Delete Excel Macro Once Finished.
                if (System.IO.File.Exists(@"C:\Users\Public\RemoveNonMatches_Sonoma.xlsb"))
                {
                    System.IO.File.Delete(@"C:\Users\Public\RemoveNonMatches_Sonoma.xlsb");
                }

               // Copy UpdatedKML.kmlcsv to the original directory
                if (System.IO.File.Exists(@"C:\Users\Public\UpdatedCSV.kmlcsv"))
                {
                    // If a version already exists, Append the time to the front of the old file
                    if (System.IO.File.Exists(Path+ "UpdatedCSV.kmlcsv"))
                    {
                        System.IO.File.Move(Path + "UpdatedCSV.kmlcsv", Path + System.DateTime.Now.ToString("HHMMSS") + "UpdatedCSV.kmlcsv");
                    }

                    // Copy the new file to the directory
                    System.IO.File.Copy(@"C:\Users\Public\UpdatedCSV.kmlcsv", Path + "UpdatedCSV.kmlcsv");

                    // Delete the copy that is in the public folder.
                    System.IO.File.Delete(@"C:\Users\Public\UpdatedCSV.kmlcsv");
                }
                
                // Delete the c++ exe from the public directory
                if (System.IO.File.Exists(@"C:\Users\Public\XML_Project.exe"))
                {
                    System.IO.File.Delete(@"C:\Users\Public\XML_Project.exe");
                }
                if (System.IO.File.Exists(@"C:\Users\Public\csvout.kmlcsv"))
                {
                    System.IO.File.Delete(@"C:\Users\Public\csvout.kmlcsv");
                }

                Completed();
            }

        }

        private void Phase2_Click(object sender, EventArgs e)
        {
            // Choose file that needs to be converted from KMLCSV to KML
            if (InputFileDialog2.ShowDialog() == DialogResult.OK)
            {
                // Get the file to convert
                InputFilePath2 = InputFileDialog2.FileName;

                // Get the path of the file without the filename
                Path = InputFilePath2.Replace(InputFileDialog2.SafeFileName, "");

                // Copy it to the public directory
                System.IO.File.Copy(InputFilePath2, @"C:\Users\Public\" + System.IO.Path.GetFileName(InputFilePath2), true);
 
                // Choose the location of where to put the converted KML file
                DialogResult result = OutputFolder.ShowDialog();

                if (result == DialogResult.OK)
                {

                    // Create a new popup to get the new file name
                    NewFilename Popup = new NewFilename();
                    Popup.ShowDialog();
                    
                 

                    if (InputFileDialog3.ShowDialog() == DialogResult.OK)
                    {
                        InputFilePath3 = InputFileDialog3.FileName;
                        // Make the path usuable for the C++ code
                        string ArgumentPath = "\"" + InputFilePath3 + "\"";
                        string outputpath = OutputFolder.SelectedPath;
                        outputpath = outputpath + "\\";

                        // Copy the C++ exe to the public directory
                        System.IO.File.Copy(@"\XML_Project.exe", @"C:\Users\Public\XML_Project.exe");

                        // Create a new process for phase 2
                        Process phase2 = new Process();

                        // Give the C++ exe and the arguments to pass to the exe
                        phase2.StartInfo.FileName = @"C:\Users\Public\XML_Project.exe";
                        phase2.StartInfo.Arguments = "/k 1 " + ArgumentPath + " " + NewFile; // + name of file

                        // Start the C++ executable and wait for the process to finish before continuing
                        phase2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        phase2.Start();
                        phase2.WaitForExit();

                        // If their is already a file with the same name in the directory, apend the time to beginning of that file.
                        if (System.IO.File.Exists(outputpath + NewFile))
                        {
                            System.IO.File.Move(outputpath + NewFile, outputpath + System.DateTime.Now.ToString("HHMMSS")  +NewFile);
                        }

                        // Copy the output file from the C++ code the original directory
                        System.IO.File.Copy(@"C:\Users\Public\" + NewFile, outputpath + (Char)92 + NewFile);

                        // If File exists, Delete it from public folder
                        if (System.IO.File.Exists(@"C:\Users\Public\" + NewFile))
                        {
                            System.IO.File.Delete(@"C:\Users\Public\" + NewFile);
                        }

                        // If exe exists, Delete it from public folder
                        if (System.IO.File.Exists(@"C:\Users\Public\XML_Project.exe"))
                        {
                            System.IO.File.Delete(@"C:\Users\Public\XML_Project.exe");
                        }
                        if (System.IO.File.Exists(@"C:\Users\Public\" + System.IO.Path.GetFileName(InputFilePath2)))
                        {
                            System.IO.File.Delete(@"C:\Users\Public\" + System.IO.Path.GetFileName(InputFilePath2));
                        }

                        Completed();
                    }
                    
                }
                else if (result == DialogResult.Cancel || result != DialogResult.OK)
                {

                    // If File exists, Delete it from public folder
                    if (System.IO.File.Exists(@"C:\Users\Public\" + System.IO.Path.GetFileName(InputFilePath2)))
                    {
                        System.IO.File.Delete(@"C:\Users\Public\" + System.IO.Path.GetFileName(InputFilePath2));
                    }
  
                }
            }
        }
        private void RunMacro(string excelMac, string macName)
        {
            // Open Excel
            object oMissing = System.Reflection.Missing.Value;
            Excel.Application oExcel = new Excel.Application();
            oExcel.Visible = true;
            Excel.Workbooks oBooks = oExcel.Workbooks;
            Excel._Workbook oBook = null;
            oBook = oBooks.Open(excelMac, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);


            // Run the Excel Macro 
            oExcel.Run(macName);

            while (System.IO.File.Exists(@"C:\Users\Public\~$RemoveNonMatches_Sonoma.xlsb"))
            {

            }
            //oBook.Close(false, oMissing, oMissing);
            //oExcel.Quit();

            // Release Resources
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
            oBook = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
            oBooks = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            oExcel = null;
        }
        private void Completed()
        {
            MessageBox.Show("THE CONVERSION IS COMPLETE", "Done", MessageBoxButtons.OK);
        }


        // Instructions link to click on in the windows form
        private void Instructions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Word.Application oWord = new Word.Application();
            oWord.Visible = true;
            oWord.Documents.Open(@"\KML Program\KMLInstructions.docx");
        }
    }
}
