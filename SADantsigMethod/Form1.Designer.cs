namespace SADantsigMethod
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.varAddBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.resAddBtn = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.GroupBox();
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.controlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataBox
            // 
            this.dataBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataBox.Location = new System.Drawing.Point(12, 12);
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size(393, 298);
            this.dataBox.TabIndex = 0;
            this.dataBox.TabStop = false;
            this.dataBox.Text = "Функция и ограничения";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Добавить переменную:";
            // 
            // varAddBtn
            // 
            this.varAddBtn.Location = new System.Drawing.Point(140, 41);
            this.varAddBtn.Name = "varAddBtn";
            this.varAddBtn.Size = new System.Drawing.Size(33, 21);
            this.varAddBtn.TabIndex = 11;
            this.varAddBtn.Text = "+";
            this.varAddBtn.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Добавить ограничение:";
            // 
            // resAddBtn
            // 
            this.resAddBtn.Location = new System.Drawing.Point(140, 65);
            this.resAddBtn.Name = "resAddBtn";
            this.resAddBtn.Size = new System.Drawing.Size(33, 21);
            this.resAddBtn.TabIndex = 3;
            this.resAddBtn.Text = "+";
            this.resAddBtn.UseVisualStyleBackColor = true;
            // 
            // statusBox
            // 
            this.statusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBox.Location = new System.Drawing.Point(411, 12);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(211, 106);
            this.statusBox.TabIndex = 1;
            this.statusBox.TabStop = false;
            this.statusBox.Text = "Статус";
            // 
            // controlBox
            // 
            this.controlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBox.Controls.Add(this.label8);
            this.controlBox.Controls.Add(this.varAddBtn);
            this.controlBox.Controls.Add(this.label2);
            this.controlBox.Controls.Add(this.label1);
            this.controlBox.Controls.Add(this.label7);
            this.controlBox.Controls.Add(this.resAddBtn);
            this.controlBox.Controls.Add(this.button2);
            this.controlBox.Controls.Add(this.button1);
            this.controlBox.Location = new System.Drawing.Point(411, 124);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(211, 187);
            this.controlBox.TabIndex = 2;
            this.controlBox.TabStop = false;
            this.controlBox.Text = "Управление";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Отобразить\r\n решение\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Решить задачу \r\nпоиска максимума";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отобразить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Решить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 322);
            this.Controls.Add(this.controlBox);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.dataBox);
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.Name = "Form1";
            this.Text = "Form1";
            this.controlBox.ResumeLayout(false);
            this.controlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox dataBox;
        private System.Windows.Forms.GroupBox statusBox;
        private System.Windows.Forms.GroupBox controlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button resAddBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button varAddBtn;
    }
}

