using System;

namespace CFR.Console.MatchingRegret
{
    public class RpsTrainer
    {
        public static int Rock = 0;
        public static int Paper = 1;
        public static int Scissors = 2;
        private readonly int _nbActions;
        private double[] _regretSum;
        private double[] _strategy;
        private double[] _strategySum;
        private double[] _oppStrategy;
        private readonly Random _random;

        public RpsTrainer()
        {
            _nbActions = 3;
            _regretSum = new double[_nbActions];
            _strategy = new double[_nbActions];
            _strategySum = new double[_nbActions];
            _oppStrategy = new[] { 0.4, 0.2, 0.4 };
            _random = new Random();
        }

        public int GetAction(double[] strategy)
        {
            var r = _random.NextDouble();
            int a = 0;
            double cumulativeProbability = 0;
            while (a < _nbActions - 1)
            {
                cumulativeProbability += strategy[a];
                if (r < cumulativeProbability)
                    break;
                a++;
            }
            return a;
        }

        private double[] GetStrategy()
        {
            var normalizingSum = .0;
            for (var a = 0; a < _nbActions; a++)
            {
                _strategy[a] = _regretSum[a] > 0 ? _regretSum[a] : .0;
                normalizingSum += _strategy[a];
            }
            for (int a = 0; a < _nbActions; a++)
            {
                if (normalizingSum > 0)
                    _strategy[a] /= normalizingSum;
                else
                {
                    _strategy[a] = 1.0 / (double)_nbActions;
                }
                _strategySum[a] += _strategy[a];
            }
            return _strategy;
        }

        public void Train(int iterations)
        {
            double[] actionsUtility = new double[_nbActions];
            for (int i = 0; i < iterations; i++)
            {
                double[] strategy = GetStrategy();
                int myAction = GetAction(strategy);
                int oppAction = GetAction(_oppStrategy);

                actionsUtility[oppAction] = 0;
                actionsUtility[(_nbActions + oppAction + 1) % _nbActions] = 1;
                actionsUtility[(_nbActions + oppAction - 1) % _nbActions] = -1;

                for (int a = 0; a < _nbActions; a++)
                {
                    _regretSum[a] += actionsUtility[a] - actionsUtility[myAction];
                }
            }
        }

        public double[] GetAverageStrategy()
        {
            double[] averageStrategy = new double[_nbActions];
            double normalizingSum = .0;
            for (int a = 0; a < _nbActions; a++)
            {
                normalizingSum += _strategySum[a];
            }
            for (int a = 0; a < _nbActions; a++)
            {
                if (normalizingSum > 0)
                    averageStrategy[a] = _strategySum[a] / normalizingSum;
                else
                {
                    averageStrategy[a] = 1.0 / (double)_nbActions;
                }
            }
            return averageStrategy;
        }


    }
}