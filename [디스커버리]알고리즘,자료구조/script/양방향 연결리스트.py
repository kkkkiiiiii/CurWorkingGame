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
        if (self.head != None):
            self.head.prev = dnode
        self.head = dnode

    def print_list(self):
        if self.head == None:
            print("empty")
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
        if target != None:
            second = target.next
            self.head = second
        if (self.head):
            self.head.prev = None
        return target

    def search(self, target):
        p = self.head
        while p:
            if target == p.item:
                return p
            p = p.next
        return None

    def delete_target(self, item):
        dnode = self.search(item)
        if (dnode == None):
            return dnode

        if (dnode == self.head):
            self.head = dnode.next
            if dnode.next != None:
                dnode.next.prev = None

        else:
            dnode.prev.next = dnode.next
            if dnode.next != None:
                dnode.next.prev = dnode.prev
        return dnode


d = DList()
d.insert_front('mango')
d.insert_front('orange')
d.insert_front('apple')
d.print_list()
dnode = d.delete_target('mango')
print(dnode.item)
d.print_list()

