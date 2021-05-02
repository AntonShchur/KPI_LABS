import sqlite3
import psycopg2
import pymysql


# Створюємо функцію для міграції з БД 1 в БД 2
def migration_postgres_to_sqlite():
    postgres_connector = psycopg2.connect(  # Створюємо конектор до БД 1
        database="Management",  # Вказуємо ім'я БД
        user="postgres",  # Вказуємо логін
        password="28731964",  # Вказуємо пароль
        host="localhost",  # Вказуємо хост
        port="5432"  # Вказуємо порт
    )

    sqlite_connector = sqlite3.connect('Management.db')  # Створюємо конектор до БД 2

    postgres_cursor = postgres_connector.cursor()  # Створюємо курсор до БД 1
    sqlite_cursor = sqlite_connector.cursor()  # Створюємо курсор до БД 2

    # Виконуємо запит до БД 1
    postgres_cursor.execute("SELECT * from Teachers")
    try:
        teachers_list = postgres_cursor.fetchall()  # Зберігаємо всю інформацію
        for teacher in teachers_list:
            id = teacher[0]  # Зберігаємо ІД
            name = teacher[1]  # Зберігаємо Ім'я
            surname = teacher[2]  # Зберігаємо Прізвище
            patronymic = teacher[3]   # Зберігаємо По-батькові
            params = (id, name, surname, patronymic)
            sqlite_cursor.execute("INSERT INTO Teachers(Id,Name, Surname, Patronymic)"
                                  " VALUES(?,?,?,?)", params)   # Виконуємо міграцію
            sqlite_connector.commit()   # Зберігаємо дані на сервері БД

    except(Exception):
        return "Дані записи вже імпортовано в SQLite3"   # Повертаємо орезультат
    # Виконуємо запит до БД 1
    postgres_cursor.execute("SELECT * from Subjects")
    try:
        subjects_list = postgres_cursor.fetchall()  # Зберігаємо всю інформацію
        for subject in subjects_list:
            id = subject[0]  # Зберігаємо ІД
            subject_name = subject[1]  # Зберігаємо Ім'я предмету
            teacher_id = subject[2]  # Зберігаємо ІД викладача
            params = (id, subject_name, teacher_id)
            sqlite_cursor.execute("INSERT INTO Subjects(Id, NameSubject,TeacherId)"
                                  " VALUES (?,?,?)", params)  # Виконуємо міграцію
            sqlite_connector.commit()   # Зберігаємо дані на сервері БД
    except(Exception):
        return "Дані записи вже імпортовано в SQLite3"   # Повертаємо орезультат
    # Виконуємо запит до БД 1
    postgres_cursor.execute("SELECT * from Tasks")
    try:
        tasks_list = postgres_cursor.fetchall()  # Зберігаємо всю інформацію
        for task in tasks_list:
            id = task[0]  # Зберігаємо ІД
            title = task[1]  # Зберігаємо заголовок завдання
            description = task[2]  # Зберігаємо опис завдання
            deadline = task[3] # Зберігаємо дату здачі завдання
            results = task[4] # Зберігаємо результати завдання
            subject_id = task[5] # Зберігаємо ІД предмету завдання
            params = (id, title, description, deadline, results, subject_id)
            sqlite_cursor.execute("INSERT INTO Tasks(Id, Title, Description, Deadline,Results, SubjectId) "  
                                  "VALUES (?,?,?,?,?,?)", params)  # Виконуємо міграцію
            sqlite_connector.commit()   # Зберігаємо дані на сервері БД
    except(Exception):
        return "Дані записи вже імпортовано в SQLite3"   # Повертаємо орезультат
    return 'Міграцію з PostgreSQL в SQLite виконано успішно'   # Повертаємо орезультат


def migration_sqlite_to_mysql():
    # sql_query = "SELECT * from Tasks where Tasks.Deadline >= '2020-00-00' and Tasks.Deadline <= '2021-01-01'"
    # Виконуємо запит до БД 2
    sql_query = "SELECT * from Teachers where Teachers.Id >= 2 and Teachers.Id <=4"
    sqlite_connector = sqlite3.connect('Management.db')  # Створюємо конектор до БД 2
    mysql_connector = pymysql.connect(   # Створюємо конектор до БД 3
        host='127.0.0.1',   # Вказуємо хост
        user='root',  # Вказуємо користувача
        password='123456',  # Вказуємо пароль
        db='Management',  # Вказуємо ім'я БД 3
    )

    sqlite_cursor = sqlite_connector.cursor()  # Створюємо курсор до БД 2
    mysql_cursor = mysql_connector.cursor()  # Створюємо курсор до БД 3

    sqlite_cursor.execute(sql_query)  # Виконуємо запит до БД 2
    result = sqlite_cursor.fetchall()  # Зберігаємо всю інформацію
    try:
        for row in result:
            sql = "INSERT INTO Teachers(Id,Name,Surname,Patronymic) VALUES(%s,%s,%s,%s)"  # Запит до БД 3 для міграції
            mysql_cursor.execute(sql, row)  # Виконуємо запит
            mysql_connector.commit()  # Зберігаємо змінм
        mysql_connector.close()   # Закриваємо з'єднання
        return "Дані про вчителів були успішно перенесені у БД 3"   # Повертаємо орезультат
    except(Exception):
        print("Дані вчителі вже є в БД 3")   # Повертаємо орезультат
        mysql_connector.close()   # Закриваємо з'єднання
        return "Дані вчителі вже є в БД 3"   # Повертаємо орезультат
