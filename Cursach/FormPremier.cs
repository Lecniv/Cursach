using System;
using System.Windows.Forms;

namespace Cursach
{
    
    public partial class FormPremier : Form
    {

        FormPtoP FormPtoP;          //объявляем новое окно
        int ColumnCommandPtoP;      //столбец управления PtoP
        int PtoP;                   //id платы выводимых разъемов 
        private bool change = false;//были ли изменения

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp formH = new FormHelp();
            formH.Show();
        }

        protected bool newRowAdding = false; //для определения создается новая трока или редактируется существующая

        public FormPremier()
        {
           InitializeComponent();
        }


        private void разъемовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConnector formC = new FormConnector();
            formC.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void областиПримененияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormArea formA = new FormArea();
            formA.Show();

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormModel formM = new FormModel();
            formM.Show();
        }
        

        private void совместимостьРазъемовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInOut formI = new FormInOut();
            formI.Show();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFind formF = new FormFind();
            formF.Show();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void FormPlaty_Load(object sender, EventArgs e)
        {
            LoadDataPlaty();
        }

        private void LoadDataPlaty()//загрузка данных из БД
        {
            try
            {
                dataGridViewPlaty.DataSource = dbConnect.dataSet.Tables["Platy"];

                var M_Platy = new DataGridViewComboBoxColumn(); //добавить новую колонку

                M_Platy.Name = "Шаблон";
                M_Platy.DataSource = dbConnect.dataSet.Tables["Model"];   //откуда данные
                M_Platy.DisplayMember = "name_Form_Pl";         //что будет отображаться
                M_Platy.ValueMember = "id_Form_Pl";             //по чем связывается из 2 таблицы
                M_Platy.DataPropertyName = "id_Form_Pl";        //по чем связывается из 1 таблицы
                dataGridViewPlaty.Columns.Insert(1, M_Platy);  //вставляем столбец в dataGridView- будет первой



                var Area_Platy = new DataGridViewComboBoxColumn(); //добавить новую колонку

                Area_Platy.Name = "Область";
                Area_Platy.DataSource = dbConnect.dataSet.Tables["Area"];   //откуда данные
                Area_Platy.DisplayMember = "name_Area";         //что будет отображаться
                Area_Platy.ValueMember = "id_Area";             //по чем связывается из 2 таблицы
                Area_Platy.DataPropertyName = "id_Area";        //по чем связывается из 1 таблицы
                dataGridViewPlaty.Columns.Insert(2, Area_Platy);  //вставляем столбец в dataGridView- будет первой



                dataGridViewPlaty.Columns["id_Plat"].Visible = false;       //[0] делаем столбец нивидимым
                dataGridViewPlaty.Columns["id_Form_Pl"].Visible = false;    //[2] делаем столбец нивидимым
                dataGridViewPlaty.Columns["id_Area"].Visible = false;    //[2] делаем столбец нивидимым
                dataGridViewPlaty.Columns["Number_Pl"].HeaderText = "Номер";
                dataGridViewPlaty.Columns["Serial_Pl"].HeaderText = "Серийный номер";
                dataGridViewPlaty.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
                dataGridViewPlaty.Columns["Other_PL"].HeaderText = "Примечание";
                dataGridViewPlaty.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком
                dataGridViewPlaty.Columns["Шаблон"].Width = 150;
                dataGridViewPlaty.Columns["Область"].Width = 150;
                dataGridViewPlaty.Columns["Number_Pl"].Width = 170;
                dataGridViewPlaty.Columns["Serial_Pl"].Width = 170;
                dataGridViewPlaty.Columns["Invint_Pl"].Width = 170;
                dataGridViewPlaty.Columns["Other_PL"].Width = 320;
                dataGridViewPlaty.Columns["Шаблон"].ReadOnly = true;    //запрет на изменение
                dataGridViewPlaty.Columns["Область"].ReadOnly = true;   //запрет на изменение

                //---------------------------------------------------------------------------------------------------------------------------------

                PtoP = Convert.ToInt32(dataGridViewPlaty[0, 0].Value);

                dbConnect.LoadDataPtoP(PtoP);

                dataGridViewPlaty_to_Platy.DataSource = dbConnect.dataSet.Tables["PtoP"];
                dataGridViewPlaty_to_Platy.AllowUserToAddRows = false;                       //запрет на добавление строк.



                ColumnCommandPtoP = dataGridViewPlaty_to_Platy.Columns.Count - 1;           //последний столбец управления

                dbConnect.dataGridViewCommand(dataGridViewPlaty_to_Platy);                      //делаем последний столбик ссылочным

                dataGridViewPlaty_to_Platy.Columns["id_Connector"].Visible = false;
                dataGridViewPlaty_to_Platy.Columns["name_Connector"].HeaderText = "Разъем";
                dataGridViewPlaty_to_Platy.Columns["Mod_Con"].HeaderText = "присоединенная\nПлата:Разъем";
                dataGridViewPlaty_to_Platy.Columns["Number_Pl"].HeaderText = "Номер";
                dataGridViewPlaty_to_Platy.Columns["Serial_Pl"].HeaderText = "Серийный номер";
                dataGridViewPlaty_to_Platy.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
                dataGridViewPlaty_to_Platy.Columns["Other_PL"].HeaderText = "Примечание";
                dataGridViewPlaty_to_Platy.Columns["Command"].HeaderText = "Управление";
                dataGridViewPlaty_to_Platy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком
                dataGridViewPlaty_to_Platy.Columns["name_Connector"].Width = 170;
                dataGridViewPlaty_to_Platy.Columns["Mod_Con"].Width = 170;
                dataGridViewPlaty_to_Platy.Columns["Number_Pl"].Width = 170;
                dataGridViewPlaty_to_Platy.Columns["Serial_Pl"].Width = 170;
                dataGridViewPlaty_to_Platy.Columns["Invint_Pl"].Width = 170;
                dataGridViewPlaty_to_Platy.Columns["Other_PL"].Width = 200;
                dataGridViewPlaty_to_Platy.Columns["Command"].Width = 85;

                SavePlaty();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridViewPlaty_to_Platy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //смотрим на какой столбец было нажатие -анализ по столбцу-управления(последний)
                if ((e.RowIndex >= 0)&&(e.ColumnIndex == ColumnCommandPtoP))
                {

                    //смотрим на значение в выбранной строке по столбцу-управления
                    string task = dataGridViewPlaty_to_Platy.Rows[e.RowIndex].Cells["Command"].Value.ToString();
                    string idCon = dataGridViewPlaty_to_Platy.Rows[e.RowIndex].Cells["id_Connector"].Value.ToString();
                    //MessageBox.Show(task);

                    if (task == "Delete")
                    {
                        //вызываем окно для переспрашивания удалить ли строку
                        if (MessageBox.Show("Разорвать связь?", "Разорвать", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                        {
                            // id разъема активной строки
                            
                            dbConnect.DelPtoP(idCon);

                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = e.RowIndex;

                        //вызываем окно для вставки платы с передачей id разъема
                        FormPtoP = new FormPtoP(Convert.ToInt32(idCon));    //передаем ID выбранного разъема
                        FormPtoP.Owner = this;//определяем родителя
                        FormPtoP.Show();
                        FormPtoP.FormClosed += (obj, args) => ReloadPtoP();  //обработчик закрытия формы (обновляем окно)

                    }

                    ReloadPtoP();                                          //перегружаем разъемы

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ReloadPtoP()
        {
            dbConnect.LoadDataPtoP(PtoP);                                          //перегружаем разъемы
            dbConnect.dataGridViewCommand(dataGridViewPlaty_to_Platy);             //делаем последний столбик ссылочным
        }


        private void dataGridViewPlaty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)//нажатие не по заголовку
                {
                    if (dataGridViewPlaty[0, e.RowIndex].Value is int)//не новая строка (есть id)
                    {
                        if (свойстваToolStripMenuItem.Checked == true)
                            splitContainer1.Panel2Collapsed = false;
                        PtoP = Convert.ToInt32(dataGridViewPlaty[0, e.RowIndex].Value);  //id нажатой платы
                    }
                    else splitContainer1.Panel2Collapsed = true;            //если элемент не сохранен - нет id  нет доступа к разъемам

                    ReloadPtoP();                                           //перегружаем разъемы

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPlaty_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    SavePlaty();
                }
            }

        }

        private void сохранитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SavePlaty(); 
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вызываем окно для переспрашивания удалить ли строку
            if (MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                   == DialogResult.Yes)
            {
                // удаляем выделенные строки
                foreach (DataGridViewRow row in dataGridViewPlaty.SelectedRows)
                {
                    dataGridViewPlaty.Rows.Remove(row);
                }
                //dbConnect.saveTable("Platy");
                ChangePlaty();

            }
        }


private void dataGridViewPlaty_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить выделенные строки?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            ChangePlaty();
        }

        private void свойстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !(splitContainer1.Panel2Collapsed);    // прячем/показываем вторую панель
        }


        private void dataGridViewPlaty_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //при добавлении строки разблокируем разъемы и области

            int lastRow = dataGridViewPlaty.Rows.Count - 2;
            //разблокируем ячейки на изменение
            dataGridViewPlaty[1, lastRow].ReadOnly = false;     //разрешение изменения
            ChangePlaty();
        }

        private void ChangePlaty()
        {
            change = true;
            labelPlaty.Visible = true;
        }

        private void SavePlaty()
        {

            if (dbConnect.saveTable("Platy"))
            {
                ReloadPtoP();                                           //перегружаем разъемы
                labelPlaty.Visible = false;
                change = false;
            }
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SavePlaty();
        }

        private void labelPlaty_Click(object sender, EventArgs e)
        {
            SavePlaty();
        }

        private void dataGridViewPlaty_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ChangePlaty();
        }


    }
}
