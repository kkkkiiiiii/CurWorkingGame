def sum(num):
    SUM = 0
    for i in range(1,num+1):
        SUM += i
    return  SUM

print(sum(10))
print()

def found_min(list):
    min = list[0]

    for i in range(0,len(list)-1):
        if list[i] <min:
            min = list[i]
    return min
def search(list,key):
    for i in range(0,len(list)-1):
        if key == list[i]:
            print("I found "+str(key))
            return
    print("Not found")
    return

mylist = [1,6,2,8,9,13]
min = found_min(mylist)
print(min)
search(mylist,2)
search(mylist,5)
print()

def get_indices(list,key):
    result =[]
    for i in range(0,len(list)):
        if key == list[i]:
            result.append(key)
    if len(result) == 0:
        print("Not found")
    else:
        print(result)
get_indices(mylist,13)
get_indices(mylist,7)

print()


def find_pattern(string,pattern):
    len1 = len(string)
    len2 = len(pattern)

    for i in range(len1 - len2):
        substring = string[i: i + len2]
        if substring == pattern:
            return i,pattern
    print(-1)


string = "how are you doing?"
pattern = "are"

found = find_pattern(string,pattern)
print(str(found[0])+"번째 인덱스 시작 : "+found[1])
print()

def selection_sort(mylist):
    result =[]
    for i in range(len(mylist)-1):
        max_value_index = 0
        for j in range(0,len(mylist)):
            if mylist[j] > mylist[max_value_index]:
                max_value_index = j
        result.append(mylist[max_value_index])
        mylist.pop(max_value_index)
    return result

list = [3,7,5,1,9,23,11,4,6]
print(list)
result = selection_sort(list)
print(result)

def closest_pair(tuple):
    min = float("inf")
    for i in range(len(tuple)-1):
        for j in range(i+1,len(tuple)):
            distance = cal_distance(tuple[i],tuple[j])
            if distance < min:
                min = distance
    return min
def cal_distance(p1,p2):
    x_dist = (p1[0]-p2[0])**2
    y_dist = (p1[1] - p2[1]) ** 2
    dist = (x_dist+y_dist)**0.5
    return dist

mytuple =[(1,1),(3,6),(3,-4),(-6,3),(-5,-5)]
print(closest_pair(mytuple))
print()

class Point(object):
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def __str__(self):
        return ("(" + str(self.x) + ", " + str(self.y) + ")")


class Points(object):
    def __init__(self, points):
        self.points = points
        self.count = len(self.points)

    def add_points(self, points):
        for point_tuple in points:
            point = Point(point_tuple[0], point_tuple[1])
            self.points.append(point)
            self.count += 1

    def add_point(self, point_tuple):
        point = Point(point_tuple[0], point_tuple[1])
        self.points.append(point)
        self.count += 1

    def cal_dist(self, p1, p2):
        x_dist = (p1.x - p2.x) ** 2
        y_dist = (p1.y - p2.y) ** 2
        dist = (x_dist + y_dist) ** 0.5
        return dist

    def closest_pair(self):
        min = float("inf")
        for i in range(self.count - 1):
            for j in range(i + 1, self.count):
                dist = self.cal_dist( \
                    self.points[i], \
                    self.points[j])
                if (dist < min):
                    min = dist
        return min

    def print_points(self):
        for point in self.points:
            print(point)


points = Points([])
points.add_points([(2, 3), (3, 5), (8, 10), (11, -1)])
points.print_points()
print(points.closest_pair())
points.add_point((4, 5))
points.print_points()
print(points.closest_pair())


class Item(object):
    def __init__(self, name, value, weight):
        self.name = name
        self.value = value
        self.weight = weight

    def __str__(self):
        return ("(" + str(self.name) + ", " + str(self.value) + ", " + str(self.weight) + ")")


class Knapsack(object):
    def __init__(self, names, values, weights, max_weight):
        self.items = []
        self.max_weight = max_weight
        self.max_value = 0
        self.opt_case = 0

        for i in range(len(names)):
            item = Item(names[i], values[i], weights[i])
            self.items.append(item)

    def printItems(self):
        for item in self.items:
            print(item)


names = ['0', '1', '2', '3', '4']
values = [10, 30, 20, 14, 23]
weights = [5, 8, 7, 3, 9]
max_weight = 20
knapsack = Knapsack(names, values, weights, max_weight)
knapsack.printItems()