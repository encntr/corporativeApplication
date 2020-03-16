namespace lab1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonReport = new System.Windows.Forms.RadioButton();
            this.radioButtonCalculation = new System.Windows.Forms.RadioButton();
            this.radioButtonProducts = new System.Windows.Forms.RadioButton();
            this.radioButtonEdIzm = new System.Windows.Forms.RadioButton();
            this.radioButtonDishs = new System.Windows.Forms.RadioButton();
            this.radioButtonDishType = new System.Windows.Forms.RadioButton();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(251, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(751, 565);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButtonReport);
            this.panel1.Controls.Add(this.radioButtonCalculation);
            this.panel1.Controls.Add(this.radioButtonProducts);
            this.panel1.Controls.Add(this.radioButtonEdIzm);
            this.panel1.Controls.Add(this.radioButtonDishs);
            this.panel1.Controls.Add(this.radioButtonDishType);
            this.panel1.Location = new System.Drawing.Point(12, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 233);
            this.panel1.TabIndex = 3;
            // 
            // radioButtonReport
            // 
            this.radioButtonReport.AutoSize = true;
            this.radioButtonReport.Location = new System.Drawing.Point(28, 195);
            this.radioButtonReport.Name = "radioButtonReport";
            this.radioButtonReport.Size = new System.Drawing.Size(67, 21);
            this.radioButtonReport.TabIndex = 5;
            this.radioButtonReport.Text = "Отчет";
            this.radioButtonReport.UseVisualStyleBackColor = true;
            this.radioButtonReport.CheckedChanged += new System.EventHandler(this.radioButtonReport_CheckedChanged);
            // 
            // radioButtonCalculation
            // 
            this.radioButtonCalculation.AutoSize = true;
            this.radioButtonCalculation.Location = new System.Drawing.Point(28, 159);
            this.radioButtonCalculation.Name = "radioButtonCalculation";
            this.radioButtonCalculation.Size = new System.Drawing.Size(112, 21);
            this.radioButtonCalculation.TabIndex = 4;
            this.radioButtonCalculation.Text = "Калькуляция";
            this.radioButtonCalculation.UseVisualStyleBackColor = true;
            this.radioButtonCalculation.CheckedChanged += new System.EventHandler(this.radioButtonCalculation_CheckedChanged);
            // 
            // radioButtonProducts
            // 
            this.radioButtonProducts.AutoSize = true;
            this.radioButtonProducts.Location = new System.Drawing.Point(28, 87);
            this.radioButtonProducts.Name = "radioButtonProducts";
            this.radioButtonProducts.Size = new System.Drawing.Size(91, 21);
            this.radioButtonProducts.TabIndex = 3;
            this.radioButtonProducts.Text = "Продукты";
            this.radioButtonProducts.UseVisualStyleBackColor = true;
            this.radioButtonProducts.CheckedChanged += new System.EventHandler(this.radioButtonProducts_CheckedChanged);
            // 
            // radioButtonEdIzm
            // 
            this.radioButtonEdIzm.AutoSize = true;
            this.radioButtonEdIzm.Location = new System.Drawing.Point(28, 51);
            this.radioButtonEdIzm.Name = "radioButtonEdIzm";
            this.radioButtonEdIzm.Size = new System.Drawing.Size(161, 21);
            this.radioButtonEdIzm.TabIndex = 2;
            this.radioButtonEdIzm.Text = "Единицы измерения";
            this.radioButtonEdIzm.UseVisualStyleBackColor = true;
            this.radioButtonEdIzm.CheckedChanged += new System.EventHandler(this.radioButtonEdIzm_CheckedChanged);
            // 
            // radioButtonDishs
            // 
            this.radioButtonDishs.AutoSize = true;
            this.radioButtonDishs.Location = new System.Drawing.Point(28, 123);
            this.radioButtonDishs.Name = "radioButtonDishs";
            this.radioButtonDishs.Size = new System.Drawing.Size(69, 21);
            this.radioButtonDishs.TabIndex = 1;
            this.radioButtonDishs.Text = "Блюда";
            this.radioButtonDishs.UseVisualStyleBackColor = true;
            this.radioButtonDishs.CheckedChanged += new System.EventHandler(this.radioButtonDishs_CheckedChanged);
            // 
            // radioButtonDishType
            // 
            this.radioButtonDishType.AutoSize = true;
            this.radioButtonDishType.Checked = true;
            this.radioButtonDishType.Location = new System.Drawing.Point(28, 15);
            this.radioButtonDishType.Name = "radioButtonDishType";
            this.radioButtonDishType.Size = new System.Drawing.Size(99, 21);
            this.radioButtonDishType.TabIndex = 0;
            this.radioButtonDishType.TabStop = true;
            this.radioButtonDishType.Text = "Типы блюд";
            this.radioButtonDishType.UseVisualStyleBackColor = true;
            this.radioButtonDishType.CheckedChanged += new System.EventHandler(this.radioButtonDishType_CheckedChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(18, 26);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(175, 38);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(18, 82);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(175, 38);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "Удалить";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(18, 138);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(175, 38);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Изменить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Enabled = false;
            this.buttonReport.Location = new System.Drawing.Point(18, 193);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(175, 38);
            this.buttonReport.TabIndex = 7;
            this.buttonReport.Text = "Отчет";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(18, 247);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(175, 38);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.buttonAdd);
            this.panel2.Controls.Add(this.buttonExit);
            this.panel2.Controls.Add(this.buttonRemove);
            this.panel2.Controls.Add(this.buttonReport);
            this.panel2.Controls.Add(this.buttonUpdate);
            this.panel2.Location = new System.Drawing.Point(12, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 309);
            this.panel2.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainForm";
            this.Text = "Кулинарные блюда";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonReport;
        private System.Windows.Forms.RadioButton radioButtonCalculation;
        private System.Windows.Forms.RadioButton radioButtonProducts;
        private System.Windows.Forms.RadioButton radioButtonEdIzm;
        private System.Windows.Forms.RadioButton radioButtonDishs;
        private System.Windows.Forms.RadioButton radioButtonDishType;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Panel panel2;
    }
}

