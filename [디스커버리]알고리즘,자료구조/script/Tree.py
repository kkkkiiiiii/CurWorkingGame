class BSTNode:
    def __init__(self, key, value, level=-1, left=None, right=None):
        self.key = key
        self.value = value
        self.left = left
        self.right = right
        self.level = level
class BST:
    def __init__(self):
        self.root = None
    def insert(self, key, value):   # 새로운 노드를 만들어 key와 value를 집어넣는 기능
        if self.root == None:   # 트리가 비워져 있는 경우
            node = BSTNode(key, value, 0)
            self.root = node
            return True
        target = self.root
        level = 1
        while target:   # 더 이상 노드가 없을 때까지 탐색
            if target.key == key:   # 삽입하려는 key가 존재하는 노드가 이미 있으면
                node = BSTNode(key, value, level)
                self.root = node
                return False    # 에러 처리
            elif target.key > key:  # 왼쪽으로 진행
                if target.left == None: # 끝에 다다르면면
                    node = BSTNode(key, value, level)
                    target.left = node  # 새 노드 추가
                    return True
                target = target.left
            elif target.key < key:  # 오른쪽으로 진행행
               if target.right == None: # 끝에 다다르면
                    node = BSTNode(key, value, level)
                    target.right = node # 새 노드 추가
                    return True
               target = target.right
            level += 1
    def print(self):
        self.doPrint(self.root)
        print('')
    def doPrint(self, node):    # 중위순회
        if node != None:    # 끝에 다다를 때까지 계속 재귀
            self.doPrint(node.left) # 왼쪽 자식노드로 이동(재귀로 인해 왼쪽 끝까지 이동)
            print(node.key, node.value, end=' ') # 왼쪽 끝에 다다르면 그 노드의 key, value를 print
            self.doPrint(node.right)    # 오른쪽 자식노드로 이동
    def get_min(self):  # 최솟값 구하기 => 왼쪽으로 계속 가기
        target = self.root
        while target and target.left:   # root와 root의 왼쪽 child가 존재할 경우
            target = target.left    # 왼쪽으로 이동 => 왼쪽 child가 존재 하지 않을 때까지 이동
        return target.key, target.value   # 맨 왼쪽 노드 반환
    def get_max(self):  # 최댓값 구하기 => 오른쪽으로 계속 가기
        target = self.root
        while target and target.right:  #root와 root의 오른쪽 child가 존재할 경우
            target = target.right   # 오른쪽으로 이동 => 오른쪽 child가 존재 하지 않을 때까지 이동
        return target.key, target.value   # 맨 오른쪽 노드 반환
    def search(self, key):
        target = self.root
        while target:   # tree 끝에 다다를 때까지 실행
            if target.key == key:
                return target
            elif target.key > key:
                target = target.left
            elif target.key < key:
                target = target.right
myBST = BST()
myBST.insert(5, 'a')
myBST.insert(7, 'b')
myBST.insert(3, 'c')
myBST.insert(1, 'd')
myBST.insert(9, 'e')
myBST.insert(15, 'f')
myBST.print()
myBST.insert(8, 'g')
myBST.print()
min_key, min_value = myBST.get_min()
max_key, max_value = myBST.get_max()
print(f'min_key : {min_key}, '
      f'min_value : {min_value}, '
      f'max_key : {max_key}, '
      f'max_value : {max_value}')
print('value :', myBST.search(9).value)