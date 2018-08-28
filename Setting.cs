using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Ranchargen
{
    public partial class Setting : Form
    {
        public bool error = false;
        public bool err = false;
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            int length=Convert.ToInt32(readwriteini.readwriteini.IniReadValue("Setting", "Length"));
            bool usenum = readwriteini.readwriteini.IniReadValue("Setting", "UseNum") == "true" ? true : false;
            bool uselow = readwriteini.readwriteini.IniReadValue("Setting", "UseLow") == "true" ? true : false;
            bool useupp = readwriteini.readwriteini.IniReadValue("Setting", "UseUpp") == "true" ? true : false;
            bool usespe = readwriteini.readwriteini.IniReadValue("Setting", "UseSpe") == "true" ? true : false;
            bool usecia = readwriteini.readwriteini.IniReadValue("Setting", "UseCia") == "true" ? true : false;
            textBox1.Text = length.ToString();
            checkBox1.Checked = usenum;
            checkBox2.Checked = uselow;
            checkBox3.Checked = useupp;
            checkBox4.Checked = usespe;
            checkBox5.Checked = usecia;
            label2.Text = "";
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            string length = textBox1.Text;
            string usenum = checkBox1.Checked ? "true" : "false";
            string uselow = checkBox2.Checked ? "true" : "false";
            string useupp = checkBox3.Checked ? "true" : "false";
            string usespe = checkBox4.Checked ? "true" : "false";
            string usecia = checkBox5.Checked ? "true" : "false";
            if (!(error||err))
            {
                readwriteini.readwriteini.IniWriteValue("Setting", "Length", length);
                readwriteini.readwriteini.IniWriteValue("Setting", "UseNum", usenum);
                readwriteini.readwriteini.IniWriteValue("Setting", "UseLow", uselow);
                readwriteini.readwriteini.IniWriteValue("Setting", "UseUpp", useupp);
                readwriteini.readwriteini.IniWriteValue("Setting", "UseSpe", usespe);
                readwriteini.readwriteini.IniWriteValue("Setting", "UseCia", usecia);
            }
            e.Cancel = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label2.Text = "不能为空";
                error = true;
            }
            else
            {
                Regex rex = new Regex(@"^(-|\+)?[0-9][0-9]*(\.)?[0-9]*$");
                if (rex.IsMatch(textBox1.Text))
                {
                    Regex reg = new Regex(@"^(-|\+)?[0-9][0-9]*$");
                    if (reg.IsMatch(textBox1.Text))
                    {
                        if (Convert.ToInt32(textBox1.Text) == 0)
                        {
                            label2.Text = "不能为0";
                            error = true;
                        }
                        else if (Convert.ToInt32(textBox1.Text) < 0)
                        {
                            label2.Text = "不能为负";
                            error = true;
                        }
                        else
                        {
                            label2.Text = "";
                            error = false;
                        }
                    }
                    else
                    {
                        label2.Text = "必须是整数";
                        error = true;
                    }
                }
                else
                {
                    label2.Text = "必须是数字";
                    error = true;
                }
            }
        }
        private void wrong()
        {
            bool isallempty =
                !(checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || 
                checkBox4.Checked || checkBox5.Checked);
            if (isallempty)
            {
                MessageBox.Show("至少要有一个勾选", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                err = true;
            }
            else
            {
                err = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            wrong();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            wrong();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            wrong();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            wrong();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            wrong();
        }
    }
}
