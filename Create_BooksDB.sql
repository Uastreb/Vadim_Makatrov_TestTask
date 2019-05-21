create database Books_DB

use Books_DB

create table Books (
[id] int not null primary key identity(1,1),
[Name] varchar(30),
[Year] int
)

create table Authors(
[id] int not null primary key identity(1,1),
[Surname] varchar(20)
)

Create table Authors_Books
(
[id_Author] int not null foreign key references Authors(id),
[id_Book] int not null foreign key references Books(id)
)

Insert Into Authros_Books(id_Author, id_Book) Values (2, 21)

drop table Authors
drop table Authros_Books
drop table Books


use Books_DB
GO
CREATE Procedure DeleteBook(@id int)
As
Delete Authors_Books
where id_Book = @id
Delete Books
where id = @id

