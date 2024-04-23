using System;
using System.IO;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class MainMenu : Form
    {
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();

        public MainMenu()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();

            LoadData();        
        }

        private void LoadData()
        {
            try
            {
                int id = 0;

                using (StreamReader reader = new StreamReader("SettingsData.txt"))
                {
                    reader.ReadLine();
                    reader.ReadLine();
                    string st = reader.ReadLine();
                    if (st != "")
                        id = Convert.ToInt32(st);
                    reader.Close();
                }

                if (id != 0)
                {
                    Properties.Settings.Default.IdUser = id;
                }
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка чтения >> {exp.Message}");
            }
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            if (File.Exists("GameData.txt") && new FileInfo("GameData.txt").Length > 0)
            {
                Form1 form = new Form1(true);
                form.Show();
                Hide();
            }
            else
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show("Начатая игра не найдена, чтобы начать игру нажмите на кнопку 'Новая игра/New game'");
            }
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            NewGameForm newGameForm = new NewGameForm();
            newGameForm.Show();
            Hide();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            SettingsForm settings = new SettingsForm();
            settings.Show();
            Hide();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Statistic statistic = new Statistic();
            statistic.Show();
            Hide();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
