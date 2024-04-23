using System;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class WinForms : Form
    {
        DataBaseConnect dataBase = new DataBaseConnect();
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();
        private bool Backing;
        public WinForms(int MapSize, int TimeMinute, int TimeSecond, int StepNum)
        {
            language.LanguageCheckAndSet();
            InitializeComponent();

            sound.PlayOneShotAudio(7);
            if(Properties.Settings.Default.IdUser != 0)
            {
                string data = dataBase.getDataAccount(Properties.Settings.Default.IdUser);
                if(data != "")
                {
                    string[] words = data.Split(' ');

                    string[] WinCatOsn = words[6].Split('-');
                    WinCatOsn[MapSize - 2] = (Convert.ToInt32(WinCatOsn[MapSize - 2]) + 1).ToString();
                    string Ret = string.Join("-", WinCatOsn);

                    int WinsOverall = Convert.ToInt32(words[5]) + 1;

                    if (Properties.Settings.Default.Language == "ru")
                    {
                        labelTime.Text = $"Время: {TimeMinute}:{TimeSecond}";
                        labelStep.Text = $"Количество шагов: {StepNum}";
                        labelWins.Text = $"Побед {MapSize}x{MapSize}: {WinCatOsn[MapSize - 2]}";
                        labelWinsOverall.Text = $"Всего побед: {WinsOverall}";
                    }
                    else
                    {
                        labelTime.Text = $"Time: {TimeMinute}:{TimeSecond}";
                        labelStep.Text = $"Number of steps: {StepNum}";
                        labelWins.Text = $"Wins {MapSize}x{MapSize}: {WinCatOsn[MapSize - 2]}";
                        labelWinsOverall.Text = $"Total wins: {WinsOverall}";
                    }


                    int losingOverall = Convert.ToInt32(words[7]);
                    int BestStep = Convert.ToInt32(words[4]);
                    int VictoryOverall = Convert.ToInt32(words[5]) + 1;
                    int BestTimeMinute = Convert.ToInt32(words[3].Substring(3, 2));
                    int BestTimeSecond = Convert.ToInt32(words[3].Substring(6, 2));
                    if (BestTimeMinute > TimeMinute)
                    {
                        //++
                        if (BestStep > StepNum)
                        {
                            dataBase.UpdateData(TimeMinute, TimeSecond, VictoryOverall, losingOverall, StepNum, MapSize);
                        }
                        else
                        {
                            dataBase.UpdateData(TimeMinute, TimeSecond, VictoryOverall, losingOverall, BestStep, MapSize);
                        }
                    }
                    else if (BestTimeMinute == TimeMinute && BestTimeSecond > TimeSecond)
                    {
                        //++
                        if (BestStep > StepNum)
                        {
                            dataBase.UpdateData(TimeMinute, TimeSecond, VictoryOverall, losingOverall, StepNum, MapSize);
                        }
                        else
                        {
                            dataBase.UpdateData(TimeMinute, TimeSecond, VictoryOverall, losingOverall, BestStep, MapSize);
                        }
                    }
                    else
                    {
                        if (BestStep > StepNum)
                        {
                            dataBase.UpdateData(0, 0, VictoryOverall, losingOverall, StepNum, MapSize);
                        }
                        else
                        {
                            dataBase.UpdateData(0, 0, VictoryOverall, losingOverall, BestStep, MapSize);
                        }
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.Language == "ru")
                {
                    labelTime.Text = $"Время: {TimeMinute}:{TimeSecond}";
                    labelStep.Text = $"Количество шагов: {StepNum}";
                    labelWins.Text = $"Побед {MapSize}x{MapSize}: -";
                    labelWinsOverall.Text = $"Всего побед: -";
                }
                else
                {
                    labelTime.Text = $"Time: {TimeMinute}:{TimeSecond}";
                    labelStep.Text = $"Number of steps: {StepNum}";
                    labelWins.Text = $"Wins {MapSize}x{MapSize}: -";
                    labelWinsOverall.Text = $"Total wins: -";
                }
            }
        }

        private void WinForms_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            NewGameForm newGameForm = new NewGameForm();
            newGameForm.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }

        private void WinForms_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }
    }
}
