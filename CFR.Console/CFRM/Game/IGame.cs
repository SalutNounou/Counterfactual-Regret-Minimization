using System.Collections.Generic;
using CFR.Console.CFRM.Nodes;

namespace CFR.Console.CFRM.Game
{
    public interface IGame
    {
        int[] GetCards();
        int GetGamePayoff(int[] cards, List<IGameAction> history);
        bool IsPartyOver(int[] cards, List<IGameAction> history);
        IGameAction GetNthAction(int n);
        IInfoSet GetInfoSet(int card, List<IGameAction> history);
        INode GetNode(IInfoSet infoset);

    }
}