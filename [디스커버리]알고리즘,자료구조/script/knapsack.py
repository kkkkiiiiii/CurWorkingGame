import random


class Knapsack():

    def __init__(self, names, values, weights, max_weight, epoch):  # num은 반복 횟수

        self.names = names

        self.values = values

        self.weights = weights

        self.epoch = epoch

    def findOptCase(self):  # 최적의 조합을 구할 함수

        max_value = 0

        chosen_items = []  # 최적의 조합에서 물건의 이름을 넣을 리스트

        for i in range(self.epoch):  # epoch만큼 반복

            sum_name = []

            sum_value = 0

            sum_weight = 0

            case = random.randint(1, 2 ** len(names))  # 경우의 수를 랜덤하게 생성

            for j in range(len(names)):  # 물건의 개수만큼 반복 비교

                target_bit_num = 2 ** j  # 비교할 비트 생성

                if (case & target_bit_num == target_bit_num):  # 조합에 포함되어 있는 경우

                    sum_value += self.values[j]  # 가치 더함

                    sum_weight += self.weights[j]  # 무게 더함

                    sum_name += self.names[j]  # 이름 더함

            if (sum_value > max_value and sum_weight <= max_weight) or (

                    sum_value == max_value and sum_weight < max_weight):
                # 총합 가치가 이전까지 총합 가치보다 높고 제한 무게보다 가볍거나 같을 때 또는 총합 가치가 이전까지 총합 가치랑은 같은데 더 가벼울 때

                max_value = sum_value  # 최대 가치를 최신화

                chosen_items = sum_name  # 최적의 조합으로 선정

        print('최대 가치는', max_value,

              '이고 그 때의 조합은', chosen_items,

              '이며 그 조합의 총 무게는', max_weight, '이다')


names = ['a', 'b', 'c', 'd', 'e']

values = [10, 30, 20, 14, 23]

weights = [5, 8, 3, 7, 9]

max_weight = 20

knapsack = Knapsack(names, values, weights, max_weight, 100)

knapsack.findOptCase()