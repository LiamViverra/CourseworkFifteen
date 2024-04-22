using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    class DataBaseConnect
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LIAMPC\SQLEXPRESS;Initial Catalog=LiamDb7;Integrated Security=True");
        SoundManager sound = new SoundManager();

        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

        public void UpdateData(int TimeMinute, int TimeSecond, int WinsOverall, int LosingOverall, int StepNum, int MapSize)
        {
            try
            {
                string data = getDataAccount(Properties.Settings.Default.IdUser);
                string[] words = data.Split(' ');
                //char[] characters = words[6].ToCharArray();
                //string[] parts = Array.ConvertAll(characters, c => c.ToString());

                string WinsInCat = WinsInCategoriesUpdate(words[6], MapSize);
                if (WinsInCat == "") WinsInCat = "0-0-0-0-0-0-0-0";

                OpenConnection();
                string query;
                query = "UPDATE ListOfUsers SET UserId = @Value1, Login = @Value2, Password = @Value3, BestTime = @Value4, BestStep = @Value5, VictoryOverall = @Value6, WinsInCategories = @Value7, LosingOverall = @Value8 WHERE UserId = @UserID";
                SqlCommand command = new SqlCommand(query, getConnection());
                command.Parameters.AddWithValue("@Value1", words[0]);
                command.Parameters.AddWithValue("@Value2", words[1]);
                command.Parameters.AddWithValue("@Value3", words[2]);
                if (TimeSecond != 0 || TimeMinute != 0)
                {
                    TimeSpan time = TimeSpan.Parse($"00:{TimeMinute}:{TimeSecond}");
                    command.Parameters.AddWithValue("@Value4", time); //Новое значение
                }
                else
                    command.Parameters.AddWithValue("@Value4", words[3]); //Старое
                command.Parameters.AddWithValue("@Value5", StepNum);
                command.Parameters.AddWithValue("@Value6", WinsOverall);
                command.Parameters.AddWithValue("@Value7", WinsInCat);
                command.Parameters.AddWithValue("@Value8", LosingOverall);
                command.Parameters.AddWithValue("@UserID", words[0]);

                command.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка обновления записи в базе данных >> {exp.Message} >> {exp.StackTrace}");
            }
            CloseConnection();
        }

        public int CheckAccount(string login, string password)
        {
            try
            {
                OpenConnection();
                if (getConnection() != null)
                {
                    string query = "SELECT * FROM ListOfUsers WHERE Login = @log";

                    SqlCommand command = new SqlCommand(query, getConnection());

                    command.Parameters.AddWithValue("@log", login);

                    SqlDataReader reader = command.ExecuteReader();

                    List<string> data = new List<string>();

                    reader.Read();
                    for (int i = 0; i < 8; i++)
                    {
                        data.Add(reader[i].ToString());
                    }

                    reader.Close();

                    if (data[2].TrimEnd() == password)
                    {
                        return Convert.ToInt32(data[0]);
                    }
                    else
                    {
                        return -1;
                    }
                }
                CloseConnection();
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка получения записи из базы данных >> {exp.Message} >> {exp.StackTrace}");
            }

            return -1;
        }

        public int AddAccount(string login, string password)
        {
            try
            {
                OpenConnection();
                if (getConnection() != null)
                {
                    string query = "SELECT COUNT(*) FROM ListOfUsers WHERE Login = '" + login + "'";
                    SqlCommand command = new SqlCommand(query, getConnection());

                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 0)
                    {
                        reader.Close();
                        string query1 = "SELECT MAX(UserId) FROM ListOfUsers";
                        SqlCommand command1 = new SqlCommand(query1, getConnection());

                        SqlDataReader reader1 = command1.ExecuteReader();

                        reader1.Read();

                        int IdAccount = Convert.ToInt32(reader1[0]) + 1;

                        reader1.Close();

                        string query2 = "INSERT INTO ListOfUsers VALUES ('" + IdAccount + "', N'" + login + "', N'" + password + "', '" + "00:00:00" + "', '" + 0 + "', '" + 0 + "', '" + "00000000" + "', '" + 0 + "')";
                        SqlCommand command3 = new SqlCommand(query2, getConnection());

                        command3.ExecuteNonQuery();
                        return IdAccount;
                    }
                    else
                    {
                        MessageBox.Show("Данный логин уже зарегистрирован");
                        return -1;
                    }
                }
                CloseConnection();
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка добавления записи в базу данных >> {exp.Message} >> {exp.StackTrace}");
            }
            return -1;
        }

        #region Leaderboard
        public List<string[]> getLider(string query)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                OpenConnection();
                if (getConnection() != null)
                {
                    //string query = "SELECT Login, BestTime, BestStep, VictoryOverall FROM ListOfUsers ORDER BY BestTime LIMIT 100";

                    SqlCommand command = new SqlCommand(query, getConnection());

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new string[4]);

                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString();
                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                    }

                    reader.Close();
                    CloseConnection();
                    return data;
                }
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка чтения базы данных >> {exp.Message} >> {exp.StackTrace}");
            }
            return data;
        }
        #endregion

        public string getDataAccount(int UserID)
        {
            if (Properties.Settings.Default.IdUser == 0)
                return "";

            string Ret = "";
            try
            {
                OpenConnection();
                if (getConnection() != null)
                {
                    string query = "SELECT * FROM ListOfUsers WHERE UserId = @Id";

                    SqlCommand command = new SqlCommand(query, getConnection());

                    command.Parameters.AddWithValue("@Id", UserID);

                    SqlDataReader reader = command.ExecuteReader();

                    List<string> data = new List<string>();

                    reader.Read();
                    for (int i = 0; i < 8; i++)
                    {
                        data.Add(reader[i].ToString());
                    }

                    reader.Close();
                    CloseConnection();



                    foreach (string s in data)
                    {
                        //DataViewer.Rows.Add(s);
                        Ret += s.TrimEnd() + " ";
                    }
                }
            }
            catch (Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка чтения базы данных >> {exp.Message} >> {exp.StackTrace}");
            }

            return Ret;
        }



        private string WinsInCategoriesUpdate(string WinCat, int MapSize)
        {
            if(WinCat != "")
            {
                string[] WinCatOsn = WinCat.Split('-');
                WinCatOsn[MapSize - 2] = (Convert.ToInt32(WinCatOsn[MapSize - 2]) + 1).ToString();

                string Ret = string.Join("-", WinCatOsn);

                return Ret;
            }
            else
            {
                return "";
            }
        }
    }
}
