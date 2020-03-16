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
    class Report : Table
    {
        public Dictionary<string, int> dishs = new Dictionary<string, int>();
        public Report()
        {
            Connection.Con.Open();

            backDt = new DataTable();
            backAdp = new SqlDataAdapter("select * from Report", Connection.Con);
            backAdp.Fill(backDt);

            frontDt = new DataTable();
            frontAdp = new SqlDataAdapter("select d.Name as [Название блюда], r.Portion as Порция from Report as r inner join Dishes as d on r.DishId = d.Id order by [Название блюда]", Connection.Con);
            frontAdp.Fill(frontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "DishId = " + dishs[values[0]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            r[0].Delete();

            Execute();
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            string sqlExp;
            DataRow[] r;

            if (!oldValues[0].Equals(newValues[0]))
            {
                sqlExp = "dishId = " + dishs[newValues[0]].ToString();
                r = backDt.Select(sqlExp);
                if (r.Length > 0) throw new Exception("Такое блюдо уже существует");
            }

            if (newValues[0].Trim().Length == 0) throw new Exception("Выберите блюдо");
            sqlExp = "dishId = " + dishs[oldValues[0]].ToString();
            r = backDt.Select(sqlExp);
            r[0]["DishId"] = dishs[newValues[0]];

            double d;
            if (Double.TryParse(newValues[1], out d))
            {
                if (d <= 0) throw new Exception("Порция должна быть положительной");
                r[0]["Portion"] = d;
            }
            else throw new Exception("Несоответствие типа в столбце Порция");

            Execute();
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0) throw new Exception("Выберите блюдо");
            string sqlExp = "dishId = " + dishs[vals[0]].ToString();
            DataRow[] r = backDt.Select(sqlExp);
            if (r.Length > 0)
            {
                List<string> newVals = new List<string>();
                newVals.Add(vals[0]);
                double d1 = Double.Parse(vals[1]);
                if (d1 <= 0) throw new Exception("Порция должна быть положительной");
                double d2 = Convert.ToDouble(r[0][2].ToString()); 
                newVals.Add((d1 + d2).ToString());
                Update(vals, newVals);
                return;
            }

            DataRow newRow = backDt.NewRow();
            newRow["DishId"] = dishs[vals[0]];
            double d;
            if (Double.TryParse(vals[1], out d))
            {
                if (d <= 0) throw new Exception("Порция должна быть положительной");
                newRow["Portion"] = d;
            }
            else throw new Exception("Несоответствие типа в столбце Порция");
            backDt.Rows.Add(newRow);

            Execute();
        }

        public override void Prepare()
        {
            dishs.Clear();
            string sql = "select Id, Name from Dishes order by Name";

            Connection.Con.Open();

            SqlCommand command = new SqlCommand(sql, Connection.Con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    dishs.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }

        public List<DataTable> GenerateReport(out string sum, out string sum1)
        {
            List<DataTable> ans = new List<DataTable>();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            Connection.Con.Open();
            string sql = "select p.Name as [Название продукта], sum(c.AmountOfProduct * r.Portion) as Количество, round(sum(p.Price * c.AmountOfProduct * r.Portion), 2) as Цена from Report as r inner join Calculations as c on r.DishId = c.DishId inner join Products as p on c.ProductId = p.Id group by p.Name";
            SqlDataAdapter adp = new SqlDataAdapter(sql, Connection.Con);
            adp.Fill(dt);
            ans.Add(dt);

            sql = "select d.Name as [Название блюда], r.Portion as [Количество порций], d.Price as Цена, d.Price*r.Portion as [Общая цена] from Report as r inner join Dishes as d on r.Dishid=d.Id";
            SqlDataAdapter adp1 = new SqlDataAdapter(sql, Connection.Con);
            adp1.Fill(dt1);
            ans.Add(dt1);
            Connection.Con.Close();

            double s1 = 0;
            double s = 0;
            foreach (DataRow dr in dt1.Rows) s1 += Convert.ToDouble(dr["Общая цена"].ToString());
            foreach (DataRow dr in dt.Rows) s += Convert.ToDouble(dr["Цена"].ToString());

            sum = s.ToString();
            sum1 = s1.ToString();

            return ans;
        }
    }
}
