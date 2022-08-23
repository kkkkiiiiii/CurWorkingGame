def min_coins_greedy(coins, left):
    chosen = []
    for coin in coins:
        count = left // coin
        chosen.append(count)
        left -= count * coin
    return chosen

coins = [500,100,10,1]
changes = 1534
print("잔돈: ",changes)
print("동전 종류 ",coins)
print("동전 개수 ",min_coins_greedy(coins,changes))