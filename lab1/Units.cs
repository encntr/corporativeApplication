using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Units : Table
    {      
        public Units()
        {
            Connection.Con.Open();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Units ", Connection.Con);
            BackAdp.Fill(BackDt);
            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select Name as Название from Units order by Название", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "select Id from Units where Name = '" + values[0] + "'; ";

            Connection.Con.Open();
            var command = new SqlCommand(sqlExp, Connection.Con);
            var reader = command.ExecuteReader();
            reader.Read();
            var id = (int)reader.GetValue(0);

            sqlExp = "select * from Products where UnitId = " + id.ToString();
            command = new SqlCommand(sqlExp, Connection.Con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.Con.Close();
                throw new Exception("Невозможно удалить, т.к. существует продукт, который измеряется в " + values[0]);
            }

            Connection.Con.Close();

            sqlExp = "Name = '" + values[0] + "'";
            var rows = BackDt.Select(sqlExp);
            rows[0].Delete();

            Execute();           
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            if (newValues[0].Trim().Length == 0)
                throw new Exception("Название не может быть пустым");

            string sqlExp = "Name = '" + oldValues[0] + "'";
            var rows = BackDt.Select(sqlExp);
            rows[0][1] = newValues[0];

            sqlExp = "Name = '" + newValues[0] + "'";
            rows = BackDt.Select(sqlExp);
            if (rows.Length > 1)
            {
                Refresh();
                throw new Exception("Такая единица измерения уже существует");
            }
            Execute();
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0)
                throw new Exception("Название не может быть пустым");

            string sqlExp = "Name = '" + vals[0] + "'";
            var rows = BackDt.Select(sqlExp);
            if (rows.Length > 0)
                throw new Exception("Такая единица измерения уже существует");

            var newRow = BackDt.NewRow();
            newRow["Name"] = vals[0];
            BackDt.Rows.Add(newRow);
            Execute();            
        }

        public override void Prepare()
        {
            
        }        
    }
}
