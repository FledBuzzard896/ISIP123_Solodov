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
