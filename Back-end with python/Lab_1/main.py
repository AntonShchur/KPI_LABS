import numpy as np
import matplotlib.pyplot as plt
import prettytable as pt



while True:
    print('Лаб№1')
    try:
        left_bound = float(input('Введіть ліву сторону інтрервалу\n'))
        right_bound = float(input('Введіть праву сторону інтрервалу\n'))
        step = float(input('Введіть крок'))
        if left_bound < right_bound:
            break
        else:
            print('Ліва границя повинна бути менша за праву')
    except ValueError:
        print('Введені дані некоректні')


def function(x):
    return x / np.cos(x)


num = int((right_bound - left_bound) / step)

x = np.arange(left_bound, right_bound, 0.001)
print(x)
y = function(x)
axes = plt.subplot()
axes.spines['left'].set_position('center')
axes.spines['bottom'].set_position('center')
axes.spines['top'].set_visible(False)
axes.spines['right'].set_visible(False)
plt.title('Графік функції У = X / COS(X)')
plt.xlabel('Х - незалежна величина')
axes.xaxis.set_label_coords(0.5, -0.03)
plt.ylabel('Y - залежна величина')
axes.yaxis.set_label_coords(-0.05, 0.5)

plt.axis([left_bound * 2, right_bound * 2, left_bound * 2, right_bound * 2])
plt.grid(True)
plt.plot(x, y, 'k--')
plt.scatter(0, 0, c='red')

plt.show()

table = pt.PrettyTable()
x = np.arange(left_bound, right_bound+1,step)
y = function(x)
print('Таблиця значеннь')
table.add_column('X', np.round(x, 2))
table.add_column('Y', np.round(y, 2))
print(table)








