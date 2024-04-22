using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class LeaderboardForm : Form
    {
        DataBaseConnect dataBase = new DataBaseConnect();
        SoundManager sound = new SoundManager();
        private bool Backing;

        public LeaderboardForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            Statistic statistic = new Statistic();
            statistic.Show();
            Close();
        }
        //Количество побед
        //Лучшее время
        //Лучшее количество шагов
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == 0) //Количество побед
                {
                    DataViewer.Rows.Clear();
                    List<string[]> data = dataBase.getLider("SELECT TOP (100) Login, BestTime, BestStep, VictoryOverall FROM ListOfUsers ORDER BY VictoryOverall DESC");

                    foreach (string[] s in data)
                    {
                        DataViewer.Rows.Add(s);
                    }
                }
                else if (comboBox1.SelectedIndex == 1) //Лучшее время
                {
                    DataViewer.Rows.Clear();
                    List<string[]> data = dataBase.getLider("SELECT TOP (100) Login, BestTime, BestStep, VictoryOverall FROM ListOfUsers ORDER BY BestTime");

                    foreach (string[] s in data)
                    {
                        DataViewer.Rows.Add(s);
                    }
                }
                else if (comboBox1.SelectedIndex == 2) //Лучшее количество шагов
                {
                    DataViewer.Rows.Clear();
                    List<string[]> data = dataBase.getLider("SELECT TOP (100) Login, BestTime, BestStep, VictoryOverall FROM ListOfUsers ORDER BY BestStep");

                    foreach (string[] s in data)
                    {
                        DataViewer.Rows.Add(s);
                    }
                }
            } 
            catch(Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка заполнения таблицы >> {exp.Message} >> {exp.StackTrace}");
            }
        }

        private void LeaderboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }
    }
}
