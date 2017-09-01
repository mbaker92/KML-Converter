namespace KML_Converter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Phase1 = new System.Windows.Forms.Button();
            this.Phase2 = new System.Windows.Forms.Button();
            this.InputFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.InputFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.OutputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.InputFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.Instructions = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Convert KML to KMLCSV";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Convert KMLCSV to KML";
            // 
            // Phase1
            // 
            this.Phase1.Location = new System.Drawing.Point(312, 18);
            this.Phase1.Name = "Phase1";
            this.Phase1.Size = new System.Drawing.Size(75, 23);
            this.Phase1.TabIndex = 2;
            this.Phase1.Text = "File Browser";
            this.Phase1.UseVisualStyleBackColor = true;
            this.Phase1.Click += new System.EventHandler(this.Phase1_Click);
            // 
            // Phase2
            // 
            this.Phase2.Location = new System.Drawing.Point(312, 58);
            this.Phase2.Name = "Phase2";
            this.Phase2.Size = new System.Drawing.Size(75, 23);
            this.Phase2.TabIndex = 3;
            this.Phase2.Text = "File Browser";
            this.Phase2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Phase2.UseVisualStyleBackColor = true;
            this.Phase2.Click += new System.EventHandler(this.Phase2_Click);
            // 
            // InputFileDialog1
            // 
            this.InputFileDialog1.Filter = " KML File |*.kml";
            this.InputFileDialog1.Title = "Choose .KML File to Convert";
            // 
            // InputFileDialog2
            // 
            this.InputFileDialog2.Filter = "KMLCSV File |*.KMLCSV";
            this.InputFileDialog2.Title = "Choose .KMLCSV File to Convert";
            // 
            // OutputFolder
            // 
            this.OutputFolder.Description = "Choose Location For Converted KML";
            this.OutputFolder.SelectedPath = "\\\\QES-RENO\\Projects";
            // 
            // InputFileDialog3
            // 
            this.InputFileDialog3.FileName = "InputFileDialog3";
            this.InputFileDialog3.Title = "Choose Original KML File";
            // 
            // Instructions
            // 
            this.Instructions.AutoSize = true;
            this.Instructions.Location = new System.Drawing.Point(171, 91);
            this.Instructions.Name = "Instructions";
            this.Instructions.Size = new System.Drawing.Size(61, 13);
            this.Instructions.TabIndex = 4;
            this.Instructions.TabStop = true;
            this.Instructions.Text = "Instructions";
            this.Instructions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Instructions_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 113);
            this.Controls.Add(this.Instructions);
            this.Controls.Add(this.Phase2);
            this.Controls.Add(this.Phase1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "KML Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Phase1;
        private System.Windows.Forms.Button Phase2;
        private System.Windows.Forms.OpenFileDialog InputFileDialog1;
        private System.Windows.Forms.OpenFileDialog InputFileDialog2;
        private System.Windows.Forms.FolderBrowserDialog OutputFolder;
        private System.Windows.Forms.OpenFileDialog InputFileDialog3;
        private System.Windows.Forms.LinkLabel Instructions;
    }
}

