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
    class Dishes : Table
    {
        public Dictionary<string, int> types = new Dictionary<string, int>();

        public Dishes()
        {          
            Connection.Con.Open();

            backDt = new DataTable();
            backAdp = new SqlDataAdapter("select * from Dishes ", Connection.Con);
            backAdp.Fill(backDt);
            frontDt = new DataTable();
            frontAdp = new SqlDataAdapter("select d.Name as Имя, t.Name as [Тип блюда], d.Price as Цена from Dishes as d inner join Types as t on d.TypeId = t.Id order by Имя; ", Connection.Con);
            frontAdp.Fill(frontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "Name = '" + values[0] + "' and TypeId = " + types[values[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            r[0].Delete();

            Execute();           
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            if (newValues[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + oldValues[0] + "' and TypeId = " + types[oldValues[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            r[0][1] = newValues[0];
            r[0][2] = types[newValues[1]];

            double d;
            if (double.TryParse(newValues[2], out d))
            {
                if (d <= 0) throw new Exception("Цена должна быть положительной");
                r[0][3] = d;
            }
            else throw new Exception("Несоответствие типа в столбце 'Цена'");

            sqlExp = "Name = '" + newValues[0] + "' and TypeId = " + types[newValues[1]].ToString();
            r = backDt.Select(sqlExp);
            if (r.Length > 1) { Refresh(); throw new Exception("Такое блюдо уже существует"); }

            Execute();            
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0) throw new Exception("Название не может быть пустым");
            string sqlExp = "Name = '" + vals[0] + "' and TypeId = " + types[vals[1]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            if (r.Length > 0) throw new Exception("Такое блюдо уже существует");

            DataRow newRow = backDt.NewRow();
            newRow["Name"] = vals[0];
            newRow["TypeId"] = types[vals[1]];
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
            types.Clear();
            string sql = "select Id, Name from Types order by Name";

            Connection.Con.Open();

            SqlCommand command = new SqlCommand(sql, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    types.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }
    }
}
