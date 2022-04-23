using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cursach
{


    public partial class FormInOut : Form
    {

        bool change;
         public FormInOut()
        {
            InitializeComponent();
        }

        private void FormInOut_Load(object sender, EventArgs e)
        {
            LoadDataInOut();
        }

        private void LoadDataInOut()
        {
            try
            {

                dataGridViewInOut.DataSource = dbConnect.dataSet.Tables["InOut"]; //че будем отображать
                //столбец А
                var ConA = new DataGridViewComboBoxColumn(); //добавить новую колонку
                ConA.Name = "Разъем А";
                ConA.DataSource = dbConnect.dataSet.Tables["FormConnector"]; //откуда данные
                ConA.DisplayMember = "name_FormConnector";         //что будет отображаться
                ConA.ValueMember = "id_FormConnector";             //по чем связывается из 2 таблицы
                ConA.DataPropertyName = "id_FormConnector_A";      //по чем связывается из 1 таблицы

                //столбец B
                var ConB = new DataGridViewComboBoxColumn(); //добавить новую колонку
                ConB.Name = "Разъем B";
                ConB.DataSource = dbConnect.dataSet.Tables["FormConnector"]; //откуда данные
                ConB.DisplayMember = "name_FormConnector";         //что будет отображаться
                ConB.ValueMember = "id_FormConnector";             //по чем связывается из 2 таблицы
                ConB.DataPropertyName = "id_FormConnector_B";      //по чем связывается из 1 таблицы

                dataGridViewInOut.Columns.Insert(0, ConA);   //добавляем столбец А в отображение dataGridView
                dataGridViewInOut.Columns.Insert(1, ConB);   //добавляем столбец А в отображение dataGridView

                dataGridViewInOut.Columns["id_FormConnector_A"].Visible = false;//делаем столбец нивидимым
                dataGridViewInOut.Columns["id_FormConnector_B"].Visible = false;//делаем столбец нивидимым
                dataGridViewInOut.Columns["Разъем А"].Width = 217;
                dataGridViewInOut.Columns["Разъем B"].Width = 217;
                dataGridViewInOut.Columns["Разъем А"].ReadOnly = true; //запрет на изменение
                dataGridViewInOut.Columns["Разъем B"].ReadOnly = true; //запрет на изменение

                dataGridViewInOut.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком

                change = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //событие при добавлении новой строки в dataGridView
        private void dataGridViewInOut_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //при добавлении строки разблокируем разъемы и области

            int lastRow = dataGridViewInOut.Rows.Count - 2;     //индекс добавляемой строки
            ////разблокируем ячейки на изменение
            dataGridViewInOut[0, lastRow].ReadOnly = false;     //разрешение изменения
            dataGridViewInOut[1, lastRow].ReadOnly = false;     //разрешение изменения

            change = true;
            buttonSave.BackColor = Color.Red;
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save_Change();
        }

        private void dataGridViewInOut_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить запись?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            change = false;
            buttonSave.BackColor = Color.Red;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вызываем окно для переспрашивания удалить ли строку
            if (MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                // удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridViewInOut.SelectedRows)
                {
                    dataGridViewInOut.Rows.Remove(row);
                }
                Save_Change();

            }
        }

        private void Save_Change()
        {
            if (dbConnect.saveTable("InOut"))
            { 
                dbConnect.ReloadInOut();
                change = false;
                buttonSave.BackColor = Color.Transparent;
            }
        }

        private void FormInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    Save_Change();
                }
            }
        }

        private void dataGridViewInOut_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex > dataGridViewInOut.Rows.Count-2) 
            {
                
                int lastRow = e.RowIndex;                           //индекс активной строки для разблокировки                                                  
                dataGridViewInOut[0, lastRow].ReadOnly = false;     //разрешение изменения
                dataGridViewInOut[1, lastRow].ReadOnly = false;     //разрешение изменения

            }
        }
    }
}
