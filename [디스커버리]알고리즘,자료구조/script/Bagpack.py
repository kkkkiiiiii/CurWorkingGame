# 가치가 높은 것들부터 담는 경우
import random
class Knapsack():
    def __init__(self, names, values, weights, capacity):    # num은 반복 횟수
        self.names = names
        self.values = values
        self.weights = weights
        self.items = []
        self.max_weight = capacity
        for i in range(len(self.names)):
            self.items.append((self.names[i], self.values[i], self.weights[i]))
            self.items.sort(reverse=True, key=lambda x:x[1])
    def findOptCase(self):  # 최적의 조합을 구할 함수
        max_value = 0
        max_weight = 0
        case = []
        for i in range(len(self.names)):
            # capacity와 조합의 무게의 차가 제일 무게가 적은 물건보다 작을 때 => 가득 찼을 때
            if self.max_weight - max_weight < self.items[len(self.names)-1][2]:
                break
            max_value += self.items[i][1]
            max_weight += self.items[i][2]
            case.append(self.items[i][0])
        print('최대 가치는', max_value,
              '이고 그 때의 조합은', case,
              '이며 그 조합의 총 무게는', max_weight, '이다')
names = ['a', 'b', 'c', 'd', 'e']
values = [10, 30, 20, 14, 23]
weights = [5, 8, 3, 7, 9]
capacity = 20
knapsack = Knapsack(names, values, weights, capacity)
knapsack.findOptCase()