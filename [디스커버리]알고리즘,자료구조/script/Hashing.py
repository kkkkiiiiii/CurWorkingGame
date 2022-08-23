class LinearProbing:
    def __init__(self, size):
        self.tablesize = size
        self.table = [(None, None)] * size

    def hash(self, key):
        return key % self.tablesize

    def add(self, key, value):
        initial_position = self.hash(key)
        position = initial_position

        while True:
            (fkey, fvalue) = self.table[position]
            if fvalue == None:
                self.table[position] = (key, value)
                return True
            elif fkey == key:
                self.table[positon] = (key, value)
                return True
            position = (position + 1) % self.tablesize

            if position == initial_position:
                return False

    def remove2(self, key):  # 삭제 연산
        initial_position = self.hash(key)
        position = initial_position

        while True:
            (fkey, fvalue) = self.table[position]
            if fkey == key:
                self.table[position] = (-1, None)
                return fvalue
            elif fkey == None:
                return None

            position = (position + 1) % self.tablesize
            if position == initial_position:
                return None

    def print_table(self):
        i = 0
        for tuple in self.table:
            print(i, tuple)
            i += 1
        print()

    def search(self, key):
        initial_position = self.hash(key)
        position = initial_position

        while True:
            (fkey, fvalue) = self.table[position]
            if fkey == key:
                return fvalue
            elif fkey == None:
                return None
            position = (position + 1) % self.tablesize

            if position == initial_position:
                return None


t = LinearProbing(7)
t.add(7, 'grape')
t.add(1, 'apple')
t.add(2, 'banana')
t.print_table()
t.add(15, 'orange')
t.remove2(7)
t.print_table()
print('탐색 결과:')
print('1의 data = ', t.search(1))
print('15의 data = ', t.search(15))
print('17의 data = ', t.search(17))