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
        public ImageStorage()
        {
            InitializeComponent();
            init_event();
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
                                listBox1.Items.Add(dir_data[i].path);

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
                for (int i = 0; i < ofg.FileNames.Count(); i++)
                {
                    listBox1.Items.Add(ofg.FileNames[i]);
                    dir_data.Add(new Image_directory() { index = index, path = ofg.FileNames[i] });
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
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            dir_data.Clear();
            listBox1.Items.Clear();
        }
    }
}
