using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Calculations : Table
    {
        public Dictionary<string, int> Products { get; set; }
        public Dictionary<string, int> Dishs { get; set; }

        public Calculations()
        {
            Connection.Con.Open();

            Init();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Calculations ", Connection.Con);
            BackAdp.Fill(BackDt);
            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select d.Name as [Название блюда], p.Name as [Название продукта], c.AmountOfProduct as [Количество продукта] from Calculations as c inner join Dishes as d on c.DishId = d.Id inner join Products as p on c.ProductId = p.Id order by [Название блюда]; ", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "DishId = " + Dishs[values[0]].ToString() + " and ProductId = " + Products[values[1]].ToString();
            DataRow[] r = BackDt.Select(sqlExp);
            r[0].Delete();

            Execute();           
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            string sqlExp = "DishId = " + Dishs[oldValues[0]].ToString() + " and ProductId = " + Products[oldValues[1]].ToString();
            DataRow[] r = BackDt.Select(sqlExp);
            r[0][1] = Dishs[newValues[0]];
            r[0][2] = Products[newValues[1]];

            double d;
            if (double.TryParse(newValues[2], out d))
            {
                if (d <= 0) throw new Exception("Количество продукта должно быть положительным");
                r[0][3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Количество продукта'");

            sqlExp = "DishId = " + Dishs[newValues[0]].ToString() + " and ProductId = " + Products[newValues[1]].ToString();
            r = BackDt.Select(sqlExp);
            if (r.Length > 1) { Refresh(); throw new Exception("Такая калькуляция уже существует"); }

            Execute();
        }

        public override void Add(List<string> vals)
        {
            string sqlExp = "DishId = " + Dishs[vals[0]].ToString() + " and ProductId = " + Products[vals[1]].ToString();
            DataRow[] r = BackDt.Select(sqlExp);
            if (r.Length > 0) throw new Exception("Такая калькуляция уже существует");

            DataRow newRow = BackDt.NewRow();
            newRow[1] = Dishs[vals[0]];
            newRow[2] = Products[vals[1]];

            double d;
            if (double.TryParse(vals[2], out d))
            {
                if (d <= 0) throw new Exception("Количество продукта должно быть положительным");
                newRow[3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Количество продукта'");

            BackDt.Rows.Add(newRow);
            Execute();            
        }

        public override void Prepare()
        {
            Products.Clear();
            string sql = "select Id, Name from Products order by Name";

            Connection.Con.Open();

            SqlCommand command = new SqlCommand(sql, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    Products.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();

            Dishs.Clear();
            sql = "select Id, Name from Dishes order by Name";

            Connection.Con.Open();

            command = new SqlCommand(sql, Connection.Con);
            reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    Dishs.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }

        private void Init()
        {
            Products = new Dictionary<string, int>();
            Dishs = new Dictionary<string, int>();
        }
    }
}
