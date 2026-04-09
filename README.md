Скрипт создания БД
```sql
-- create database Solodov_NailNail
use Solodov_NailNail

-- 1. Роли пользователей (справочник)
create table Roles (
    ID int identity(1,1) primary key,
    RoleName nvarchar(50) not null unique -- 'Client', 'Master', 'Manager', 'Admin'
);

-- 2. Пользователи
create table Users (
    ID int identity(1,1) primary key,
	Login nvarchar(50),
	Password nvarchar(50),
	FullName nvarchar(50),
	PhoneNumber nvarchar(20),
	Role int not null,
    CreatedAt datetime default getdate(),

	foreign key (Role) references Roles(ID)
);

-- 3. Производители косметики
create table Manufacturers (
    ID int identity(1,1) primary key,
    ManufacturerName nvarchar(100) not null unique
);

-- 4. Типы товаров (шампуни, маски и т.д.)
create table ProductTypes (
    ID int identity(1,1) primary key,
    TypeName nvarchar(100) not null unique
);

-- 5. Товары (косметика)
create table Products (
    ID int identity(1,1) primary key,
    ProductName nvarchar(200) not null,
    Price decimal(10,2) not null check (Price >= 0),
    Description nvarchar(max) null,
    DiscountPercent decimal(5,2) not null default 0 check (DiscountPercent >= 0 and DiscountPercent <= 100),
    ManufacturerID INT not null,
    ProductTypeID INT not null, 
    Rating decimal(3,2) null check (Rating >= 0 and Rating <= 5), -- средняя оценка

    foreign key (ManufacturerID) references Manufacturers(ID),
    foreign key (ProductTypeID) references ProductTypes(ID)
);

-- 6. Типы услуг (маникюр, педикюр и т.д.)
create table ServiceTypes (
    ID int identity(1,1) primary key,
    ServiceName nvarchar(100) not null unique,
    Price decimal(10,2) not null check (Price >= 0),
    DurationMinutes INT not null check (DurationMinutes > 0) -- длительность услуги
);

-- 7. Какие услуги оказывает мастер (связь многие ко многим)
create table MasterServices (
    ID int identity(1,1) primary key,
    MasterID int not null, 
    ServiceTypeID int not null, 

    foreign key (MasterID) references Users(ID),
    foreign key (ServiceTypeID) references ServiceTypes(ID),

    unique (MasterID, ServiceTypeID)
);

-- 8. Записи клиентов
create table Appointments (
    ID int identity(1,1) primary key,
    ClientID int not null, 
    MasterID int not null,
    ServiceTypeID int not null, 
    AppointmentDateTime datetime not null,
    TotalPrice decimal(10,2) not null,
    PaymentMethod nvarchar(50) not null check (PaymentMethod in ('Cash', 'Card', 'Online')),
    Comment nvarchar(500) null,
    IsCompleted bit not null default 0, -- выполнена ли услуга
    IsCancelled bit not null default 0, -- отменена ли запись
    CreatedAt datetime default getdate(),

    foreign key (ClientID) references Users(ID),
    foreign key (MasterID) references Users(ID),
    foreign key (ServiceTypeID) references ServiceTypes(ID)
);

-- 9. Корзина (незаказанные товары пользователя)
create table Carts (
    ID int identity(1,1) primary key,
    UserID int not null, 
    ProductID int not null, 
    Quantity int not null check (Quantity > 0),
    AddedAt datetime default getdate(),

    foreign key (UserID) references Users(ID),
    foreign key (ProductID) references Products(ID),

    unique (UserID, ProductID) -- один пользователь - один товар в корзине
);

-- 10. Заказы товаров (оформленные)
CREATE TABLE Orders (
    ID int identity(1,1) primary key,
    UserID int not null,
    OrderDateTime datetime not null default getdate(),
    DeliveryDate date not null, -- не более 7 дней от даты заказа (проверка через приложение)
    PaymentMethod nvarchar(50) not null check (PaymentMethod in ('Cash', 'Card', 'Online')),
    TotalPrice decimal(10,2) not null,
    IsIssued bit not null default 0, -- выдан ли заказ (для менеджера)
    Comment nvarchar(500) null,

    foreign key (UserID) references Users(ID)
);

-- 11. Состав заказа (товары в заказе)
create table OrderItems (
    ID int identity(1,1) primary key,
    OrderID int not null,
    ProductID int not null,
    Quantity int not null check (Quantity > 0),
    PriceAtOrder decimal(10,2) not null, -- цена на момент заказа (с учётом скидки)

    foreign key (OrderID) references Orders(ID)
    on delete cascade,
    foreign key (ProductID) references Products(ID)
);

-- 12. Отзывы на товары (оценки)
create table ProductReviews (
    ID int identity(1,1) primary key,
    ProductID int not null,
    UserID int not null, 
    Rating int not null check (Rating >= 1 and Rating <= 5),
    ReviewText nvarchar(1000) null,
    CreatedAt datetime default getdate(),

    foreign key (ProductID) references Products(ID),
    foreign key (UserID) references Users(ID),

    unique (ProductID, UserID) -- один пользователь - один отзыв на товар
);
```

Вставка данных
```sql
insert into Roles (RoleName)
values 
	(N'Client'),
	(N'Master'),
	(N'Manager'),
	(N'Admin');

insert into Users (Login, Password, FullName, PhoneNumber, Role, CreatedAt)
values 
	(N'sobaka224', N'kot12345!', N'Барабанов Пётр Александрович', N'+9 826 123 4567', 1, N'2026-04-07T14:12:00'),
	(N'BirRussianBoss', N'moneymoneybaby', N'Богатый Макс Максбетович', N'+111 999 521 6711', 4, N'2025-08-25T19:30:00'),
	(N'Burmalda123', N'mellstroyWIN', N'Андреевский Андрей Андреевич', N'8 922 123 3223', 3, N'2025-12-12T20:30:00' ),
	(N'Popka228', N'pipka114', N'Асхаб Кулдыр Банманович', N'1 231 231 2312', 2, N'2026-01-01T12:30:00' ),
	(N'VovaShkroba', N'vovashkrobaPassword', N'Вова Шкроба Гламурович', N'8 800 555 3535', 2, N'2000-01-01T00:00:01' );

insert into Manufacturers (ManufacturerName)
values 
	(N'L Oréal'),
	(N'Estée Lauder'),
	(N'Shiseido'),
	(N'Coty'),
	(N'Beiersdorf');

insert into ProductTypes (TypeName)
values 
	(N'Шампунь'),
	(N'Маска для волос'),
	(N'Кондиционер'),
	(N'Сыворотка'),
	(N'Бальзам');

insert into Products (ProductName, Price, Description, DiscountPercent, ManufacturerID, ProductTypeID, Rating)
values 
	(N'L Oréal Shampoo', 450.00, N'Укрепляющий шампунь', 5, 1, 1, 4.5),
	(N'Estée Lauder Serum', 8500.00, N'Восстанавливающая сыворотка', 10, 2, 4, 4.9),
	(N'Shiseido Mask', 3200.00, N'Маска для поврежденных волос', 0, 3, 2, 4.8),
	(N'Beiersdorf Shampoo', 300.00, N'Освежающий шампунь', 15, 5, 1, 4.2),
	(N'Coty Balm', 1200.00, N'Питательный бальзам', 0, 4, 5, 4.7),
	(N'L Oréal Conditioner', 1800.00, N'Кондиционер для блеска', 7, 1, 3, 4.6),
	(N'Estée Lauder Balm', 7200.00, N'Омолаживающий бальзам', 20, 2, 5, 4.9),
	(N'Shiseido Hair Mask', 1500.00, N'Интенсивная маска', 0, 3, 2, 5.0),
	(N'Beiersdorf Serum', 1100.00, N'Сыворотка для кожи головы', 5, 5, 4, 4.4),
	(N'Coty Conditioner', 950.00, N'Кондиционер-праймер', 0, 4, 3, 4.1),
	(N'L Oréal Hair Mask', 550.00, N'Увлажняющая маска', 12, 1, 2, 4.3),
	(N'Shiseido Shampoo', 1400.00, N'Восстанавливающий шампунь', 0, 3, 1, 4.8),
	(N'Estée Lauder Serum Night', 4500.00, N'Питательная сыворотка', 25, 2, 4, 4.7),
	(N'Beiersdorf Balm', 350.00, N'Бальзам-ополаскиватель', 0, 5, 5, 4.0),
	(N'Coty Serum', 800.00, N'Сыворотка для роста', 10, 4, 4, 3.9);

insert into ServiceTypes (ServiceName, Price, DurationMinutes)
values 
	(N'Маникюр с покрытием', 2500.00, 90),
	(N'Педикюр', 3000.00, 120),
	(N'Стрижка женская', 2000.00, 60),
	(N'Окрашивание волос', 5500.00, 180),
	(N'Коррекция бровей', 800.00, 30);

-- Вова (маникюр, педикюр, брови)
insert into MasterServices (MasterID, ServiceTypeID)
	values (27, 1), (27, 2), (27, 5);
-- Асхаб (all inclusive)
insert into MasterServices (MasterID, ServiceTypeID)
	values (19, 1), (19, 2), (19, 3), (19, 4), (19, 5);

insert into Appointments (ClientID, MasterID, ServiceTypeID, AppointmentDateTime, TotalPrice, PaymentMethod, Comment, IsCompleted, IsCancelled)
values 
-- Запись 1: Маникюр у Асхаба
	(16, 19, 1, '2026-04-10T10:00:00', 2500.00, 'Card', N'Нужен дизайн с собачкой', 0, 0),

-- Запись 2: Стрижка у Асхаба
	(16, 19, 3, '2026-04-12T14:30:00', 2000.00, 'Cash', N'Покороче по бокам', 0, 0),

-- Запись 3: Окрашивание у Асхаба
	(16, 19, 4, '2026-04-15T12:00:00', 5500.00, 'Online', N'В ярко-рыжий', 0, 0),

-- Запись 4: Педикюр у Вовы
	(16, 27, 2, '2026-04-18T16:00:00', 3000.00, 'Card', NULL, 0, 0),

-- Запись 5: Коррекция бровей у Вовы
	(16, 27, 5, '2026-04-20T11:00:00', 800.00, 'Cash', N'Сделать пошире', 0, 0);

insert into Carts (UserID, ProductID, Quantity, AddedAt)
values 
-- Товары для Петра (ID 1)
	(16, 1, 2, GETDATE()), -- L Oréal Shampoo (2 шт)
	(16, 2, 1, GETDATE()), -- Estée Lauder Serum
	(16, 3, 1, GETDATE()), -- Shiseido Mask
	(16, 6, 3, GETDATE()), -- L Oréal Conditioner
	(16, 12, 1, GETDATE()), -- Shiseido Shampoo

-- Товары для Асхаба (ID 4, роль 2)
	(19, 4, 5, GETDATE()), -- Beiersdorf Shampoo (оптом)

-- Товары для Вовы (ID 5, роль 2)
	(27, 8, 2, GETDATE()); -- Shiseido Hair Mask

insert into Orders (UserID, OrderDateTime, DeliveryDate, PaymentMethod, TotalPrice, IsIssued, Comment)
values 
-- Заказы Петра (ID 1)
	(16, '2026-04-08T10:00:00', '2026-04-12', 'Card', 5400.00, 1, N'Заказ выдан вовремя'),
	(16, '2026-04-09T15:30:00', '2026-04-15', 'Online', 3200.00, 0, N'Оставил у консьержа'),

-- Заказы Асхаба (ID 4)
	(19, '2026-04-07T12:00:00', '2026-04-10', 'Cash', 1500.00, 1, NULL),

-- Заказы Вовы (ID 5)
	(27, '2026-04-09T09:00:00', '2026-04-14', 'Online', 12400.50, 0, N'Позвонить за час до доставки'),
	(27, '2026-04-10T11:45:00', '2026-04-16', 'Card', 850.00, 0, N'Нужна подарочная упаковка');

insert into OrderItems (OrderID, ProductID, Quantity, PriceAtOrder)
values 
-- Заказ №1 (Пётр, ID=1): Шампунь L Oréal и Маска Shiseido
	(1, 1, 1, 427.50),  -- 450 - 5% скидка
	(1, 3, 1, 3200.00), -- 3200 (скидки нет)

-- Заказ №2 (Пётр, ID=1): Кондиционер L Oréal (3 шт)
	(2, 6, 3, 1674.00), -- 1800 - 7% скидка (цена за ед.)

-- Заказ №3 (Асхаб, ID=4): Шампунь Shiseido
	(3, 12, 1, 1400.00), -- 1400 (скидки нет)

-- Заказ №4 (Вова, ID=5): Сыворотка Estée Lauder и Бальзам Estée Lauder
	(4, 2, 1, 7650.00),  -- 8500 - 10% скидка
	(4, 7, 1, 5760.00),  -- 7200 - 20% скидка

-- Заказ №5 (Вова, ID=5): Сыворотка Coty
	(5, 15, 1, 720.00);  -- 800 - 10% скидка

insert into ProductReviews (ProductID, UserID, Rating, ReviewText)
values 
-- Отзывы Петра (ID 1)
	(1, 16, 5, N'Отличный шампунь L Oréal, волосы стали намного крепче. Рекомендую!'),
	(3, 16, 4, N'Маска Shiseido хорошая, но цена кусается. Эффект заметен после первого раза.'),

-- Отзыв Асхаба (ID 4)
	(12, 19, 5, N'Shiseido Shampoo — это мощь! Для бороды тоже отлично подошел.'),

-- Отзывы Вовы (ID 5)
	(2, 27, 5, N'Сыворотка Estée Lauder — мой фаворит. Ночной уход просто на высоте.'),
	(7, 27, 3, N'Бальзам Estée Lauder неплохой, но ожидал большего за такую стоимость.'),
	(15, 27, 2, N'Сыворотка Coty не подошла, пошло раздражение. Видимо, индивидуальная реакция.');
```
