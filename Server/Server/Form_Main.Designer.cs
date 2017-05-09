namespace Server
{
    partial class Form_Main
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
            this.serverinfo_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.user_button = new System.Windows.Forms.Button();
            this.log_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverinfo_button
            // 
            this.serverinfo_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.serverinfo_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serverinfo_button.Location = new System.Drawing.Point(12, 28);
            this.serverinfo_button.Name = "serverinfo_button";
            this.serverinfo_button.Size = new System.Drawing.Size(34, 75);
            this.serverinfo_button.TabIndex = 0;
            this.serverinfo_button.Text = "服务器";
            this.serverinfo_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.serverinfo_button.UseVisualStyleBackColor = true;
            this.serverinfo_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(70, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 305);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // user_button
            // 
            this.user_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.user_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_button.Location = new System.Drawing.Point(12, 135);
            this.user_button.Name = "user_button";
            this.user_button.Size = new System.Drawing.Size(34, 87);
            this.user_button.TabIndex = 3;
            this.user_button.Text = "用户管理";
            this.user_button.UseVisualStyleBackColor = true;
            this.user_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // log_button
            // 
            this.log_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.log_button.Location = new System.Drawing.Point(12, 256);
            this.log_button.Name = "log_button";
            this.log_button.Size = new System.Drawing.Size(34, 49);
            this.log_button.TabIndex = 4;
            this.log_button.Text = "日志";
            this.log_button.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 351);
            this.Controls.Add(this.log_button);
            this.Controls.Add(this.user_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.serverinfo_button);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button serverinfo_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button user_button;
        private System.Windows.Forms.Button log_button;


    }
}

