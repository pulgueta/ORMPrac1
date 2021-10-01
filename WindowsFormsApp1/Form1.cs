using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //creating the objects

        public List<WindowsFormsApp1.Model.ALUMNI> oAlumni;
        public List<WindowsFormsApp1.Model.DIRECTIVE> oDirective;
        public List<WindowsFormsApp1.Model.SUBJECT> oSubject;
        public List<WindowsFormsApp1.Model.JOINED> oJoined;

        int index = 0;

        private void Form1_Load(object sender, EventArgs e) {
            //fill combobox
            comboBox1.Items.Add("ALUMNI");
            comboBox1.Items.Add("DIRECTIVE");
            comboBox1.Items.Add("SUBJECT");
            comboBox1.Items.Add("JOINED");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            using (WindowsFormsApp1.Model.DBPractEntities2 db = new WindowsFormsApp1.Model.DBPractEntities2()) {
                switch (comboBox1.SelectedIndex) {
                    case 0:
                        oAlumni = db.ALUMNI.ToList();
                        break;

                    case 1:
                        oDirective = db.DIRECTIVE.ToList();
                        break;

                    case 2:
                        oSubject = db.SUBJECT.ToList();
                        break;

                    case 3:
                        oJoined = db.JOINED.ToList();
                        break;
                }
            }

            index = 0;
            // fill function searches data in tables
            fill();
        }


        public void fill() {
            if (index < 0)
                index = 0;

            string str = "";

            switch (comboBox1.SelectedIndex) {
                // alumni searching
                case 0:
                    if (index >= oAlumni.Count)
                        index = oAlumni.Count - 1;
                    str = oAlumni[index].Id.ToString() + ". " + oAlumni[index].Name + ", from " + oAlumni[index].City + ", " + oAlumni[index].Age + " years old";
                    break;

                // case 1 searches for different tables and uses find and model pracentities, we have the id, name and all stuff,
                // relates every information with the other tables
                case 1:
                    if (index >= oAlumni.Count)
                        index = oDirective.Count - 1;
                    using (WindowsFormsApp1.Model.DBPractEntities2 db = new WindowsFormsApp1.Model.DBPractEntities2()) {
                        oAlumni = db.ALUMNI.ToList();
                        str = oDirective[index].Id.ToString() + ". " + oDirective[index].Name + ", is the teacher of " + oAlumni.Find(a => a.Id == (int)oDirective[index].Id_Alumni).Name; ;
                    }
                    break;

                // subject table
                case 2:
                    if (index >= oSubject.Count)
                        index = oSubject.Count - 1;
                    str = oSubject[index].Cod.ToString() + ". " + oSubject[index].Name + " -- Starting date:  " + oSubject[index].Init_Date + " -- Takes: " + oSubject[index].Duration;
                    break;

                // case 3 finds alumni and subject table where we get the info
                case 3:
                    if (index >= oJoined.Count)
                        index = oJoined.Count - 1;

                    using (WindowsFormsApp1.Model.DBPractEntities2 db = new WindowsFormsApp1.Model.DBPractEntities2()) {
                        oAlumni = db.ALUMNI.ToList();
                        oSubject = db.SUBJECT.ToList();

                        str = oJoined[index].Id.ToString() + ". " + oAlumni.Find(a => a.Id == (int)oJoined[index].Id_Alumni).Name + "  studies " + oSubject.Find(a => a.Cod == (int)oJoined[index].Cod_Subject).Name;
                    }

                    break;
            }

            // textbox shows the string of switch case

            textBox1.Text = str;
        }

        private void button1_Click(object sender, EventArgs e) {
            index--;
            fill();
        }

        private void button2_Click(object sender, EventArgs e) {
            index++;
            fill();
        }
    }
}