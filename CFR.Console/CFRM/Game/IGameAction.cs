using System;

namespace CFR.Console.CFRM.Game
{
    public interface IGameAction
    {
        String Action { get; set; }
        int GetHashCode();
    }

    public class KuhnPokerAction : IGameAction
    {
        public String Action { get; set; }
        public override int GetHashCode()
        {
            if (Action != null)
                return Action.GetHashCode();
            return 0;
        }
    }
}