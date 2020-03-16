if db_id('dbDishes') is not null drop database dbDishes;
go

create database dbDishes;
go

use dbDishes;
go

create table Types
(
	Id int identity(1,1) primary key,
	Name nvarchar(100) not null
)

create table Units
(
	Id int identity(1,1) primary key,
	Name nvarchar(100) not null	
)

create table Products
(
	Id int identity(1,1) primary key,
	Name nvarchar(100) not null,
	UnitId int not null,
	Price money not null,

	foreign key (UnitId) references Units (Id) on delete cascade on update cascade
)

create table Dishes
(
	Id int identity(1,1) primary key,
	Name nvarchar(100) not null,
	TypeId int not null,
	Price money not null,

	foreign key (TypeId) references Types(Id) on delete cascade on update cascade
)

create table Calculations
(
	Id int identity(1,1) primary key,
	DishId int not null,
	ProductId int not null,
	AmountOfProduct float not null,
	
	foreign key (DishId) references Dishes(Id) on delete cascade on update cascade,
	foreign key (ProductId) references Products(Id) on delete cascade on update cascade
)

create table Report
(
	Id int identity(1,1) primary key,
	Dishid int not null,
	Portion float not null,

	foreign key (DishId) references Dishes(Id) on delete cascade on update cascade
)

insert into Types(Name)
values
('Салат'),
('Пироги'),
('Жареное'),
('Супы'),
('Соус'),
('Пицца'),
('Десерт'),
('Бутерброд'),
('Закуски'),
('Напитки'),
('Мясное'),
('Рыбное')

insert into Units(Name)
values
('кг'),
('л'),
('шт')

insert into Products(Name, UnitId, Price)
values
('Курица', 1, 250.00),
('Яйцо', 3, 10.00),
('Мясо', 1, 300.00),
('Грибы', 3, 100.00),
('Малина', 1, 150.00),
('Сухофрукты', 1, 160.00),
('Мука', 1, 200.00),
('Картошка', 1, 40.5),
('Лук', 1, 30.7),
('Капуста', 1, 49.89),
('Фасоль', 1, 56.81),
('Масло', 2, 80.00)

insert into Dishes(Name, TypeId, Price)
values
('Куриное филе', 3, 200.00),
('Блины', 3, 100.00),
('Мясные рогалики', 3, 400.00),
('Суп из капусты и фасоли', 4, 100.00),
('Итальянская пицца', 6, 500.00),
('Грибная пицца', 6, 500.00),
('Вегетарианская пицца', 6, 400.00),
('Малиновый десерт', 7, 150.00),
('Смузи из сухофруктов', 7, 150.00)

insert into Calculations(DishId, ProductId, AmountOfProduct)
values
(1 , 1, 0.4),
(1 , 12, 0.2),
(1 , 9, 2.5),
(2 , 12, 0.1),
(2 , 2, 5.00),
(3 , 3, 1.00),
(3 , 12, 0.3),
(3 , 8, 0.4),
(4 , 10, 1.05),
(4 , 11, 0.5),
(4 , 12, 0.2),
(5 , 12, 0.45),
(5 , 1, 0.23),
(5 , 8, 0.45),
(5 , 9, 0.57),
(5 , 7, 0.32),
(6 , 12, 0.38),
(6 , 4, 5.00),
(6 , 9, 0.19),
(6 , 7, 0.59),
(7 , 12, 0.34)