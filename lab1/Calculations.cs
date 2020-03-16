using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Configuration;
using System.Data.SqlClient;

namespace lab1
{
    class Calculations : Table
    {
        public Dictionary<string, int> products = new Dictionary<string, int>();
        public Dictionary<string, int> dishs = new Dictionary<string, int>();

        public Calculations()
        {
            Connection.Con.Open();

            backDt = new DataTable();
            backAdp = new SqlDataAdapter("select * from Calculations ", Connection.Con);
            backAdp.Fill(backDt);
            frontDt = new DataTable();
            frontAdp = new SqlDataAdapter("select d.Name as [Название блюда], p.Name as [Название продукта], c.AmountOfProduct as [Количество продукта] from Calculations as c inner join Dishes as d on c.DishId = d.Id inner join Products as p on c.ProductId = p.Id order by [Название блюда]; ", Connection.Con);
            frontAdp.Fill(frontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "DishId = " + dishs[values[0]].ToString() + " and ProductId = " + products[values[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            r[0].Delete();

            Execute();           
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            string sqlExp = "DishId = " + dishs[oldValues[0]].ToString() + " and ProductId = " + products[oldValues[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            r[0][1] = dishs[newValues[0]];
            r[0][2] = products[newValues[1]];

            double d;
            if (double.TryParse(newValues[2], out d))
            {
                if (d <= 0) throw new Exception("Количество продукта должно быть положительным");
                r[0][3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Количество продукта'");

            sqlExp = "DishId = " + dishs[newValues[0]].ToString() + " and ProductId = " + products[newValues[1]].ToString();
            r = backDt.Select(sqlExp);
            if (r.Length > 1) { Refresh(); throw new Exception("Такая калькуляция уже существует"); }

            Execute();
        }

        public override void Add(List<string> vals)
        {
            string sqlExp = "DishId = " + dishs[vals[0]].ToString() + " and ProductId = " + products[vals[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            if (r.Length > 0) throw new Exception("Такая калькуляция уже существует");

            DataRow newRow = backDt.NewRow();
            newRow[1] = dishs[vals[0]];
            newRow[2] = products[vals[1]];

            double d;
            if (double.TryParse(vals[2], out d))
            {
                if (d <= 0) throw new Exception("Количество продукта должно быть положительным");
                newRow[3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Количество продукта'");

            backDt.Rows.Add(newRow);
            Execute();            
        }

        public override void Prepare()
        {
            products.Clear();
            string sql = "select Id, Name from Products order by Name";

            Connection.Con.Open();

            SqlCommand command = new SqlCommand(sql, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    products.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();

            dishs.Clear();
            sql = "select Id, Name from Dishes order by Name";

            Connection.Con.Open();

            command = new SqlCommand(sql, Connection.Con);
            reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    dishs.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }
    }
}
