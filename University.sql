use University

create table Students(
	Id int identity(1,1) constraint PK_Students primary key,
	Name nvarchar(100),
	Age int
)

create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	Name nvarchar(100),
)

create table Together(
	StudentsId int constraint PK_Post_Students references Students(Id),
	GroupsId int constraint PK_Post_Groups references Groups(Id)
)

select * from Students
select * from Groups
select * from Together