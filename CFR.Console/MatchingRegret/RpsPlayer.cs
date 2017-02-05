using System;

namespace CFR.Console.MatchingRegret
{

    public abstract class Player
    {
        protected double[] RegretSum;
        protected double[] Strategy;
        protected double[] StrategySum;
        private readonly Random _random;
        protected int _nbAction;


        public Player()
        {
            _random = new Random();
        }

        public int GetAction(double[] strategy)
        {
            var r = _random.NextDouble();
            int a = 0;
            double cumulativeProbability = 0;
            while (a < _nbAction - 1)
            {
                cumulativeProbability += strategy[a];
                if (r < cumulativeProbability)
                    break;
                a++;
            }
            return a;
        }

        public double[] GetStrategy()
        {
            var normalizingSum = .0;
            for (var a = 0; a < _nbAction; a++)
            {
                Strategy[a] = RegretSum[a] > 0 ? RegretSum[a] : .0;
                normalizingSum += Strategy[a];
            }
            for (int a = 0; a < _nbAction; a++)
            {
                if (normalizingSum > 0)
                    Strategy[a] /= normalizingSum;
                else
                {
                    Strategy[a] = 1.0 / (double)_nbAction;
                }
                StrategySum[a] += Strategy[a];
            }
            return Strategy;
        }

        public void TrainUnitary(int playerAction, int opponentAction)
        {
            var actionsUtility = GetUtility(opponentAction);
            for (int a = 0; a < _nbAction; a++)
            {
                RegretSum[a] += actionsUtility[a] - actionsUtility[playerAction];
            }
        }

        protected abstract double[] GetUtility(int opponentAction);

        public double[] GetAverageStrategy()
        {
            double[] averageStrategy = new double[_nbAction];
            double normalizingSum = .0;
            for (int a = 0; a < _nbAction; a++)
            {
                normalizingSum += StrategySum[a];
            }
            for (int a = 0; a < _nbAction; a++)
            {
                if (normalizingSum > 0)
                    averageStrategy[a] = StrategySum[a] / normalizingSum;
                else
                {
                    averageStrategy[a] = 1.0 / (double)_nbAction;
                }
            }
            return averageStrategy;
        }

    }





    public class RpsPlayer : Player
    {
        public RpsPlayer() : base()
        {
            _nbAction = Constants.RpsNbActions;
            RegretSum = new double[Constants.RpsNbActions];
            Strategy = new double[Constants.RpsNbActions];
            StrategySum = new double[Constants.RpsNbActions];

        }

        protected override double[] GetUtility(int opponentAction)
        {
            double[] actionsUtility = new double[_nbAction];
            actionsUtility[opponentAction] = 0;
            actionsUtility[(Constants.RpsNbActions + opponentAction + 1) % Constants.RpsNbActions] = 1;
            actionsUtility[(Constants.RpsNbActions + opponentAction - 1) % Constants.RpsNbActions] = -1;
            return actionsUtility;
        }
    }


    public class ColonelBlottoPlayer : Player
    {

        public ColonelBlottoPlayer() : base()
        {
            _nbAction = Constants.ColonelBlottoNbActions;
            RegretSum = new double[Constants.ColonelBlottoNbActions];
            Strategy = new double[Constants.ColonelBlottoNbActions];
            StrategySum = new double[Constants.ColonelBlottoNbActions];
            InitializeActions();
        }


        private void InitializeActions()
        {
            _actions = new int[56][];
            _actions[0] = new int[] { 0, 0, 0 };
            _actions[1] = new int[] { 0, 0, 1 };
            _actions[2] = new int[] { 0, 1, 0 };
            _actions[3] = new int[] { 1, 0, 0 };

            _actions[4] = new int[] { 0, 0, 2 };
            _actions[5] = new int[] { 0, 2, 0 };
            _actions[6] = new int[] { 2, 0, 0 };
            _actions[7] = new int[] { 0, 1, 1 };
            _actions[8] = new int[] { 1, 1, 0 };
            _actions[9] = new int[] { 1, 0, 1 };


            _actions[10] = new int[] { 0, 0, 3 };
            _actions[11] = new int[] { 0, 3, 0 };
            _actions[12] = new int[] { 3, 0, 0 };
            _actions[13] = new int[] { 1, 1, 1 };
            _actions[14] = new int[] { 2, 1, 0 };
            _actions[15] = new int[] { 2, 0, 1 };
            _actions[16] = new int[] { 0, 2, 1 };
            _actions[17] = new int[] { 1, 2, 0 };
            _actions[18] = new int[] { 0, 1, 2 };
            _actions[19] = new int[] { 1, 0, 2 };


            _actions[20] = new int[] { 0, 0, 4 };
            _actions[21] = new int[] { 0, 4, 0 };
            _actions[22] = new int[] { 4, 0, 0 };
            _actions[23] = new int[] { 3, 0, 1 };
            _actions[24] = new int[] { 3, 1, 0 };
            _actions[25] = new int[] { 0, 3, 1 };
            _actions[26] = new int[] { 1, 3, 0 };
            _actions[27] = new int[] { 0, 1, 3 };
            _actions[28] = new int[] { 1, 0, 3 };

            _actions[29] = new int[] { 2, 2, 0 };
            _actions[30] = new int[] { 2, 0, 2 };
            _actions[31] = new int[] { 0, 2, 2 };

            _actions[32] = new int[] { 2, 1, 1 };
            _actions[33] = new int[] { 1, 2, 1 };
            _actions[34] = new int[] { 1, 1, 2 };



            _actions[35] = new int[] { 5, 0, 0 };
            _actions[36] = new int[] { 0, 5, 0 };
            _actions[37] = new int[] { 0, 0, 5 };

            _actions[38] = new int[] { 4, 1, 0 };
            _actions[39] = new int[] { 4, 0, 1 };
            _actions[40] = new int[] { 0, 4, 1 };
            _actions[41] = new int[] { 1, 4, 0 };
            _actions[42] = new int[] { 0, 1, 4 };
            _actions[43] = new int[] { 1, 0, 4 };

            _actions[44] = new int[] { 3, 2, 0 };
            _actions[45] = new int[] { 3, 0, 2 };
            _actions[46] = new int[] { 0, 3, 2 };
            _actions[47] = new int[] { 2, 3, 0 };
            _actions[48] = new int[] { 0, 2, 3 };
            _actions[49] = new int[] { 2, 0, 3 };

            _actions[50] = new int[] { 3, 1, 1 };
            _actions[51] = new int[] { 1, 3, 1 };
            _actions[52] = new int[] { 1, 1, 3 };
            _actions[53] = new int[] { 2, 2, 1 };
            _actions[54] = new int[] { 2, 1, 2 };
            _actions[55] = new int[] { 1, 2, 2 };
        }


        private int[][] _actions;





        private int WhoWins(int[] myStrategy, int[] opponentStrategy)
        {
            if (myStrategy.Length != opponentStrategy.Length) throw new Exception("Invalid size of action!");
            int myPoints = 0, opponentPoints = 0;
            for (int i = 0; i < myStrategy.Length; i++)
            {
                if (myStrategy[i] > opponentStrategy[i]) myPoints++;
                if (myStrategy[i] < opponentStrategy[i]) opponentPoints++;
            }
            if (myPoints > opponentPoints) return 1;
            if (opponentPoints > myPoints) return -1;
            return 0;
        }




        protected override double[] GetUtility(int opponentAction)
        {
            double[] actionsUtility = new double[_nbAction];
            for (int i = 0; i < _nbAction; i++)
            {
                actionsUtility[i] = WhoWins(_actions[i], _actions[opponentAction]);
            }
            return actionsUtility;
        }

    }
}


