using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cursach
{
    public partial class FormPlaty : Form
    {
        //public static dbConnect dbcon;
        FormPtoP FormPtoP;      //объявляем новое окно
        //private DataSet dataSet = null;
        //private SqlConnection sqlConnection = null;
        //private SqlDataAdapter sqlDataAdapterPlaty = null;
        //private SqlDataAdapter sqlDataAdapterModel = null;
        //private SqlDataAdapter sqlDataAdapterPtoP = null;
        //private SqlCommandBuilder sqlBuilderPlaty = null;
        //int ColumnCommand;  //столбец управления
        int ColumnCommandPtoP;  //столбец управления PtoP
        private SqlCommand delConPl;    //для хранимой процедуры - удаление разъемов при удалении платы
        int PtoP;   //id платы выводимых разъемов 
        private SqlCommand delPtoP;     //ХП для удаления связей (удаляет сразу две)
        private bool change = false;



        protected bool newRowAdding = false; //для определения создается новая трока или редактируется существующая

        public FormPlaty()
        {
            InitializeComponent();
        }

        private void FormPlaty_Load(object sender, EventArgs e)
        {
            //sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\Learn\курсовой\Cursach\Cursach\dbcursovoy.mdf; Integrated Security = True");

            //открытие подключения
            //sqlConnection.Open();

            LoadDataPlaty();
        }

        private void LoadDataPlaty()//загрузка данных из БД
        {
            try
            {
                //инициализируем экземпляр класса SQL adapter (запрос, соединение)

                
                //sqlDataAdapterModel = new SqlDataAdapter("SELECT * FROM  Model", sqlConnection);

                                                        //delConPl = new SqlCommand("delConPl", sqlConnection);
                                                        //delConPl.CommandType = CommandType.StoredProcedure; //создание хранимой процедуры

                                                        //delPtoP = new SqlCommand("delPtoP", sqlConnection);
                                                        //delPtoP.CommandType = CommandType.StoredProcedure; //создание хранимой процедуры для удаления связей

                //sqlBuilderPlaty = new SqlCommandBuilder(sqlDataAdapterPlaty);
                //sqlBuilderPlaty.GetInsertCommand();
                //sqlBuilderPlaty.GetUpdateCommand();
                //sqlBuilderPlaty.GetDeleteCommand();

                //dataSet = new DataSet();
                //sqlDataAdapterPlaty.Fill(dataSet, "Platy");
                //sqlDataAdapterModel.Fill(dataSet, "Model");

                //dataSet.Relations.Add(new DataRelation("rlPl_Model", dataSet.Tables["Model"].Columns["id_Form_Pl"], dataSet.Tables["Platy"].Columns["id_Form_Pl"]));   //связываем таблицы в DataSet

                //MessageBox.Show("Данные из БД загружены");
                //
                //dataGridViewPlaty.Columns["Command"].SortMode = DataGridViewColumnSortMode.NotSortable; //отключаем сортировку по столбцу управления

                dataGridViewPlaty.DataSource = FormPremier.dbcon.dataSet.Tables["v_Platy"];

                var M_Platy = new DataGridViewComboBoxColumn(); //добавить новую колонку

                M_Platy.Name = "Шаблон";
                M_Platy.DataSource = FormPremier.dbcon.dataSet.Tables["Model"];   //откуда данные
                M_Platy.DisplayMember = "name_Form_Pl";         //что будет отображаться
                M_Platy.ValueMember = "id_Form_Pl";             //по чем связывается из 2 таблицы
                M_Platy.DataPropertyName = "id_Form_Pl";        //по чем связывается из 1 таблицы
                dataGridViewPlaty.Columns.Insert(1, M_Platy);  //вставляем столбец в dataGridView- будет первой

                
                //dataGridViewPlaty.Columns[1].ReadOnly = true;         //блокировка

                //делаем последний столбец управления в виде ссылки
                //ColumnCommand = dataGridViewPlaty.Columns.Count - 1;
                //for (int i = 0; i < dataGridViewPlaty.Rows.Count; i++)
                //{
                //    DataGridViewLinkCell linkcellArea = new DataGridViewLinkCell();
                //    dataGridViewPlaty[ColumnCommand, i] = linkcellArea;
                //}

                //dataGridViewPlaty.Columns["id_Plat"].Visible = false;       //[0] делаем столбец нивидимым
                //dataGridViewPlaty.Columns["id_Form_Pl"].Visible = false;    //[2] делаем столбец нивидимым
                dataGridViewPlaty.Columns["Number_Pl"].HeaderText = "Номер";
                dataGridViewPlaty.Columns["Serial_Pl"].HeaderText = "Серийный номер";
                dataGridViewPlaty.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
                dataGridViewPlaty.Columns["Other_PL"].HeaderText = "Примечание";

                //---------------------------------------------------------------------------------------------------------------------------------


                PtoP = Convert.ToInt32(dataGridViewPlaty[0, 0].Value);

                //MessageBox.Show(PtoP.ToString());


                /////////////////////////////////sqlDataAdapterPtoP = new SqlDataAdapter("exec PlatyToPlaty '"+ PtoP + "'", sqlConnection); //вызываем хранимую процедуру
                //dataGridViewPlaty_to_Platy.Visible = false;



                //sqlDataAdapterPtoP.Fill(dataSet, "PtoP");

                //////////////////////////////////////dataGridViewPlaty_to_Platy.DataSource = dataSet.Tables["PtoP"];
                //////////////////////////////////////dataGridViewPlaty_to_Platy.AllowUserToAddRows = false;                       //запрет на добавление строк
                //ColumnCommandPtoP = dataGridViewPlaty_to_Platy.Columns.Count - 1;           //последний столбец управления
                //for (int i = 0; i < dataGridViewPlaty_to_Platy.Rows.Count; i++)
                //{
                //    DataGridViewLinkCell linkcellArea = new DataGridViewLinkCell();
                //    dataGridViewPlaty_to_Platy[ColumnCommandPtoP, i] = linkcellArea;
                //}

                //dataGridViewPlaty_to_Platy.Columns["id_Connector"].Visible = false;
                //dataGridViewPlaty_to_Platy.Columns["name_Connector"].HeaderText = "Разъем";
                //dataGridViewPlaty_to_Platy.Columns["Mod_Con"].HeaderText = "присоединенная\nПлата:Разъем";
                //dataGridViewPlaty_to_Platy.Columns["Number_Pl"].HeaderText = "Номер";
                //dataGridViewPlaty_to_Platy.Columns["Serial_Pl"].HeaderText = "Серийный номер";
                //dataGridViewPlaty_to_Platy.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
                //dataGridViewPlaty_to_Platy.Columns["Other_PL"].HeaderText = "Примечание";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //protected void ReLoadDataPtoP()//загрузка данных из БД
        //{
        //    try
        //    {
        //        //инициализируем экземпляр класса SQL adapter (запрос, соединение)


        //        //MessageBox.Show(PtoP.ToString());

        //        sqlDataAdapterPtoP = new SqlDataAdapter("exec PlatyToPlaty '" + PtoP + "'", sqlConnection); //вызываем хранимую процедуру
        //        //dataGridViewPlaty_to_Platy.Visible = false;


        //        dataSet.Tables["PtoP"].Clear();            //очистка

        //        sqlDataAdapterPtoP.Fill(dataSet, "PtoP");

        //        dataGridViewPlaty_to_Platy.DataSource = dataSet.Tables["PtoP"];
        //        dataGridViewPlaty_to_Platy.AllowUserToAddRows = false;                       //запрет на добавление строк
        //        for (int i = 0; i < dataGridViewPlaty_to_Platy.Rows.Count; i++)
        //        {
        //            DataGridViewLinkCell linkcellArea = new DataGridViewLinkCell();
        //            dataGridViewPlaty_to_Platy[ColumnCommandPtoP, i] = linkcellArea;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dataGridViewPlaty_to_Platy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            //try
            //{


            //    //смотрим на какой столбец было нажатие - анализ по столбцу-управления (последний)
            //    //if ((e.ColumnIndex == ColumnCommandPtoP)&& (e.RowIndex >= 0))
            //    //{
                    
            //    //    //смотрим на значение в выбранной строке по столбцу-управления
            //    //    string task = dataGridViewPlaty_to_Platy.Rows[e.RowIndex].Cells["Command"].Value.ToString();

            //    //    //MessageBox.Show(task);

            //    //    //if (task == "Delete")
            //    //    //{
            //    //    //    //вызываем окно для переспрашивания удалить ли строку
            //    //    //    if (MessageBox.Show("Разорвать связь?", "Разорвать", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //    //    //            == DialogResult.Yes)
            //    //    //    {
            //    //    //        // запоминаем строку удаления
            //    //    //        int rowIndex = e.RowIndex;

            //    //    //        delPtoP.Parameters.Clear();

            //    //    //        delPtoP.Parameters.Add(new SqlParameter("@id_Con", dataGridViewPlaty_to_Platy.Rows[rowIndex].Cells["id_Connector"].Value.ToString()));     //обязательный

            //    //    //        delPtoP.ExecuteNonQuery();              //выполнение ХП

            //    //    //        ReLoadDataPtoP();
            //    //    //    }
            //    //    //}
            //    //    //else if (task == "Insert")
            //    //    //{
            //    //    //    int rowIndex = e.RowIndex;
                        
            //    //    //    //вызываем окно для вставки платы с передачей id разъема
            //    //    //    FormPtoP = new FormPtoP(Convert.ToInt32(dataGridViewPlaty_to_Platy.Rows[rowIndex].Cells["id_Connector"].Value));    //передаем ID выбранного разъема
            //    //    //    FormPtoP.Owner = this;//определяем родителя
            //    //    //    FormPtoP.Show();

            //    //    //}

            //    //    //ReLoadDataPlaty();
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }



        private void dataGridViewPlaty_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            try
            {

                if (e.RowIndex >=0)
                {
                    if (dataGridViewPlaty[0, e.RowIndex].Value is int)
                        PtoP = Convert.ToInt32(dataGridViewPlaty[0, e.RowIndex].Value);
                    // MessageBox.Show(PtoP.ToString());
                    //ReLoadDataPtoP();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void dataGridViewPlaty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            //try
            //{


            //    //смотрим на какой столбец было нажатие - анализ по столбцу-управления (последний)
            //    if (e.ColumnIndex == ColumnCommand)
            //    {
            //        //смотрим на значение в выбранной строке по столбцу-управления
            //        string task = dataGridViewPlaty.Rows[e.RowIndex].Cells[ColumnCommand].Value.ToString();

            //        //if (task == "Delete")
            //        //{
            //        //    //вызываем окно для переспрашивания удалить ли строку
            //        //    if (MessageBox.Show("Удалить это оборудование?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //        //            == DialogResult.Yes)
            //        //    {
            //        //        // запоминаем строку удаления
            //        //        int rowIndex = e.RowIndex;

            //        //        delConPl.Parameters.Clear();

            //        //        //----------------------сначало удалим зависимые "порты"

            //        //        SqlParameter id_Com_PlParam = new SqlParameter
            //        //        {
            //        //            ParameterName = "@del_Pl",
            //        //            Value = dataSet.Tables["Platy"].Rows[rowIndex]["id_Plat"]
            //        //        };

            //        //        //добавляем триггер на удаление связей между разъемами

            //        //        delConPl.Parameters.Add(id_Com_PlParam);

            //        //        delConPl.ExecuteNonQuery();              //выполнение ХП
            //        //        //--------------удаляем саму плату----------------------------
            //        //        //удаляем из dataGridView (из таблицы отображения)
            //        //        dataGridViewPlaty.Rows.RemoveAt(rowIndex); //??? чем отличается от Remove ???
            //        //        //удаляем из dataSet
            //        //        dataSet.Tables["Platy"].Rows[rowIndex].Delete();
            //        //        //обновляем БД через адаптер ??? и построитель команд ???
            //        //        sqlDataAdapterPlaty.Update(dataSet, "Platy");
            //        //        //MessageBox.Show("3");

            //        //    }
            //        //}
            //        //else if (task == "Insert")
            //        //{
            //        //    //индекс строки из dataGridView для записи в dataset
            //        //    int rowIndex = dataGridViewPlaty.Rows.Count - 2;
            //        //    //делаем новую переменную типа DataRow в которую запишем ссылку на новую строку в dataSet
            //        //    DataRow row = dataSet.Tables["Platy"].NewRow();
            //        //    //записываем в новую строку данные из dataGridView

            //        //    row["id_Form_Pl"] = dataGridViewPlaty.Rows[rowIndex].Cells["id_Form_Pl"].Value;
            //        //    //запись в поле id_Area делаем по триггеру
            //        //    row["Number_Pl"] = dataGridViewPlaty.Rows[rowIndex].Cells["Number_Pl"].Value;
            //        //    row["Serial_Pl"] = dataGridViewPlaty.Rows[rowIndex].Cells["Serial_Pl"].Value;
            //        //    row["Invint_Pl"] = dataGridViewPlaty.Rows[rowIndex].Cells["Invint_Pl"].Value;
            //        //    row["Other_PL"] = dataGridViewPlaty.Rows[rowIndex].Cells["Other_PL"].Value;

            //        //    //добавляем в dataset новую строку заполненную данными
            //        //    dataSet.Tables["Platy"].Rows.Add(row);

            //        //    dataSet.Tables["Platy"].Rows.RemoveAt(dataSet.Tables["Platy"].Rows.Count - 1);

            //        //    dataGridViewPlaty.Rows.RemoveAt(dataGridViewPlaty.Rows.Count - 2);
            //        //    // изменяем значение последней колонки
            //        //    dataGridViewPlaty.Rows[e.RowIndex].Cells[ColumnCommand].Value = "Delete";
            //        //    //обновляем БД через адаптер

            //        //    sqlDataAdapterPlaty.Update(dataSet, "Platy");

            //        //    //dataSet.Tables["Model"].AcceptChanges();

            //        //    // MessageBox.Show(id_Form_Pl.ToString());

            //        //    newRowAdding = false;

            //        //}
            //        //else if (task == "Update")
            //        //{

            //        //    int r = e.RowIndex;

            //        //    //неизменяемые параметры
            //        //    //dataSet.Tables["Platy"].Rows[r]["id_Form_Pl"] = dataGridViewPlaty.Rows[r].Cells["id_Form_Pl"].Value;
            //        //    //dataSet.Tables["Platy"].Rows[r]["id_Area"] = dataGridViewPlaty.Rows[r].Cells["id_Area"].Value;
            //        //    //обновляем dataset
            //        //    dataSet.Tables["Platy"].Rows[r]["Number_Pl"] = dataGridViewPlaty.Rows[r].Cells["Number_Pl"].Value;
            //        //    dataSet.Tables["Platy"].Rows[r]["Serial_Pl"] = dataGridViewPlaty.Rows[r].Cells["Serial_Pl"].Value;
            //        //    dataSet.Tables["Platy"].Rows[r]["Invint_Pl"] = dataGridViewPlaty.Rows[r].Cells["Invint_Pl"].Value;
            //        //    dataSet.Tables["Platy"].Rows[r]["Other_PL"] = dataGridViewPlaty.Rows[r].Cells["Other_PL"].Value;





            //        //    //обновляем БД
            //        //    sqlDataAdapterPlaty.Update(dataSet, "Platy");
            //        //    // изменяем значение последней колонки
            //        //    dataGridViewPlaty.Rows[e.RowIndex].Cells[ColumnCommand].Value = "Delete";
            //        //}


            //        //ReLoadDataPlaty();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void dataGridViewPlaty_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
             
            //try
            //{
               
            //    if (newRowAdding == false)
            //    {
                    
            //        newRowAdding = true; //Добавляем новую строку
            //        // изменяем надпись в последней ячейке
            //        int lastRow = dataGridViewPlaty.Rows.Count - 2;
            //        //разблокируем ячейки на изменение
            //        dataGridViewPlaty[1,lastRow].ReadOnly = false;
            //        //??? создаем экземпляр класса DataGridViewRow
            //        DataGridViewRow row = dataGridViewPlaty.Rows[lastRow];
            //        DataGridViewLinkCell linkcell = new DataGridViewLinkCell(); // чтобы последний столбец был ссылочный, как при создании dataGridViewModel
            //        dataGridViewPlaty[ColumnCommand, lastRow] = linkcell;
            //        row.Cells["Command"].Value = "Insert";  // изменяем надпись в последней ячейке
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        private void dataGridViewPlaty_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if ((newRowAdding == false)&& (e.RowIndex >= 0))
                //{
                //    //получаем индекс строки по выделенной ячейке 
                //    int rowIndex = dataGridViewPlaty.SelectedCells[0].RowIndex;
                //    //??? создаем экземпляр класса DataGridViewRow
                //    DataGridViewRow editingRow = dataGridViewPlaty.Rows[rowIndex];

                //    DataGridViewLinkCell linkcell = new DataGridViewLinkCell(); // чтобы последний столбец был ссылочный, как при создании dataGridView
                //    dataGridViewPlaty[ColumnCommand, rowIndex] = linkcell;
                //    editingRow.Cells["Command"].Value = "Update";  // изменяем надпись в последней ячейке

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //dataGridViewPlaty_to_Platy


        private void обновитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
                //ReLoadDataPlaty();
        }



        private void FormPlaty_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (change == true)
            {
                if (MessageBox.Show("Сохранить изменения в базу данных?", "Сохранить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    FormPremier.dbcon.saveTable("Platy");
                }
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPremier.dbcon.saveTable("Platy");
        }
    }
}
