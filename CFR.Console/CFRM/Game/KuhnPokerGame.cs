using System;
using System.Collections.Generic;
using CFR.Console.CFRM.Nodes;

namespace CFR.Console.CFRM.Game
{
    public class KuhnPokerGame : IGame
    {
        private readonly Random _random = new Random();

        public int[] GetCards()
        {
            int[] cards = { 1, 2, 3,4,5,6,7,8,9,10,11,12,13 };
            for (var c1 = cards.Length - 1; c1 > 0; c1--)
            {
                var c2 = _random.Next(c1 + 1);
                var tmp = cards[c1];
                cards[c1] = cards[c2];
                cards[c2] = tmp;
            }
            return cards;
        }
        public int GetGamePayoff(int[] cards, List<IGameAction> history)
        {
            var plays = history.Count;
            var player = plays % 2;
            var opponent = 1 - player;
            if (plays <= 1) throw new Exception("Game cannot be ended now.");
            var terminalPass = GetTerminalPass(history, plays);
            var doubleBet = GetDoubleBet(history, plays);
            var isPlayerCardHigher = cards[player] > cards[opponent];
            if (terminalPass)
                if (IsDoublePass(history, plays))
                    return isPlayerCardHigher ? 1 : -1;
                else
                    return 1;
            else if (doubleBet)
                return isPlayerCardHigher ? 2 : -2;
            return 0;
        }

        public bool IsPartyOver(int[] cards, List<IGameAction> history)
        {
            var plays = history.Count;
            if (plays <= 1) return false;
            var terminalPass = GetTerminalPass(history, plays);
            var doubleBet = GetDoubleBet(history, plays);
            if (terminalPass)
                return true;
            else if (doubleBet)
                return true;
            return false;
        }

        private static bool GetDoubleBet(List<IGameAction> history, int plays)
        {
            return history[plays - 1].Action == KuhnPokerConsts.Bet && history[plays - 2].Action == KuhnPokerConsts.Bet;
        }

        private static bool GetTerminalPass(List<IGameAction> history, int plays)
        {
            return history[plays - 1].Action == KuhnPokerConsts.Pass;
        }

        private static bool IsDoublePass(List<IGameAction> history, int plays)
        {
            return plays == 2 && history[plays - 1].Action == KuhnPokerConsts.Pass && history[plays - 2].Action == KuhnPokerConsts.Pass;
        }

        public IGameAction GetNthAction(int n)
        {
            return (n == 0 ?
                new KuhnPokerAction { Action = KuhnPokerConsts.Pass }
                : new KuhnPokerAction { Action = KuhnPokerConsts.Bet });
        }

        public IInfoSet GetInfoSet(int card, List<IGameAction> history)
        {
            return new KuhnPokerInfosetQuick(card, history);
        }

        public INode GetNode(IInfoSet infoset)
        {
            return new KuhnPokerNode(infoset);
        }

    }
}