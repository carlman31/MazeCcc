namespace WinFormsMaze
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(29, 109);
            pictureBox1.Margin = new Padding(7, 8, 7, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(2773, 2875);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(29, 30);
            button1.Margin = new Padding(7, 8, 7, 8);
            button1.Name = "button1";
            button1.Size = new Size(182, 63);
            button1.TabIndex = 1;
            button1.Text = "Iniciar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(226, 30);
            button2.Margin = new Padding(7, 8, 7, 8);
            button2.Name = "button2";
            button2.Size = new Size(245, 63);
            button2.TabIndex = 2;
            button2.Text = "Probar canvas";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(486, 33);
            button3.Margin = new Padding(7, 8, 7, 8);
            button3.Name = "button3";
            button3.Size = new Size(182, 63);
            button3.TabIndex = 3;
            button3.Text = "Reset Game";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2832, 3018);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Margin = new Padding(7, 8, 7, 8);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}
