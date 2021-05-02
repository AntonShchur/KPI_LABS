import psycopg2
import tkinter as tk
from tkinter import *
from tkinter import ttk
from tkinter import messagebox
from tkinter.ttk import Treeview


def update(rows):
    trv.delete(*trv.get_children())
    for i in rows:
        trv.insert('','end', values=i)

def search():
    q2 = q.get()
    query = "SELECT Id, Name, Surname, Patronymic from Teachers where Name like '%"+q2+"%' or Surname like '%"+q2+"%'"
    cursor.execute(query)
    rows = cursor.fetchall()
    update(rows)

def clear():
    query = "SELECT Id, Name, Surname, Patronymic from Teachers"
    cursor.execute(query)
    rows = cursor.fetchall()
    update(rows)
    q.set('')

def getrow(event):
    rowid = trv.identify_row(event.y)
    item = trv.item(trv.focus())
    t1.set(item['values'][0])
    t2.set(item['values'][1])
    t3.set(item['values'][2])
    t4.set(item['values'][3])

def update_teacher():
    id=t1.get()
    name=t2.get()
    surname=t3.get()
    patronymic=t4.get()
    if messagebox.askyesno("Підтвердження дії", "Ви впевнені, що хочете оновити дані про даного викладача?"):
        # try:
            query = "UPDATE Teachers SET Name=%s, Surname=%s, Patronymic=%s where Id=%s"
            cursor.execute(query,(name,surname,patronymic,id))
            clear()
        # except:
        #     messagebox.showerror('Err','err')
    else:
        return True

def add_new():
    try:
        id=t1.get()
        name=t2.get()
        surname=t3.get()
        patronymic=t4.get()
        query = "INSERT into Teachers(Id,Name,Surname,Patronymic) values(%s, %s, %s, %s)"
        cursor.execute(query, (id,name,surname,patronymic))
        clear()
    except:
        messagebox.showerror('Помилка!', 'Невірно введені дані. Перевірте поле id.')


def delete_teacher():
    teacher_id = t1.get()
    if messagebox.askyesno("Підтвердження дії","Ви впевнені, що хочете видалити даного вчителя?"):
        try:
            query = "DELETE FROM Teachers where Id="+teacher_id
            cursor.execute(query)
            clear()
        except:
            messagebox.showerror('Помилка!', 'Неможливо видалити даного вчителя, оскільки існують завдання та/або предмети, що пов"язані з даним викладачем.')
    else:
        return True

mydb = psycopg2.connect(
  database="Management",
  user="postgres",
  password="28731964",
  host="localhost",
  port="5432"
)
cursor = mydb.cursor()

window = Tk()
q = StringVar()
t1 = StringVar()
t2 = StringVar()
t3 = StringVar()
t4 = StringVar()

window.title("Kobets, Shchur - Lab#4")
window.geometry('800x600')
tab_control = ttk.Notebook(window)

tab1 = ttk.Frame(tab_control)
tab2 = ttk.Frame(tab_control)
tab3 = ttk.Frame(tab_control)

tab_control.add(tab1, text='Teachers')
tab_control.add(tab2, text='Subjects')
tab_control.add(tab3, text='Tasks')

tab_control.pack(expand=1, fill='both')

wrapper1 = LabelFrame(tab1, text = "Teachers List")
wrapper2 = LabelFrame(tab1, text = "Search")
wrapper3 = LabelFrame(tab1, text = "Teacher")

wrapper1.pack(fill="both", expand="yes", padx=20, pady=10)
wrapper2.pack(fill="both", expand="yes", padx=20, pady=10)
wrapper3.pack(fill="both", expand="yes", padx=20, pady=10)

trv = Treeview(wrapper1, columns=(1, 2, 3, 4), show="headings", height="6")
trv.pack()

trv.heading(1, text = "Teacher ID")
trv.heading(2, text = "Name")
trv.heading(3, text = "Surname")
trv.heading(4, text = "Patronymic")

trv.bind('<Double 1>', getrow)


query="SELECT Id, Name, Surname, Patronymic from Teachers"
cursor.execute(query)
rows = cursor.fetchall()
update(rows)

# Пошук
lbl=Label(wrapper2, text="Search")
lbl.pack(side=tk.LEFT, padx=10)
ent=Entry(wrapper2, textvariable=q)
ent.pack(side=tk.LEFT, padx=6)
btn = Button(wrapper2, text="Search", command=search)
btn.pack(side=tk.LEFT, padx=6)
cbtn = Button(wrapper2, text="Clear", command=clear)
cbtn.pack(side=tk.LEFT, padx=6)


# Заповнення полів
lbl1 = Label(wrapper3, text="Teacher Id")
lbl1.grid(row=0, column = 0, padx=5, pady=3)
ent1 = Entry(wrapper3, textvariable = t1)
ent1.grid(row=0, column=1,padx=5,pady=3)

lbl2 = Label(wrapper3, text="Name")
lbl2.grid(row=1,column=0,padx=5,pady=3)
ent2 = Entry(wrapper3, textvariable = t2)
ent2.grid(row=1, column=1,padx=5,pady=3)

lbl3 = Label(wrapper3, text="Surname")
lbl3.grid(row=2,column=0,padx=5,pady=3)
ent3 = Entry(wrapper3, textvariable = t3)
ent3.grid(row=2, column=1,padx=5,pady=3)

lbl4 = Label(wrapper3, text="Patronymic")
lbl4.grid(row=3,column=0,padx=5,pady=3)
ent4 = Entry(wrapper3, textvariable = t4)
ent4.grid(row=3, column=1,padx=5,pady=3)

up_btn=Button(wrapper3, text="Update", command=update_teacher)
add_btn = Button(wrapper3, text="Add New", command = add_new)
delete_btn = Button(wrapper3, text = "Delete", command=delete_teacher)

add_btn.grid(row = 4, column = 0, padx=5, pady=3)
up_btn.grid(row = 4, column = 1, padx=5, pady=3)
delete_btn.grid(row = 4, column = 2, padx=5, pady=3)

window.mainloop()