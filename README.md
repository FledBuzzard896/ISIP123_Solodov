```sql
use Solodov_NailNail;

create table _User
(
	ID int identity(1,1) primary key,
	Login nvarchar(50),
	Password nvarchar(50),
	FullName nvarchar(50),
	PhoneNumber nvarchar(20),
	Role int not null,

	foreign key (Role) references Roles(ID)
);

create table Roles 
(	
	ID int identity(1,1) primary key,
	Title nvarchar(50)
);
```

```
-- Создание базы данных (если нужна)
-- CREATE DATABASE BeautySalonManagement;
-- GO
-- USE BeautySalonManagement;
-- GO

-- 1. Роли пользователей (справочник)
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE -- 'Client', 'Master', 'Manager', 'Admin'
);

-- 2. Пользователи
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    PasswordHash NVARCHAR(255) NOT NULL, -- для аутентификации
    RoleID INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleID),
    IsFrozen BIT NOT NULL DEFAULT 0, -- заморозка пользователя
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 3. Производители косметики
CREATE TABLE Manufacturers (
    ManufacturerID INT IDENTITY(1,1) PRIMARY KEY,
    ManufacturerName NVARCHAR(100) NOT NULL UNIQUE
);

-- 4. Типы товаров (шампуни, маски и т.д.)
CREATE TABLE ProductTypes (
    ProductTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL UNIQUE
);

-- 5. Товары (косметика)
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(200) NOT NULL,
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    Description NVARCHAR(MAX) NULL,
    DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (DiscountPercent >= 0 AND DiscountPercent <= 100),
    ManufacturerID INT NOT NULL FOREIGN KEY REFERENCES Manufacturers(ManufacturerID),
    ProductTypeID INT NOT NULL FOREIGN KEY REFERENCES ProductTypes(ProductTypeID),
    Rating DECIMAL(3,2) NULL CHECK (Rating >= 0 AND Rating <= 5), -- средняя оценка
    IsFrozen BIT NOT NULL DEFAULT 0, -- заморозка (не продаётся)
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 6. Типы услуг (маникюр, педикюр и т.д.)
CREATE TABLE ServiceTypes (
    ServiceTypeID INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL UNIQUE,
    BasePrice DECIMAL(10,2) NOT NULL CHECK (BasePrice >= 0),
    DurationMinutes INT NOT NULL CHECK (DurationMinutes > 0) -- длительность услуги
);

-- 7. Какие услуги оказывает мастер (связь многие ко многим)
CREATE TABLE MasterServices (
    MasterServiceID INT IDENTITY(1,1) PRIMARY KEY,
    MasterID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    ServiceTypeID INT NOT NULL FOREIGN KEY REFERENCES ServiceTypes(ServiceTypeID),
    UNIQUE (MasterID, ServiceTypeID)
);

-- 8. Записи клиентов
CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    ClientID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    MasterID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    ServiceTypeID INT NOT NULL FOREIGN KEY REFERENCES ServiceTypes(ServiceTypeID),
    AppointmentDateTime DATETIME NOT NULL,
    FinalPrice DECIMAL(10,2) NOT NULL, -- цена на момент записи (может быть со скидкой)
    PaymentMethod NVARCHAR(50) NOT NULL CHECK (PaymentMethod IN ('Cash', 'Card', 'Online')),
    Comment NVARCHAR(500) NULL,
    IsCompleted BIT NOT NULL DEFAULT 0, -- выполнена ли услуга
    IsCancelled BIT NOT NULL DEFAULT 0, -- отменена ли запись
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 9. Корзина (незаказанные товары пользователя)
CREATE TABLE Carts (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    AddedAt DATETIME DEFAULT GETDATE(),
    UNIQUE (UserID, ProductID) -- один пользователь - один товар в корзине
);

-- 10. Заказы товаров (оформленные)
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    OrderDateTime DATETIME NOT NULL DEFAULT GETDATE(),
    DeliveryDate DATE NOT NULL, -- не более 7 дней от даты заказа (проверка через приложение)
    PaymentMethod NVARCHAR(50) NOT NULL CHECK (PaymentMethod IN ('Cash', 'Card', 'Online')),
    TotalAmount DECIMAL(10,2) NOT NULL,
    IsIssued BIT NOT NULL DEFAULT 0, -- выдан ли заказ (для менеджера)
    Comment NVARCHAR(500) NULL
);

-- 11. Состав заказа (товары в заказе)
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    PriceAtOrder DECIMAL(10,2) NOT NULL -- цена на момент заказа (с учётом скидки)
);

-- 12. Отзывы на товары (оценки)
CREATE TABLE ProductReviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    ReviewText NVARCHAR(1000) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UNIQUE (ProductID, UserID) -- один пользователь - один отзыв на товар
);

-- Индексы для производительности
CREATE INDEX IX_Appointments_DateTime ON Appointments(AppointmentDateTime);
CREATE INDEX IX_Appointments_MasterID ON Appointments(MasterID);
CREATE INDEX IX_Products_ProductName ON Products(ProductName);
CREATE INDEX IX_Orders_UserID ON Orders(UserID);
CREATE INDEX IX_Users_RoleID ON Users(RoleID);
```
