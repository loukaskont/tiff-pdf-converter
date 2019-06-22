using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace tiff_pdf_converter
{
    public partial class Form1 : Form
    {
        List<String> filePaths = new List<String>();
        
        public Form1()
        {
            InitializeComponent();
            inputFileTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inputFileTypeComboBox.SelectedIndex = 1;
            comboBoxInputType.SelectedIndex = 0;
            outputFileTypeComboBox.SelectedIndex = 1;
            inputDirTextBox.ReadOnly = true;
            xlsFilePathTextBox.ReadOnly = true;
            imagesFolderPathTextBox.ReadOnly = true;
            outputFolderTextBox.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressLabel.Text = "Παρακαλώ Περιμένετε...";
            this.Refresh();
            zbarExtractCodes();
            int currentFileGroupIndex = 0;
            progressBar1.Maximum = filePaths.Count;
            progressBar1.Value = 0;
            String outputFileType = outputFileTypeComboBox.Text;
            String inputFilesType = comboBoxInputType.Text;
            String inputDir = inputDirTextBox.Text;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                String outputFile = "";
                if (filePaths.Count > 0)
                {
                    if (outputFileType != "")
                    {
                        Dictionary<String, List<String>> groupsInputFilePaths = new Dictionary<String, List<String>>();
                        String fileGroup = "";
                        List<String> srcFilePaths = new List<String>();
                        for (int i = 0; i < filePaths.Count; i++)
                        {
                            String fileName = Path.GetFileNameWithoutExtension(filePaths[i]);
                            String[] splitingName = fileName.Split('_');
                            if (splitingName.Length > 1)
                            {
                                String currentGroup = splitingName[1];
                                if (fileGroup == "")
                                {
                                    fileGroup = currentGroup;
                                }
                                if (fileGroup != currentGroup && fileGroup != "")
                                {
                                    groupsInputFilePaths.Add(fileGroup, srcFilePaths);
                                    srcFilePaths = new List<String>();
                                    fileGroup = currentGroup;
                                }
                                if (fileGroup == currentGroup)
                                {
                                    srcFilePaths.Add(filePaths[i]);
                                }
                                if (i == filePaths.Count - 1)
                                {
                                    groupsInputFilePaths.Add(fileGroup, srcFilePaths);
                                }
                            }
                            else 
                            {
                                MessageBox.Show("Η ονομασία των αρχείων δεν είναι σωστή.");
                            }
                        }
                        List<System.Drawing.Image> allPagesPaths = new List<System.Drawing.Image>();
                        int fileIndex = 0;
                        progressLabel.Invoke((MethodInvoker)delegate { progressLabel.Text = fileIndex + " / " + filePaths.Count; });
                        progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Maximum = filePaths.Count; progressBar1.Value = fileIndex; });
                        foreach (KeyValuePair<String, List<String>> currentGroup in groupsInputFilePaths)
                        {
                            outputFile = Path.GetDirectoryName(currentGroup.Value[0]) + "\\" + currentGroup.Key + "." + outputFileType;
                            if (inputFilesType == "tif")
                            {
                                for (int i = 0; i < currentGroup.Value.Count; i++)
                                {
                                    List<System.Drawing.Image> tempList = getImagesFromTifFile(currentGroup.Value[i]);
                                    allPagesPaths.AddRange(tempList);
                                }
                                if (outputFileType == "pdf")
                                {
                                    ConvertImageToPdf(allPagesPaths, outputFile);
                                }
                                if (outputFileType == "tif")
                                {
                                    ConvertImageToTif(allPagesPaths, outputFile);
                                }
                                allPagesPaths = new List<System.Drawing.Image>();
                            }
                            if (inputFilesType == "pdf")
                            {
                                if (outputFileType == "pdf")
                                {
                                    MargeMultiplePDF(currentGroup.Value.ToArray(), outputFile);
                                }
                                if (outputFileType == "tif")
                                {
                                    convertPdfToTif(currentGroup.Value, outputFile);
                                }
                            }
                            currentFileGroupIndex++;
                            for (int i = 0; i < currentGroup.Value.Count; i++) { fileIndex++; }
                            progressLabel.Invoke((MethodInvoker)delegate { progressLabel.Text = fileIndex + " / " + filePaths.Count; });
                            progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = fileIndex; });
                            if (fileIndex == filePaths.Count) 
                            {
                                progressLabel.Invoke((MethodInvoker)delegate { progressLabel.Text = fileIndex + " / " + filePaths.Count + "        Η διαδικασία ολοκληρώθηκε με επιτυχία."; });
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Δεν έχετε επιλέξει τύπο-επέκταση για το αρχείου εξόδου.");
                    }
                }
                else
                {
                    MessageBox.Show("Δεν έχετε επιλέξει κάποιον φάκελο ή ο φάκελος που επιλέξατε δεν περιέχει αρχεία του επιλεγμένου τύπου.");
                }
            }).Start();
        }


        public void ConvertImageToPdf(List<System.Drawing.Image> pagesForMerge, string dstFilename)
        {
            if (pagesForMerge.Count > 0)
            {
                var document = new Document();
                var ms = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                iTextSharp.text.Rectangle pageSize;
                document.Open();
                for (int i = 0; i < pagesForMerge.Count; i++)
                {
                    pageSize = new iTextSharp.text.Rectangle(pagesForMerge[i].Width + 80, pagesForMerge[i].Height + 80);
                    System.Drawing.Image img = pagesForMerge[i];
                    byte[] imgBytes = getBytesFromImage(img);
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imgBytes);
                    document.SetPageSize(pageSize);
                    document.NewPage();
                    document.Add(image);
                }
                document.Close();
                File.WriteAllBytes(dstFilename, ms.ToArray());
            }
        }


        public void ConvertImageToTif(List<System.Drawing.Image> pagesForMerge, String dstFilename)
        {
            if (pagesForMerge.Count > 0)
            {
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.SaveFlag;
                ImageCodecInfo encoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);
                Bitmap firstImage = new Bitmap(pagesForMerge[0]);
                firstImage.Save(dstFilename, encoderInfo, encoderParameters);
                encoderParameters.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.FrameDimensionPage);
                for (int i = 1; i < pagesForMerge.Count; i++)
                {
                    Bitmap img = new Bitmap(pagesForMerge[i]);
                    firstImage.SaveAdd(img, encoderParameters);
                }
                encoderParameters.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.Flush);
                firstImage.SaveAdd(encoderParameters);
            }
        }


        private List<System.Drawing.Image> getImagesFromTifFile(String filePath)
        {
            List<System.Drawing.Image> pagesList = new List<System.Drawing.Image>();
            int activePage;
            int pages;
            String fileName = Path.GetFileNameWithoutExtension(filePath);
            String fileExtension = Path.GetExtension(filePath);
            if (fileExtension == ".tif" || fileExtension == ".TIF" || fileExtension == ".tiff" || fileExtension == ".TIFF")
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);
                pages = image.GetFrameCount(FrameDimension.Page);
                for (int index = 0; index < pages; index++)
                {
                    activePage = index + 1;
                    image.SelectActiveFrame(FrameDimension.Page, index);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, image.RawFormat);
                    Bitmap currentPage = new Bitmap(memoryStream);
                    pagesList.Add(currentPage);
                }
            }
            return pagesList;
        }


        private List<System.Drawing.Image> getImagesFromPdfFile(String filePath, String outputFile)
        {
            MessageBox.Show("Δεν έχει υλοποιηθεί ακόμη η μέθοδος.");
            return new List<System.Drawing.Image>();
        }


        public void MargeMultiplePDF(string[] PDFfileNames, string OutputFile)
        {
            iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
            using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
            {
                iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
                if (PDFwriter == null)
                {
                    return;
                }
                PDFdoc.Open();
                foreach (string fileName in PDFfileNames)
                {
                    iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);
                    PDFreader.ConsolidateNamedDestinations();
                    for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                        PDFwriter.AddPage(page);
                    }
                    iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                    if (form != null)
                    {
                        PDFwriter.CopyAcroForm(PDFreader);
                    }
                    PDFreader.Close();
                }
                PDFwriter.Close();
                PDFdoc.Close();
            }
        }


        public byte[] getBytesFromImage(System.Drawing.Image imageIn)
        {
            byte[] returnBytes;
            MemoryStream memoryStream = new MemoryStream();

            ImageCodecInfo myImageCodecInfo;
            myImageCodecInfo = GetEncoderInfo("image/tiff");
            System.Drawing.Imaging.Encoder myEncoder;
            myEncoder = System.Drawing.Imaging.Encoder.Compression;
            EncoderParameters myEncoderParameters;
            myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter;
            if (imageIn.PixelFormat == PixelFormat.Format24bppRgb)
            {
                myEncoderParameter = new EncoderParameter(myEncoder, (long)EncoderValue.CompressionCCITT4);
            }
            else 
            {
                myEncoderParameter = new EncoderParameter(myEncoder, (long)EncoderValue.CompressionLZW);
            }
            myEncoderParameters.Param[0] = myEncoderParameter;

            imageIn.Save(memoryStream, myImageCodecInfo, myEncoderParameters);
            returnBytes = memoryStream.ToArray();
            return returnBytes;
        }


        private void selectDirButton_Click(object sender, EventArgs e)
        {
            if (comboBoxInputType.Text != "" || outputFileTypeComboBox.Text != "")
            {
                filePaths.Clear();
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        String capitalInputExtension = comboBoxInputType.Text.ToUpper();
                        String capitalInputExtensionFF = comboBoxInputType.Text.ToUpper()+"F";
                        String inputExtensionff = comboBoxInputType.Text.ToUpper()+"f";
                        filePaths = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith("." + comboBoxInputType.Text + "") || s.EndsWith("." + capitalInputExtension + "") || s.EndsWith("." + capitalInputExtensionFF + "") || s.EndsWith("." + inputExtensionff + "")).ToList();
                        inputFilesCountLabel.Text = filePaths.Count + " Επιλεγμένα αρχεία.";
                        filePaths.OrderBy(f => f);
                        inputDirTextBox.Text = fbd.SelectedPath;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Πρέπει να επιλέξετε τύπο Αρχείων Εισόδου και Εξόδου για να συνεχίσετε.");
            }
        }


        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        private void convertPdfToTif(List<String> groupFilePaths, String outputFilePath) 
        {
            String outputDir = Path.GetDirectoryName(outputFilePath);
            List<String> splitingFiles = new List<String>();
            List<int> pagesCount = new List<int>();
            for (int i = 0; i < groupFilePaths.Count; i++)
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                String projectDir = System.IO.Directory.GetCurrentDirectory();
                startInfo.FileName = projectDir + "\\PDF_to_TIFF.exe";
                startInfo.Arguments = "\"" + groupFilePaths[i] + "\"";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                System.Diagnostics.Process.Start(startInfo);
                String newFileName = "";
                newFileName = outputDir + "\\" + Path.GetFileNameWithoutExtension(groupFilePaths[i]) + "-1.tif";
                splitingFiles.Add(newFileName);
                pagesCount.Add(getPagesCountFromPDF(groupFilePaths[i]));
            }
            List<System.Drawing.Image> allPagesPaths = new List<System.Drawing.Image>();
            for (int i = 0; i < splitingFiles.Count; i++)
            {
                String pdfFilePath = outputDir + "\\" + Path.GetFileNameWithoutExtension(groupFilePaths[i]) + ".pdf";
                List<System.Drawing.Image> tempList = mergeTifFiles(pdfFilePath, splitingFiles[i], pagesCount[i]);
                allPagesPaths.AddRange(tempList);
            }
            ConvertImageToTif(allPagesPaths, outputFilePath);
            for (int i = 0; i < filesForDelete.Count; i++)
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers(); 
                File.Delete(filesForDelete[i]);
            }
        }


        List<String> filesForDelete = new List<String>();
        private List<System.Drawing.Image> mergeTifFiles(String pdfFilePath, String filePath, int pdfFilePageCount)
        {
            List<System.Drawing.Image> pagesList = new List<System.Drawing.Image>();
            while (!File.Exists(filePath)) { }
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i < pdfFilePageCount; i++)
            {
                int activePage;
                int pages;
                String fileName = Path.GetFileNameWithoutExtension(filePath);
                String tempTifFileDir = Path.GetDirectoryName(pdfFilePath);
                String tempTifFilePath = filePath.Replace("-1.tif", "-" + (i + 1).ToString().TrimStart('0') + ".tif");
                filesForDelete.Add(tempTifFilePath);
                System.Drawing.Image image = System.Drawing.Image.FromFile(tempTifFilePath);
                pages = image.GetFrameCount(FrameDimension.Page);
                for (int index = 0; index < pages; index++)
                {
                    activePage = index + 1;
                    image.SelectActiveFrame(FrameDimension.Page, index);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, image.RawFormat);
                    Bitmap currentPage = new Bitmap(memoryStream);
                    pagesList.Add(currentPage);
                }
            }
            return pagesList;
        }


        private int getPagesCountFromPDF(String pdfFilePath) 
        {
            PdfReader pdfReader = new PdfReader(pdfFilePath);
            int numberOfPages = pdfReader.NumberOfPages;
            return numberOfPages;
        }


        private void zbarExtractCodes()
        {
            Dictionary<string,List<string>> qrCode_FileIndexes = new Dictionary<string,List<string>>();
            String programFiles32 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            String programFiles64 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            String programFiles = "";
            if (File.Exists("\"" + programFiles32 + @"\ZBar\bin\zbarimg.exe"))
            {
                programFiles = programFiles32;
            }
            else 
            {
                programFiles = programFiles64;
            }
            for (int i = 0; i < filePaths.Count; i++)
            {
                string nextFileName = "";
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = "\"" + programFiles + "\\ZBar\\bin\\zbarimg\"";
                pProcess.StartInfo.Arguments = "-q \"" + filePaths[i] + "\"";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();
                string strOutput = pProcess.StandardOutput.ReadToEnd();
                pProcess.WaitForExit();
                if (strOutput != "")
                {
                    String[] qrCodeSpliting = strOutput.Split(':');
                    String qrCode = "";
                    if (qrCodeSpliting.Length > 0)
                    {
                        String[] qrCodeSpliting_1 = qrCodeSpliting[1].Split('.');
                        if (qrCodeSpliting_1.Length > 0)
                        {
                            qrCode = qrCodeSpliting_1[0];
                            String[] qrCodeSpliting_2 = qrCode.Split('\r');
                            if (qrCodeSpliting_2.Length > 0)
                            {
                                qrCode = qrCodeSpliting_2[0];
                            }
                        }
                    }
                    if (qrCode != "")
                    {
                        String inputFileExtension = Path.GetExtension(filePaths[i]);
                        String newFileName = qrCode + inputFileExtension;
                        if (!qrCode_FileIndexes.Keys.Contains(qrCode))
                        {
                            qrCode_FileIndexes.Add(qrCode, new List<string>() { "1" });
                            nextFileName = Path.GetDirectoryName(filePaths[i]) + "\\1_" + qrCode + inputFileExtension;
                        }
                        else
                        {
                            int lastIndex = Convert.ToInt32(qrCode_FileIndexes[qrCode][qrCode_FileIndexes[qrCode].Count - 1]);
                            qrCode_FileIndexes[qrCode].Add((lastIndex + 1).ToString());
                            nextFileName = Path.GetDirectoryName(filePaths[i]) + "\\" + (lastIndex + 1).ToString() + "_" + qrCode + inputFileExtension;
                        }
                        System.IO.File.Move(filePaths[i], nextFileName);
                        filePaths[i] = nextFileName;
                    }
                }
            }
            filePaths = filePaths.OrderBy(q => q).ToList();
        }

        string inputFilePath = "";
        List<String> imageFilesFromXlsDir = new List<String>();
        private void selectXlsFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (inputFileTypeComboBox.Text == "XLS")
            {
                openFileDialog.Title = "Επιλογή " + inputFileTypeComboBox.Text + " αρχείου";
                openFileDialog.Filter = "Files (XLS)|*.XLS;*.XLSX;*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inputFilePath = openFileDialog.FileName;
                    xlsFilePathTextBox.Text = inputFilePath;
                }
            }
            if (inputFileTypeComboBox.Text == "CSV")
            {
                openFileDialog.Title = "Επιλογή " + inputFileTypeComboBox.Text + " αρχείου";
                openFileDialog.Filter = "Files (CSV,csv)|*.CSV;*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inputFilePath = openFileDialog.FileName;
                    xlsFilePathTextBox.Text = inputFilePath;
                }
            }
        }


        private void readXlsFileButton_Click(object sender, EventArgs e)
        {
            if (xlsFilePathTextBox.Text == "")
            {
                MessageBox.Show("Δεν έχετε επιλέξει ερχείο xls - csv.");
                return;
            }
            if (imagesFolderPathTextBox.Text == "")
            {
                MessageBox.Show("Δεν έχετε επιλέξει φάκελο Εικόνων.");
                return;
            }
            if (imageFilesFromXlsDir.Count == 0)
            {
                MessageBox.Show("Ο φάκελος εικόνων που επιλέξατε δεν περιέχει αρχεία εικόνων.");
                return;
            }
            if (inputFileTypeComboBox.Text == "XLS")
            {
                if (inputFilePath != "")
                {
                    new Thread(() =>
                    {
                        Dictionary<String, List<String>> geisId_gdocId = getGeisGpropFromXLSFile(inputFilePath);
                        createGeisIdFolders(geisId_gdocId);
                    }).Start();
                }
                else 
                {
                    MessageBox.Show("Δεν έχετε επιλέξει κάποιο xls-csv αρχείο.");
                }
            }
            if (inputFileTypeComboBox.Text == "CSV")
            {
                new Thread(() => {
                    Dictionary<String, List<String>> geisId_gdocId = getGeisGpropFromCSVFile(inputFilePath, ';');
                    if (geisId_gdocId.Count == 0) 
                    {
                        geisId_gdocId = getGeisGpropFromCSVFile(inputFilePath, ',');
                    }
                    createGeisIdFolders(geisId_gdocId);
                }).Start();
            }
        }

        public Dictionary<String, List<String>> getGeisGpropFromXLSFile(string strFilePath)
        {
            if (strFilePath == "") 
            {
                MessageBox.Show("Δεν έχεται επιλέξει κάποιο xls-csv αρχείο.");
                return new Dictionary<String, List<String>>();
            }
            label4.Invoke((MethodInvoker)delegate { label4.Text = "Ανάγνωση XLS αρχείου..."; });
            Dictionary<String, List<String>> geisId_gdocId = new Dictionary<String, List<String>>();
            Excel.Application excelApp = new Excel.Application();
            if (excelApp != null)
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(@"" + strFilePath + "", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                Excel.Range excelRange = excelWorksheet.UsedRange;
                int rowCount = excelRange.Rows.Count;
                int colCount = excelRange.Columns.Count;
                progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Maximum = rowCount; });
                object[,] values = excelRange.Value2;
                int geisIdColIndex = 0, gpropIdColIndex = 0;
                for (int i = 1; i <= colCount; i++)
                {
                    if (values[1, i].ToString().Trim() == "G_EIS_ID") 
                    {
                        geisIdColIndex = i;
                    }
                    if (values[1, i].ToString().Trim() == "G_DOC_ID") 
                    {
                        gpropIdColIndex = i;
                    }
                }
                if (geisIdColIndex == 0 || gpropIdColIndex == 0)
                {
                    MessageBox.Show("Στο xls αρχείο, δεν υπάρχει η στήλη G_EIS_ID ή G_DOC_ID.");
                }
                else
                {
                    for (int i = 1; i <= rowCount; i++)
                    {
                        string geisId = "", gdocId = "";
                        if (values[i, geisIdColIndex] != null) { geisId = values[i, geisIdColIndex].ToString().Trim(); }
                        if (values[i, gpropIdColIndex] != null) { gdocId = values[i, gpropIdColIndex].ToString().Trim(); }
                        if (!geisId_gdocId.Keys.Contains(geisId))
                        {
                            List<String> docs = new List<String>();
                            docs.Add(gdocId);
                            geisId_gdocId.Add(geisId, docs);
                        }
                        else
                        {
                            geisId_gdocId[geisId].Add(gdocId);
                        }
                        progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Value = i; });
                    }
                }
                object misValue = System.Reflection.Missing.Value;
                excelWorkbook.Close(false, misValue, misValue);
                excelApp.Quit();
            }
            return geisId_gdocId;
        }


        public Dictionary<String, List<String>> getGeisGpropFromCSVFile(string strFilePath, char csvSeperatedChar)
        {
            Dictionary<String, List<String>> geisId_gdocId = new Dictionary<String, List<String>>();
            string[] lines = System.IO.File.ReadAllLines(strFilePath);
            int colCount = lines[0].Split(csvSeperatedChar).Length, rowCount = lines.Length;
            int geisIdColIndex = 0, gpropIdColIndex = 0;
            string[] firstLine = lines[0].Split(csvSeperatedChar);
            if (firstLine.Length == 0) 
            {
                return geisId_gdocId;
            }
            for (int i = 0; i <= colCount-1; i++)
            {
                if (firstLine[i].ToString().Trim() == "G_EIS_ID")
                {
                    geisIdColIndex = i;
                }
                if (firstLine[i].ToString().Trim() == "G_DOC_ID")
                {
                    gpropIdColIndex = i;
                }
            }
            if (geisIdColIndex == 0 || gpropIdColIndex == 0)
            {
                MessageBox.Show("Στο xls αρχείο, δεν υπάρχει η στήλη G_EIS_ID ή G_DOC_ID.");
            }
            else
            {
                progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Maximum = lines.Length; });
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitingLine = lines[i].Split(csvSeperatedChar);
                    string geisId = "", gdocId = "";
                    if (splitingLine[geisIdColIndex] != null) { geisId = splitingLine[geisIdColIndex].ToString().Trim(); }
                    if (splitingLine[gpropIdColIndex] != null) { gdocId = splitingLine[gpropIdColIndex].ToString().Trim(); }
                    if (!geisId_gdocId.Keys.Contains(geisId))
                    {
                        List<String> docs = new List<String>();
                        docs.Add(gdocId);
                        geisId_gdocId.Add(geisId, docs);
                    }
                    else
                    {
                        geisId_gdocId[geisId].Add(gdocId);
                    }
                    progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Value = i; });
                }
            }
            return geisId_gdocId;
        }


        private void createGeisIdFolders(Dictionary<String, List<String>> geisId_gdocId) 
        {
            String outputFolder = "";
            if (outputFolderTextBox.Text != "")
            {
                outputFolder = outputFolderTextBox.Text;
            }
            else 
            {
                outputFolder = imagesFolderPathTextBox.Text;
            }
            StreamWriter g_doc_ids_withOutFile = new StreamWriter(outputFolder + "\\g_doc_ids_withOutFile.txt");
            label4.Invoke((MethodInvoker)delegate { label4.Text = "Κατασκευή φακέλων - Αντιγραφή αρχείων..."; });
            progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Value = 0; });
            progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Maximum = imageFilesFromXlsDir.Count; });
            if (geisId_gdocId.Count == 0) 
            {
                label4.Invoke((MethodInvoker)delegate { label4.Text = "Η διαδικασία διεκόπει."; });
                return;
            }
            for (int i = 0; i < imageFilesFromXlsDir.Count; i++) 
            {
                String imageFileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFilesFromXlsDir[i]);
                String imageFileDir = Path.GetDirectoryName(imageFilesFromXlsDir[i]);
                String imageFileName= Path.GetFileName(imageFilesFromXlsDir[i]);
                if (imageFileNameWithoutExtension[0] == 'i' || imageFileNameWithoutExtension[0] == 'I' || imageFileNameWithoutExtension[0] == 'Ι') 
                {
                    String geiIdFromFileName = imageFileNameWithoutExtension.Substring(1, imageFileNameWithoutExtension.Length - 1);
                    List<String> docsForCurrentGeisId = geisId_gdocId[geiIdFromFileName];
                    String geis_id_Dir = outputFolder + "\\" + geiIdFromFileName;
                    if (!Directory.Exists(geis_id_Dir))
                    {
                        Directory.CreateDirectory(geis_id_Dir);
                        if (File.Exists(imageFilesFromXlsDir[i]))
                        {
                            File.Copy(imageFilesFromXlsDir[i], geis_id_Dir + "\\" + imageFileName);
                        }
                        for (int j = 0; j < docsForCurrentGeisId.Count; j++)
                        {
                            List<String> docFilePathList = Directory.GetFiles(imagesFolderPathTextBox.Text, "" + docsForCurrentGeisId[j] + ".*", SearchOption.AllDirectories).ToList();
                            String docFilePath = "";
                            if (docFilePathList.Count > 0)
                            {
                                docFilePath = docFilePathList[0];
                                if (File.Exists(docFilePath))
                                {
                                    File.Copy(docFilePath, geis_id_Dir + "\\" + docsForCurrentGeisId[j] + Path.GetExtension(docFilePath));
                                }
                            }
                            else
                            {
                                g_doc_ids_withOutFile.WriteLine("Δεν εντοπίστηκε αρχείο για το gdocId = " + docsForCurrentGeisId[j]);
                            }
                        }
                    }
                }
                progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Maximum = i; });
            }
            g_doc_ids_withOutFile.Close();
            label4.Invoke((MethodInvoker)delegate { label4.Text = "Η διαδικασία ολοκληρώθηκε."; });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdImages = new FolderBrowserDialog();
            DialogResult resultImages = fbdImages.ShowDialog();
            if (resultImages == DialogResult.OK && !string.IsNullOrWhiteSpace(fbdImages.SelectedPath))
            {
                try
                {
                    imagesFolderPathTextBox.Text = fbdImages.SelectedPath;
                    //String excelldirPath = Path.GetDirectoryName(fbdImages.SelectedPath);
                    List<string> filePathsFromXlsDir = Directory.GetFiles(fbdImages.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".PDF") || s.EndsWith(".pdf") || s.EndsWith(".TIF") || s.EndsWith(".TIFF") || s.EndsWith(".tiff") || s.EndsWith(".tif") || s.EndsWith(".jpg")).ToList();
                    for (int i = 0; i < filePathsFromXlsDir.Count; i++)
                    {
                        String filePath = filePathsFromXlsDir[i];
                        imageFilesFromXlsDir.Add(filePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } 
        }

        private void outputFilderDialogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdOutput = new FolderBrowserDialog();
            DialogResult resultOutput = fbdOutput.ShowDialog();
            if (resultOutput == DialogResult.OK && !string.IsNullOrWhiteSpace(fbdOutput.SelectedPath))
            {
                outputFolderTextBox.Text = fbdOutput.SelectedPath;
            }
        }




    }
}
