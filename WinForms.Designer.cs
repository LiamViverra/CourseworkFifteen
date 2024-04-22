
namespace CourseworkFifteen
{
    partial class WinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForms));
            this.label1 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelStep = new System.Windows.Forms.Label();
            this.labelWinsOverall = new System.Windows.Forms.Label();
            this.labelWins = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // labelTime
            // 
            this.labelTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(100)))));
            resources.ApplyResources(this.labelTime, "labelTime");
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Name = "labelTime";
            // 
            // labelStep
            // 
            this.labelStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(100)))));
            resources.ApplyResources(this.labelStep, "labelStep");
            this.labelStep.ForeColor = System.Drawing.Color.White;
            this.labelStep.Name = "labelStep";
            // 
            // labelWinsOverall
            // 
            this.labelWinsOverall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(100)))));
            resources.ApplyResources(this.labelWinsOverall, "labelWinsOverall");
            this.labelWinsOverall.ForeColor = System.Drawing.Color.White;
            this.labelWinsOverall.Name = "labelWinsOverall";
            // 
            // labelWins
            // 
            this.labelWins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(100)))));
            resources.ApplyResources(this.labelWins, "labelWins");
            this.labelWins.ForeColor = System.Drawing.Color.White;
            this.labelWins.Name = "labelWins";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WinForms
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CourseworkFifteen.Properties.Resources.Img2;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelWinsOverall);
            this.Controls.Add(this.labelWins);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "WinForms";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinForms_FormClosed);
            this.Load += new System.EventHandler(this.WinForms_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Label labelWinsOverall;
        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}