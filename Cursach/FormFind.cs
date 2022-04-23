using System;
using System.Windows.Forms;

namespace Cursach
{
    public partial class FormFind : Form
    {

        public FormFind()
        {
            InitializeComponent();
        }

        private void FormFind_Load(object sender, EventArgs e)
        {

            dbConnect.FindNumber("---");    //поиск --- для формирования таблицы в dataset

            dataGridViewFind.DataSource = dbConnect.dataSet.Tables["Find"];

            dataGridViewFind.Columns["id_Plat"].Visible = false;
            dataGridViewFind.Columns["name_Form_Pl"].HeaderText = "Шаблон";
            dataGridViewFind.Columns["name_Area"].HeaderText = "Область";
            dataGridViewFind.Columns["Number_Pl"].HeaderText = "Номер";
            dataGridViewFind.Columns["Serial_Pl"].HeaderText = "Серийный номер";
            dataGridViewFind.Columns["Invint_Pl"].HeaderText = "Инвентарный номер";
            dataGridViewFind.Columns["Other_PL"].HeaderText = "Примечание";
            dataGridViewFind.Columns["name_Form_Pl"].Width = 150;
            dataGridViewFind.Columns["name_Area"].Width = 150;
            dataGridViewFind.Columns["Number_Pl"].Width = 170;
            dataGridViewFind.Columns["Serial_Pl"].Width = 170;
            dataGridViewFind.Columns["Invint_Pl"].Width = 170;
            dataGridViewFind.Columns["Other_PL"].Width = 320;

            dataGridViewFind.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   //выделяется строка целиком
            dataGridViewFind.AllowUserToAddRows = false;                                //запрет на добавление строк

        }



        private void buttonFind_Click(object sender, EventArgs e)
        {

            if (radioButtonNum.Checked)
            {
                dbConnect.FindNumber(textBoxFind.Text);
            }
            else if (radioButtonSerial.Checked)
            {
                dbConnect.FindSerial(textBoxFind.Text);
            }
            else if (radioButtonInv.Checked)
            {
                dbConnect.FindInv(textBoxFind.Text);
            }
            else if (radioButtonOther.Checked)
            {
                dbConnect.FindOther(textBoxFind.Text);
            }
        }

        private void dataGridViewFind_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dbConnect.idFind = Convert.ToInt32(dataGridViewFind.Rows[e.RowIndex].Cells["id_Plat"].Value);
            //return idFind;
            this.Close();                       //закрытие окна
        }
    }
}
