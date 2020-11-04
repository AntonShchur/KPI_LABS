use University

CREATE TABLE Teachers(
Teacher_Id INT PRIMARY KEY IDENTITY,
Teacher_Name VARCHAR(20) NOT NULL,
Teacher_Surname VARCHAR(20) NOT NULL,
Teacher_Email VARCHAR(40) NOT NULL,
Teacher_Phone VARCHAR(40) NOT NULL,
)

CREATE TABLE Disciplines(
Discipline_Id INT PRIMARY KEY IDENTITY,
Discipline_Name VARCHAR(40) NOT NULL
)

CREATE TABLE Faculties(
Faculty_Id INT PRIMARY KEY IDENTITY,
Faculty_Name VARCHAR(30) NOT NULL,
Faculty_Short_Name VARCHAR(10) NOT NULL
)

CREATE TABLE Departaments(
Departament_Id INT PRIMARY KEY IDENTITY,
Faculty_Id INT FOREIGN KEY REFERENCES Faculties(Faculty_Id),
Departament_Name VARCHAR(30) NOT NULL,
Departament_Short_Name VARCHAR(10) NOT NULL
)


CREATE TABLE Groups(
Group_Id INT PRIMARY KEY IDENTITY,
Departament_Id INT FOREIGN KEY REFERENCES Departaments(Departament_Id),
Group_Name VARCHAR(30) NOT NULL,
Group_Course INT NOT NULL
)

CREATE TABLE Students(
Student_Id INT PRIMARY KEY IDENTITY,
Group_Id INT FOREIGN KEY REFERENCES Groups(Group_Id),
Student_Name VARCHAR(50) NOT NULL,
Student_Email VARCHAR(50) NOT NULL,
Student_Phone VARCHAR(50) NOT NULL,
)


CREATE TABLE Schedule(
Schedule_Id INT PRIMARY KEY IDENTITY,
Schedule_Name VARCHAR(50) NOT NULL,
Teacher_Id INT FOREIGN KEY REFERENCES Teachers(Teacher_Id),
Discipline_Id INT FOREIGN KEY REFERENCES Disciplines(Discipline_Id),
Group_Id INT FOREIGN KEY REFERENCES Groups(Group_Id),
Schedule_Time VARCHAR(30),
Schedule_Classroom VARCHAR(20)
)


CREATE TABLE Setting_University(
University_Id INT PRIMARY KEY IDENTITY,
University_Name VARCHAR(30) NOT NULL,
University_Short_Name VARCHAR(10) NOT NULL,
University_Adress VARCHAR(50) NOT NULL,
University_Phone VARCHAR(40) NOT NULL,
University_Site VARCHAR(60) NOT NULL
)
