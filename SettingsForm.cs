using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class SettingsForm : Form
    {
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();

        public bool Backing;
        public bool AudioOnOff;

        public SettingsForm()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            EducationForm education = new EducationForm();
            education.Show();
            Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            try
            {
                string Aud = AudioOnOff.ToString();
                string Lan = comboBox1.Text.TrimEnd();

                using (StreamWriter writer = new StreamWriter("SettingsData.txt"))
                {
                    writer.WriteLine(Aud);
                    writer.WriteLine(Lan);
                    writer.Close();
                }
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка записи >> {exp.Message}");
            }

            Backing = true;
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }

        private void LoadData()
        {
            try
            {
                string Aud;
                string Lan;

                using (StreamReader reader = new StreamReader("SettingsData.txt"))
                {
                    Aud = reader.ReadLine();
                    Lan = reader.ReadLine();
                    reader.Close();
                }

                if (Aud == "True")
                {
                    AudioOnOff = true;
                    Image image = Properties.Resources.ImgBut3;
                    button3.BackgroundImage = image;
                }
                else
                {
                    AudioOnOff = false;
                    Image image = Properties.Resources.ImgBut4;
                    button3.BackgroundImage = image;
                }

                comboBox1.SelectedIndex = comboBox1.FindString(Lan);
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка чтения >> {exp.Message}");
                AudioOnOff = true;
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            if (AudioOnOff)
            {
                AudioOnOff = false;
                Image image = Properties.Resources.ImgBut4;
                button3.BackgroundImage = image;
            }
            else
            {
                AudioOnOff = true;
                Image image = Properties.Resources.ImgBut3;
                button3.BackgroundImage = image;
            }
        }

        private void AccountBut_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            texture.AccountForm form = new texture.AccountForm();
            form.Show();
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(6);
        }
    }
}
