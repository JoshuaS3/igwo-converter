using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Drawing;
using InstantGameworksObject;
using System.Diagnostics;

namespace IGWOConverter
{
    public class Dialog
    {
        public Dialog()
        {
            string openPath = null;
            string savePath = null;

            Form converterDialog = new Form();
            converterDialog.Text = "OBJ to Instant Gameworks Object (IGWO) Converter";
            converterDialog.Width = 680;
            converterDialog.Height = 490;
            converterDialog.BackColor = Color.FromArgb(245, 245, 245);
            converterDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            converterDialog.MaximizeBox = false;
            converterDialog.Icon = new Icon("InstantGameworksObject.ico");
            Label title = new Label();
            title.Text = "Instant Gameworks Object";
            title.Width = 640;
            title.Height = 30;
            title.Left = 20;
            title.Top = 15;
            title.Font = new Font("Arial", 18f);
            title.Parent = converterDialog;
            Button openFileButton = new Button();
            openFileButton.Width = 200;
            openFileButton.Height = 50;
            openFileButton.Left = 20;
            openFileButton.Top = 60;
            openFileButton.Font = new Font("Arial", 12f);
            openFileButton.BackColor = Color.LightGray;
            openFileButton.FlatStyle = FlatStyle.System;
            openFileButton.Text = "SELECT INPUT FILE";
            openFileButton.Parent = converterDialog;
            TextBox selectedFile = new TextBox();
            selectedFile.ReadOnly = true;
            selectedFile.Multiline = false;
            selectedFile.BackColor = Color.White;
            selectedFile.Width = 400;
            selectedFile.Height = 50;
            selectedFile.Text = "<input file>";
            selectedFile.Left = 240;
            selectedFile.Top = 60 + (25-12);
            selectedFile.Parent = converterDialog;
            Button saveFileButton = new Button();
            saveFileButton.Width = 200;
            saveFileButton.Height = 50;
            saveFileButton.Left = 20;
            saveFileButton.Top = 130;
            saveFileButton.Font = new Font("Arial", 12f);
            saveFileButton.BackColor = Color.LightGray;
            saveFileButton.FlatStyle = FlatStyle.System;
            saveFileButton.Text = "SELECT SAVE FILE";
            saveFileButton.Parent = converterDialog;
            TextBox selectedSaveFile = new TextBox();
            selectedSaveFile.ReadOnly = true;
            selectedSaveFile.Multiline = false;
            selectedSaveFile.BackColor = Color.White;
            selectedSaveFile.Width = 400;
            selectedSaveFile.Height = 50;
            selectedSaveFile.Text = "<output file>";
            selectedSaveFile.Left = 240;
            selectedSaveFile.Top = 130 + (25 - 12);
            selectedSaveFile.Parent = converterDialog;
            Button convertButton = new Button();
            convertButton.Width = 300;
            convertButton.Height = 50;
            convertButton.Left = 20;
            convertButton.Top = 200;
            convertButton.Font = new Font("Arial", 16f);
            convertButton.BackColor = Color.LightGray;
            convertButton.FlatStyle = FlatStyle.System;
            convertButton.Text = "CONVERT";
            convertButton.Parent = converterDialog;
            Button clearButton = new Button();
            clearButton.Width = 300;
            clearButton.Height = 50;
            clearButton.Left = 340;
            clearButton.Top = 200;
            clearButton.Font = new Font("Arial", 16f);
            clearButton.BackColor = Color.LightGray;
            clearButton.FlatStyle = FlatStyle.System;
            clearButton.Text = "CLEAR";
            clearButton.Parent = converterDialog;
            TextBox outputLog = new TextBox();
            outputLog.ReadOnly = true;
            outputLog.Multiline = true;
            outputLog.AcceptsReturn = true;
            outputLog.ScrollBars = ScrollBars.Vertical;
            outputLog.BackColor = Color.White;
            outputLog.Width = 620;
            outputLog.Height = 160;
            outputLog.Font = new Font("Arial", 11f);
            outputLog.Text = "Select a file and output to begin conversion";
            outputLog.Left = 20;
            outputLog.Top = 270;
            outputLog.Parent = converterDialog;
            clearButton.Click += (object sender, EventArgs e) =>
            {
                openPath = null;
                savePath = null;
                selectedFile.Text = "<input file>";
                selectedSaveFile.Text = "<output file>";
                outputLog.Text = "Select a file and output to begin conversion";
                selectedFile.Update();
                selectedSaveFile.Update();
                outputLog.Update();
            };
            openFileButton.Click += (object sender, EventArgs e) =>
            {
                OpenFileDialog newOpenFileDialog = new OpenFileDialog();
                newOpenFileDialog.Filter = "OBJ File|*.obj";
                newOpenFileDialog.Multiselect = false;
                newOpenFileDialog.InitialDirectory = @"C:\";
                if (newOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openPath = newOpenFileDialog.FileName;
                    selectedFile.Text = openPath;
                    selectedFile.Update();
                }
            };
            saveFileButton.Click += (object sender, EventArgs e) =>
            {
                SaveFileDialog newSaveFileDialog = new SaveFileDialog();
                newSaveFileDialog.Filter = "IGWO File|*.igwo";
                newSaveFileDialog.InitialDirectory = openPath != null ? Path.GetDirectoryName(openPath) : @"C:\";
                if (newSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    savePath = newSaveFileDialog.FileName;
                    selectedSaveFile.Text = savePath;
                    selectedSaveFile.Update();
                }
            };
            convertButton.Click += (object sender, EventArgs e) =>
            {
                if (openPath == null | savePath == null)
                {
                    outputLog.AppendText(Environment.NewLine);
                    outputLog.AppendText("Please select file input and output");
                    return;
                }
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("-----------------------------------------");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("Input: " + openPath);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("Output: " + savePath);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("Beginning conversion process");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);

                Stopwatch timer = new Stopwatch();
                timer.Start();

                var output = InstantGameworksObject.Convert.ConvertOBJToIGWO(File.ReadAllLines(openPath));

                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(output.Positions.Length + "\tvertices");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(output.TextureCoordinates.Length + "\ttexture coordinates");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(output.Normals.Length + "\tnormals");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(output.Faces.Length + "\tfaces");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);

                DataHandler.WriteFile(savePath, output);

                long inputSize = new FileInfo(openPath).Length;
                long outputSize = new FileInfo(savePath).Length;

                outputLog.AppendText("Input file size:  " + inputSize / 1024f + " KB\t(" + inputSize / 1048576f + " MB)");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("Output file size: " + outputSize / 1024f + " KB\t(" + outputSize / 1048576f + " MB)");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("File conversion efficiency: " + ((float)inputSize / (float)outputSize) * 100f + "% (output is " + ((float)outputSize / (float)inputSize) * 100f + "% the size of the original)");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText(Environment.NewLine);

                timer.Stop();

                outputLog.AppendText("Finished (" + timer.ElapsedTicks / 1000000f + " seconds)");
                outputLog.AppendText(Environment.NewLine);
                outputLog.AppendText("Success!");
            };

            converterDialog.ShowDialog();
            
        }
    }
}
