namespace labdagyar
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.ball_btn = new System.Windows.Forms.Button();
            this.car_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.createTimer = new System.Windows.Forms.Timer(this.components);
            this.conveyorTimer = new System.Windows.Forms.Timer(this.components);
            this.color_btn = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.color_btn);
            this.mainPanel.Controls.Add(this.ball_btn);
            this.mainPanel.Controls.Add(this.car_btn);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Location = new System.Drawing.Point(-3, -4);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1070, 558);
            this.mainPanel.TabIndex = 0;
            // 
            // ball_btn
            // 
            this.ball_btn.Location = new System.Drawing.Point(32, 208);
            this.ball_btn.Name = "ball_btn";
            this.ball_btn.Size = new System.Drawing.Size(75, 23);
            this.ball_btn.TabIndex = 2;
            this.ball_btn.Text = "BALL";
            this.ball_btn.UseVisualStyleBackColor = true;
            this.ball_btn.Click += new System.EventHandler(this.ball_btn_Click);
            // 
            // car_btn
            // 
            this.car_btn.Location = new System.Drawing.Point(32, 165);
            this.car_btn.Name = "car_btn";
            this.car_btn.Size = new System.Drawing.Size(75, 23);
            this.car_btn.TabIndex = 1;
            this.car_btn.Text = "CAR";
            this.car_btn.UseVisualStyleBackColor = true;
            this.car_btn.Click += new System.EventHandler(this.car_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Coming next:";
            // 
            // createTimer
            // 
            this.createTimer.Enabled = true;
            this.createTimer.Interval = 3000;
            this.createTimer.Tick += new System.EventHandler(this.createTimer_Tick_1);
            // 
            // conveyorTimer
            // 
            this.conveyorTimer.Enabled = true;
            this.conveyorTimer.Interval = 10;
            this.conveyorTimer.Tick += new System.EventHandler(this.conveyorTimer_Tick_1);
            // 
            // color_btn
            // 
            this.color_btn.BackColor = System.Drawing.Color.Blue;
            this.color_btn.Location = new System.Drawing.Point(32, 250);
            this.color_btn.Name = "color_btn";
            this.color_btn.Size = new System.Drawing.Size(75, 23);
            this.color_btn.TabIndex = 3;
            this.color_btn.UseVisualStyleBackColor = false;
            this.color_btn.Click += new System.EventHandler(this.color_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer createTimer;
        private System.Windows.Forms.Timer conveyorTimer;
        private System.Windows.Forms.Button ball_btn;
        private System.Windows.Forms.Button car_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button color_btn;
    }
}

