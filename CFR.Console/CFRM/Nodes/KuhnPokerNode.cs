namespace CFR.Console.CFRM.Nodes
{
    public class KuhnPokerNode : AbstractNode
    {
        public override sealed IInfoSet InfoSet { get; set; }

        public KuhnPokerNode(IInfoSet infoSet)
        {
            InfoSet = infoSet;
            NbActions = KuhnPokerConsts.NbActions;
            Strategy = new double[NbActions];
            StrategySum = new double[NbActions];
            RegretSum = new double[NbActions];
        }
    }
}