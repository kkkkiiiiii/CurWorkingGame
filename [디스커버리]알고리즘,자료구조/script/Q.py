import DList


class Queue:
    def __init__(self):
        self.queue = DList.DList()
        self.count = 0

    def enqueue(self, item):
        self.queue.insert_back(item)
        self.count += 1

    def dequeue(self):
        if (self.count > 0):
            self.count -= 1
            dnode = self.queue.delete_front()
            return dnode.item
        return None

    def size(self):
        return self.count

    def print_queue(self):  # 리스트 출력
        self.queue.print_list()


if __name__ == '__main__':
    q = Queue()

    q.enqueue('mango')
    q.enqueue('apple')
    q.enqueue('orange')
    q.print_queue()

    q.dequeue()
    q.print_queue()
    q.dequeue()
    q.print_queue()
    q.dequeue()
    q.print_queue()
    q.dequeue()
    q.print_queue()