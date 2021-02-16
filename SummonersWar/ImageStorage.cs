using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SummonersWar
{
    public partial class ImageStorage : Form
    {
        OpenFileDialog ofg = new OpenFileDialog();
        List<Image_directory> dir_data = new List<Image_directory>();
        
        string SetupFilePath = System.IO.Directory.GetCurrentDirectory() + "\\ImgSetup.json";
        int index = 0;
        bool IsForced = false;
        public ImageStorage()
        {
            InitializeComponent();
            init_event();
            this.textBox1.Text = "500";
        }

        public void init_event()
        {
            index = 0;
            ofg.Multiselect = true;

            bool IsSetupFile = File.Exists(SetupFilePath);
            if (IsSetupFile)
            {
                using (StreamReader sr = new StreamReader(SetupFilePath))
                {
                    string json = sr.ReadToEnd();

                    if (json != string.Empty)
                    {
                        try
                        {
                            dir_data = JsonConvert.DeserializeObject<List<Image_directory>>(json);
                            listBox1.Items.Clear();
                            for (int i = 0; i < dir_data.Count; i++)
                            {
                                listBox1.Items.Add(dir_data[i].index + " , " + Path.GetFileName(dir_data[i].path) + " , " + Convert.ToChar('A' + dir_data[i].index) + " , " + dir_data[i].delaytime + "ms" + " , F : " + dir_data[i].IsForceClick);
                                index++;
                            }

                        }
                        catch (Exception ex)
                        {
                            // class json format error
                            listBox1.Items.Clear();
                        }
                    }

                }
                //JsonConvert
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(SetupFilePath))
                {
                    sw.Close();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //dir_data = new List<Image_directory>();
            using (StreamWriter sw = new StreamWriter(SetupFilePath))
            {
                string json = JsonConvert.SerializeObject(dir_data);
                sw.WriteLine(json);
                sw.Close();
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ofg.ShowDialog() == DialogResult.OK)
            {
                int time = (this.textBox1.Text != string.Empty) ? Convert.ToInt32(this.textBox1.Text) : 500;
                for (int i = 0; i < ofg.FileNames.Count(); i++)
                {
                    listBox1.Items.Add(index.ToString() + " , " + Path.GetFileName(ofg.FileNames[i]) + " , " + Convert.ToChar(index + 'A') + " , " + time + " F : " + this.IsForced);
                    //listBox1.Items.Add(ofg.FileNames[i]);
                    dir_data.Add(new Image_directory() { index = index, path = ofg.FileNames[i], delaytime = time, IsForceClick = this.IsForced });
                    index++;
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                dir_data.RemoveAt(dir_data.Count - 1);
                index = (index > 0) ? index - 1 : index; 
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            dir_data.Clear();
            listBox1.Items.Clear();
            index = 0;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsForced = !IsForced;
        }
    }
}
