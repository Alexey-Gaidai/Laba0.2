using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public List<People> Peoples = new List<People>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                label1.Text = "vvedite dannie";
            else
            {
                People pep = new People(textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text));
                Peoples.Add(pep);
                dataGridView1.Rows.Add(pep.name, pep.lastname, pep.sex, pep.height);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(People));

           
            using (FileStream fs = new FileStream(@"D:\persons.xml", FileMode.OpenOrCreate))
            {

                formatter.Serialize(fs, Peoples);

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(People));
            using (FileStream fs = new FileStream(textBox5.Text, FileMode.Open))
            {
               
                 People[] newPerson = (People[])formatter.Deserialize(fs);
                foreach (People p in newPerson)
                {
                    Peoples.Add(p);
                }
               
            }
            for (int i = 0; i < Peoples.Count; i++)
                dataGridView1.Rows.Add(Peoples[i].name, Peoples[i].lastname, Peoples[i].sex, Peoples[i].height);
        }
    }

    public class People
    {
        public string name;
        public string lastname;
        public string sex;
        public int height;

        public People()
        {

        }
        public People(string _name, string _lastname, string _sex, int _height)
        {
            this.name = _name;
            this.lastname = _lastname;
            this.sex = _sex;
            this.height = _height;
        }

    }
}
