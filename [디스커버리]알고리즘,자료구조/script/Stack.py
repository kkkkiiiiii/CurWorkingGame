import DList


class Stack:
    def __init__(self):
        self.stack = DList.DList()

    def push(self, item):
        self.stack.insert_front(item)

    def pop(self):
        node = self.stack.delete_front()
        if (node == None):
            return None
        return node.item

    def print_stack(self):  # 리스트 출력
        self.stack.print_list()


if __name__ == '__main__':
    s = Stack()

    s.push('mango')
    s.push('apple')
    s.push('orange')
    s.print_stack()

    for i in range(4):
        item = s.pop()
        if (item):
            print("The popped item is ", item)
            print("The current stack : ", end='')
            s.print_stack()
        else:
            print("The stack is empty")