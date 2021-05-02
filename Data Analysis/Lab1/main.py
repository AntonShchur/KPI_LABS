import matplotlib.pyplot as plt
import numpy as np
import math
import statistics as st
import prettytable as pt


def import_data(filename):
    return np.genfromtxt(filename, delimiter=',', dtype=int)


data = import_data('lab1.csv')


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
           list(freq_var_range.values())


# s1, s2, f1 = variation_range(data)


def histogram(input_array, intervals_count, draw_histogram=True):
    x, bins = interval_variation_range(input_array, intervals_count)
    fig, ax = plt.subplots()
    x, bins, y = plt.hist(input_array, bins=bins, edgecolor='black')
    ax.set_xticks(bins)
    ax.set_title('Гістограма частот')
    ax.set_xlabel('Інтервали')
    ax.set_ylabel('Кількість')
    if draw_histogram:
        plt.show()


def interval_variation_range(input_array, intervals_count, print_table=False):
    min_num = np.min(input_array)
    max_num = np.max(input_array)
    interval_length = (max_num - min_num) / intervals_count
    n = math.ceil((max_num - min_num) / interval_length)
    x, bins = np.histogram(input_array, bins=n)
    range_column = []
    number_column = x
    for i in range(1, len(bins)):
        range_column.append(f'{np.round(bins[i-1],2)}-{np.round(bins[i],2)}')
    table = pt.PrettyTable()
    table.add_column('Інтервали', range_column)
    table.add_column('К-сть входжень', number_column)
    if print_table:
        print(table)
    return x, bins


# histogram(data, 11, draw_histogram=True)


def numerical_characteristic(input_data):
    mode = st.mode(input_data)
    median = st.median(input_data)
    mean = st.mean(input_data)
    scope = np.max(input_data) - np.min(input_data)
    Q1 = np.percentile(input_data, 25, interpolation='midpoint')
    Q2 = np.percentile(input_data, 50, interpolation='midpoint')
    Q3 = np.percentile(input_data, 75, interpolation='midpoint')
    Q_scope = Q3 - Q1
    variance = st.variance(input_data)
    std = st.stdev(input_data)

    return mode, median, mean, scope, Q1, Q2, Q3, Q_scope, variance, std


# numerical_characteristic(data)


def ECD_function(input_data):

    x, bins = interval_variation_range(input_data, 8)
    x = x / len(input_data)

    plt.clf()
    ax = plt.subplots()[1]
    ax.set_xticks(bins)
    ax.set_xlabel('Інтервали')
    plt.title('Імперична функція розподілу для інтервального ряду')
    y = x.cumsum()
    for i in range(len(y)):
        plt.plot([bins[i], bins[i+1]],[y[i], y[i]], c="black")
    plt.show()


# ECD_function(data)



def moment_method(input_data):
    mean = st.mean(input_data)
    std = st.stdev(input_data)









