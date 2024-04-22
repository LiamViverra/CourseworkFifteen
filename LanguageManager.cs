using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    class LanguageManager
    {
        SoundManager sound = new SoundManager();

        public void LanguageCheckAndSet()
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

                //Русский
                //English
                //Татарский

                if (!String.IsNullOrEmpty(Lan))
                {
                    if (Lan == "Русский")
                    {
                        Lan = "ru-RU";
                        Properties.Settings.Default.Language = "ru";
                    }
                    else
                    {
                        Lan = "en-US";
                        Properties.Settings.Default.Language = "en";
                    }

                    System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Lan);
                    System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Lan);
                }
                else
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                    System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
                }

                if (!String.IsNullOrEmpty(Aud))
                {
                    if (Aud == "True")
                    {
                        Properties.Settings.Default.AudioOnOff = true;
                    }
                    else
                    {
                        Properties.Settings.Default.AudioOnOff = false;
                    }
                }
            }
            catch
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show("Ошибка чтения файла настроек // Файл будет пересоздан", "Error", MessageBoxButtons.OK);
                try
                {
                    string Aud = "True";
                    string Lan = "Русский";
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
                    MessageBox.Show($"Ошибка пересоздания файла >> {exp.Message}", "Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
