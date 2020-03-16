using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1
{
    class Report : Table
    {
        public Dictionary<string, int> Dishs = new Dictionary<string, int>();

        public Report()
        {
            Connection.Con.Open();

            BackDt = new DataTable();
            BackAdp = new SqlDataAdapter("select * from Report", Connection.Con);
            BackAdp.Fill(BackDt);

            FrontDt = new DataTable();
            FrontAdp = new SqlDataAdapter("select d.Name as [Название блюда], r.Portion as Порция from Report as r inner join Dishes as d on r.DishId = d.Id order by [Название блюда]", Connection.Con);
            FrontAdp.Fill(FrontDt);

            Connection.Con.Close();
        }

        public override void Delete(List<string> values)
        {
            string sqlExp = "DishId = " + Dishs[values[0]].ToString();
            var rows = BackDt.Select(sqlExp);
            rows[0].Delete();

            Execute();
        }

        public override void Update(List<string> oldValues, List<string> newValues)
        {
            string sqlExp;
            DataRow[] rows;

            if (!oldValues[0].Equals(newValues[0]))
            {
                sqlExp = "dishId = " + Dishs[newValues[0]];
                rows = BackDt.Select(sqlExp);
                if (rows.Length > 0)
                    throw new Exception("Такое блюдо уже существует");
            }

            if (newValues[0].Trim().Length == 0)
                throw new Exception("Выберите блюдо");

            sqlExp = "dishId = " + Dishs[oldValues[0]];
            rows = BackDt.Select(sqlExp);
            rows[0]["DishId"] = Dishs[newValues[0]];

            if (Double.TryParse(newValues[1], out var portion))
            {
                if (portion <= 0)
                    throw new Exception("Порция должна быть положительной");
                rows[0]["Portion"] = portion;
            }
            else
                throw new Exception("Несоответствие типа в столбце Порция");

            Execute();
        }

        public override void Add(List<string> vals)
        {
            if (vals[0].Trim().Length == 0)
                throw new Exception("Выберите блюдо");

            string sqlExp = "dishId = " + Dishs[vals[0]];
            var rows = BackDt.Select(sqlExp);
            if (rows.Length > 0)
            {
                var newVals = new List<string>();
                newVals.Add(vals[0]);
                double port = Double.Parse(vals[1]);
                if (port <= 0)
                    throw new Exception("Порция должна быть положительной");

                double d2 = Convert.ToDouble(rows[0][2].ToString()); 
                newVals.Add((port + d2).ToString());
                Update(vals, newVals);
                return;
            }

            var newRow = BackDt.NewRow();
            newRow["DishId"] = Dishs[vals[0]];
            if (Double.TryParse(vals[1], out var portion))
            {
                if (portion <= 0)
                    throw new Exception("Порция должна быть положительной");
                newRow["Portion"] = portion;
            }
            else
                throw new Exception("Несоответствие типа в столбце Порция");
            BackDt.Rows.Add(newRow);

            Execute();
        }

        public override void Prepare()
        {
            Dishs.Clear();
            string sql = "select Id, Name from Dishes order by Name";

            Connection.Con.Open();

            var command = new SqlCommand(sql, Connection.Con);
            var reader = command.ExecuteReader();
            if (reader.HasRows) // если есть данные                      
                while (reader.Read()) // построчно считываем данные                
                    Dishs.Add((string)reader.GetValue(1), (int)reader.GetValue(0));

            Connection.Con.Close();
        }

        public List<DataTable> GenerateReport(out string sum, out string sum1)
        {
            var ans = new List<DataTable>();
            var dt = new DataTable();
            var dt1 = new DataTable();

            Connection.Con.Open();
            string sql = "select p.Name as [Название продукта], sum(c.AmountOfProduct * r.Portion) as Количество, round(sum(p.Price * c.AmountOfProduct * r.Portion), 2) as Цена from Report as r inner join Calculations as c on r.DishId = c.DishId inner join Products as p on c.ProductId = p.Id group by p.Name";
            var adp = new SqlDataAdapter(sql, Connection.Con);
            adp.Fill(dt);
            ans.Add(dt);

            sql = "select d.Name as [Название блюда], r.Portion as [Количество порций], d.Price as Цена, d.Price*r.Portion as [Общая цена] from Report as r inner join Dishes as d on r.Dishid=d.Id";
            var adp1 = new SqlDataAdapter(sql, Connection.Con);
            adp1.Fill(dt1);
            ans.Add(dt1);
            Connection.Con.Close();

            double s1 = 0;
            double s = 0;
            foreach (DataRow dr in dt1.Rows)
                s1 += Convert.ToDouble(dr["Общая цена"].ToString());
            foreach (DataRow dr in dt.Rows)
                s += Convert.ToDouble(dr["Цена"].ToString());

            sum = s.ToString();
            sum1 = s1.ToString();

            return ans;
        }
    }
}
