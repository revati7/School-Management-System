
use MusicStore_174875

CREATE TABLE tbl_User
(
	UserID int Constraint PK_email primary key identity(100,1),
	Email varchar(150) unique,
	Password varchar(100) not null,
	UserRole varchar(15) not null
)

select * from tbl_User

CREATE TABLE tbl_Grade
(
	GradeID int primary key identity(1,1),
	GradeName varchar(20),
	Description varchar(250)
)

insert into tbl_Grade values('First','First Standard Class')

select * from tbl_Grade

CREATE TABLE tbl_Teacher
(
	TeacherID int primary key identity(100,1),
	Name varchar(50) not null,
	Gender varchar(1) not null,
	DOB date not null,
	Contact varchar(10) not null,
	Email varchar(150) CONSTRAINT Fk_Teacher FOREIGN KEY(Email) REFERENCES tbl_User(Email),
	Address varchar(250) not null,
)

alter table tbl_Teacher add GradeID int Constraint Fk_GradeID FOREIGN KEY(GradeID) REFERENCES tbl_Grade(GradeID)

insert into tbl_Teacher Values ('Vivek','M','1995/10/12','9898979796','vivek@capgemini.com','Airoli')

update tbl_Teacher set Email='vivek@capgemini.com' where TeacherID=103

delete from tbl_Teacher where TeacherID=102

select * from tbl_Teacher

CREATE TABLE tbl_Student
(
	StudentID int primary key identity(1,1),
	StudentName varchar(50),
	GradeID int Constraint Fk_GradeIDStudent foreign key REFERENCES tbl_Grade(GradeID),
	Gender varchar(1),
	DOB date,
	BloodGroup varchar(5),
	Contact varchar(10),
	Address varchar(250)
)

insert into tbl_Student values('Raj',1,'M','2000/02/12','AB+','9879879876','Mumbai')

update tbl_Student set StudentName='Raju' where StudentID=2

Select * from tbl_Student


CREATE TABLE tbl_Absence
(
	AttendanceID int primary key identity(1,1),
	StudID int REFERENCES tbl_Student(StudentID),
	GradeID int REFERENCES tbl_Grade(GradeID),
	AbsenceDate date,
)

SELECT * FROM tbl_Absence

--CREATE TABLE tbl_Attendance
--(
--	AttendanceID int primary key identity(1,1),
--	StudID int REFERENCES tbl_Student(StudentID),
--	GradeID int REFERENCES tbl_Grade(GradeID),
--	AbsenceDate date,
--	Status varchar(15)
--)

--SELECT * FROM tbl_Attendance

CREATE TABLE tbl_Fees
(
	InvoiceID int primary key identity(1000,1),
	PaymentDate date,
	StudentID int REFERENCES tbl_Student(StudentID),
	GradeID int REFERENCES tbl_Grade(GradeID),
	Paid float,
	Pending float
)


select * from tbl_Fees




