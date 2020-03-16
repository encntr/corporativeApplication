using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Dishes : Table
    {
        public Dictionary<string, int> Types = new Dictionary<string, int>();

        public Dishes()
        {          
            Connection.Con.Open();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Dishes ", Connection.Con);
            BackAdp.Fill(BackDt);
            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select d.Name as Имя, t.Name as [Тип блюда], d.Price as Цена from Dishes as d inner join Types as t on d.TypeId = t.Id order by Имя; ", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "Name = '" + values[0] + "' and TypeId = " + Types[values[1]];
            var rows = BackDt.Select(sqlExp);
            rows[0].Delete();

            Execute();           
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            if (newValues[0].Trim().Length == 0)
                throw new Exception("Название не может быть пустым");

            string sqlExp = "Name = '" + oldValues[0] + "' and TypeId = " + Types[oldValues[1]];
            var rows = BackDt.Select(sqlExp);
            rows[0][1] = newValues[0];
            rows[0][2] = Types[newValues[1]];

            if (double.TryParse(newValues[2], out var price))
            {
                if (price <= 0)
                    throw new Exception("Цена должна быть положительной");
                rows[0][3] = price;
            }
            else
                throw new Exception("Несоответствие типа в столбце 'Цена'");

            sqlExp = "Name = '" + newValues[0] + "' and TypeId = " + Types[newValues[1]].ToString();
            rows = BackDt.Select(sqlExp);
            if (rows.Length > 1)
            {
                Refresh();
                throw new Exception("Такое блюдо уже существует");
            }

            Execute();            
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0)
                throw new Exception("Название не может быть пустым");

            string sqlExp = "Name = '" + vals[0] + "' and TypeId = " + Types[vals[1]];
            var rows = BackDt.Select(sqlExp);
            if (rows.Length > 0)
                throw new Exception("Такое блюдо уже существует");

            var newRow = BackDt.NewRow();
            newRow["Name"] = vals[0];
            newRow["TypeId"] = Types[vals[1]];
            if (double.TryParse(vals[2], out double price))
            {
                if (price <= 0)
                    throw new Exception("Цена должна быть положительной");
                newRow["Price"] = price;
            }
            else
                throw new Exception("Несоответствие типа в столбце 'Цена'");

            BackDt.Rows.Add(newRow);
            Execute();
        }

        public override void Prepare()
        {
            Types.Clear();
            string sql = "select Id, Name from Types order by Name";

            Connection.Con.Open();

            var command = new SqlCommand(sql, Connection.Con);
            var reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    Types.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }
    }
}
