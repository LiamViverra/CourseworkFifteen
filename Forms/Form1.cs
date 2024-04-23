using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    public partial class Form1 : Form
    {
        DataBaseConnect dataBase = new DataBaseConnect();
        SoundManager sound = new SoundManager();

        private int RoundTimeSecond; //Кол-во пройденных секунд
        private int RoundTimeMinute; //Кол-во пройденных секунд
        private int NumberSteps;
        private bool AnimRunning;
        private bool Backing;
        private bool OnSaveGame = false;
        private bool OnWin;

        public int MapSize;
        RoundedButton[,] buttons;

        public Form1(bool OnSave, int mapSize = 2)
        {
            InitializeComponent();
            OnSaveGame = OnSave;
            if (!OnSave)
                MapSize = mapSize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            MapCreate();
        }

        private void LoadData()
        {
            try
            {
                if(Properties.Settings.Default.IdUser > 0)
                {
                    string data = dataBase.getDataAccount(Properties.Settings.Default.IdUser);
                    string[] words = data.Split(' ');
                    label3.Text = words[3].Substring(3);
                    label4.Text = words[4];
                }
            }
            catch (Exception)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show("Получение данных аккаунта не удалось");
            }

        }

        public void MapCreate()
        {
            if (!OnSaveGame)
            {
                try
                {
                    buttons = new RoundedButton[MapSize, MapSize];

                    int FormSize = 500;
                    int padding = 10 - MapSize; //Отступ от краев поля и между клеток
                    int numSquares = MapSize * MapSize;
                    int totalPadding = (int)Math.Sqrt(numSquares) * 2 * padding; //Общий отступ для всех клеток
                    int availableSpace = FormSize - totalPadding; //Доступное пространство для размещения клеток
                    int SlotSize = (availableSpace / (int)Math.Sqrt(numSquares)); //Размер каждого квадрата
                                                                                  //MessageBox.Show(SlotSize.ToString());

                    int paddingForNumSquares = 0;
                    switch (MapSize)
                    {
                        case 2:
                            paddingForNumSquares = 4;
                            break;
                        case 3:
                            paddingForNumSquares = 7;
                            break;
                        case 4:
                            paddingForNumSquares = 7;
                            break;
                        case 5:
                            paddingForNumSquares = 7;
                            break;
                        case 6:
                            paddingForNumSquares = 7;
                            break;
                        case 7:
                            paddingForNumSquares = 6;
                            break;
                        case 8:
                            paddingForNumSquares = 3;
                            break;
                    }

                    List<int> Number = new List<int>();
                    for (int i = 0; i != (numSquares - 1); i++)
                    {
                        int s = i + 1;
                        Number.Add(s);
                    }
                    Random rng = new Random();


                    int x = 0;
                    int y = 0;

                    int j = 0; //Строка
                    int k = 0; //Столбец
                    for (int i = 0; i != numSquares; i++)
                    {
                        RoundedButton but = new RoundedButton();
                        but.Size = new Size(SlotSize, SlotSize);
                        but.Location = new Point(x + paddingForNumSquares, y + paddingForNumSquares);
                        but.TextAlign = ContentAlignment.MiddleCenter;
                        but.Font = SystemFonts.DefaultFont;
                        but.Font = new Font("Microsoft YaHei UI", 19, FontStyle.Regular);
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 1;
                        but.RoundingEnable = true;
                        but.Rounding = 50;

                        //Заполнение числами
                        if (Number.Count > 0)
                        {
                            int randomNumberIndex = rng.Next(0, Number.Count);
                            but.Text = Number[randomNumberIndex].ToString();
                            Number.RemoveAt(randomNumberIndex);
                        }
                        else
                        {
                            but.Text = "";
                        }

                        buttons[j, k] = but;
                        but.Click += new EventHandler(Button_Click);
                        panel1.Controls.Add(but);

                        x += SlotSize + padding;
                        k++;

                        if (x + SlotSize > FormSize)
                        {
                            x = 0;
                            y += SlotSize + padding;
                            j++;
                            k = 0;
                        }
                    }

                    ButtonSetColor(buttons);
                }
                catch (Exception exp)
                {
                    sound.PlayOneShotAudio(2);
                    MessageBox.Show($"Ошибка генерации карты >> {exp.Message} >> {exp.StackTrace}");
                }
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader("GameData.txt"))
                    {
                        string st = reader.ReadLine();
                        MapSize = Convert.ToInt32(st);

                        buttons = new RoundedButton[MapSize, MapSize];

                        int FormSize = 500;
                        int padding = 10 - MapSize; //Отступ от краев поля и между клеток
                        int numSquares = MapSize * MapSize;
                        int totalPadding = (int)Math.Sqrt(numSquares) * 2 * padding; //Общий отступ для всех клеток
                        int availableSpace = FormSize - totalPadding; //Доступное пространство для размещения клеток
                        int SlotSize = (availableSpace / (int)Math.Sqrt(numSquares)); //Размер каждого квадрата
                                                                                      //MessageBox.Show(SlotSize.ToString());

                        int paddingForNumSquares = 0;
                        switch (MapSize)
                        {
                            case 2:
                                paddingForNumSquares = 4;
                                break;
                            case 3:
                                paddingForNumSquares = 7;
                                break;
                            case 4:
                                paddingForNumSquares = 7;
                                break;
                            case 5:
                                paddingForNumSquares = 7;
                                break;
                            case 6:
                                paddingForNumSquares = 7;
                                break;
                            case 7:
                                paddingForNumSquares = 6;
                                break;
                            case 8:
                                paddingForNumSquares = 3;
                                break;
                        }

                        int x = 0;
                        int y = 0;

                        int j = 0; //Строка
                        int k = 0; //Столбец

                        string ButText, Param;
                        for (int i = 0; i != numSquares; i++)
                        {
                            RoundedButton but = new RoundedButton();
                            but.Size = new Size(SlotSize, SlotSize);
                            but.Location = new Point(x + paddingForNumSquares, y + paddingForNumSquares);
                            but.BackColor = Color.White;
                            but.TextAlign = ContentAlignment.MiddleCenter;
                            but.Font = SystemFonts.DefaultFont;
                            but.Font = new Font("Microsoft YaHei UI", 19, FontStyle.Regular);
                            but.FlatStyle = FlatStyle.Flat;
                            but.FlatAppearance.BorderSize = 1;
                            but.RoundingEnable = true;
                            but.Rounding = 50;

                            //Заполнение числами
                            ButText = reader.ReadLine();
                            but.Text = ButText;

                            buttons[j, k] = but;
                            but.Click += new EventHandler(Button_Click);
                            panel1.Controls.Add(but);

                            x += SlotSize + padding;
                            k++;

                            if (x + SlotSize > FormSize)
                            {
                                x = 0;
                                y += SlotSize + padding;
                                j++;
                                k = 0;
                            }
                        }

                        ButtonSetColor(buttons);

                        Param = reader.ReadLine();
                        MapSize = Convert.ToInt32(Param);
                        Param = reader.ReadLine();
                        NumberSteps = Convert.ToInt32(Param);
                        Param = reader.ReadLine();
                        RoundTimeMinute = Convert.ToInt32(Param);
                        Param = reader.ReadLine();
                        RoundTimeSecond = Convert.ToInt32(Param);
                        MessageBox.Show("MapSize: " + MapSize.ToString() + " | NumberSteps: " + NumberSteps.ToString() + " | " + RoundTimeMinute + ":" + RoundTimeSecond);

                        label2.Text = NumberSteps.ToString();

                        reader.Close();
                    }
                }
                catch (Exception exp)
                {
                    sound.PlayOneShotAudio(2);
                    MessageBox.Show($"Ошибка чтения: {exp.Message} >> {exp.StackTrace}", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            sound.PlayOneShotAudio(1);
            if (!AnimRunning)
            {
                Button button = (Button)sender;
                if (button.Text != "")
                {
                    for (int y = 0; y < MapSize; y++)
                    {
                        for (int x = 0; x < MapSize; x++)
                        {
                            if (buttons[x, y] == button)
                            {
                                CheckNeighbours(x, y, button);
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void CheckNeighbours(int xB, int yB, Button button)
        {
            if (xB - 1 >= 0)
            {
                if (buttons[xB - 1, yB].Text == "")
                {
                    SwapButtonAnim(buttons[xB - 1, yB], button);
                    RoundedButton but = buttons[xB - 1, yB];
                    buttons[xB - 1, yB] = buttons[xB, yB];
                    buttons[xB, yB] = but;

                    NumberSteps++;
                    label2.Text = NumberSteps.ToString();
                }
            }
            if (xB + 1 <= MapSize - 1)
            {
                if (buttons[xB + 1, yB].Text == "")
                {
                    SwapButtonAnim(buttons[xB + 1, yB], button);
                    RoundedButton but = buttons[xB + 1, yB];
                    buttons[xB + 1, yB] = buttons[xB, yB];
                    buttons[xB, yB] = but;

                    NumberSteps++;
                    label2.Text = NumberSteps.ToString();
                }
            }
            if (yB - 1 >= 0)
            {
                if (buttons[xB, yB - 1].Text == "")
                {
                    SwapButtonAnim(buttons[xB, yB - 1], button);
                    RoundedButton but = buttons[xB, yB - 1];
                    buttons[xB, yB - 1] = buttons[xB, yB];
                    buttons[xB, yB] = but;

                    NumberSteps++;
                    label2.Text = NumberSteps.ToString();
                }
            }
            if (yB + 1 <= MapSize - 1)
            {
                if (buttons[xB, yB + 1].Text == "")
                {
                    SwapButtonAnim(buttons[xB, yB + 1], button);
                    RoundedButton but = buttons[xB, yB + 1];
                    buttons[xB, yB + 1] = buttons[xB, yB];
                    buttons[xB, yB] = but;

                    NumberSteps++;
                    label2.Text = NumberSteps.ToString();
                }
            }
        }

        private void SwapButtonAnim(Button button1, Button button2) //Анимация для свайпа кнопок P.S Потому что я хочу плавного перемещения
        {
            AnimRunning = true;
            Point button1Point = button1.Location;
            Point button2Point = button2.Location;

            Timer timer = new Timer();
            timer.Interval = 15;
            timer.Tick += (sender, e) =>
            {
                //Меняем позицию кнопок для плавного перемещения
                button1.Location = new Point(button1.Location.X + (button2Point.X - button1Point.X) / timer.Interval,
                                             button1.Location.Y + (button2Point.Y - button1Point.Y) / timer.Interval);
                button2.Location = new Point(button2.Location.X + (button1Point.X - button2Point.X) / timer.Interval,
                                             button2.Location.Y + (button1Point.Y - button2Point.Y) / timer.Interval);

                //Делаем дистанцию между нужными точками 1 из кнопок и если она маленькая,
                //то ставим их на нужное место и выключаем анимацию
                double distance = Math.Sqrt(Math.Pow(button2Point.X - button1.Location.X, 2) + Math.Pow(button2Point.Y - button1.Location.Y, 2));
                if (distance <= 5)
                {
                    button1.Location = button2Point;
                    button2.Location = button1Point;
                    AnimRunning = false;
                    CheckWin();
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void CheckWin()
        {
            int numSquares = 1;
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (buttons[i, j].Text != numSquares.ToString())
                    {
                        return;
                    }
                    numSquares++;
                    if (numSquares == MapSize * MapSize)
                    {
                        if (buttons[i, j + 1].Text == "")
                        {
                            //MessageBox.Show("ПАБЕДАААААААААААААААААААААААААААААААААААААААААААААААААА!!!!!!!!!");
                            OnWin = true;
                            File.WriteAllBytes("GameData.txt", new byte[0]); //Уничтожаем содержимое файла с сохранением

                            WinForms win = new WinForms(MapSize, RoundTimeMinute, RoundTimeSecond, NumberSteps);
                            win.Show();
                            Backing = true;
                            Close();
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RoundTimeSecond < 60)
            {
                RoundTimeSecond++;
            }
            else if (RoundTimeMinute < 99)
            {
                RoundTimeMinute++;
                RoundTimeSecond = 0;
            }
/*            else if(RoundTimeMinute == 99)
            {
                //ВЫ ПРАИГРАЛИ ХАХАХПХХДАВХЗАЩЩХВЗЛЫПАЗЩЫВОЛАЗЩО
                MessageBox.Show("Вы проиграли");
            }*/
            if (RoundTimeSecond >= 10)
            {
                if (RoundTimeMinute >= 10)
                {
                    label1.Text = RoundTimeMinute + ":" + RoundTimeSecond;
                }
                else
                {
                    label1.Text = "0" + RoundTimeMinute + ":" + RoundTimeSecond;
                }
            }
            else
            {
                if (RoundTimeMinute >= 10)
                {
                    label1.Text = RoundTimeMinute + ":0" + RoundTimeSecond;
                }
                else
                {
                    label1.Text = "0" + RoundTimeMinute + ":0" + RoundTimeSecond;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Регенератор чисел
        {
            sound.PlayOneShotAudio(1);
            try
            {
                int numSquares = MapSize * MapSize;
                List<int> Number = new List<int>();
                for (int i = 0; i != (numSquares - 1); i++)
                {
                    int s = i + 1;
                    Number.Add(s);
                }
                Random rng = new Random();

                for (int i = 0; i < MapSize; i++)
                {
                    for (int j = 0; j < MapSize; j++)
                    {
                        if (Number.Count > 0)
                        {
                            int randomNumberIndex = rng.Next(0, Number.Count);
                            buttons[i, j].Text = Number[randomNumberIndex].ToString();
                            Number.RemoveAt(randomNumberIndex);
                        }
                        else
                        {
                            buttons[i, j].Text = "";
                        }
                    }
                }

                ButtonSetColor(buttons);

                RoundTimeMinute = 0;
                RoundTimeSecond = 0;
                label1.Text = "00:00";
                NumberSteps = 0;
                label2.Text = "0";
            } catch(Exception exp)
            {
                sound.PlayOneShotAudio(2);
                MessageBox.Show($"Ошибка регенерации карты: {exp.Message}");
            }
        }

        private void ButtonSetColor(RoundedButton[,] buttons)
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int c = 0; c < MapSize; c++)
                {
                    if (buttons[i, c].Text != "")
                    {
                        int Num = Convert.ToInt32(buttons[i, c].Text);

                        #region Цвет кнопки
                        if (Num <= MapSize) //Устанавливаем цвет в соответствии со строкой кнопки
                        {
                            buttons[i, c].BackColor = Color.FromArgb(239, 229, 219);
                            buttons[i, c].ForeColor = Color.FromArgb(119, 111, 100);
                        }
                        else if (Num <= MapSize * 2)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(238, 223, 202);
                            buttons[i, c].ForeColor = Color.FromArgb(119, 111, 100);
                        }
                        else if (Num <= MapSize * 3)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(237, 177, 123);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 4)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(242, 151, 104);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 5)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(243, 124, 100);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 6)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(251, 91, 79);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 7)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(234, 208, 115);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 8)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(236, 189, 75);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        else if (Num <= MapSize * 9)
                        {
                            buttons[i, c].BackColor = Color.FromArgb(237, 162, 33);
                            buttons[i, c].ForeColor = Color.FromArgb(249, 248, 243);
                        }
                        #endregion
                    }
                    else
                    {
                        switch (MapSize)
                        {
                            case 2:
                                buttons[i, c].BackColor = Color.FromArgb(238, 223, 202);
                                break;
                            case 3:
                                buttons[i, c].BackColor = Color.FromArgb(237, 177, 123);
                                break;
                            case 4:
                                buttons[i, c].BackColor = Color.FromArgb(242, 151, 104);
                                break;
                            case 5:
                                buttons[i, c].BackColor = Color.FromArgb(243, 124, 100);
                                break;
                            case 6:
                                buttons[i, c].BackColor = Color.FromArgb(251, 91, 79);
                                break;
                            case 7:
                                buttons[i, c].BackColor = Color.FromArgb(234, 208, 115);
                                break;
                            case 8:
                                buttons[i, c].BackColor = Color.FromArgb(236, 189, 75);
                                break;
                            case 9:
                                buttons[i, c].BackColor = Color.FromArgb(237, 162, 33);
                                break;
                        }
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Backing)
                Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!OnWin)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter("GameData.txt"))
                    {
                        writer.WriteLine(MapSize.ToString());

                        for (int i = 0; i < MapSize; i++)
                        {
                            for (int l = 0; l < MapSize; l++)
                            {
                                string But = buttons[i, l].Text;

                                writer.WriteLine(But);
                            }
                        }
                        string Param = MapSize.ToString();
                        writer.WriteLine(Param);
                        Param = NumberSteps.ToString();
                        writer.WriteLine(Param);
                        Param = RoundTimeMinute.ToString();
                        writer.WriteLine(Param);
                        Param = RoundTimeSecond.ToString();
                        writer.WriteLine(Param);

                        writer.Close();
                    }
                }
                catch (Exception exp)
                {
                    sound.PlayOneShotAudio(2);
                    MessageBox.Show($"Ошибка сохранения: {exp.Message}");
                }
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
    }
}
