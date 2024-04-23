using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class EducationForm : Form
    {
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();
        private bool Backing;

        public EducationForm()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Backing = true;
            SettingsForm settings = new SettingsForm();
            settings.Show();
            Close();
        }

        private void EducationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }
    }
}
