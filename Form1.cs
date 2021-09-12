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

        public void averageHeight()//метод расчета среднего роста
        {
            double heights = 0;
            double avg = 0;
            foreach (var p in peoples1.Peoples)
            {
                heights += p.height;
            }
            avg = heights / peoples1.Peoples.Count;
            label13.Text = "Средний рост:"+avg.ToString();
        }

        public void maxHeightMale()//метод расчета максимального роста среди мужиков
        {
            double max = 0;
            string maxname = "", maxlastname = "";
            foreach (var p in peoples1.Peoples)
            {
                if (max < p.height & p.sex == "male")
                {
                    max = p.height;
                    maxname = p.name;
                    maxlastname = p.lastname;
                }
            }
            label8.Text = "Max male height: " + max.ToString() + "\n" + maxname + " " + maxlastname;
        }
        public void maxHeightFemale()//метод расчета максимального роста среди женщин
        {
            double max = 0;
            string maxfemname = "", maxfemlastname = "";
            foreach (var p in peoples1.Peoples)//форыч просто форыч
            {
                if (max < p.height & p.sex == "female")
                {
                    max = p.height;
                    maxfemname = p.name;
                    maxfemlastname = p.lastname;
                }

            }
            label9.Text = "Max fem height: " + max.ToString()+"\n"+maxfemname+" "+maxfemlastname;
        }

        private void button1_Click(object sender, EventArgs e)//добавление нового пипла в таблицу
        {
            try
            {
                if (textBoxName.Text == "" || textBoxLastname.Text == "" || textBoxSex.Text == "" || textBoxHeight.Text == "")//проверка на то все ли данные введены
                    label1.Text = "Введите все данные!!!";
                else if (textBoxSex.Text == "male" || textBoxSex.Text == "female")//выполняется добавление только при условии что пол указан верно
                {
                    People pep = new People(textBoxName.Text, textBoxLastname.Text, textBoxSex.Text, Convert.ToInt32(textBoxHeight.Text));
                    peoples1.Peoples.Add(pep);
                    dataGridView1.Rows.Add(pep.name, pep.lastname, pep.sex, pep.height);
                    label1.Text = "";//зануляем 
                    textBoxName.Text = "";
                    textBoxLastname.Text = "";
                    textBoxSex.Text = "";
                    textBoxHeight.Text = "";
                }
            }
            catch(System.FormatException)
            {
                label1.Text = "Введенный рост не число";
            }
        }

        private void button2_Click(object sender, EventArgs e)//сохранение таблицы в файл
        {
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                using (StreamWriter sw = new StreamWriter(textBoxSave.Text))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    if (peoples1.Peoples.Count != 0)
                    {
                        serializer.Serialize(writer, peoples1);
                    }
                    else
                        label12.Text = "В таблице нет данных!";
                }
            }
            catch (System.ArgumentException)
            {
                label12.Text = "некорректный путь";
            }
            textBoxSave.Text = "";
        }


        private void button3_Click(object sender, EventArgs e)//чтение файла
        {
            try
            {
                if (textBoxOpen.Text == "")//обрубаем если не ввели директорию
                    label7.Text = "Введите Директорию!";
                else
                {
                    StreamReader SR = new StreamReader(textBoxOpen.Text);
                    string readtext = SR.ReadToEnd();
                    mylistcollection deserialized = JsonConvert.DeserializeObject<mylistcollection>(readtext);
                    for (int i = 0; i < deserialized.Peoples.Count; i++)
                    {
                        People dd = new People(deserialized.Peoples[i].name, deserialized.Peoples[i].lastname, deserialized.Peoples[i].sex, deserialized.Peoples[i].height);
                        peoples1.Peoples.Add(dd);
                        dataGridView1.Rows.Add(deserialized.Peoples[i].name, deserialized.Peoples[i].lastname, deserialized.Peoples[i].sex, deserialized.Peoples[i].height);
                    }
                    SR.Close();
                    textBoxOpen.Text = "";
                }
            }
            catch(System.IO.FileNotFoundException)//обработка ошибки не найденного файла
            {
                label7.Text = "invalid directory";
                textBoxOpen.Text = "";
            }
            catch(System.NullReferenceException)//обработка ошибки пустого файла
            {
                label7.Text = "invalid file";
                textBoxOpen.Text = "";
            }
            catch (System.ArgumentException)//обработка ошибки пути
            {
                label7.Text = "некорректный путь";
                textBoxOpen.Text = "";
            }
        }
        private void button4_Click(object sender, EventArgs e)//получить информацию по заданию
        {
            maxHeightMale();
            maxHeightFemale();
            averageHeight();
        }
    }
    public class mylistcollection//класс содержащий список экземпляров класса пипл
    {
        public List<People> Peoples = new List<People>();
    }

    public class People // создали класс для хранения информации о людях
    {
        public string name;
        public string lastname;
        public string sex;
        public int height;

        public People()//пустой конструктор
        {

        }
        public People(string _name, string _lastname, string _sex, int _height)//конструктор
        {
            this.name = _name;
            this.lastname = _lastname;
            this.sex = _sex;
            this.height = _height;
        }

    }
}
