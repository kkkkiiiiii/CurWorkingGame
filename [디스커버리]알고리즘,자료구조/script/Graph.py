class DNode:
    def __init__(self, item, prev = None, next= None):
        self.item = item
        self.prev = prev
        self.next = next

class DList:
    def __init__(self):
        self.head = None

    def insert_back(self, item):
        p = self.head
        if p == None:
            self.head = DNode(item, None, self.head)
        else:
            while p.next:
                p = p.next
            p.next = DNode(item, None, None)
            p.next.prev = p

    def delete_front(self):
        target = self.head
        if target != None:
            self.head = self.head.next
            if self.head != None:
                self.head.prev = None
        return target

    def print_list(self):
        if self.head == None:
            print("empty")
        else:
            p = self.head
            while p:
                print(p.item, end = ' ')
                p = p.next
            print("\n")

class Queue:
    def __init__(self):
        self.queue = DList()
        self.count = 0

    def enqueue(self, item):
        self.queue.insert_back(item)
        self.count += 1

    def dequeue(self):
        if self.count > 0:
            self.count -= 1
            return self.queue.delete_front().item

    def size(self):
        return self.count

    def print_queue(self):
        self.queue.print_list()

class Graph:
    def __init__(self, graph):
        self.graph = graph
        self.vertex_count = len(self.graph)
        self.visited = [False] * self.vertex_count
        self.bfsQ = Queue()

    def bfs(self, start):
        for vertex in range(self.vertex_count):
            self.visited[vertex] = False
        self.bfsQ.enqueue(start)
        self.do_bfs()
        print()

    def do_bfs(self):
        while self.bfsQ.size() > 0:
            vertex = self.bfsQ.dequeue()
            self.visited[vertex] = True
            print(vertex, end = ' ')

            for neighbor in self.graph[vertex]:
                if not self.visited[neighbor]:
                    self.bfsQ.enqueue(neighbor)

if __name__ == '__main__':
    graph_adjlist = [[1, 4], [0, 2, 3], [1], [1], [0], []]
    graph = Graph(graph_adjlist)
    graph.bfs(0)
    graph.bfs(1)