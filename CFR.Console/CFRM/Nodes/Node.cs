using System;
using System.Linq;

namespace CFR.Console.CFRM.Nodes
{
    public interface INode
    {
        IInfoSet InfoSet { get; set; }
        double[] GetStrategy(double realisationWeight);
        double[] GetAverageStrategy();
        String ToString();
        double[] RegretSum { get; set; }
    }


    public abstract class AbstractNode : INode
    {
        public abstract IInfoSet InfoSet { get; set; }
        public double[] RegretSum { get; set; }
        protected int NbActions;
        protected double[] Strategy;
        protected double[] StrategySum;

        public double[] GetStrategy(double realisationWeight)
        {
            var normalizingSum = .0;
            for (var a = 0; a < NbActions; a++)
            {
                Strategy[a] = RegretSum[a] > 0 ? RegretSum[a] : 0;
                normalizingSum += Strategy[a];
            }
            for (var a = 0; a < NbActions; a++)
            {
                if (normalizingSum > 0)
                    Strategy[a] /= normalizingSum;
                else
                    Strategy[a] = 1.0 / NbActions;
                StrategySum[a] += realisationWeight * Strategy[a];
            }
            return Strategy;
        }

        public double[] GetAverageStrategy()
        {
            var avgStrategy = new double[NbActions];
            double normalizingSum = 0;
            for (var a = 0; a < NbActions; a++)
                normalizingSum += StrategySum[a];
            for (var a = 0; a < NbActions; a++)
                if (normalizingSum > 0)
                    avgStrategy[a] = StrategySum[a] / normalizingSum;
                else
                    avgStrategy[a] = 1.0 / NbActions;
            return avgStrategy;
        }

        public override String ToString()
        {
            var averageStrategy = GetAverageStrategy();
            return String.Format("{0}: {1}", InfoSet, String.Join(" ", averageStrategy.ToList()));
        }
    }
}