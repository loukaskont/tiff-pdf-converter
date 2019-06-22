namespace tiff_pdf_converter
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
            this.button1 = new System.Windows.Forms.Button();
            this.selectDirButton = new System.Windows.Forms.Button();
            this.inputDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputFileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.inputFilesCountLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.comboBoxInputType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inputFileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.readXlsFileButton = new System.Windows.Forms.Button();
            this.selectXlsFileButton = new System.Windows.Forms.Button();
            this.xlsFilePathTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.imagesFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.outputFilderDialogButton = new System.Windows.Forms.Button();
            this.outputFolderTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.button1.Location = new System.Drawing.Point(339, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Συγχώνευση - Μετατροπή";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // selectDirButton
            // 
            this.selectDirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.selectDirButton.Location = new System.Drawing.Point(33, 53);
            this.selectDirButton.Name = "selectDirButton";
            this.selectDirButton.Size = new System.Drawing.Size(146, 29);
            this.selectDirButton.TabIndex = 1;
            this.selectDirButton.Text = "Επιλογή Φακέλου";
            this.selectDirButton.UseVisualStyleBackColor = true;
            this.selectDirButton.Click += new System.EventHandler(this.selectDirButton_Click);
            // 
            // inputDirTextBox
            // 
            this.inputDirTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.inputDirTextBox.Location = new System.Drawing.Point(185, 57);
            this.inputDirTextBox.Name = "inputDirTextBox";
            this.inputDirTextBox.Size = new System.Drawing.Size(451, 21);
            this.inputDirTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.Location = new System.Drawing.Point(38, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Τύπος Αρχείου Εξόδου:";
            // 
            // outputFileTypeComboBox
            // 
            this.outputFileTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputFileTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.outputFileTypeComboBox.FormattingEnabled = true;
            this.outputFileTypeComboBox.Items.AddRange(new object[] {
            "tif",
            "pdf"});
            this.outputFileTypeComboBox.Location = new System.Drawing.Point(195, 230);
            this.outputFileTypeComboBox.Name = "outputFileTypeComboBox";
            this.outputFileTypeComboBox.Size = new System.Drawing.Size(117, 23);
            this.outputFileTypeComboBox.TabIndex = 4;
            // 
            // inputFilesCountLabel
            // 
            this.inputFilesCountLabel.AutoSize = true;
            this.inputFilesCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.inputFilesCountLabel.Location = new System.Drawing.Point(656, 60);
            this.inputFilesCountLabel.Name = "inputFilesCountLabel";
            this.inputFilesCountLabel.Size = new System.Drawing.Size(132, 16);
            this.inputFilesCountLabel.TabIndex = 5;
            this.inputFilesCountLabel.Text = "0 Επιλεγμένα αρχεία.";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 442);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(848, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // comboBoxInputType
            // 
            this.comboBoxInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.comboBoxInputType.FormattingEnabled = true;
            this.comboBoxInputType.Items.AddRange(new object[] {
            "tif",
            "pdf"});
            this.comboBoxInputType.Location = new System.Drawing.Point(195, 147);
            this.comboBoxInputType.Name = "comboBoxInputType";
            this.comboBoxInputType.Size = new System.Drawing.Size(117, 23);
            this.comboBoxInputType.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(35, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Τύπος Αρχείων Εισόδου:";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(404, 419);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(32, 16);
            this.progressLabel.TabIndex = 9;
            this.progressLabel.Text = "0 / 0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(870, 514);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.progressLabel);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.comboBoxInputType);
            this.tabPage1.Controls.Add(this.selectDirButton);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.inputDirTextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.inputFilesCountLabel);
            this.tabPage1.Controls.Add(this.outputFileTypeComboBox);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(862, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Συγχώνευση Αρχείων";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.outputFilderDialogButton);
            this.tabPage2.Controls.Add(this.outputFolderTextBox);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.imagesFolderPathTextBox);
            this.tabPage2.Controls.Add(this.progressBar2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.inputFileTypeComboBox);
            this.tabPage2.Controls.Add(this.readXlsFileButton);
            this.tabPage2.Controls.Add(this.selectXlsFileButton);
            this.tabPage2.Controls.Add(this.xlsFilePathTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(862, 486);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ανάγνωση CSV Δηλώσεων";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(6, 452);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(850, 23);
            this.progressBar2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 427);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "0 / 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Τύπος Αρχείου Εισόδου: ";
            // 
            // inputFileTypeComboBox
            // 
            this.inputFileTypeComboBox.FormattingEnabled = true;
            this.inputFileTypeComboBox.Items.AddRange(new object[] {
            "CSV",
            "XLS"});
            this.inputFileTypeComboBox.Location = new System.Drawing.Point(371, 41);
            this.inputFileTypeComboBox.Name = "inputFileTypeComboBox";
            this.inputFileTypeComboBox.Size = new System.Drawing.Size(121, 23);
            this.inputFileTypeComboBox.TabIndex = 9;
            // 
            // readXlsFileButton
            // 
            this.readXlsFileButton.Location = new System.Drawing.Point(352, 353);
            this.readXlsFileButton.Name = "readXlsFileButton";
            this.readXlsFileButton.Size = new System.Drawing.Size(140, 47);
            this.readXlsFileButton.TabIndex = 8;
            this.readXlsFileButton.Text = "Έναρξη Ανάγνωσης XLS-CSV Αρχείου";
            this.readXlsFileButton.UseVisualStyleBackColor = true;
            this.readXlsFileButton.Click += new System.EventHandler(this.readXlsFileButton_Click);
            // 
            // selectXlsFileButton
            // 
            this.selectXlsFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.selectXlsFileButton.Location = new System.Drawing.Point(18, 114);
            this.selectXlsFileButton.Name = "selectXlsFileButton";
            this.selectXlsFileButton.Size = new System.Drawing.Size(186, 29);
            this.selectXlsFileButton.TabIndex = 6;
            this.selectXlsFileButton.Text = "Επιλογή Αρχείου XLS-CSV";
            this.selectXlsFileButton.UseVisualStyleBackColor = true;
            this.selectXlsFileButton.Click += new System.EventHandler(this.selectXlsFileButton_Click);
            // 
            // xlsFilePathTextBox
            // 
            this.xlsFilePathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.xlsFilePathTextBox.Location = new System.Drawing.Point(210, 118);
            this.xlsFilePathTextBox.Name = "xlsFilePathTextBox";
            this.xlsFilePathTextBox.Size = new System.Drawing.Size(618, 21);
            this.xlsFilePathTextBox.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.button2.Location = new System.Drawing.Point(18, 183);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 29);
            this.button2.TabIndex = 13;
            this.button2.Text = "Επιλογή Φακέλου Εικόνων";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imagesFolderPathTextBox
            // 
            this.imagesFolderPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.imagesFolderPathTextBox.Location = new System.Drawing.Point(210, 187);
            this.imagesFolderPathTextBox.Name = "imagesFolderPathTextBox";
            this.imagesFolderPathTextBox.Size = new System.Drawing.Size(618, 21);
            this.imagesFolderPathTextBox.TabIndex = 14;
            // 
            // outputFilderDialogButton
            // 
            this.outputFilderDialogButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.outputFilderDialogButton.Location = new System.Drawing.Point(19, 248);
            this.outputFilderDialogButton.Name = "outputFilderDialogButton";
            this.outputFilderDialogButton.Size = new System.Drawing.Size(186, 29);
            this.outputFilderDialogButton.TabIndex = 15;
            this.outputFilderDialogButton.Text = "Επιλογή Output Folder";
            this.outputFilderDialogButton.UseVisualStyleBackColor = true;
            this.outputFilderDialogButton.Click += new System.EventHandler(this.outputFilderDialogButton_Click);
            // 
            // outputFolderTextBox
            // 
            this.outputFolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.outputFolderTextBox.Location = new System.Drawing.Point(211, 252);
            this.outputFolderTextBox.Name = "outputFolderTextBox";
            this.outputFolderTextBox.Size = new System.Drawing.Size(618, 21);
            this.outputFolderTextBox.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 531);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Συγχώνευση - Μετατροπή Αρχείων tif - Pdf";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button selectDirButton;
        private System.Windows.Forms.TextBox inputDirTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox outputFileTypeComboBox;
        private System.Windows.Forms.Label inputFilesCountLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox comboBoxInputType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button selectXlsFileButton;
        private System.Windows.Forms.TextBox xlsFilePathTextBox;
        private System.Windows.Forms.Button readXlsFileButton;
        private System.Windows.Forms.ComboBox inputFileTypeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox imagesFolderPathTextBox;
        private System.Windows.Forms.Button outputFilderDialogButton;
        private System.Windows.Forms.TextBox outputFolderTextBox;
    }
}

