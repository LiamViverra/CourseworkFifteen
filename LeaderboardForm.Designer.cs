
namespace CourseworkFifteen
{
    partial class LeaderboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaderboardForm));
            this.DataViewer = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VictoryOverall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // DataViewer
            // 
            this.DataViewer.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(174)))), ((int)(((byte)(161)))));
            this.DataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login,
            this.BestTime,
            this.BestStep,
            this.VictoryOverall});
            this.DataViewer.Location = new System.Drawing.Point(12, 79);
            this.DataViewer.MultiSelect = false;
            this.DataViewer.Name = "DataViewer";
            this.DataViewer.ReadOnly = true;
            this.DataViewer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataViewer.Size = new System.Drawing.Size(320, 295);
            this.DataViewer.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.BackgroundImage = global::CourseworkFifteen.Properties.Resources.ImgBut1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(12, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 45);
            this.button1.TabIndex = 26;
            this.button1.Text = "  ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 19F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(65, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 45);
            this.label1.TabIndex = 25;
            this.label1.Text = "Таблица лидеров";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(174)))), ((int)(((byte)(161)))));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Количество побед",
            "Лучшее время",
            "Лучшее количество шагов"});
            this.comboBox1.Location = new System.Drawing.Point(87, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(245, 24);
            this.comboBox1.TabIndex = 27;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(174)))), ((int)(((byte)(161)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 24);
            this.label2.TabIndex = 28;
            this.label2.Text = "Режим:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            this.Login.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Login.HeaderText = "Имя";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BestTime
            // 
            this.BestTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BestTime.HeaderText = "Время";
            this.BestTime.Name = "BestTime";
            this.BestTime.ReadOnly = true;
            this.BestTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BestStep
            // 
            this.BestStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BestStep.HeaderText = "Шагов";
            this.BestStep.Name = "BestStep";
            this.BestStep.ReadOnly = true;
            this.BestStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VictoryOverall
            // 
            this.VictoryOverall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VictoryOverall.HeaderText = "Побед";
            this.VictoryOverall.Name = "VictoryOverall";
            this.VictoryOverall.ReadOnly = true;
            this.VictoryOverall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LeaderboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CourseworkFifteen.Properties.Resources.Img2;
            this.ClientSize = new System.Drawing.Size(344, 376);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataViewer);
            this.MaximumSize = new System.Drawing.Size(360, 415);
            this.MinimumSize = new System.Drawing.Size(360, 415);
            this.Name = "LeaderboardForm";
            this.Text = "Таблица лидеров";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LeaderboardForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataViewer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn VictoryOverall;
    }
}