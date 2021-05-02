import numpy as np

import matplotlib.pyplot as plt

x = np.linspace(0, 10, 100)[::2]
y = x ** 2
# z = np.linspace(1.1, 10, 100)
# k = z ** 2
plt.plot(x,y)
# plt.plot(z,k)
plt.show()
