namespace Cursach
{
    partial class FormFind
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFind));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.radioButtonInv = new System.Windows.Forms.RadioButton();
            this.radioButtonSerial = new System.Windows.Forms.RadioButton();
            this.radioButtonNum = new System.Windows.Forms.RadioButton();
            this.dataGridViewFind = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFind)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewFind, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 561);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonFind);
            this.groupBox1.Controls.Add(this.radioButtonOther);
            this.groupBox1.Controls.Add(this.textBoxFind);
            this.groupBox1.Controls.Add(this.radioButtonInv);
            this.groupBox1.Controls.Add(this.radioButtonSerial);
            this.groupBox1.Controls.Add(this.radioButtonNum);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1178, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Искать по ...";
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(927, 28);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(227, 23);
            this.buttonFind.TabIndex = 4;
            this.buttonFind.Text = "Поиск";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonOther.Location = new System.Drawing.Point(185, 42);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(98, 19);
            this.radioButtonOther.TabIndex = 3;
            this.radioButtonOther.TabStop = true;
            this.radioButtonOther.Text = "примечанию";
            this.radioButtonOther.UseVisualStyleBackColor = true;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFind.Location = new System.Drawing.Point(479, 28);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(338, 21);
            this.textBoxFind.TabIndex = 1;
            // 
            // radioButtonInv
            // 
            this.radioButtonInv.AutoSize = true;
            this.radioButtonInv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonInv.Location = new System.Drawing.Point(185, 19);
            this.radioButtonInv.Name = "radioButtonInv";
            this.radioButtonInv.Size = new System.Drawing.Size(154, 19);
            this.radioButtonInv.TabIndex = 2;
            this.radioButtonInv.TabStop = true;
            this.radioButtonInv.Text = "инвентарному номеру";
            this.radioButtonInv.UseVisualStyleBackColor = true;
            // 
            // radioButtonSerial
            // 
            this.radioButtonSerial.AutoSize = true;
            this.radioButtonSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonSerial.Location = new System.Drawing.Point(10, 42);
            this.radioButtonSerial.Name = "radioButtonSerial";
            this.radioButtonSerial.Size = new System.Drawing.Size(132, 19);
            this.radioButtonSerial.TabIndex = 1;
            this.radioButtonSerial.TabStop = true;
            this.radioButtonSerial.Text = "серийному номеру";
            this.radioButtonSerial.UseVisualStyleBackColor = true;
            // 
            // radioButtonNum
            // 
            this.radioButtonNum.AutoSize = true;
            this.radioButtonNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonNum.Location = new System.Drawing.Point(10, 19);
            this.radioButtonNum.Name = "radioButtonNum";
            this.radioButtonNum.Size = new System.Drawing.Size(164, 19);
            this.radioButtonNum.TabIndex = 0;
            this.radioButtonNum.TabStop = true;
            this.radioButtonNum.Text = "наименованию / номеру";
            this.radioButtonNum.UseVisualStyleBackColor = true;
            // 
            // dataGridViewFind
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFind.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFind.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFind.Location = new System.Drawing.Point(3, 83);
            this.dataGridViewFind.Name = "dataGridViewFind";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFind.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFind.Size = new System.Drawing.Size(1178, 475);
            this.dataGridViewFind.TabIndex = 1;
            this.dataGridViewFind.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFind_CellDoubleClick);
            // 
            // FormFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFind";
            this.Text = "Поиск";
            this.Load += new System.EventHandler(this.FormFind_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.RadioButton radioButtonInv;
        private System.Windows.Forms.RadioButton radioButtonSerial;
        private System.Windows.Forms.RadioButton radioButtonNum;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.DataGridView dataGridViewFind;
    }
}