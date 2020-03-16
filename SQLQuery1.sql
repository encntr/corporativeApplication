select p.Name as [Название продукта], sum(c.AmountOfProduct) as Количество, sum(p.Price * c.AmountOfProduct) as Цена
from Dishes as d inner join Calculations as c on d.Id = c.DishId
	inner join Products as p on c.ProductId = p.Id
group by p.Name
--where d.Name = 'Итальянская пицца'