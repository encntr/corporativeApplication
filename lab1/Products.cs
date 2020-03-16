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
    class Products : Table
    {
        public Dictionary<string, int> units = new Dictionary<string, int>();

        public Products()
        {
            Connection.Con.Open();

            backDt = new DataTable();
            backAdp = new SqlDataAdapter("select * from Products ", Connection.Con);
            backAdp.Fill(backDt);
            frontDt = new DataTable();
            frontAdp = new SqlDataAdapter("select p.Name as Название, u.Name as [Единица измерения], p.Price as Цена from Products as p inner join Units as u on p.UnitId = u.Id order by Название", Connection.Con);
            frontAdp.Fill(frontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "select Id from Products where Name = '" + values[0] + "'; ";

            Connection.Con.Open();
            SqlCommand command = new SqlCommand(sqlExp, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            int id;
            reader.Read();
            id = (int)reader.GetValue(0);

            sqlExp = "select * from Calculations where ProductId = " + id.ToString();
            command = new SqlCommand(sqlExp, Connection.Con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.Con.Close();
                throw new Exception("Невозможно удалить, т.к. продукт " + values[0] + " используется");
            }
            Connection.Con.Close();

            sqlExp = "Name = '" + values[0]+"'";
            DataRow[] r = backDt.Select(sqlExp);
            r[0].Delete();
            Execute();            
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            if (newValues[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + oldValues[0] + "'";
            DataRow[] r = backDt.Select(sqlExp);
            r[0][1] = newValues[0];
            r[0][2] = units[newValues[1]];

            double d;
            if (double.TryParse(newValues[2], out d))
            {
                if (d <= 0) throw new Exception("Цена должна быть положительной");
                r[0][3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Цена'");

            sqlExp = "Name = '" + newValues[0] + "'";
            r = backDt.Select(sqlExp);
            if(r.Length > 1) { Refresh(); throw new Exception("Такой продукт уже существует"); }
            Execute();           
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + vals[0] + "'";
            DataRow[] r = backDt.Select(sqlExp);
            if (r.Length > 0) throw new Exception("Такой продукт уже существует");

            DataRow newRow = backDt.NewRow();
            newRow["Name"] = vals[0];
            newRow["UnitId"] = units[vals[1]];
            double d;
            if (double.TryParse(vals[2], out d))
            {
                if (d <= 0) throw new Exception("Цена должна быть положительной");
                newRow["Price"] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Цена'");

            backDt.Rows.Add(newRow);
            Execute();
        }

        public override void Prepare()
        {
            units.Clear();
            string sql = "select Id, Name from Units order by Name";

            Connection.Con.Open();

            SqlCommand command = new SqlCommand(sql, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    units.Add((string)reader.GetValue(1), (int)reader.GetValue(0));              

            Connection.Con.Close();
        }
    }
}
