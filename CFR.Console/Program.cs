using CFR.Console.CFRM;
using CFR.Console.CFRM.Game;

namespace CFR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //RpsTrainer trainer = new RpsTrainer();
            //trainer.Train(100000);
            //var strat = trainer.GetAverageStrategy();
            //System.Console.WriteLine("{0} {1} {2}", strat[0], strat[1], strat[2]);
            //System.Console.ReadLine();

            //var player1 = new RpsPlayer();
            //var player2 = new RpsPlayer();
            //var player1 = new ColonelBlottoPlayer();
            //var player2 = new ColonelBlottoPlayer();
            //var trainer = new MatchingRegretTrainer();
            //trainer.Train(10000000, player1, player2);

            //var strat1 = player1.GetAverageStrategy();
            //var strat2 = player2.GetAverageStrategy();

            //System.Console.WriteLine("{0} {1} {2}", strat1[0], strat1[1], strat1[2]);
            //System.Console.WriteLine("{0} {1} {2}", strat2[0], strat2[1], strat2[2]);


            int iterations = 10000000;
            var game = new KuhnPokerGame();
            var gameCfrTrainer = new CfrGameTrainer(game);
            gameCfrTrainer.Train(iterations);
            
            System.Console.ReadLine();

        }
    }
}
