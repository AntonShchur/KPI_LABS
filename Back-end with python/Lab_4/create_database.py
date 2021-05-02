import psycopg2
import sqlite3
import pymysql

# Підключаємось до БД 1
postgres_connector = psycopg2.connect(
  database="Management",
  user="postgres",
  password="28731964",
  host="localhost",
  port="5432"
)
# Створюємо курсор
cur = postgres_connector.cursor()
# Створюємо таблицю Вчителі
cur.execute('''CREATE TABLE IF NOT EXISTS Teachers
     (Id INT PRIMARY KEY NOT NULL,
     Name VARCHAR(30) NOT NULL,
     Surname VARCHAR(30) NOT NULL,
     Patronymic VARCHAR(30) NOT NULL)''')
# Зберігаємо зміни
postgres_connector.commit()
# Створюємо таблицю Предмети
cur.execute('''CREATE TABLE IF NOT EXISTS Subjects 
     (Id INT PRIMARY KEY NOT NULL,
     NameSubject VARCHAR(50) NOT NULL,
     TeacherId INT NOT NULL,
     FOREIGN KEY (TeacherId) REFERENCES Teachers(Id))''')
# Зберігаємо зміни
postgres_connector.commit()
# Створюємо таблицю Завдання
cur.execute('''CREATE TABLE IF NOT EXISTS Tasks 
     (Id INT PRIMARY KEY NOT NULL,
     Title VARCHAR(100) NOT NULL,
     Description TEXT NOT NULL,
     Deadline TIMESTAMP,
     Results TEXT,
     SubjectId INT NOT NULL,
     FOREIGN KEY (SubjectId) REFERENCES Subjects (Id))''')
postgres_connector.commit()
# Зберігаємо зміни
# Заповнюємо даними БД 1
# cur.execute('''
# INSERT INTO Teachers(Id, Name, Surname, Patronymic) VALUES
# (1,'Семен','Боднарчук','Володимирович'),
# (2,'Тетяна','Ліхоузова','Анатоліївна'),
# (3,'Солдатова','Марія','Олександрівна'),
# (4,'Піхорович','Василь','Дмитрович'),
# (5,'Резніков','Сергій','Анатолійович');
# ''')
# postgres_connector.commit()
# cur.execute('''
# INSERT INTO Subjects(Id,NameSubject,TeacherId) VALUES
# (1,'Математичний аналіз',1),
# (2,'Дискретна математика',2),
# (3,'Аналіз даних',2),
# (4,'Компоненти програмної інженерії',3),
# (5,'Логіка',4),
# (6,'Загальна теорія розвитку',4),
# (7,'Основи Васк-еnd технологій',5);
# ''')
# postgres_connector.commit()
# cur.execute('''
# INSERT INTO Tasks(Id,Title,Description,Deadline,Results,SubjectId) VALUES
# (1,'Вступ до мат. аналізу','Завдання: повторити лекцію №1, виконати №1,2,3 з посібника', '2020-12-08',
# 'Антипенко - 5;
# Бойко - 6;
# Бондаренко - 6;
# Кобець - 7;
# Щур - 7', 1),
# (2,'Вектори','Завдання: повторити лекцію №2, виконати №4,5,6 з посібника', '2020-12-25',
# 'Антипенко - 7;
# Демиденко - 4;
# Кобець - 7;
# Максименко - 3;
# Щур - 7', 1),
# (3,'Практикум №4. Використання СУБД SQLite, MySQL, PostgreSQL з мовою програмування Python 3',
# 'Розробити інформаційну систему з використанням мови Python 3 та компонентів бібліотеки TkInter та СУБД (SQLite, MySQL, PostgreSQL) згідно № варіанту та додаткових умов. Кількість звязаних таблиць БД в системі 3',
# '2021-04-20',
# 'Петров, Іванов - 20;
# Семенцов, Барабаш - 18;
# Кобець, Щур - 25',
# 5),
# (4,'пр1 Описова статистика','Розрахувати:
# варіаційний ряд для простої вибірки;
# інтервальний варіаційний ряд для згрупованої вибірки;
# числові характеристики вибірки.
# Побудувати для згрупованої вибірки:
# гістограму частот;
# емпіричну функцію розподілу.
# Зробити висновок про інформативність гістограми частот при різній кількості інтервалів групування (дослідити 3-4 варіанти).',
# '2021-03-10',
# 'Барабаш - 7;
# Іванов - 8;
# Кобець - 8;
# Семенцов - 4;
# Петров - 9;
# Щур - 9',
# 3),
# (5,'пр2 Точкові оцінки параметрів розподілу',
# 'Визначити точкові оцінки параметрів розподілу (вважаємо, що випадкова величина розподілена нормально):
# методом моментів;
# методом найбільшої подібності.
# Порівняти отримані оцінки та вказати, яка з них краща та чому.
# Дослідити залежність оцінок від об’єму вибірки (по одному графіку для математичного сподівання та СКВ).
# Зробити висновки про виконання закону великих чисел.',
# '2021-03-25',
# 'Барабаш - 7;
# Іванов - 8;
# Кобець - 9;
# Семенцов - 4;
# Петров - 9;
# Щур - 9', 3),
# (6,'ПРАКТИЧНА РОБОТА1','Технологія розробки ПЗ Scram. Робота в Jira.
# Метою даної роботи є набуття практичних навичок планування процесу розробки програмного забезпечення з використанням програми Jira Software.',
# '2021-02-07',
# 'Барабаш - 3;
# Іванов - 4;
# Кобець - 4;
# Семенцов - 3;
# Петров - 4;
# Щур - 4', 4),
# (7,'Вступ до логіки','Повторити лекцію №1, виконати тест за посиланням: https://test.com/1.',
# '2021-12-08',
# 'Барабаш - 15;
# Іванов - 12;
# Кобець - 15;
# Семенцов - 13;
# Петров - 14;
# Щур - 15', 5);
# ''')
# Зберігаємо зміни
postgres_connector.commit()
# закриваємо з'єднання
postgres_connector.close()
# Створюємо з'єднання з БД 2
sqlite_connector = sqlite3.connect('Management.db')
# Створюємо курсор
cur = sqlite_connector.cursor()
# Створюємо таблицю Вчителі
cur.execute('''CREATE TABLE IF NOT EXISTS Teachers
     (Id INT PRIMARY KEY NOT NULL,
     Name VARCHAR(30) NOT NULL,
     Surname VARCHAR(30) NOT NULL,
     Patronymic VARCHAR(30) NOT NULL)''')
# Зберігаємо зміни
sqlite_connector.commit()
# Створюємо таблицю Предмети
cur.execute('''CREATE TABLE IF NOT EXISTS Subjects 
     (Id INT PRIMARY KEY NOT NULL,
     NameSubject VARCHAR(50) NOT NULL,
     TeacherId INT NOT NULL,
     FOREIGN KEY (TeacherId) REFERENCES Teachers(Id))''')
# Зберігаємо зміни
sqlite_connector.commit()
# Створюємо таблицю Завдання
cur.execute('''CREATE TABLE IF NOT EXISTS Tasks 
     (Id INT PRIMARY KEY NOT NULL,
     Title VARCHAR(100) NOT NULL,
     Description TEXT NOT NULL,
     Deadline TIMESTAMP,
     Results TEXT,
     SubjectId INT NOT NULL,
     FOREIGN KEY (SubjectId) REFERENCES Subjects (Id))''')
# Зберігаємо зміни
sqlite_connector.commit()
# закриваємо з'єднання з БД 2
sqlite_connector.close()

# Створюємо з'єднання з БД 3
mysql_connector = pymysql.connect(
  host='127.0.0.1',
  user='root',
  password='123456',
  db='Management',
  charset='utf8mb4',
)
# Створюємо курсор
mysql_cursor = mysql_connector.cursor()
# Створюємо таблицю Вчителі
mysql_cursor.execute('''CREATE TABLE IF NOT EXISTS Teachers
     (Id INT PRIMARY KEY NOT NULL,
     Name VARCHAR(30) NOT NULL,
     Surname VARCHAR(30) NOT NULL,
     Patronymic VARCHAR(30) NOT NULL)''')
# Зберігаємо зміни
mysql_connector.commit()
# Створюємо таблицю Вчителі
mysql_cursor.execute('''CREATE TABLE IF NOT EXISTS Subjects 
     (Id INT PRIMARY KEY NOT NULL,
     NameSubject VARCHAR(50) NOT NULL,
     TeacherId INT NOT NULL,
     FOREIGN KEY (TeacherId) REFERENCES Teachers(Id))''')
# Зберігаємо зміни
mysql_connector.commit()
# Створюємо таблицю Вчителі
mysql_cursor.execute('''CREATE TABLE IF NOT EXISTS Tasks 
     (Id INT PRIMARY KEY NOT NULL,
     Title VARCHAR(100) NOT NULL,
     Description TEXT NOT NULL,
     Deadline TIMESTAMP,
     Results TEXT,
     SubjectId INT NOT NULL,
     FOREIGN KEY (SubjectId) REFERENCES Subjects (Id))''')
# Зберігаємо зміни
mysql_connector.commit()
# Закриваємо зміни
mysql_connector.close()

