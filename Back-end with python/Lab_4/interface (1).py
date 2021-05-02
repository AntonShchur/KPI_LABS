import psycopg2
import tkinter as tk
from tkinter import *
from tkinter import ttk
from tkinter import messagebox
from tkinter.ttk import Treeview
from Migrations import migration_postgres_to_sqlite
from Migrations import migration_sqlite_to_mysql

# Створюємо функцію для оновлення інформації про викладачів
def update(rows):
    trv.delete(*trv.get_children())
    for i in rows:
        trv.insert('','end', values=i)


# Створюємо функція для знаходження викладачів в базі даних
def search_teacher():
    q2 = q.get()
    query = "SELECT Id, Name, Surname, Patronymic from Teachers where Name like '%"+q2+"%' or Surname like '%"+q2+"%'"
    cursor.execute(query)
    rows = cursor.fetchall()
    update(rows)


# Створюємо функцію для очищення полів введення
def clear_teacher():
    query = "SELECT Id, Name, Surname, Patronymic from Teachers"
    cursor.execute(query)
    rows = cursor.fetchall()
    update(rows)
    q.set('')



# Створюємо функцію для отримання інформації про вчителя
def getrow_teacher(event):
    rowid = trv.identify_row(event.y)
    item = trv.item(trv.focus())
    t1.set(item['values'][0])
    t2.set(item['values'][1])
    t3.set(item['values'][2])
    t4.set(item['values'][3])


# Створюємо функцію для оновленння інформації про викладачі
def update_teacher():
    id=t1.get()
    name=t2.get()
    surname=t3.get()
    patronymic=t4.get()
    if messagebox.askyesno("Підтвердження дії", "Ви впевнені, що хочете оновити дані про даного викладача?"):
        # try:
            query = "UPDATE Teachers SET Name=%s, Surname=%s, Patronymic=%s where Id=%s"
            cursor.execute(query,(name,surname,patronymic,id))
            mydb.commit()
            clear_teacher()
        # except:
        #     messagebox.showerror('Err','err')
    else:
        return True


# Створюємо функцію для додавання нового викладача
def add_new_teacher():
    try:
        id=t1.get()
        name=t2.get()
        surname=t3.get()
        patronymic=t4.get()
        query = "INSERT into Teachers(Id,Name,Surname,Patronymic) values(%s, %s, %s, %s)"
        cursor.execute(query, (id,name,surname,patronymic))
        mydb.commit()
        clear_teacher()
    except:
        messagebox.showerror('Помилка!', 'Невірно введені дані. Перевірте поле id.')


# Створюємо функцію для видалення викладача
def delete_teacher():
    teacher_id = t1.get()
    if messagebox.askyesno("Підтвердження дії","Ви впевнені, що хочете видалити даного вчителя?"):
        try:
            query = "DELETE FROM Teachers where Id="+teacher_id
            cursor.execute(query)
            mydb.commit()
            clear_teacher()
        except:
            messagebox.showerror('Помилка!', 'Неможливо видалити даного вчителя, оскільки існують завдання та/або предмети, що пов"язані з даним викладачем.')
    else:
        return True

# Створюємо конектор до бази даних PostgreSQL
mydb = psycopg2.connect(
  database="Management",  # Назва БД
  user="postgres",  # Ім'я користувача БД
  password="28731964",  # Пароль
  host="localhost",  # Хост підключення до БД
  port="5432"  # Порт підключення
)
cursor = mydb.cursor()  # Створюємо курсор
# Створюємо вікно інтерфейсу
window = Tk()
q = StringVar()
t1 = StringVar()
t2 = StringVar()
t3 = StringVar()
t4 = StringVar()
# Прописуємо назву вікна
window.title("Kobets, Shchur - Lab#4")
window.geometry('800x600')  # Вказуємо розміри вікна
tab_control = ttk.Notebook(window)  # Створюємо менеджер вкладинок

# Створюємо чртири вкладинки
tab1 = ttk.Frame(tab_control)
tab2 = ttk.Frame(tab_control)
tab3 = ttk.Frame(tab_control)
tab4 = ttk.Frame(tab_control)
# Вказуємо для вкладинок назви
tab_control.add(tab1, text='Teachers')
tab_control.add(tab2, text='Subjects')
tab_control.add(tab3, text='Tasks')
tab_control.add(tab4, text='Migrations')
# Пакуємо менеджер вкладинок
tab_control.pack(expand=1, fill='both')
# Створюємо три блоки інтерфейсу на першій вкладинці
wrapper1 = LabelFrame(tab1, text = "Teachers List")
wrapper2 = LabelFrame(tab1, text = "Search")
wrapper3 = LabelFrame(tab1, text = "Teacher")
# Пакуємо три блоки інтерфейсу
wrapper1.pack(fill="both", expand="yes", padx=20, pady=10)
wrapper2.pack(fill="both", expand="yes", padx=20, pady=10)
wrapper3.pack(fill="both", expand="yes", padx=20, pady=10)
# Створюємо таблицю для перегляду даних з БД
trv = Treeview(wrapper1, columns=(1, 2, 3, 4), show="headings", height="6")
trv.pack()  # Пакуємо цю таблицю
# Створюємо колонки в таблиці
trv.heading(1, text = "Teacher ID")
trv.heading(2, text = "Name")
trv.heading(3, text = "Surname")
trv.heading(4, text = "Patronymic")

trv.bind('<Double 1>', getrow_teacher)

# Створюємо запит до БД для витягання інформації з таблиці про викладчів
query="SELECT Id, Name, Surname, Patronymic from Teachers"
cursor.execute(query)  # Виконуємо цез запит
rows = cursor.fetchall()  # записуємо інформацію
update(rows)  # Оновлюємо рядки в таблиці

# Пошук
lbl=Label(wrapper2, text="Search")  # Створюємо напис та вказуємо текст в ньому
lbl.pack(side=tk.LEFT, padx=10)  # Пакуємо цей напис
ent=Entry(wrapper2, textvariable=q)  # Створюємо поле введення
ent.pack(side=tk.LEFT, padx=6)  # Пакуємо його
btn = Button(wrapper2, text="Search", command=search_teacher)  # Створюємо кнопку та вказуємо її функцію
btn.pack(side=tk.LEFT, padx=6)  # Пакуємо дану кнопку
cbtn = Button(wrapper2, text="Clear", command=clear_teacher)  # Створюємо кнопку та вказуємо її функцію
cbtn.pack(side=tk.LEFT, padx=6)  # Пакуємо дану кнопку


# Заповнення полів
lbl1 = Label(wrapper3, text="Teacher Id")  # Створюємо напис та вказуємо текст в ньому
lbl1.grid(row=0, column = 0, padx=5, pady=3)  # Пакуємо цей напис
ent1 = Entry(wrapper3, textvariable = t1)  # Створюємо поле введення
ent1.grid(row=0, column=1,padx=5,pady=3)  # Пакуємо його

lbl2 = Label(wrapper3, text="Name") # Створюємо напис та вказуємо текст в ньому
lbl2.grid(row=1,column=0,padx=5,pady=3)  # Пакуємо цей напис
ent2 = Entry(wrapper3, textvariable = t2)  # Створюємо поле введення
ent2.grid(row=1, column=1,padx=5,pady=3)  # Пакуємо його

lbl3 = Label(wrapper3, text="Surname") # Створюємо напис та вказуємо текст в ньому
lbl3.grid(row=2,column=0,padx=5,pady=3)  # Пакуємо цей напис
ent3 = Entry(wrapper3, textvariable = t3)  # Створюємо поле введення
ent3.grid(row=2, column=1,padx=5,pady=3)  # Пакуємо його

lbl4 = Label(wrapper3, text="Patronymic")  # Створюємо напис та вказуємо текст в ньому
lbl4.grid(row=3,column=0,padx=5,pady=3)  # Пакуємо цей напис
ent4 = Entry(wrapper3, textvariable = t4)  # Створюємо поле введення
ent4.grid(row=3, column=1,padx=5,pady=3)  # Пакуємо його

up_btn = Button(wrapper3, text="Update", command=update_teacher)   # Додаємо кнопку оновлення і призначаємо функцію оновлення даних
add_btn = Button(wrapper3, text="Add New", command = add_new_teacher)   # Додаємо кнопку додавання і призначаємо функцію додавання даних
delete_btn = Button(wrapper3, text = "Delete", command=delete_teacher)   # Додаємо кнопку видалення і призначаємо функцію видалення даних

add_btn.grid(row = 4, column = 0, padx=5, pady=3)  # Пакуємо кнопку додавання
up_btn.grid(row = 4, column = 1, padx=5, pady=3)  # Пакуємо кнопку оновлення
delete_btn.grid(row = 4, column = 2, padx=5, pady=3)  # Пакуємо кнопку видалення


# ПЕРЕХОДИМО ДО РОБОТИ З РОЗДІЛОМ ПРЕДМЕТІВ
# /////////////////////////SUBJ///////////////////////////
# Створємо функію для оновлення рядків предметів
def update2(rows):
    trv2.delete(*trv2.get_children())
    for i in rows:
        trv2.insert('','end', values=i)


# Створємо функію для пошуку предметів
def search_subject():
    qq = q2.get()
    query = "SELECT Id, NameSubject, TeacherId from Subjects where NameSubject like '%"+qq+"%'"
    cursor.execute(query)
    rows = cursor.fetchall()
    update2(rows)


# Створємо функію для очищення рядків предметів
def clear_subject():
    query = "SELECT Id, NameSubject, TeacherId from Subjects"
    cursor.execute(query)
    rows = cursor.fetchall()
    update2(rows)
    q2.set('')


# Створємо функію для отримання рядків предметів
def getrow_subject(event):
    rowid = trv2.identify_row(event.y)
    item = trv2.item(trv2.focus())
    s1.set(item['values'][0])
    s2.set(item['values'][1])
    s3.set(item['values'][2])


# Створємо функію для оновлення предметів
def update_subject():
    id=s1.get()
    name=s2.get()
    teacherid=s3.get()
    if messagebox.askyesno("Підтвердження дії", "Ви впевнені, що хочете оновити дані про даний предмет?"):
        try:
            query = "UPDATE Subjects SET NameSubject=%s, TeacherId=%s where Id=%s"
            cursor.execute(query,(name,teacherid,id))
            mydb.commit()
            clear_subject()
        except:
            messagebox.showerror('Err','err')
    else:
        return True


# Створємо функію для додвання предметів
def add_new_subject():
    try:
        id=s1.get()
        name=s2.get()
        teacherid=s3.get()
        query = "INSERT into Subjects(Id,NameSubject,TeacherId) values(%s, %s, %s)"
        cursor.execute(query, (id,name,teacherid))
        mydb.commit()
        clear_subject()
    except:
        messagebox.showerror('Помилка!', 'Невірно введені дані. Перевірте поле id.')


# Створюємо функію для видалення предметів
def delete_subject():
    subject_id = s1.get()
    if messagebox.askyesno("Підтвердження дії","Ви впевнені, що хочете видалити даний предмет?"):
        try:
            query = "DELETE FROM Subjects where Id="+subject_id
            cursor.execute(query)
            mydb.commit()
            clear_subject()
        except:
            messagebox.showerror('Помилка!', 'Неможливо видалити даний предмет, оскільки існують завдання, що пов"язані з даним предметом.')
    else:
        return True
q2 = StringVar()
s1 = StringVar()
s2 = StringVar()
s3 = StringVar()


wrapper4 = LabelFrame(tab2, text = "Subjects List")  # Створюємо блок списку предметів на другому розділі
wrapper5 = LabelFrame(tab2, text = "Search")  # Створюємо блок пошуку предметів на другому розділі
wrapper6 = LabelFrame(tab2, text = "Subject")  # Створюємо блок предметів на другому розділі

wrapper4.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок
wrapper5.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок
wrapper6.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок

trv2 = Treeview(wrapper4, columns=(1, 2, 3), show="headings", height="6")  # Створюємо таблицю для перегляду предметів
trv2.pack()  # Пакуємо таблицю
# Назвиваємо поля в таблиці
trv2.heading(1, text = "Subject ID")
trv2.heading(2, text = "Name")
trv2.heading(3, text = "Teacher ID")
trv2.bind('<Double 1>', getrow_subject)

# Створюємо запит до БД
query2="SELECT Id, NameSubject, TeacherId from Subjects"  # Запит до БД
cursor.execute(query2)  # Виконуємо його
rows2 = cursor.fetchall()  # Зберігаємо інформацію
update2(rows2)  # Оновлюємо інформацію

# Пошук
lbl=Label(wrapper5, text="Search")  # Створюємо напис і даємо йому текст
lbl.pack(side=tk.LEFT, padx=10)  # Пакуємо напис
ent=Entry(wrapper5, textvariable=q2)  # Створюємо поле введення
ent.pack(side=tk.LEFT, padx=6)  # Пакуємо поле введення
btn = Button(wrapper5, text="Search", command=search_subject)  # Створюємо кнопку пошуку і даємо її команду пошуку
btn.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку
cbtn = Button(wrapper5, text="Clear", command=clear_subject) # Створюємо кнопку очищення і даємо її команду очищення
cbtn.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку


# Заповнення полів
lbl1 = Label(wrapper6, text="Subject Id")  # Створюємо напис і даємо йому текст
lbl1.grid(row=0, column = 0, padx=5, pady=3)  # Пакуємо напис
ent1 = Entry(wrapper6, textvariable = s1)  # Створюємо поле введення
ent1.grid(row=0, column=1,padx=5,pady=3)  # Пакуємо поле введення

lbl2 = Label(wrapper6, text="NameSubject")  # Створюємо напис і даємо йому текст
lbl2.grid(row=1,column=0,padx=5,pady=3)  # Пакуємо напис
ent2 = Entry(wrapper6, textvariable = s2)  # Створюємо поле введення
ent2.grid(row=1, column=1,padx=5,pady=3)  # Пакуємо поле введення

lbl3 = Label(wrapper6, text="Teacher Id")  # Створюємо напис і даємо йому текст
lbl3.grid(row=2,column=0,padx=5,pady=3)  # Пакуємо напис
ent3 = Entry(wrapper6, textvariable = s3)  # Створюємо поле введення
ent3.grid(row=2, column=1,padx=5,pady=3)  # Пакуємо поле введення

up_btn_subj=Button(wrapper6, text="Update", command=update_subject)   # Додаємо кнопку оновлення і призначаємо функцію оновлення даних
add_btn_subj = Button(wrapper6, text="Add New", command = add_new_subject)   # Додаємо кнопку додавання і призначаємо функцію додавання даних
delete_btn_subj = Button(wrapper6, text = "Delete", command=delete_subject)   # Додаємо кнопку видалення і призначаємо функцію видалення даних

add_btn_subj.grid(row = 4, column = 0, padx=5, pady=3)  # Пакуємо кнопку додавання
up_btn_subj.grid(row = 4, column = 1, padx=5, pady=3)  # Пакуємо кнопку оновленя
delete_btn_subj.grid(row = 4, column = 2, padx=5, pady=3)  # Пакуємо кнопку видалення


# ПЕРЕХОДИМО ДО ВКЛАДИНКИ ЗАВДАННЯ
# /////////////////////////TASKS///////////////////////////
# Створємо функію для оновлення рядків завданнь
def update3(rows):
    trv3.delete(*trv3.get_children())
    for i in rows:
        trv3.insert('','end', values=i)


# Створємо функію для пошуку завданнь
def search_task():
    qq = q3.get()
    query = "SELECT Id, Title, Description, Deadline, Results, SubjectId from Tasks " \
            "where Title like '%"+qq+"%' or Description like '%"+qq+"%' or Results like '%"+qq+"%'"
    cursor.execute(query)
    rows = cursor.fetchall()
    update3(rows)


# Створємо функію для очищення завданнь
def clear_task():
    query = "SELECT Id, Title, Description, Deadline, Results, SubjectId from Tasks"
    cursor.execute(query)
    rows = cursor.fetchall()
    update3(rows)
    q3.set('')

# Створємо функію для отримання рядків завданнь
def getrow_task(event):
    rowid = trv3.identify_row(event.y)
    item = trv3.item(trv3.focus())
    ta1.set(item['values'][0])
    ta2.set(item['values'][1])
    ta3.set(item['values'][2])
    ta4.set(item['values'][3])
    ta5.set(item['values'][4])
    ta6.set(item['values'][5])

# Створємо функію для оновлення завданнь
def update_task():
    id=ta1.get()
    title=ta2.get()
    description=ta3.get()
    deadline = ta4.get()
    results = ta5.get()
    subjectid = ta6.get()
    if messagebox.askyesno("Підтвердження дії", "Ви впевнені, що хочете оновити дані про дане завдання?"):
        try:
            query = "UPDATE Tasks SET Title=%s, Description=%s, Deadline=%s, Results=%s, SubjectId=%s where Id=%s"
            cursor.execute(query,(title,description,deadline,results,subjectid,id))
            mydb.commit()
            clear_task()
        except:
            messagebox.showerror('Err','err')
    else:
        return True

# Створємо функію для пошуку завданнь
def add_new_task():
    try:
        id=ta1.get()
        title=ta2.get()
        description=ta3.get()
        deadline = ta4.get()
        results = ta5.get()
        subjectid = ta6.get()
        query = "INSERT into Tasks(Id,Title,Description,Deadline,Results,SubjectId) values(%s, %s, %s, %s, %s, %s)"
        cursor.execute(query, (id,title,description,deadline,results,subjectid))
        mydb.commit()
        clear_task()
    except:
        messagebox.showerror('Помилка!', 'Невірно введені дані. Перевірте поле id.')

# Створємо функію для видаленння завданнь
def delete_task():
    task_id = ta1.get()
    if messagebox.askyesno("Підтвердження дії","Ви впевнені, що хочете видалити даний предмет?"):
        query = "DELETE FROM Tasks where Id="+task_id
        cursor.execute(query)
        mydb.commit()
        clear_task()
    else:
        return True

q3 = StringVar()
ta1 = StringVar()
ta2 = StringVar()
ta3 = StringVar()
ta4 = StringVar()
ta5 = StringVar()
ta6 = StringVar()


wrapper4 = LabelFrame(tab3, text = "Subjects List")  # Створємо блок списку завдань
wrapper5 = LabelFrame(tab3, text = "Search")  # Створємо блок пошуку завдань
wrapper6 = LabelFrame(tab3, text = "Subject")  # Створємо блок завдань

wrapper4.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок
wrapper5.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок
wrapper6.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо блок
# Створюємо таблиця для перегляду завдань
trv3 = Treeview(wrapper4, columns=(1, 2, 3, 4, 5, 6), show="headings", height="8")
trv3.pack()  # Пакуємо таблицю
# Називаємо стовбці таблиці
trv3.heading(1, text = "Subject ID")
trv3.heading(2, text = "Title")
trv3.heading(3, text = "Description")
trv3.heading(4, text = "Deadline")
trv3.heading(5, text = "Results")
trv3.heading(6, text = "SubjectId")
trv3.bind('<Double 1>', getrow_task)

# Створюємо запит до БД
query3="SELECT Id, Title,Description,Deadline,Results,SubjectId from Tasks"  # Запит до БД
cursor.execute(query3)  # Виконуємо запит
rows3 = cursor.fetchall()  # Збергіаємо інформацію
update3(rows3)  # Оновлюємо рядки

# Пошук
lbl=Label(wrapper5, text="Search")  # Створюємо напис пошуку
lbl.pack(side=tk.LEFT, padx=10)  # Пакуємо напис
ent=Entry(wrapper5, textvariable=q3)  # Створюємо поле для введення
ent.pack(side=tk.LEFT, padx=6)  # Пакууємо поле для введення
btn = Button(wrapper5, text="Search", command=search_task)  # Створюємо кнопку пошуку
btn.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку пошуку
cbtn = Button(wrapper5, text="Clear", command=clear_task)  # Створюємо кнопку очищення
cbtn.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку очищення


# Заповнення полів
lbl1 = Label(wrapper6, text="Task Id")  # Створюємо напис і даємо йому текст Task Id
lbl1.grid(row=0, column = 0, padx=5, pady=3)  # Пакуємо напис
ent1 = Entry(wrapper6, textvariable = ta1)  # Створюємо поле введення
ent1.grid(row=0, column=1,padx=5,pady=3)  # Пакуємо поле введення

lbl2 = Label(wrapper6, text="Title")  # Створюємо напис і даємо йому текст Title
lbl2.grid(row=1,column=0,padx=5,pady=3)  # Пакуємо напис
ent2 = Entry(wrapper6, textvariable = ta2)  # Створюємо поле введення
ent2.grid(row=1, column=1,padx=5,pady=3)  # Пакуємо поле введення

lbl3 = Label(wrapper6, text="Description")  # Створюємо напис і даємо йому текст Description
lbl3.grid(row=2,column=0,padx=5,pady=3)  # Пакуємо напис
ent3 = Entry(wrapper6, textvariable = ta3)  # Створюємо поле введення
ent3.grid(row=2, column=1,padx=5,pady=3)  # Пакуємо поле введення

lbl1 = Label(wrapper6, text="Deadline")  # Створюємо напис і даємо йому текст Deadline
lbl1.grid(row=0, column = 2, padx=5, pady=3)  # Пакуємо напис
ent1 = Entry(wrapper6, textvariable = ta4)  # Створюємо поле введення
ent1.grid(row=0, column=3,padx=5,pady=3)  # Пакуємо поле введення

lbl2 = Label(wrapper6, text="Results")  # Створюємо напис і даємо йому текст Results
lbl2.grid(row=1,column=2,padx=5,pady=3)  # Пакуємо напис
ent2 = Entry(wrapper6, textvariable = ta5)  # Створюємо поле введення
ent2.grid(row=1, column=3,padx=5,pady=3)  # Пакуємо поле введення

lbl3 = Label(wrapper6, text="SubjectId")  # Створюємо напис і даємо йому текст SubjectId
lbl3.grid(row=2,column=2,padx=5,pady=3)  # Пакуємо напис
ent3 = Entry(wrapper6, textvariable = ta6)  # Створюємо поле введення
ent3.grid(row=2, column=3,padx=5,pady=3)  # Пакуємо поле введення

up_btn_task=Button(wrapper6, text="Update", command=update_task)  # Створюємо кнопку оновлення завдання
add_btn_task = Button(wrapper6, text="Add New", command = add_new_task)  # Створюємо кнопку додавання завдання
delete_btn_task = Button(wrapper6, text = "Delete", command=delete_task)  # Створюємо кнопку видалення завдання

add_btn_task.grid(row = 6, column = 0, padx=5, pady=3)  # Пакуємо кнопку додавання завдання
up_btn_task.grid(row = 6, column = 1, padx=5, pady=3)  # Пакуємо кнопку оновлення завдання
delete_btn_task.grid(row = 6, column = 2, padx=5, pady=3)  # Пакуємо кнопку видалення завдання


# ПЕРЕХОДИМО ДО РОЗДІЛУ МІГРАЦІЙ
# Створюємо блок для міграції з БД 1 в БД 2
wrapper7 = LabelFrame(tab4, text = "Зробити міграцію з БД1 в БД 2")
# Створюємо блок для міграції з БД 2 в БД 3
wrapper8 = LabelFrame(tab4, text = 'Зробити міграцію з БД2 в БД 3 за SQL виразом: "SELECT * from Teachers where id between 2 and 4"')
wrapper7.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо перший блок
wrapper8.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо другий блок


# Створюємо функцію для міграції з БД 1 В БД 2
def migration_post_lite():
    result = migration_postgres_to_sqlite()  # Виконуємо і записуємо результат
    label = Label(wrapper7, text=result)  # Створюємо напис з результатом
    label.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо його


# Створюємо функцію для міграції з БД 2 В БД 3
def migration_lite_mysql():
    result = migration_sqlite_to_mysql()  # Виконуємо і записуємо результат
    label = Label(wrapper8, text=result)  # Створюємо напис з результатом
    label.pack(fill="both", expand="yes", padx=20, pady=10)  # Пакуємо його


# Створюємо кнопку для міграції з БД 1 В БД 2
button_make_migration_1 = Button(wrapper7, text='Виконати міграцію', command=migration_post_lite)  # Створюємо кнопку
button_make_migration_1.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку

# Створюємо кнопку для міграції з БД 2 В БД 3
button_make_migration_2 = Button(wrapper8, text='Виконати міграцію', command=migration_lite_mysql)# Створюємо кнопку
button_make_migration_2.pack(side=tk.LEFT, padx=6)  # Пакуємо кнопку

# Запускаємо наше вікно на постійний рендер
window.mainloop()