class SNode:
    def __init__(self, item, next=None):
        self.item = item
        self.next = next


class SList:
    def __init__(self):
        self.head = None

    def insert_front(self, item):
        snode = SNode(item, self.head)
        self.head = snode
        return

    def print_list(self):
        p = self.head
        while p:
            if p.next != None:
                print(p.item, '->', end='')
            else:
                print(p.item)
            p = p.next

    def delete_front(self):
        self.head = self.head.next

    def delete_target(self, item):
        if self.head == None:
            return None
        elif self.head == item:
            self.delete_front()
        p = self.head
        while p.next.item != item:
            p = p.next
        q = p.next.next
        p.next = q
        return


s = SList()
s.insert_front("mango")
s.insert_front("tomato")
s.insert_front("orange")
s.insert_front("apple")
s.print_list()
s.delete_target("tomato")
s.print_list()