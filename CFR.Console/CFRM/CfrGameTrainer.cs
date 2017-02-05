using System.Collections.Generic;
using CFR.Console.CFRM.Game;
using CFR.Console.CFRM.Nodes;

namespace CFR.Console.CFRM
{
    public class CfrGameTrainer
    {
        private readonly IGame _game;
        public Dictionary<IInfoSet, INode> NodeMap = new Dictionary<IInfoSet, INode>();

        public CfrGameTrainer(IGame game)
        {
            _game = game;
        }

        public void Train(int iterations)
        {
            double util = 0;
            for (var i = 0; i < iterations; i++)
            {
                var cards = _game.GetCards();
                util += Cfr(cards, new List<IGameAction>(), 1, 1);
            }
            System.Console.WriteLine("Average game value: " + util / iterations);
            foreach (var n in NodeMap.Values)
            {
                System.Console.WriteLine(n);
            }

        }


        private double Cfr(int[] cards, List<IGameAction> history, double p0, double p1)
        {
            var player = history.Count % 2;
            if (_game.IsPartyOver(cards, history))
                return _game.GetGamePayoff(cards, history);


            var infoSet = _game.GetInfoSet(cards[player], history);

            if (!NodeMap.ContainsKey(infoSet))
                NodeMap.Add(infoSet, _game.GetNode(infoSet));
            INode node = NodeMap[infoSet];

            double[] strategy = node.GetStrategy(player == 0 ? p0 : p1);
            var numberOfActions = strategy.Length;


            double[] util = new double[numberOfActions];
            double nodeUtil = 0;
            for (int a = 0; a < numberOfActions; a++)
            {
                var nextHistory = new List<IGameAction>(history);
                nextHistory.Add(_game.GetNthAction(a));
                util[a] = player == 0
                    ? -Cfr(cards, nextHistory, p0 * strategy[a], p1)
                    : -Cfr(cards, nextHistory, p0, p1 * strategy[a]);
                nodeUtil += strategy[a] * util[a];
            }
            for (int a = 0; a < numberOfActions; a++)
            {
                double regret = util[a] - nodeUtil;
                node.RegretSum[a] += (player == 0 ? p1 : p0) * regret;
            }
            return nodeUtil;
        }

    }






















}