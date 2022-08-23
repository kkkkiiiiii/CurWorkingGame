class DNode:
    def __init__(self,item, prev =None, next =None):
        self.item = item
        self.prev = prev
        self.next = next

class DList:
    def __init__(self):
        self.head = None

    def insert_front(self,item):
        dnode= DNode(item,None,self.head)
        if (self.head != None):
            self.head.prev = dnode
        self.head = dnode

    def print_list(self):
        if self.head == None:
            print("empty")
        else:
            p = self.head
            while p:
                if p.next != None:
                    print(p.item,'<=>',end='')
                else:
                    print(p.item)
                p = p.next

    def delete_front(self):
        target = self.head
        if target != None:
            self.head = target.next
            first.next.prev