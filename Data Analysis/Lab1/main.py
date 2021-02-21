import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import math
import statistics as st


def import_data(filename):
    dataframe = pd.read_csv(filename, delimiter=',', header=None)
    return dataframe


data = import_data('lab2.csv').values[0]


def variation_range(input_array):
    input_array.sort()
    simple_var_range = dict()
    for number in input_array:
        if simple_var_range.get(number, 0) != 0:
            simple_var_range[number] += 1
        else:
            simple_var_range[number] = 1
    freq_var_range = simple_var_range.copy()

    num_count = len(input_array)
    for number in freq_var_range.keys():
        freq_var_range[number] = np.round(freq_var_range[number] / num_count, 4)

    return list(simple_var_range.keys()),\
           list(simple_var_range.values()),\
           list(freq_var_range.keys()),\
           list(freq_var_range.values())


s1, s2, f1, f2 = variation_range(data)

print(f'{s1}\n{s2}\n{f1}\n{f2}\n')


def interval_variation_range(input_array, intervals_count):
    plt.style.use('seaborn-white')
    ax = plt.subplot()
    ax.set_xticks([45,50,55])
    min_num = np.min(input_array)
    max_num = np.max(input_array)
    interval_length = (max_num - min_num) // intervals_count
    print(interval_length)
    intervals = list()
    a = min_num
    while a < max_num:
        intervals.append(a)
        a += interval_length
    print(intervals)
    print(f'Групування: {list(range(min_num, max_num, interval_length))}')
    n = math.ceil((max_num - min_num)/interval_length)
    x, bins, y = plt.hist(input_array,bins=11,rwidth=3,edgecolor='black')
    print(bins)

    plt.show()


def numerical_characteristic(input_array):
    mode = st.mode(input_array)
    median = st.median(input_array)
    mean = st.mean(input_array)
    scope = np.max(input_array) - np.min(input_array)
    Q1 = np.percentile(data, 25, interpolation='midpoint')

    return mode, median


# print(numerical_characteristic(data)[1])
#
# interval_variation_range(data,9)

def function(input_data):

    numbers = variation_range(input_data)[2]
    freq_range = variation_range(input_data)[3]
    print(freq_range)
    sum = 0
    x = numbers
    y = list()
    for i in range(len(numbers)):
        print(f'iter: {i} - sum{sum}')
        sum += freq_range[i]
        y.append(sum)
    plt.plot(x,y)
    plt.show()

function(data)










