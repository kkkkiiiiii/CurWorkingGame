# import Item
class DNode:
    def __init__(self, item, prev=None, next=None):
        self.item = item
        self.prev = prev
        self.next = next


class DList:
    def __init__(self):
        self.head = None

    def insert_front(self, item):
        dnode = DNode(item, None, self.head)

        first = self.head
        if (first != None):
            first.prev = dnode

        self.head = dnode

    def insert_back(self, item):
        if (self.head == None):
            self.insert_front(item)
            return

        dnode = DNode(item, None, None)

        last = self.head
        while last.next:
            last = last.next

        last.next = dnode
        dnode.prev = last

    def insert_back2(self, item):
        dnode = DNode(item, None, None)

        if (self.head == None):
            self.head = dnode
            return

        last = self.head
        while last.next:
            last = last.next

        last.next = dnode
        dnode.prev = last

    def print_list(self):  # 리스트 출력
        if self.head == None:
            print('리스트 비어있음')
        else:
            p = self.head
            while p:
                if (p.next != None):
                    print(p.item, ' <=> ', end='')
                else:
                    print(p.item)
                p = p.next

    def delete_front(self):
        target = self.head
        if (target != None):
            second = target.next
            self.head = second
            if (second):
                second.prev = None
        return target

    def search(self, target):  # target 탐색
        p = self.head
        while p:
            if target == p.item:
                break

            p = p.next
        return p

    def delete_back(self):
        if (self.head == None):
            return None

        target = self.head
        while target.next:
            target = target.next

        if (target.prev != None):
            target.prev.next = None
        else:
            self.head = None

        return target

    def delete_target(self, item):
        dnode = self.search(item)
        if (dnode == None):
            return None

        if (dnode == self.head):
            new_first = dnode.next
            self.head = new_first
            if (new_first != None):
                new_first.prev = None

        else:
            target_next = dnode.next
            target_prev = dnode.prev

            target_prev.next = target_next
            if (target_next != None):
                target_next.prev = target_prev

        return dnode

    def delete_target2(self, target):  # target 탐색
        p = self.head
        while p:
            if (target == p.item):
                target_prev = p.prev
                target_next = p.next

                if (target_prev != None):
                    target_prev.next = target_next
                else:
                    self.head = target_next
                if (target_next != None):
                    target_next.prev = target_prev

            p = p.next
        return p


if __name__ == '__main__':
    d = DList()
    d.insert_front('mango')
    d.insert_front('orange')
    d.insert_front('apple')
    d.insert_front('banana')
    d.print_list()

    dnode = d.delete_target('orange')
    d.print_list()
    dnode = d.delete_target('banana')
    d.print_list()

    dnode = d.delete_target('mango')
    d.print_list()

    dnode = d.delete_target('kiwi')
    d.print_list()