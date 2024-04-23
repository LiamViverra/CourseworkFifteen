using System;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class Statistic : Form
    {
        DataBaseConnect dataBase = new DataBaseConnect();
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();
        private bool Backing;

        public Statistic()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();
        }

        private void Statistic_Load(object sender, EventArgs e)
        {
            try
            {
               if (Properties.Settings.Default.IdUser != 0)
               {
                    string data = dataBase.getDataAccount(Properties.Settings.Default.IdUser);
                    if(data != "")
                    {
                        string[] words = data.Split(' ');

                        if (words[6] == "" || words[6] == "00000000") words[6] = "0-0-0-0-0-0-0-0";
                        string[] WinCatOsn = words[6].Split('-');

                        //MessageBox.Show($"Language >> {Properties.Settings.Default.Language}");
                        if (Properties.Settings.Default.Language == "ru")
                        {
                            labelTime.Text = "Лучшее время: " + words[3];
                            labelStep.Text = "Лучшее кол-во шагов: " + words[4];
                            labelWinsOverall.Text = "Всего побед: " + words[5];
                            label3.Text = "Всего поражений: " + words[7];

                            label2.Text = "Побед 2x2: " + WinCatOsn[0];
                            label4.Text = "Побед 3x3: " + WinCatOsn[1];
                            label5.Text = "Побед 4x4: " + WinCatOsn[2];
                            label6.Text = "Побед 5x5: " + WinCatOsn[3];

                            label10.Text = "Побед 6x6: " + WinCatOsn[4];
                            label9.Text = "Побед 7x7: " + WinCatOsn[5];
                            label8.Text = "Побед 8x8: " + WinCatOsn[6];
                            label7.Text = "Побед 9x9: " + WinCatOsn[7];
                        }
                        else
                        {
                            labelTime.Text = "Best time: " + words[3];
                            labelStep.Text = "Best number of steps: " + words[4];
                            labelWinsOverall.Text = "Total wins: " + words[5];
                            label3.Text = "Total defeats: " + words[7];

                            label2.Text = "Wins 2x2: " + WinCatOsn[0];
                            label4.Text = "Wins 3x3: " + WinCatOsn[1];
                            label5.Text = "Wins 4x4: " + WinCatOsn[2];
                            label6.Text = "Wins 5x5: " + WinCatOsn[3];

                            label10.Text = "Wins 6x6: " + WinCatOsn[4];
                            label9.Text = "Wins 7x7: " + WinCatOsn[5];
                            label8.Text = "Wins 8x8: " + WinCatOsn[6];
                            label7.Text = "Wins 9x9: " + WinCatOsn[7];
                        }
                    }
                    else
                    {
                        labelTime.Text = "Лучшее время: -";
                        labelStep.Text = "Лучшее кол-во шагов:  -";
                        labelWinsOverall.Text = "Всего побед:  -";
                        label3.Text = "Всего поражений:  -";

                        label2.Text = "Побед 2x2:  -";
                        label4.Text = "Побед 3x3:  -";
                        label5.Text = "Побед 4x4:  -";
                        label6.Text = "Побед 5x5:  -";

                        label10.Text = "Побед 6x6:  -";
                        label9.Text = "Побед 7x7:  -";
                        label8.Text = "Побед 8x8:  -";
                        label7.Text = "Побед 9x9:  -";
                    }

               }
            } 
            catch(Exception exp)
            {
                MessageBox.Show($"Ошибка получения данных об аккаунте >> {exp.Message} >> {exp.StackTrace}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }

        private void Statistic_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) //Таблица лидеров
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            LeaderboardForm leaderboardForm = new LeaderboardForm();
            leaderboardForm.Show();
            Close();
        }
    }
}
