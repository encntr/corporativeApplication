select p.Name as [�������� ��������], sum(c.AmountOfProduct) as ����������, sum(p.Price * c.AmountOfProduct) as ����
from Dishes as d inner join Calculations as c on d.Id = c.DishId
	inner join Products as p on c.ProductId = p.Id
group by p.Name
--where d.Name = '����������� �����'