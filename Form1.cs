using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Ranchargen
{
    public partial class Form1 : Form
    {
        public bool ismax = false;
        public int NormalWidth, NormalHeight;
        public int NormalLTX, NormalLTY;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Setting().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {      
            refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!(File.Exists(readwriteini.readwriteini.iniPath)))
            {
                readwriteini.readwriteini.IniWriteValue("Setting", "Length", "1000");
                readwriteini.readwriteini.IniWriteValue("Setting", "UseNum", "true");
                readwriteini.readwriteini.IniWriteValue("Setting", "UseLow", "true");
                readwriteini.readwriteini.IniWriteValue("Setting", "UseUpp", "true");
                readwriteini.readwriteini.IniWriteValue("Setting", "UseSpe", "false");
                readwriteini.readwriteini.IniWriteValue("Setting", "UseCia", "false");
                readwriteini.readwriteini.IniWriteValue("Size", "Width", this.Width.ToString());
                readwriteini.readwriteini.IniWriteValue("Size", "Height", this.Height.ToString());
                readwriteini.readwriteini.IniWriteValue("WindowsState", "WindowsState", "Normal");
                readwriteini.readwriteini.IniWriteValue("Location", "X",this.Location.X.ToString());
                readwriteini.readwriteini.IniWriteValue("Location", "Y", this.Location.Y.ToString());
            }
            this.Width=Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Size", "Width"));
            this.Height=Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Size", "Height"));
            switch (readwriteini.readwriteini.IniReadValue("WindowsState", "WindowsState"))
            {
                case "Normal": 
                    this.WindowState = FormWindowState.Normal; 
                    NormalWidth = this.Width; NormalHeight = this.Height;              
                    break;
                case "Maximized": 
                    this.WindowState = FormWindowState.Maximized; 
                    ismax = true; 
                    break;
                default: break;
            }
            NormalLTX = Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Location", "X"));
            NormalLTY = Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Location", "Y"));
            this.Location = new Point(NormalLTX, NormalLTY);
        }
        private string randword(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, bool useCia)
        {
            if (useCia && (useNum || useLow || useUpp || useSpe))
            {
                string str = null;
                Ranchicha.Ranchicha rancc = new Ranchicha.Ranchicha();
                System.Random r = new Random(Guid.NewGuid().GetHashCode());
                for (int i = 0; i < length; i++)
                {
                    int a = r.Next(0, 3);
                    string cc = string.Join("", rancc.GenerateChineseWords(1).ToArray());
                    string rnd = ranord.ranord.GetRandomString(1, useNum, useLow, useUpp, useSpe, "");
                    if (a == 0)
                    {
                        str += cc;
                    }
                    else
                    {
                        str += rnd;
                    }
                }
                return str;
            }
            else if ((!useCia) && (useNum || useLow || useUpp || useSpe))
            {
                return ranord.ranord.GetRandomString(length, useNum, useLow, useUpp, useSpe, "");
            }
            else
            {
                string sr = null;
                Ranchicha.Ranchicha rancc = new Ranchicha.Ranchicha();
                for (int a = 0; a < length; a++)
                {
                    string xx = string.Join("", rancc.GenerateChineseWords(1).ToArray());
                    sr += xx;
                }
                return sr;
            }
        }
        private void refresh()
        {
            int length = Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Setting", "Length"));
            bool usenum = readwriteini.readwriteini.IniReadValue("Setting", "UseNum") == "true" ? true : false;
            bool uselow = readwriteini.readwriteini.IniReadValue("Setting", "UseLow") == "true" ? true : false;
            bool useupp = readwriteini.readwriteini.IniReadValue("Setting", "UseUpp") == "true" ? true : false;
            bool usespe = readwriteini.readwriteini.IniReadValue("Setting", "UseSpe") == "true" ? true : false;
            bool usecia = readwriteini.readwriteini.IniReadValue("Setting", "UseCia") == "true" ? true : false;
            richTextBox1.Text = randword(length, usenum, uselow, useupp, usespe, usecia);
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string FilePath = String.Empty;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|All files(*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = String.Empty;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FilePath = saveFileDialog1.FileName.ToString();
                }
                StreamWriter sw = new StreamWriter(FilePath);
                sw.Write(richTextBox1.Text);
                sw.Dispose();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string FilePath = String.Empty;
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "文本文件(*.txt)|*.txt|All files(*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = String.Empty;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog1.FileName.ToString();
                }
                StreamReader sr = new StreamReader(FilePath);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Dispose();
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {   
            string windowstate = string.Empty;
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: windowstate = "Maximized"; break;
                case FormWindowState.Normal: windowstate = "Normal"; break;
                default: break;
            }
            if (!(this.WindowState == FormWindowState.Normal))
            {
                readwriteini.readwriteini.IniWriteValue("Size", "Width", NormalWidth.ToString());
                readwriteini.readwriteini.IniWriteValue("Size", "Height", NormalHeight.ToString());
            }
            else
            {
                readwriteini.readwriteini.IniWriteValue("Size", "Width", this.Width.ToString());
                readwriteini.readwriteini.IniWriteValue("Size", "Height", this.Height.ToString());
            }
            readwriteini.readwriteini.IniWriteValue("WindowsState", "WindowsState", windowstate);
            readwriteini.readwriteini.IniWriteValue("Location", "X", NormalLTX.ToString());
            readwriteini.readwriteini.IniWriteValue("Location", "Y", NormalLTY.ToString());
            System.Environment.Exit(0);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                NormalWidth = this.Width;
                NormalHeight = this.Height;
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                NormalLTX = this.Location.X;
                NormalLTY = this.Location.Y;
            }
        }
    }
}
