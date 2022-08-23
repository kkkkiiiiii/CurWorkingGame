import random


class Card:
    def __init__(self, kind, number):
        self.kind = kind
        self.number = number


class Player:
    def __init__(self):
        self.cards = []

    def printCards(self):
        for card in self.cards:
            print(card.kind + " " + str(card.number))


class Poker:
    def __init__(self, playerCnt, distCardCnt):
        self.distCardCnt = distCardCnt
        self.playerCnt = playerCnt
        self.cards = []
        self.players = []
        self.generateCards()
        self.shuffleCards()
        self.createPlayers()

    def generateCards(self):
        self.cards = []
        kinds = ['spade', 'heart', 'diamond', 'clover']

        for i in range(4):
            for j in range(13):
                card = Card(kinds[i], j + 1)
                self.cards.append(card)
        return

    def shuffleCards(self):
        random.shuffle(self.cards)

    def createPlayers(self):
        for j in range(self.playerCnt):
            player = Player()
            self.players.append(player)

    def printCards(self):
        for card in self.cards:
            print(card.kind + "" + str(card.number))

    def playCards(self):
        for i in range(self.distCardCnt):
            for j in range(self.playerCnt):
                card = self.cards.pop()
                self.players[j].cards.append(card)

    def printPlayerCards(self):
        player_num = 1
        for player in self.players:
            print("\nplayer", player_num, ":\n")
            player.printCards()
            player_num += 1

    def countFlush(self):
        count = 0
        for player in self.players:
            if (self.isFlush(player.cards) == True):
                count += 1
        return count

    def isFlush(self, targetCards):
        kindCnt = {'spade': 0, 'heart': 0, 'diamond': 0, 'clover': 0}
        for card in targetCards:
            kindCnt[card.kind] += 1

        for key in kindCnt.keys():
            if (kindCnt[key] >= 5):
                return True
        return False


playerCnt = 1
distCardCnt = 5
gameCnt = 10000
flushCnt = 0
for i in range(gameCnt):
    poker = Poker(playerCnt, distCardCnt)
    poker.playCards()
    flushCnt += poker.countFlush()

print(flushCnt)
print(flushCnt / (gameCnt * playerCnt))