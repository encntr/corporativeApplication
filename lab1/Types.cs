using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Types : Table
    {
        public Types()
        {
            Connection.Con.Open();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Types ", Connection.Con);
            BackAdp.Fill(BackDt);
            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select Name as Название from Types order by Название", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "select Id from Types where Name = '" + values[0] + "'; ";

            Connection.Con.Open();
            SqlCommand command = new SqlCommand(sqlExp, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            int id;
            reader.Read();
            id = (int)reader.GetValue(0);

            sqlExp = "select * from Dishes where TypeId = " + id.ToString();
            command = new SqlCommand(sqlExp, Connection.Con);
            reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                Connection.Con.Close();
                throw new Exception("Невозможно удалить, т.к. блюдо типа " + values[0] + " существует");
            }

            Connection.Con.Close();

            sqlExp = "Name = '" + values[0] + "'";
            DataRow[] r = BackDt.Select(sqlExp);
            r[0].Delete();
            Execute();            
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            if (newValues[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + oldValues[0] + "'";
            DataRow[] r = BackDt.Select(sqlExp);
            r[0][1] = newValues[0];

            sqlExp = "Name = '" + newValues[0] + "'";
            r = BackDt.Select(sqlExp);
            if (r.Length > 1)
            {
                Refresh();
                throw new Exception("Такой тип блюда уже существует");
            }
            Execute();            
        }

        public override void Add(List<string> vals)
        {
            if(vals[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + vals[0] + "'";
            DataRow[] r = BackDt.Select(sqlExp);
            if (r.Length > 0) throw new Exception("Такой тип блюда уже существует!");

            DataRow newRow = BackDt.NewRow();
            newRow["Name"] = vals[0];
            BackDt.Rows.Add(newRow);
            Execute();            
        }

        public override void Prepare()
        {

        }
    }
}
