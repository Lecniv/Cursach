using System;
using System.Windows.Forms;

namespace Cursach
{
    public partial class FormPtoP : Form
    {
        int findToId;
        int ColumnCommand;//колонка управления

        public FormPtoP(int findToId)    //перегружаем конструктор - делаем окно со входными параметрами
        {
            InitializeComponent();
            this.findToId = findToId;   //делаем входную переменныю глобальной в классе
        }

        private void FormPtoP_Load(object sender, EventArgs e)
        {
            LoadConnectDataPtoP();
        }

        public void LoadConnectDataPtoP()
        {
            try
            {

                dbConnect.LoadConnectPtoP(findToId);

                dataGridViewFindPtoP.DataSource = dbConnect.dataSet.Tables["FindPtoP"];

                ColumnCommand = dataGridViewFindPtoP.Columns.Count - 1;

                dbConnect.dataGridViewCommand(dataGridViewFindPtoP);               //делаем последний столбик ссылочным

                dataGridViewFindPtoP.Columns["findID"].Visible = false;
                dataGridViewFindPtoP.Columns["name_Connector"].Visible = false;
                dataGridViewFindPtoP.Columns["FindPlaty"].HeaderText = "Плата:Разъем";
                dataGridViewFindPtoP.Columns["Number_Pl"].HeaderText = "Номер";
                dataGridViewFindPtoP.Columns["Serial_Pl"].HeaderText = "Серийный номер";
                dataGridViewFindPtoP.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
                dataGridViewFindPtoP.Columns["Other_PL"].HeaderText = "Примечание";
                dataGridViewFindPtoP.Columns["Other_PL"].Width = 150;
                dataGridViewFindPtoP.Columns["FindPlaty"].Width = 150;
                dataGridViewFindPtoP.Columns["Number_Pl"].Width = 150;
                dataGridViewFindPtoP.Columns["Serial_Pl"].Width = 150;
                dataGridViewFindPtoP.Columns["Invint_Pl"].Width = 150;
                dataGridViewFindPtoP.Columns["Other_PL"].Width = 190;
                dataGridViewFindPtoP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridViewFindPtoP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            //смотрим на какой столбец было нажатие - анализ по столбцу-управления (последний)
            if (e.ColumnIndex == ColumnCommand)
            {
                
                if (MessageBox.Show("Выполнить соединение ?", "Подключить", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes)
                {
                    // запоминаем строку 
                    int rowIndex = e.RowIndex;  //индекс строки
                    int Toid = Convert.ToInt32(dataGridViewFindPtoP.Rows[rowIndex].Cells["findID"].Value);  //индекс соединяемого разъема
                    dbConnect.ConnectPtoP(Toid, findToId);  //создание соединия, передаем id двух разъемов

                    this.Close();                       //закрытие окна

                }
            }


        }


    }
}
