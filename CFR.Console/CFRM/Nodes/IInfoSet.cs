using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using CFR.Console.CFRM.Game;

namespace CFR.Console.CFRM.Nodes
{
    public interface IInfoSet : IEquatable<IInfoSet>
    {
        String ToString();
    }


    public class KuhnPokerInFoSet : IInfoSet
    {
        #region IEquaity Members

        protected bool Equals(KuhnPokerInFoSet other)
        {
            return Card == other.Card && AreActionHistoryEquals(ActionHistory, other.ActionHistory) &&
                   string.Equals(StringValue, other.StringValue);
        }


        private bool AreActionHistoryEquals(List<IGameAction> a, List<IGameAction> b)
        {
            if (a.Count != b.Count) return false;
            return !a.Where((t, i) => t.Action != b[i].Action).Any();
        }

        public bool Equals(IInfoSet other)
        {
            var a = other as KuhnPokerInFoSet;
            return a != null && Equals(a);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KuhnPokerInFoSet)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Card;
                hashCode = (hashCode * 397) ^ (ActionHistory != null ? GetActionListHashCode() : 0);
                hashCode = (hashCode * 397) ^ (StringValue != null ? StringValue.GetHashCode() : 0);
                return hashCode;
            }
        }

        public int GetActionListHashCode()
        {
            var hashCode = 0;
            ActionHistory.ForEach(x=>hashCode = (hashCode *397)^ x.Action.GetHashCode());
            return hashCode;
        }


        #endregion

        public int Card { get; set; }
        public List<IGameAction> ActionHistory { get; set; }

        public KuhnPokerInFoSet()
        {
            ActionHistory = new List<IGameAction>();
        }


        public string StringValue
        {
            get
            {
                return String.Format("Card : {0}. History : {1}", Card, String.Join(" ", ActionHistory.Select(x => x.Action)));
            }
        }

        public override String ToString()
        {
            return String.Format("Card : {0}. History : {1}", Card, String.Join(" ", ActionHistory.Select(x => x.Action)));
        }
    }



    /// <summary>
    /// This implemetation of IInfoSet is much quicker
    /// </summary>
    public class KuhnPokerInfosetQuick : IInfoSet
    {
        #region IEquatable implementation

        public override int GetHashCode()
        {
            return StringValue.GetHashCode();
        }

        protected bool Equals(KuhnPokerInfosetQuick other)
        {
            return other.ToString() == ToString();
        }


        public bool Equals(IInfoSet other)
        {
            var a = other as KuhnPokerInfosetQuick;
            return a != null && Equals(a);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KuhnPokerInfosetQuick) obj);
        }

        #endregion




        public KuhnPokerInfosetQuick(int card, List<IGameAction> history )
        {
            StringValue =  String.Format("Card : {0}. History {1}", card, history.Any()?String.Join(" ", history.Select(x => x.Action)) :"Empty");
        }
        public readonly string StringValue;

        public override string ToString()
        {
            return StringValue;
        }
    }


   





}