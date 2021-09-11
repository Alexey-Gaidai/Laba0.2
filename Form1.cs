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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        mylistcollection peoples1 = new mylistcollection();
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
                peoples1.Peoples.Add(pep);
                dataGridView1.Rows.Add(pep.name, pep.lastname, pep.sex, pep.height);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"D:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, peoples1);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader sw = new StreamReader(textBox5.Text);
            textBox5.Text = "";
            string readtext = sw.ReadToEnd();
            mylistcollection deserialized = JsonConvert.DeserializeObject<mylistcollection>(readtext);
            for (int i = 0; i < deserialized.Peoples.Count; i++)
            {
                 People dd = new People(deserialized.Peoples[i].name, deserialized.Peoples[i].lastname, deserialized.Peoples[i].sex, deserialized.Peoples[i].height);
                 peoples1.Peoples.Add(dd);
                 dataGridView1.Rows.Add(deserialized.Peoples[i].name, deserialized.Peoples[i].lastname, deserialized.Peoples[i].sex, deserialized.Peoples[i].height);
            }
            sw.Close();
        }
    }
    public class mylistcollection
    {
        public List<People> Peoples = new List<People>();
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
