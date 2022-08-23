import Stack

s = Stack.Stack()
result = True
parenthesis_dict = {
    '{': '}',
    '[': ']',
    '(': ')',
}
left_parenthesis = parenthesis_dict.keys()
right_parenthesis = parenthesis_dict.values()
string = input("input a string : ")
for char in string:
    if char in left_parenthesis:
        s.push(char)
    elif char in right_parenthesis:
        left = s.pop()
        if (left == None or parenthesis_dict[left] != char):
            print("wrong match", left, char)
            result = False
            break

if (s.pop() != None):
    print("wrong match")
    result = 0

if (result == 1):
    print('right match')