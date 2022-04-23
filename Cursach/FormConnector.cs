using System;

using System.Windows.Forms;



namespace Cursach
{
    public partial class FormConnector : Form
    {
        //объявление переменных без инициализации

        private bool change = false; 

        public FormConnector()
        {
            InitializeComponent();
        }

        private void FormConnector_Load(object sender, EventArgs e)
        {

            LoadDataCon();

        }

        private void LoadDataCon()//загрузка данных из БД
        {
            try
            {


                dataGridViewCon.DataSource = dbConnect.dataSet.Tables["FormConnector"];

                dataGridViewCon.Columns["id_FormConnector"].Visible = false;//делаем столбец нивидимым
                dataGridViewCon.Columns["id_Area"].Visible = false;//делаем столбец нивидимым
                dataGridViewCon.Columns["name_FormConnector"].Width = 230;
                dataGridViewCon.Columns["Other_Con"].Width = 230;
                dataGridViewCon.Columns["bool_patch"].Width = 40;

                dataGridViewCon.Columns["name_FormConnector"].HeaderText = "Наименование разъема";
                dataGridViewCon.Columns["bool_patch"].HeaderText = "Шнур";
                dataGridViewCon.Columns["Other_Con"].HeaderText = "Примечание";
                dataGridViewCon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком


                var C_Area = new DataGridViewComboBoxColumn(); //добавить новую колонку

                C_Area.Name = "Область";
                C_Area.DataSource = dbConnect.dataSet.Tables["Area"]; //откуда данные
                C_Area.DisplayMember = "name_Area";         //что будет отображаться
                C_Area.ValueMember = "id_Area";             //по чем связывается из 2 таблицы
                C_Area.DataPropertyName = "id_Area";        //по чем связывается из 1 таблицы

                dataGridViewCon.Columns.Insert(dataGridViewCon.Columns.Count, C_Area);  //вставляем столбец в dataGridView- будет предпоследний
                dataGridViewCon.Columns["Область"].Width = 110;

                change = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
        //событие при изменении строки в dataGridView
        private void dataGridViewCon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ChangeConnector();
        }



        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {
                // удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridViewCon.SelectedRows)
                {
                    dataGridViewCon.Rows.Remove(row);
                }

                ChangeConnector();

            }
        }

        private void FormConnector_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    SaveConnector();
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConnector();
        }

        private void dataGridViewCon_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            ChangeConnector();
        }

        private void ChangeConnector()
        {
            change = true;
        }

        private void SaveConnector()
        {
            if (dbConnect.saveTable("FormConnector"))
                change = false;
        }

        private void dataGridViewCon_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ChangeConnector();
        }
    }
}
