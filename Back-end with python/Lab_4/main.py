import psycopg2
from tkinter import *
import tkinter as ttk

con = psycopg2.connect(
  database="Management",
  user="postgres",
  password="28731964",
  host="localhost",
  port="5432"
)

from tkinter import *
from tkinter import ttk

window = Tk()
window.title("Добро пожаловать в приложение PythonRu")
window.geometry('400x250')
tab_control = ttk.Notebook(window)
tab1 = ttk.Frame(tab_control)
tab2 = ttk.Frame(tab_control)
tab_control.add(tab1, text='Первая')
tab_control.add(tab2, text='Вторая')
lbl1 = Label(tab1, text='Вкладка 1')
lbl1.grid(column=0, row=0)
lbl2 = Label(tab2, text='Вкладка 2')
lbl2.grid(column=0, row=0)
tab_control.pack(expand=1, fill='both')
window.mainloop()


cursor = con.cursor()
cursor.execute('select * from Teachers')

teachers = cursor.fetchall()
for t in teachers:
  print(f'ID = {t[0]}\n'
        f'Name = {t[1].strip()}')
con.commit()
con.close()
