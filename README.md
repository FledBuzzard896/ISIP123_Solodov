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
