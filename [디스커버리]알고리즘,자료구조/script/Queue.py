class DNode:
    def __init__(self, item, pre=None, next=None):
        self.item = item
        self.pre = pre
        self.next = next


class DList:
    def __init__(self):
        self.head = None

    def insert_front(self, item):
        dnode = DNode(item, None, self.head)

        if self.head != None:
            self.head.pre = dnode
        self.head = dnode

    def insert_back(self, item):
        if self.head == None:
            self.insert_front(item)
            return

        dnode = DNode(item, None, None)

        last = self.head
        while last.next:
            last = last.next

        last.next = dnode
        dnode.pre = last

    def print_list(self):
        if self.head == None:
            print('empty')

        else:
            p = self.head
            while p:
                if p.next != None:
                    print(p.item, '<=>', end='')
                else:
                    print(p.item)
                p = p.next

    def delete_front(self):
        target = self.head
        if self.head != None:
            self.head = target.next
            if self.head:
                self.head.pre = None
        return target

    def search(self, target):
        p = self.head
        while p:
            if p.item == target:
                return p
            p = p.next
        return None

    def del_target(self, item):
        target = self.search(item)
        if target == None:
            return None

        if target == self.head:
            self.head = target.next
            if target.next != None:
                target.next.pre = None

        else:
            target.pre.next = target.next
            if target.next != None:
                target.next.pre = target.pre

        return target


import DList
class Queue:
    def __init__(self):
        self.queue = DList.DList()

    def enqueue(self, item):
        self.queue.insert_back(item)

    def dequeue(self):
        return self.queue.delete_front()

    def print_queue(self):
        self.queue.print_list()


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