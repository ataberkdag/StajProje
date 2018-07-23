using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Threading;

namespace APIProje
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            Sınıflar.DLLCount dllCounter = new Sınıflar.DLLCount();
            lblDLL.Text = "DLL Sayısı: " + Convert.ToString(dllCounter.dllFiles.Count());
            try
            {
                readXmlFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void readXmlFile()
        {
            XmlTextReader xmlReader = new XmlTextReader("System.config");
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchWord")
                {
                    xmlReader.Read();
                    if (!string.IsNullOrWhiteSpace(xmlReader.Value)) tbWord.Text = xmlReader.Value;
                    else tbWord.Text = "";
                }
                else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchUser")
                {
                    xmlReader.Read();
                    if (!string.IsNullOrWhiteSpace(xmlReader.Value)) tbUser.Text = xmlReader.Value;
                    else tbUser.Text = "";
                }
                else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SleepTime")
                {
                    xmlReader.Read();
                    cbTime.Text = xmlReader.Value;
                }
                else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ThreadCount")
                {
                    xmlReader.Read();
                    cbThread.Text = xmlReader.Value;
                }
            }
            xmlReader.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Sınıflar.DLLCount dllCounter = new Sınıflar.DLLCount();

            if (string.IsNullOrEmpty(tbUser.Text) && string.IsNullOrEmpty(tbWord.Text))
            {
                MessageBox.Show("İki alan boş bırakılamaz!");
            }
            else
            {
                double cbTimeInt = Convert.ToInt32(cbTime.Text);
                double dllCount = dllCounter.dllFiles.Count();
                
                if (dllCount < 20)
                {
                    if (cbTimeInt < dllCount * 2)
                    {
                        MessageBox.Show("Süre DLL sayısının en az 2 katı olmalıdır!");
                    }
                    else
                    {
                        Apply();
                    }
                }
                else
                {
                    if (cbTimeInt < dllCount * 0.7)
                    {
                        MessageBox.Show("Süre DLL sayısının en az 0.6 katı olmalıdır!");
                    }
                    else
                    {
                        Apply();
                    }
                }
            }
            
        }

        private void Apply()
        {
            UpdateXmlFileTry();
            MessageBox.Show("İşlem Başarılı!");
            ActiveForm.Close();
        }

        private void UpdateXmlFile(string keyword, int typeId) {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("System.config");

            XmlNodeList nodeList = xmlDoc.SelectNodes("/Settings/SearchWord");
            XmlNode nodeWord = nodeList[0];

            XmlNodeList nodeListForUser = xmlDoc.SelectNodes("/Settings/SearchUser");
            XmlNode nodeUser = nodeListForUser[0];
            if (typeId == 1)
            {
                nodeWord.InnerText = keyword;
                nodeUser.InnerText = "";
            }
            else if (typeId == 2)
            {
                nodeWord.InnerText = "";
                nodeUser.InnerText = keyword;
            }

            XmlNodeList nodeListSleep = xmlDoc.SelectNodes("/Settings/SleepTime");
            XmlNode nodeSleep = nodeListSleep[0];
            nodeSleep.InnerText = cbTime.Text;
            xmlDoc.Save("System.config");
        }

        private void UpdateXmlFileTry() {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("System.config");

            XmlNodeList nodeList = xmlDoc.SelectNodes("/Settings/SearchWord");
            XmlNode nodeWord = nodeList[0];
            
            XmlNodeList nodeListForUser = xmlDoc.SelectNodes("/Settings/SearchUser");
            XmlNode nodeUser = nodeListForUser[0];
            
            XmlNodeList nodeListSleep = xmlDoc.SelectNodes("/Settings/SleepTime");
            XmlNode nodeSleep = nodeListSleep[0];
            
            nodeWord.InnerText = tbWord.Text;
            nodeUser.InnerText = tbUser.Text;
            nodeSleep.InnerText = cbTime.Text;

            xmlDoc.Save("System.config");
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
