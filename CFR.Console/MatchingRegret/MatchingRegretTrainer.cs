namespace CFR.Console.MatchingRegret
{
    public class MatchingRegretTrainer
    {
        public void Train(int iterations, Player player1, Player player2)
        {
            for (int i = 0; i < iterations; i++)
            {
                var strategy1 = player1.GetStrategy();
                var strategy2 = player2.GetStrategy();
                var action1 = player1.GetAction(strategy1);
                var action2 = player2.GetAction(strategy2);
                
                player1.TrainUnitary(action1, action2);
                player2.TrainUnitary(action2, action1);
            }

        }

    }
}