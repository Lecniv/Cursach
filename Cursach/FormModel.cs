using System;
using System.Windows.Forms;

namespace Cursach
{
    public partial class FormModel : Form
    {
        public FormModel()
        {
            InitializeComponent();
        }


        protected bool newRowAdding = false; //для определения создается новая трока или редактируется существующая
        private bool change = false;
        int viewNM;                   //строка для вывода разъемов плат
        int sum_Connector;


        private void FormModel_Load(object sender, EventArgs e)
        {

            LoadDataModel();

        }

        private void LoadDataModel()//загрузка данных из БД
        {
            dataGridViewModel.DataSource = dbConnect.dataSet.Tables["Model"];

            var C_Area = new DataGridViewComboBoxColumn(); //добавить новую колонку

            C_Area.Name = "Область";
            C_Area.DataSource = dbConnect.dataSet.Tables["Area"]; //откуда данные
            C_Area.DisplayMember = "name_Area";         //что будет отображаться
            C_Area.ValueMember = "id_Area";             //по чем связывается из 2 таблицы
            C_Area.DataPropertyName = "id_Area";        //по чем связывается из 1 таблицы

            dataGridViewModel.Columns.Insert(dataGridViewModel.Columns.Count , C_Area);  //вставляем столбец в dataGridView- будет последний
            dataGridViewModel.Columns["id_Form_Pl"].Visible = false;        //делаем столбец невидимым
            dataGridViewModel.Columns["id_Area"].Visible = false;  //делаем столбец невидимым
            //dataGridViewModel.Columns["Область"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;    //автоподбор размера колонки
            dataGridViewModel.Columns["Область"].ReadOnly = true;       //запрет на изменение
            dataGridViewModel.Columns["sum_Connector"].ReadOnly = true; //запрет на изменение
            dataGridViewModel.Columns["name_Form_Pl"].HeaderText = "Наименование";    //переименовываем колонки
            dataGridViewModel.Columns["sum_Connector"].HeaderText = "Разъемов";    //переименовываем колонки
            dataGridViewModel.Columns["sum_Connector"].Width = 50;
            dataGridViewModel.Columns["sum_Property"].HeaderText = "Примечание";    //переименовываем колонки
            dataGridViewModel.Columns["sum_Property"].Width = 300;
            dataGridViewModel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком
            dataGridViewModel.Columns["name_Form_Pl"].Width = 150;
            dataGridViewModel.Columns["Область"].Width = 150;

            viewNM = Convert.ToInt32(dataGridViewModel.Rows[0].Cells["id_Form_Pl"].Value);
            dbConnect.LoadDataFiltrNM(viewNM.ToString());

            dataGridViewNameModel.DataSource = dbConnect.dataSet.Tables["filtrName_Model"];

            var NM_Con = new DataGridViewComboBoxColumn(); //добавить новую колонку форма коннектора в Name_Model

            NM_Con.Name = "Разъем";
            NM_Con.DataSource = dbConnect.dataSet.Tables["FormConnector"]; //откуда данные
            NM_Con.DisplayMember = "name_FormConnector";         //что будет отображаться
            NM_Con.ValueMember = "id_FormConnector";             //по чем связывается из 2 таблицы
            NM_Con.DataPropertyName = "id_FormConnector";        //по чем связывается из 1 таблицы

            dataGridViewNameModel.Columns.Insert(0, NM_Con);  //вставляем столбец в dataGridView- будет предпоследний


            dataGridViewNameModel.Columns["id_Form_Pl"].Visible = false;        //делаем столбец невидимым
            //dataGridViewNameModel.Columns["name_Form_Pl"].Visible = false;      //делаем столбец невидимым
            dataGridViewNameModel.Columns["id_FormConnector"].Visible = false;  //делаем столбец невидимым
            dataGridViewNameModel.Columns["Num_Connector"].ReadOnly = true;     //запрет на изменение
            dataGridViewNameModel.Columns["Разъем"].Width = 150;
            dataGridViewNameModel.Columns["Num_Connector"].Width = 50;
            dataGridViewNameModel.Columns["Num_Connector"].HeaderText = "№";    //переименовываем колонки
            dataGridViewNameModel.Columns["Разъем"].HeaderText = "Тип Разъема";    //переименовываем колонки
            dataGridViewNameModel.Columns["name_Connector"].HeaderText = "Обозначение";    //переименовываем колонки

            dataGridViewNameModel.AllowUserToAddRows = false;                       //запрет на добавление строк


            change = false;
        }

        private void dataGridViewModel_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //при добавлении строки разблокируем разъемы и области

            int lastRow = dataGridViewModel.Rows.Count - 2;
            //разблокируем ячейки на изменение
            dataGridViewModel[3, lastRow].ReadOnly = false;     //разрешение изменения
            dataGridViewModel[5, lastRow].ReadOnly = false;     //разрешение изменения

            change = true;

        }


        private void dataGridViewModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

                if (e.RowIndex >= 0)
                {
                    
                    if (dataGridViewModel.Rows[e.RowIndex].Cells["id_Form_Pl"].Value is int)    
                    {

                        //id для вывода ХП в dataGridView
                        viewNM = Convert.ToInt32(dataGridViewModel.Rows[e.RowIndex].Cells["id_Form_Pl"].Value);
                        dbConnect.LoadDataFiltrNM(viewNM.ToString());

                        sum_Connector = Convert.ToInt32(dataGridViewModel.Rows[e.RowIndex].Cells["sum_Connector"].Value);

                        int sumfiltrName_Model = dataGridViewNameModel.Rows.Count;
                        //если нет разъемов то создаем
                        if (sumfiltrName_Model == 0)
                            dbConnect.AddRowsNM(viewNM, sum_Connector);

                    }
                    splitContainer1.Panel2Collapsed = false;    //показываем вторую панель

                }

                
  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void buttonSaveNM_Click(object sender, EventArgs e)
        {
            dbConnect.saveTable("filtrName_Model");
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //вызываем окно для переспрашивания удалить ли строку
            if (MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                // удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridViewModel.SelectedRows)
                {
                    dataGridViewModel.Rows.Remove(row);
                }
                dbConnect.saveTable("Model");
                change = true;

            }

        }



        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbConnect.saveTable("Model");
        }

        private void FormModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    dbConnect.saveTable("Model");
                }
            }

        }

        private void dataGridViewModel_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            change = true;
        }
    }
}
