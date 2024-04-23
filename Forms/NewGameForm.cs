using System;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class NewGameForm : Form
    {
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();
        public bool Backing;

        public NewGameForm()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)control;
                    if (radioButton.Checked)
                    {
                        string MapSizeCh = radioButton.Text[0].ToString();
                        int MapSize = Convert.ToInt32(MapSizeCh);
                        //MessageBox.Show("Размер карты: " + MapSize.ToString() + " клеток");

                        //MessageBox.Show($"MapSize >> {MapSize}");

                        Backing = true;
                        Form1 form = new Form1(false, MapSize);
                        form.Show();
                        Close();
                    }
                }
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }

        private void NewGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }

        private void NewGameForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(6);
        }
    }
}
