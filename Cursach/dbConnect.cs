using System;
//using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach
{
    //класс для работы между dataset и БД
    public class dbConnect
    {
        public static int idFind; //переменная для передачи результата поиска
        //объявление переменных без инициализации
        private static SqlConnection sqlConnection = null;


        public static DataSet dataSet = null;

        private static SqlDataAdapter sqlDataAdapterArea = null;
        private static SqlDataAdapter sqlDataAdapterFormConnector = null;
        private static SqlDataAdapter sqlDataAdapterInOut = null;
        private static SqlDataAdapter sqlDataAdapterModel = null;
        private static SqlDataAdapter sqlDataAdapterNM = null;
        private static SqlDataAdapter sqlDataAdapterfiltrNM = null;
        private static SqlDataAdapter sqlDataAdapterPlaty = null;
        private static SqlDataAdapter sqlDataAdapterPtoP = null;
        private static SqlDataAdapter sqlDataAdapterConnectPtoP = null;
        private static SqlDataAdapter sqlDataAdapterFind = null;
        private static SqlCommandBuilder sqlBuilderArea = null;
        private static SqlCommandBuilder sqlBuilderCon = null;
        private static SqlCommandBuilder sqlBuilderModel = null;
        private static SqlCommandBuilder sqlBuilderNM = null;
                
        private static SqlCommand AddConnectoin;   //ХП для добавления связей
        private static SqlCommand delPtoP;         //ХП для удаления связей (удаляет сразу две)
                                                   //public SqlCommand delNM;       //делаем глобальной хранимую процедуру по удалению разъемов в NM по id_Form_Pl
                                                   //public SqlCommand insertModel; //ХП по добавлению сторк в Model

         //достаем строку подключения из конфигурационного файла
         private static readonly string connect = System.Configuration.ConfigurationManager.ConnectionStrings["connect_db"].ConnectionString;

        static dbConnect()
{
    LoadData();
}

static void LoadData()
{
    //делаем ссылку на БД
    sqlConnection = new SqlConnection(connect);
    //открываем подключение
    sqlConnection.Open();


    sqlDataAdapterArea          = new SqlDataAdapter("SELECT * FROM Area", sqlConnection);
    sqlDataAdapterFormConnector = new SqlDataAdapter("SELECT * FROM FormConnector", sqlConnection);
    sqlDataAdapterModel         = new SqlDataAdapter("SELECT * FROM Model", sqlConnection);
    sqlDataAdapterInOut         = new SqlDataAdapter("SELECT * FROM InOut", sqlConnection);
    sqlDataAdapterNM            = new SqlDataAdapter("SELECT * FROM Name_Model", sqlConnection);
    sqlDataAdapterPlaty         = new SqlDataAdapter("SELECT * FROM Platy", sqlConnection);
    sqlDataAdapterInOut         = new SqlDataAdapter("SELECT * FROM InOut", sqlConnection);

    delPtoP = new SqlCommand("delPtoP", sqlConnection); //создание хранимой процедуры для удаления связей
    delPtoP.CommandType = CommandType.StoredProcedure;

    AddConnectoin = new SqlCommand("AddConnectoin", sqlConnection);//создание хранимой процедуры для добавления связей
    AddConnectoin.CommandType = CommandType.StoredProcedure;



    //работа с таблицей Area

    sqlBuilderArea = new SqlCommandBuilder(sqlDataAdapterArea);
    ////для адаптера формирует команды (для sqlDataAdapterArea)
    //sqlBuilderArea.GetInsertCommand();
    sqlBuilderArea.GetUpdateCommand();
    sqlBuilderArea.GetDeleteCommand();

    //ХП добавления с возвратом ID
    sqlDataAdapterArea.InsertCommand = new SqlCommand("insertArea", sqlConnection);
    sqlDataAdapterArea.InsertCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterArea.InsertCommand.Parameters.Add(new SqlParameter("@name_Area", SqlDbType.NVarChar, 40, "name_Area"));
    SqlParameter parameterArea = sqlDataAdapterArea.InsertCommand.Parameters.Add("@id_Area", SqlDbType.Int, 0, "id_Area");
    parameterArea.Direction = ParameterDirection.Output;

    //работа с таблицей FormConnector

    sqlBuilderCon = new SqlCommandBuilder(sqlDataAdapterFormConnector);
    //sqlBuilderCon.GetInsertCommand();
    sqlBuilderCon.GetUpdateCommand();
    sqlBuilderCon.GetDeleteCommand();

    //ХП добавления с возвратом ID
    sqlDataAdapterFormConnector.InsertCommand = new SqlCommand("insertCon", sqlConnection);
    sqlDataAdapterFormConnector.InsertCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterFormConnector.InsertCommand.Parameters.Add(new SqlParameter("@name_FormConnector", SqlDbType.NVarChar, 40, "name_FormConnector"));
    sqlDataAdapterFormConnector.InsertCommand.Parameters.Add(new SqlParameter("@bool_patch", SqlDbType.Bit, 0, "bool_patch"));
    sqlDataAdapterFormConnector.InsertCommand.Parameters.Add(new SqlParameter("@id_Area", SqlDbType.Int, 0, "id_Area"));
    sqlDataAdapterFormConnector.InsertCommand.Parameters.Add(new SqlParameter("@Other_Con", SqlDbType.NVarChar, 40, "Other_Con"));
    SqlParameter parameterCon = sqlDataAdapterFormConnector.InsertCommand.Parameters.Add("@id_FormConnector", SqlDbType.Int, 0, "id_FormConnector");
    parameterCon.Direction = ParameterDirection.Output;



    //работа с таблицей FormModel

    sqlBuilderModel = new SqlCommandBuilder(sqlDataAdapterModel);
    sqlBuilderModel.GetUpdateCommand();

    //вставка через хранимую процедуру
    sqlDataAdapterModel.InsertCommand = new SqlCommand("insertModel", sqlConnection);
    sqlDataAdapterModel.InsertCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterModel.InsertCommand.Parameters.Add(new SqlParameter("@name_Form_Pl", SqlDbType.NVarChar, 40, "name_Form_Pl"));
    sqlDataAdapterModel.InsertCommand.Parameters.Add(new SqlParameter("@id_Area", SqlDbType.Int, 0, "id_Area"));
    sqlDataAdapterModel.InsertCommand.Parameters.Add(new SqlParameter("@sum_Connector", SqlDbType.Int, 0, "sum_Connector"));
    sqlDataAdapterModel.InsertCommand.Parameters.Add(new SqlParameter("@sum_Property", SqlDbType.NVarChar, 40, "sum_Property"));
    SqlParameter parameterMod = sqlDataAdapterModel.InsertCommand.Parameters.Add("@id_Form_Pl", SqlDbType.Int, 0, "id_Form_Pl");
    parameterMod.Direction = ParameterDirection.Output;

    //удаление через хранимую процедуру, т.к. надо сначало удалить зависимые разъёмы в таблице Connector

    sqlDataAdapterModel.DeleteCommand = new SqlCommand("delConPl", sqlConnection);
    sqlDataAdapterModel.DeleteCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterModel.DeleteCommand.Parameters.Add(new SqlParameter("@del_Pl", SqlDbType.Int, 0, "id_Form_Pl"));


    //работа с таблицей Name_Model

    sqlBuilderNM = new SqlCommandBuilder(sqlDataAdapterNM);
    sqlBuilderNM.GetInsertCommand();
    sqlBuilderNM.GetUpdateCommand();
    sqlBuilderNM.GetDeleteCommand();


    //работа с таблицей InOut

    sqlDataAdapterInOut.InsertCommand = new SqlCommand("insertInOut", sqlConnection);
    sqlDataAdapterInOut.InsertCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterInOut.InsertCommand.Parameters.Add(new SqlParameter("@a", SqlDbType.Int, 0, "id_FormConnector_A"));
    sqlDataAdapterInOut.InsertCommand.Parameters.Add(new SqlParameter("@b", SqlDbType.Int, 0, "id_FormConnector_B"));

    sqlDataAdapterInOut.DeleteCommand = new SqlCommand("delInOut", sqlConnection);
    sqlDataAdapterInOut.DeleteCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterInOut.DeleteCommand.Parameters.Add(new SqlParameter("@a", SqlDbType.Int, 0, "id_FormConnector_A"));
    sqlDataAdapterInOut.DeleteCommand.Parameters.Add(new SqlParameter("@b", SqlDbType.Int, 0, "id_FormConnector_B"));




    //работа с таблицей Platy

    sqlDataAdapterPlaty.InsertCommand = new SqlCommand("insertPlaty", sqlConnection);
    sqlDataAdapterPlaty.InsertCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterPlaty.InsertCommand.Parameters.Add(new SqlParameter("@id_Form_Pl", SqlDbType.Int, 0, "id_Form_Pl"));
    sqlDataAdapterPlaty.InsertCommand.Parameters.Add(new SqlParameter("@Number_Pl", SqlDbType.NVarChar, 40, "Number_Pl"));
    sqlDataAdapterPlaty.InsertCommand.Parameters.Add(new SqlParameter("@Serial_Pl", SqlDbType.NVarChar, 40, "Serial_Pl"));
    sqlDataAdapterPlaty.InsertCommand.Parameters.Add(new SqlParameter("@Invint_Pl", SqlDbType.NVarChar, 40, "Invint_Pl"));
    sqlDataAdapterPlaty.InsertCommand.Parameters.Add(new SqlParameter("@Other_PL", SqlDbType.NVarChar, 40, "Other_PL"));
    //Два выходных параметра - id платы и id области, вытащенное из шаблона платы
    SqlParameter parameter_ID_Platy = sqlDataAdapterPlaty.InsertCommand.Parameters.Add("@id_Plat", SqlDbType.Int, 0, "id_Plat");
    parameter_ID_Platy.Direction = ParameterDirection.Output;
    SqlParameter parameter_ID_Area = sqlDataAdapterPlaty.InsertCommand.Parameters.Add("@id_Area", SqlDbType.Int, 0, "id_Area");
    parameter_ID_Area.Direction = ParameterDirection.Output;


    sqlDataAdapterPlaty.UpdateCommand = new SqlCommand("updatePlaty", sqlConnection);
    sqlDataAdapterPlaty.UpdateCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterPlaty.UpdateCommand.Parameters.Add(new SqlParameter("@id_Plat", SqlDbType.Int, 0, "id_Plat"));
    sqlDataAdapterPlaty.UpdateCommand.Parameters.Add(new SqlParameter("@Number_Pl", SqlDbType.NVarChar, 40, "Number_Pl"));
    sqlDataAdapterPlaty.UpdateCommand.Parameters.Add(new SqlParameter("@Serial_Pl", SqlDbType.NVarChar, 40, "Serial_Pl"));
    sqlDataAdapterPlaty.UpdateCommand.Parameters.Add(new SqlParameter("@Invint_Pl", SqlDbType.NVarChar, 40, "Invint_Pl"));
    sqlDataAdapterPlaty.UpdateCommand.Parameters.Add(new SqlParameter("@Other_PL", SqlDbType.NVarChar, 40, "Other_PL"));


    sqlDataAdapterPlaty.DeleteCommand = new SqlCommand("delPlaty", sqlConnection);
    sqlDataAdapterPlaty.DeleteCommand.CommandType = CommandType.StoredProcedure;
    sqlDataAdapterPlaty.DeleteCommand.Parameters.Add(new SqlParameter("@del_Pl", SqlDbType.Int, 0, "id_Plat"));


    dataSet = new DataSet();

    //заполеяем dataSet
    sqlDataAdapterArea.Fill         (dataSet, "Area");
    sqlDataAdapterFormConnector.Fill(dataSet, "FormConnector");
    sqlDataAdapterInOut.Fill        (dataSet, "InOut");
    sqlDataAdapterModel.Fill        (dataSet, "Model");
    sqlDataAdapterNM.Fill           (dataSet, "Name_Model");
    sqlDataAdapterPlaty.Fill        (dataSet, "Platy");
    dataSet.Tables.Add("FindPtoP");             //создаем таблицу
    dataSet.Tables.Add("PtoP");                 //создаем таблицу
    dataSet.Tables.Add("filtrName_Model");      //создаем таблицу
    dataSet.Tables.Add("Find");                 //создаем таблицу для поиска



    //создаем связь в dataset между таблицами
    dataSet.Relations.Add(new DataRelation("rlAreaCon", dataSet.Tables["Area"].Columns["id_Area"], dataSet.Tables["FormConnector"].Columns["id_Area"]));
    dataSet.Relations.Add(new DataRelation("rlAreaModel", dataSet.Tables["Area"].Columns["id_Area"], dataSet.Tables["Model"].Columns["id_Area"]));
    dataSet.Relations.Add(new DataRelation("rlModelNM", dataSet.Tables["Model"].Columns["id_Form_Pl"], dataSet.Tables["Name_Model"].Columns["id_Form_Pl"]));
    dataSet.Relations.Add(new DataRelation("rlFC_NM", dataSet.Tables["FormConnector"].Columns["id_FormConnector"], dataSet.Tables["Name_Model"].Columns["id_FormConnector"]));
    dataSet.Relations.Add(new DataRelation("rlModel_Platy", dataSet.Tables["Model"].Columns["id_Form_Pl"], dataSet.Tables["Platy"].Columns["id_Form_Pl"]));



    sqlConnection.Close();

}

public static bool saveTable(string Table)
{
    try
    {
        sqlConnection.Open();


        switch (Table)
        {
            case "Area":
                {
                    sqlDataAdapterArea.Update(dataSet, "Area");
                    break;
                }
            case "FormConnector":
                {
                    sqlDataAdapterFormConnector.Update(dataSet, "FormConnector");
                    break;
                }
            case "Model":
                {
                    sqlDataAdapterModel.Update(dataSet, "Model");
                    break;
                }
            case "filtrName_Model":
                {
                    sqlDataAdapterNM.Update(dataSet, "filtrName_Model");// адаптер то же что и в Name_Model
                    break;
                }
            case "Platy":
                {
                    sqlDataAdapterPlaty.Update(dataSet, "Platy");
                    break;
                }
            case "InOut":
                {
                    sqlDataAdapterInOut.Update(dataSet, "InOut");
                    break;
                }
        }
        return true;    //операция выполнена без ошибок

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Операция недопустима!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;   ////операция не выполнена

    }
    finally
    {
        sqlConnection.Close();
    }
}

//добавление строк в dataset 
public static void AddRowsNM(int id_Form_Pl, int sum_Connector)
{
    var result = MessageBox.Show("Требуется описать все разъемы уборудования !!!", "Внимание", MessageBoxButtons.YesNo);
    if (result == DialogResult.Yes)
    {
        for (int i = 1; i <= sum_Connector; i++)
        {
            DataRow rowNM = dataSet.Tables["filtrName_Model"].NewRow();
            rowNM["id_Form_Pl"] = id_Form_Pl;
            rowNM["Num_Connector"] = i;
            dataSet.Tables["filtrName_Model"].Rows.Add(rowNM);
        }

    }
}



public static void LoadDataFiltrNM(string viewNM)
{
    try
    {
        sqlConnection.Open();
        dataSet.Tables["filtrName_Model"].Clear();
        sqlDataAdapterfiltrNM = new SqlDataAdapter("exec sortNM_to_Model '" + viewNM + "'", sqlConnection);

        sqlDataAdapterfiltrNM.Fill(dataSet, "filtrName_Model");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

public static void LoadDataPtoP(int PtoP)
{
    try
    {
        sqlConnection.Open();
        dataSet.Tables["PtoP"].Clear();
        sqlDataAdapterPtoP = new SqlDataAdapter("exec PlatyToPlaty '" + PtoP + "'", sqlConnection); //вызываем хранимую процедуру
        sqlDataAdapterPtoP.Fill(dataSet, "PtoP");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

//делает в присылаемом DataGridView последний столбец ссылочный- для управления
public static void dataGridViewCommand(DataGridView dataGridViewTable)
{
    for (int i = 0; i < dataGridViewTable.Rows.Count; i++)
    {
        DataGridViewLinkCell linkcell = new DataGridViewLinkCell();
        dataGridViewTable[dataGridViewTable.Columns.Count - 1, i] = linkcell;
    }
}


//для вызоав ХП для удаления связей
public static void DelPtoP(string idCon)
{
    try
    {
        sqlConnection.Open();
        delPtoP.Parameters.Clear();
        delPtoP.Parameters.Add(new SqlParameter("@id_Con", idCon));     //обязательный
        delPtoP.ExecuteNonQuery();              //выполнение ХП

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}



//поиск совместимых разъемов и загрузка данных в окно отображения
public static void LoadConnectPtoP(int findToId)
{
    try
    {
        sqlConnection.Open();
        sqlDataAdapterConnectPtoP = new SqlDataAdapter("exec PlatyFindPlaty '" + findToId + "'", sqlConnection); //вызываем хранимую процедуру
        dataSet.Tables["FindPtoP"].Clear();
        sqlDataAdapterConnectPtoP.Fill(dataSet, "FindPtoP");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

//соединение разъемов - выполнение ХП
public static void ConnectPtoP(int a, int b)
{
    try
    {
        sqlConnection.Open();
        AddConnectoin.Parameters.Clear();   //на всякий случай

        //MessageBox.Show(Toid.ToString()+ findToId.ToString());
        AddConnectoin.Parameters.Add(new SqlParameter("@idA", a.ToString()));
        AddConnectoin.Parameters.Add(new SqlParameter("@idB", b.ToString()));

        AddConnectoin.ExecuteNonQuery();    //выполнение ХП
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

public static void ReloadInOut()
{
    try
    {
        sqlConnection.Open();
        dataSet.Tables["InOut"].Clear();
        sqlDataAdapterInOut.Fill(dataSet, "InOut");
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

    
}

public static void FindNumber(string findText)
{
    try
    {
        sqlConnection.Open();
        sqlDataAdapterFind = new SqlDataAdapter("exec FindPlatyNum '" + findText + "'", sqlConnection); //вызываем хранимую процедуру
        dataSet.Tables["Find"].Clear();
        sqlDataAdapterFind.Fill(dataSet, "Find");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

public static void FindSerial(string findText)
{
    try
    {
        sqlConnection.Open();
        sqlDataAdapterFind = new SqlDataAdapter("exec FindPlatySerial '" + findText + "'", sqlConnection); //вызываем хранимую процедуру
        dataSet.Tables["Find"].Clear();
        sqlDataAdapterFind.Fill(dataSet, "Find");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

public static void FindInv(string findText)
{
    try
    {
        sqlConnection.Open();
        sqlDataAdapterFind = new SqlDataAdapter("exec FindPlatyInv '" + findText + "'", sqlConnection); //вызываем хранимую процедуру
        dataSet.Tables["Find"].Clear();
        sqlDataAdapterFind.Fill(dataSet, "Find");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

public static void FindOther(string findText)
{
    try
    {
        sqlConnection.Open();
        sqlDataAdapterFind = new SqlDataAdapter("exec FindPlatyOther '" + findText + "'", sqlConnection); //вызываем хранимую процедуру
        dataSet.Tables["Find"].Clear();
        sqlDataAdapterFind.Fill(dataSet, "Find");

    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }
    finally
    {
        sqlConnection.Close();
    }

}

}
}
