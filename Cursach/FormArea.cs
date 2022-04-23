using System;
using System.Windows.Forms;
using System.Drawing;

namespace Cursach
{
    public partial class FormArea : Form
    {
        //объявление переменных без инициализации
        private bool change = false; //для определения менялись ли данные

        public FormArea()
        {
            InitializeComponent();
        }
        private void LoadDataArea()//загрузка данных из БД
        {
            try
            {

                dataGridViewArea.DataSource = dbConnect.dataSet.Tables["Area"];

                dataGridViewArea.Columns["id_Area"].Visible = false;                        //делаем столбец нивидимым
                dataGridViewArea.Columns["name_Area"].Width = 335;
                dataGridViewArea.Columns["name_Area"].HeaderText = "Область применения";
                dataGridViewArea.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком

                change = false;
                buttonUpArea.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void FormArea_Load(object sender, EventArgs e)
        {

            LoadDataArea();
        }

        private void buttonUpArea_Click(object sender, EventArgs e)
        {
            //сохраняем данные в БД
            Save_Change();

        }

        private void FormArea_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    Save_Change();
                }
            }

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //вызываем окно для переспрашивания удалить ли строку
            if (MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                // удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridViewArea.SelectedRows)
                {
                    dataGridViewArea.Rows.Remove(row);
                }

                change = true;
                buttonUpArea.BackColor = Color.Red;
            }

        }
        private void dataGridViewArea_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            change = true;
            buttonUpArea.BackColor = Color.Red;
        }

        private void dataGridViewArea_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //e.Cancel = true;    //запрещаем удалять через Delete


            DialogResult dr = MessageBox.Show("Удалить запись?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                    e.Cancel = true;    //выход без удаления
            }

            if (!(dbConnect.saveTable("Area")))
            {
                e.Cancel = true;        //выход без удаления при ошибке записи в БД
            }
            buttonUpArea.BackColor = Color.Transparent;
            change = false;

        }

        private void dataGridViewArea_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            change = true;
            buttonUpArea.BackColor = Color.Red;
        }

        private void Save_Change()
        {
            
            if (dbConnect.saveTable("Area"))
            {
                    change = false;
                    buttonUpArea.BackColor = Color.Transparent;
            }
        }
    }
}
