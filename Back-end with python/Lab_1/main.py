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

x = np.linspace(start=left_bound, stop=right_bound, num=1000)
y = function(x)
axes = plt.gca()
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
plt.plot(x, y)
plt.scatter(0, 0, c='red')

plt.show()






