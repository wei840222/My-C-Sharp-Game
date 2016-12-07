namespace My_C_Sharp_Game
{
    partial class Game
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
            this.label_title = new System.Windows.Forms.Label();
            this.label_start = new System.Windows.Forms.Label();
            this.label_exit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("新細明體", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_title.Location = new System.Drawing.Point(276, 117);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(232, 96);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "打泡";
            // 
            // label_start
            // 
            this.label_start.AutoSize = true;
            this.label_start.BackColor = System.Drawing.Color.Transparent;
            this.label_start.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_start.Location = new System.Drawing.Point(352, 274);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(79, 32);
            this.label_start.TabIndex = 1;
            this.label_start.Text = "開始";
            this.label_start.Click += new System.EventHandler(this.label_start_Click);
            // 
            // label_exit
            // 
            this.label_exit.AutoSize = true;
            this.label_exit.BackColor = System.Drawing.Color.Transparent;
            this.label_exit.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_exit.Location = new System.Drawing.Point(352, 337);
            this.label_exit.Name = "label_exit";
            this.label_exit.Size = new System.Drawing.Size(79, 32);
            this.label_exit.TabIndex = 2;
            this.label_exit.Text = "離開";
            this.label_exit.Click += new System.EventHandler(this.label_exit_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label_exit);
            this.Controls.Add(this.label_start);
            this.Controls.Add(this.label_title);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "My C Sharp Game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameDraw);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.onKeyUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onMouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_start;
        private System.Windows.Forms.Label label_exit;
    }
}

