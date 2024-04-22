using System;
using System.IO;
using System.Windows.Forms;

namespace CourseworkFifteen.texture
{
    public partial class AccountForm : Form
    {
        DataBaseConnect dataBase = new DataBaseConnect();
        LanguageManager language = new LanguageManager();
        SoundManager sound = new SoundManager();
        private bool Bacing;

        public AccountForm()
        {
            language.LanguageCheckAndSet();
            InitializeComponent();
            LoadData();
            LoadForm();
        }

        private void LoginAccount_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            AccountOnPanel.Visible = false;
            AccountOffPanel.Visible = false;
            EntrancePanel.Visible = true;

            if (Properties.Settings.Default.Language == "ru")
                LoginOnBut.Text = "Войти";
            else
                LoginOnBut.Text = "Login to account";
        }

        private void RegisterAccount_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            AccountOnPanel.Visible = false;
            AccountOffPanel.Visible = false;
            EntrancePanel.Visible = true;
            if (Properties.Settings.Default.Language == "ru")
                LoginOnBut.Text = "Зарегистрировать";
            else
                LoginOnBut.Text = "Register";
        }

        private void ExitAccount_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            string Aud;
            string Lan;

            using (StreamReader reader = new StreamReader("SettingsData.txt"))
            {
                Aud = reader.ReadLine();
                Lan = reader.ReadLine();
                reader.Close();
            }

            using (StreamWriter writer = new StreamWriter("SettingsData.txt"))
            {
                writer.WriteLine(Aud);
                writer.WriteLine(Lan);
                writer.Close();
            }

            Properties.Settings.Default.IdUser = 0;

            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            AccountOnPanel.Visible = false;
            AccountOffPanel.Visible = true;
            EntrancePanel.Visible = false;
        }

        private void LoadForm()
        {
            if (Properties.Settings.Default.IdUser != 0)
            {
                AccountOnPanel.Visible = true;
                AccountOffPanel.Visible = false;
                EntrancePanel.Visible = false;

                string data = dataBase.getDataAccount(Properties.Settings.Default.IdUser);
                MessageBox.Show("DATA >> " + data);

                if (data != "")
                {
                    string[] words = data.Split(' ');
                    if (Properties.Settings.Default.Language == "ru")
                    {
                        label2.Text = $"Логин: {words[1]}";
                        label3.Text = $"Побед: {words[5]}";
                        label4.Text = $"Поражений: {words[7]}";
                    }
                    else
                    {
                        label2.Text = $"Login: {words[1]}";
                        label3.Text = $"Wins: {words[5]}";
                        label4.Text = $"Defeats: {words[7]}";
                    }
                }
            }
            else
            {
                AccountOnPanel.Visible = false;
                AccountOffPanel.Visible = true;
                EntrancePanel.Visible = false;
            }
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

        private void LoginOnBut_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            if (LoginOnBut.Text == "Зарегистрировать" || LoginOnBut.Text == "Register")
            {
                string log = textBox1.Text;
                string pas = textBox2.Text;

                if (log != "" && pas != "")
                {
                    int id = dataBase.AddAccount(log, pas);
                    if (id > 0)
                    {
                        Properties.Settings.Default.IdUser = id;
                        LoadForm();
                        SaveAccountInFile(id);
                    }
                    else
                    {
                        AccountOnPanel.Visible = false;
                        AccountOffPanel.Visible = true;
                        EntrancePanel.Visible = false;
                        MessageBox.Show("Непредвиденная ошибка");
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    sound.PlayOneShotAudio(2);
                    MessageBox.Show("Логин или пароль не могут быть пустыми");
                }
            }
            else
            {
                string log = textBox1.Text;
                string pas = textBox2.Text;

                if (log != "" && pas != "")
                {
                    int id = dataBase.CheckAccount(log, pas);
                    if (id > 0)
                    {
                        Properties.Settings.Default.IdUser = id;
                        LoadForm();
                        SaveAccountInFile(id);
                    }
                    else
                    {
                        AccountOnPanel.Visible = false;
                        AccountOffPanel.Visible = true;
                        EntrancePanel.Visible = false;
                        sound.PlayOneShotAudio(2);
                        MessageBox.Show("Непредвиденная ошибка");
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    sound.PlayOneShotAudio(2);
                    MessageBox.Show("Логин или пароль не могут быть пустыми");
                }
            }
        }

        private void SaveAccountInFile(int idAccount)
        {
            string Aud;
            string Lan;

            using (StreamReader reader = new StreamReader("SettingsData.txt"))
            {
                Aud = reader.ReadLine();
                Lan = reader.ReadLine();
                reader.Close();
            }

            using (StreamWriter writer = new StreamWriter("SettingsData.txt"))
            {
                writer.WriteLine(Aud);
                writer.WriteLine(Lan);
                writer.WriteLine(idAccount);
                writer.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            Bacing = true;
            SettingsForm settings = new SettingsForm();
            settings.Show();
            Close();
        }

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Bacing)
                Application.Exit();
        }
    }
}
