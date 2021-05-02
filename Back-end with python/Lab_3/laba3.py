from tkinter import *
from tkinter import scrolledtext
from tkinter import messagebox
import numpy as np
import re


def clicked():
    data = txt.get("1.0",END)
    print(data)
    sortResults(data)


def sortResults(data):
    try:
        # numbers_list = re.sub('-\W', ' ', data).split()
        # numbers_list = re.findall('-?[0-9]+[.]?[0-9]*', data)
        print(data)
        numbers_list = re.split('([;,.]?\s+)',data.strip())

        print(numbers_list)
        int_list = [float(x) for x in numbers_list]
        # neg_list = []
        # pos_list = []
        # for n in int_list:
        #     if n < 0:
        #         neg_list.append(n)
        #     else:
        #         pos_list.append(n)
        neg = ''
        pos = ''
        for number in int_list:
            if number < 0:
                if number == int(number):
                    neg += f'{int(np.round(number))}\n'
                else:
                    neg += f'{number}\n'
            else:
                if number == int(number):
                    pos += f'{int(np.round(number))}\n'
                else:
                    pos += f'{number}\n'

        txtpos.delete(1.0, END)
        txtneg.delete(1.0, END)
        txtpos.insert(INSERT, pos)
        txtneg.insert(INSERT, neg)

    except :
        messagebox.showinfo('Помилка!', 'Невірно введені дані. Спробуйте ще раз.')


window = Tk()
window.title("Lab#3 Kobets, Shchur")
w = 710
h = 305
window.geometry('{}x{}'.format(w, h))

top_frame = Frame(window, width=w, height=h, bg='tan1')
top_frame.pack(side=TOP)

bottom_frame = Frame(window, width=w, height=15)
bottom_frame.pack(side=TOP)


lbl = Label(top_frame, text="Введіть список чисел:", font=("Arial", 15), bg='tan1')
lbl.grid(column=0, row=0)

txt = scrolledtext.ScrolledText(top_frame, width=30, height=15)
txt.grid(column=0, row=1)

btn = Button(bottom_frame, text="Відобразити числа:",
             command=clicked,
             width=w,
             height=30,
             font=("Arial", 15),
             bg='tan1')
btn.pack(side=LEFT, fill="both", expand=True)


lbl1 = Label(top_frame, text="Позитивні:", font=("Arial", 15), bg='tan1')
lbl1.grid(column=2, row=0)

lbl2 = Label(top_frame, text="Негативні:", font=("Arial", 15), bg='tan1')
lbl2.grid(column=3, row=0)

txtpos = scrolledtext.ScrolledText(top_frame, width=25, height=10)
txtpos.grid(column=2, row=1)

txtneg = scrolledtext.ScrolledText(top_frame, width=25, height=10)
txtneg.grid(column=3, row=1)

window["bg"] = "white"
window.mainloop()
