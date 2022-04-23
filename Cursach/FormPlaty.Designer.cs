namespace Cursach
{
    partial class FormPlaty
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
            this.groupBoxPlaty = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewPlaty = new System.Windows.Forms.DataGridView();
            this.contextMenuStripPlaty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewPlaty_to_Platy = new System.Windows.Forms.DataGridView();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxPlaty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlaty)).BeginInit();
            this.contextMenuStripPlaty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlaty_to_Platy)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPlaty
            // 
            this.groupBoxPlaty.Controls.Add(this.splitContainer1);
            this.groupBoxPlaty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPlaty.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPlaty.Name = "groupBoxPlaty";
            this.groupBoxPlaty.Size = new System.Drawing.Size(984, 661);
            this.groupBoxPlaty.TabIndex = 0;
            this.groupBoxPlaty.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewPlaty);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewPlaty_to_Platy);
            this.splitContainer1.Size = new System.Drawing.Size(978, 642);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridViewPlaty
            // 
            this.dataGridViewPlaty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlaty.ContextMenuStrip = this.contextMenuStripPlaty;
            this.dataGridViewPlaty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPlaty.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPlaty.Name = "dataGridViewPlaty";
            this.dataGridViewPlaty.Size = new System.Drawing.Size(978, 438);
            this.dataGridViewPlaty.TabIndex = 0;
            this.dataGridViewPlaty.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlaty_CellClick);
            this.dataGridViewPlaty.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlaty_CellContentClick);
            this.dataGridViewPlaty.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlaty_CellValueChanged);
            this.dataGridViewPlaty.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewPlaty_UserAddedRow);
            // 
            // contextMenuStripPlaty
            // 
            this.contextMenuStripPlaty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.contextMenuStripPlaty.Name = "contextMenuStripPlaty";
            this.contextMenuStripPlaty.Size = new System.Drawing.Size(181, 70);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.обновитьToolStripMenuItem.Text = "обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click_1);
            // 
            // dataGridViewPlaty_to_Platy
            // 
            this.dataGridViewPlaty_to_Platy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlaty_to_Platy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPlaty_to_Platy.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPlaty_to_Platy.Name = "dataGridViewPlaty_to_Platy";
            this.dataGridViewPlaty_to_Platy.Size = new System.Drawing.Size(978, 200);
            this.dataGridViewPlaty_to_Platy.TabIndex = 0;
            this.dataGridViewPlaty_to_Platy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlaty_to_Platy_CellContentClick);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сохранитьToolStripMenuItem.Text = "сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // FormPlaty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.groupBoxPlaty);
            this.Name = "FormPlaty";
            this.Text = "FormPlaty";
            this.Load += new System.EventHandler(this.FormPlaty_Load);
            this.groupBoxPlaty.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlaty)).EndInit();
            this.contextMenuStripPlaty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlaty_to_Platy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPlaty;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewPlaty;
        private System.Windows.Forms.DataGridView dataGridViewPlaty_to_Platy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPlaty;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
    }
}