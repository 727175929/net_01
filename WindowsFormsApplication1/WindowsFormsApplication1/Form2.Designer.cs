﻿namespace WindowsFormsApplication1
{
    partial class Form2
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卡片操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.非接触操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 338);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(645, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作设置ToolStripMenuItem,
            this.卡片操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(669, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作设置ToolStripMenuItem
            // 
            this.操作设置ToolStripMenuItem.Name = "操作设置ToolStripMenuItem";
            this.操作设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.操作设置ToolStripMenuItem.Text = "操作设置";
            this.操作设置ToolStripMenuItem.Click += new System.EventHandler(this.操作设置ToolStripMenuItem_Click);
            // 
            // 卡片操作ToolStripMenuItem
            // 
            this.卡片操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.非接触操作ToolStripMenuItem});
            this.卡片操作ToolStripMenuItem.Name = "卡片操作ToolStripMenuItem";
            this.卡片操作ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.卡片操作ToolStripMenuItem.Text = "卡片操作";
            // 
            // 非接触操作ToolStripMenuItem
            // 
            this.非接触操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m1ToolStripMenuItem});
            this.非接触操作ToolStripMenuItem.Name = "非接触操作ToolStripMenuItem";
            this.非接触操作ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.非接触操作ToolStripMenuItem.Text = "非接触操作";
            // 
            // m1ToolStripMenuItem
            // 
            this.m1ToolStripMenuItem.Name = "m1ToolStripMenuItem";
            this.m1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m1ToolStripMenuItem.Text = "M1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "请求卡片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "卡片类型";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(168, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(129, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 446);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Form2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 卡片操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 非接触操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m1ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}