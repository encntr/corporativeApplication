using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Products : Table
    {
        public Dictionary<string, int> Units = new Dictionary<string, int>();

        public Products()
        {
            Connection.Con.Open();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Products ", Connection.Con);
            BackAdp.Fill(BackDt);
            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select p.Name as Название, u.Name as [Единица измерения], p.Price as Цена from Products as p inner join Units as u on p.UnitId = u.Id order by Название", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "select Id from Products where Name = '" + values[0] + "'; ";

            Connection.Con.Open();
            var command = new SqlCommand(sqlExp, Connection.Con);
            var reader = command.ExecuteReader();
            reader.Read();
            var id = (int)reader.GetValue(0);

            sqlExp = "select * from Calculations where ProductId = " + id;
            command = new SqlCommand(sqlExp, Connection.Con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.Con.Close();
                throw new Exception("Невозможно удалить, т.к. продукт " + values[0] + " используется");
            }
            Connection.Con.Close();

            sqlExp = "Name = '" + values[0]+"'";
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
            rows[0][2] = Units[newValues[1]];

            if (double.TryParse(newValues[2], out var price))
            {
                if (price <= 0)
                    throw new Exception("Цена должна быть положительной");
                rows[0][3] = price;
            }
            else
                throw new Exception("Несоответствие типа в столбце 'Цена'");

            sqlExp = "Name = '" + newValues[0] + "'";
            rows = BackDt.Select(sqlExp);
            if (rows.Length > 1)
            {
                Refresh();
                throw new Exception("Такой продукт уже существует");
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
                throw new Exception("Такой продукт уже существует");

            var newRow = BackDt.NewRow();
            newRow["Name"] = vals[0];
            newRow["UnitId"] = Units[vals[1]];

            if (double.TryParse(vals[2], out var price))
            {
                if (price <= 0) throw new Exception("Цена должна быть положительной");
                newRow["Price"] = price;
            }
            else
                throw new Exception("Несоответствие типа в столбце 'Цена'");

            BackDt.Rows.Add(newRow);
            Execute();
        }

        public override void Prepare()
        {
            Units.Clear();
            string sql = "select Id, Name from Units order by Name";

            Connection.Con.Open();

            var command = new SqlCommand(sql, Connection.Con);
            var reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    Units.Add((string)reader.GetValue(1), (int)reader.GetValue(0));              

            Connection.Con.Close();
        }
    }
}
