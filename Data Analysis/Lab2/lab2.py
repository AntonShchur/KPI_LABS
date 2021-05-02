import random
import math
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import statistics as st
from scipy.optimize import minimize

data = np.genfromtxt('lab1.csv', delimiter=',')
print(st.mean(data))
print(st.stdev(data))

def random_numbers(input_data, k):
    indexes = set()
    while len(set(indexes)) != k:
        indexes.add(random.randint(0, len(input_data) - 1))
    return input_data[list(indexes)]

def MME(input_data):
    mu = st.mean(input_data)
    sigma = st.stdev(input_data)
    return mu, sigma

def lik(parameters):
    mu = parameters[0]
    sigma = parameters[1]
    n = len(x)
#     L     = n/2.0 * np.log(2 * np.pi) + n/2.0 * math.log(sigma**2 ) + 1/(2*sigma**2) * sum([(x_ - mu)**2 for x_ in x ])
    L = (n/2 * np.log(2 * np.pi) + len(x)/2 * np.log(sigma ** 2)) + 1 / (2 * sigma ** 2) * sum([(x_ - mu)**2 for x_ in x ])
    return L

def MLE(input_data):
    global x
    x = input_data

    lik_model = minimize(lik, np.array([5,5]), method='SLSQP')

    mu = lik_model['x'][0]
    sigma = lik_model['x'][1]
    return mu, sigma


# l1 = list()
# l2 = list()
# for i in range(10,300,10):
#     print(i)
#     data = random_numbers(data,30)
#     mu1, sigma1 = MME(data)
#     l1.append(np.abs(mu1 - mu0))
#     l2.append(np.abs(sigma1 - sigma0))
#     mu2, sigma2 = MLE(data)
# l1.append(np.abs(mu1 - mu0))
# l2.append(np.abs(sigma1 - sigma0))


mu0 = st.mean(data)
sigma0 = st.stdev(data)

mu1_avg = list()
sigma1_avg = list()
mu2_avg = list()
sigma2_avg = list()

# for i in range(10,250,10):
#     for j in range(10):
#         print(i)
#         data = random_numbers(data, i)
#         mu1, sigma1 = MME(data)
#         mu2, sigma2 = MLE(data)
#
#         mu1_avg.append(mu1)
#         sigma1_avg.append(sigma1)
#         mu2_avg.append(mu2)
#         sigma2_avg.append(sigma2)
#
#     print(f'MME - {np.mean(mu1_avg)},{np.mean(sigma1_avg)}')
#     print(f'MLE - {np.mean(mu2_avg)},{np.mean(sigma2_avg)}')


for i in range(10):
    data = random_numbers(data, 6)
    print(data)

    mu1, sigma1 = MME(data)
    mu2, sigma2 = MLE(data)


    mu1_avg.append(mu1)
    sigma1_avg.append(sigma1)
    mu2_avg.append(mu2)
    sigma2_avg.append(sigma2)

print(f'MME - {np.mean(mu1_avg)},{np.mean(sigma1_avg)}')
print(f'MLE - {np.mean(mu2_avg)},{np.mean(sigma2_avg)}')