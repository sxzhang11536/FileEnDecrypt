using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string newFileName;
        string key;
        byte[] buff;

        private void button3_Click(object sender, EventArgs e)  //open file button
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "All files (*.*)|*.*|Encrypted files (*.enc)|*.enc";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                //try
                //{
                    //string text = File.ReadAllText(file);
                    //size = text.Length;
                 //   if ((myStream=openFileDialog1.OpenFile())!=null)
                 //   {
                 //       using (myStream)
                 //       {
                 //
                 //       }
                 //   }
                //}
                //catch(Exception ex)
                //{
                 //   MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                //}
            }
            else
            {
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //this.textBox1.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)  //encrypt
        {
            
            //Stream myStream = null;
            if ((textBox2.Text == "")&&(textBox1.Text==""))
            {
                MessageBox.Show("Please enter a key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter a key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                newFileName = textBox1.Text + ".enc";   //new output file name after encrypted
                key = textBox2.Text;
                if (File.Exists(newFileName))       //if the output file already exists
                {
                    if (MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                try
                {
                    //if (openFileDialog1.OpenFile() != null)
                    //{
                        buff = File.ReadAllBytes(textBox1.Text);
                        for (int i = 0; i < buff.Length; i++)
                        {
                            buff[i] = (byte)(buff[i] ^ key[i % key.Length]);    //do XOR for each byte
                        }
                        try
                        {
                            File.WriteAllBytes(newFileName, buff);  //write the result to the output file

                        }
                        catch
                        {
                            MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        MessageBox.Show("Operation completed successfully.");
                        return;
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)  //decrypt
        {
            
            if ((textBox2.Text == "") && (textBox1.Text == ""))
            {
                MessageBox.Show("Please enter a key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter a key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                key = textBox2.Text;
                string fn = textBox1.Text;  //output file name
                if (fn.Length <= 4)
                {
                    MessageBox.Show("Not a .enc file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (fn.Substring(fn.Length - 4) != ".enc")
                    {
                        MessageBox.Show("Not a .enc file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        newFileName = fn.Substring(0, fn.Length - 4);
                        if (File.Exists(newFileName))
                        {
                            if (MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }
                        try
                        {
                            //if (openFileDialog1.OpenFile() != null)
                            //{
                            buff = File.ReadAllBytes(textBox1.Text);
                            for (int i = 0; i < buff.Length; i++)
                            {
                                buff[i] = (byte)(buff[i] ^ key[i % key.Length]);
                            }
                            try
                            {
                                File.WriteAllBytes(newFileName, buff);
                            }
                            catch
                            {
                                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            MessageBox.Show("Operation completed successfully.");
                            return;
                            //}
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
               

            }

        }
    }
}
