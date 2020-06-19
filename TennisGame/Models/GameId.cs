using System;

namespace TennisGame.Models
{
    public class GameId
    {
        #region Constructors

        public GameId()
        {
            Seq = 0;
            OccuredDate = DateTimeOffset.Now;
        }

        public GameId(int seq, DateTimeOffset? occuredDate = null)
        {
            Seq = seq;
            OccuredDate = occuredDate switch
            {
                null => DateTimeOffset.Now,
                _ => occuredDate.Value
            };
        }

        #endregion Constructors

        public int Seq { get; }

        public DateTimeOffset OccuredDate { get; }

        public override string ToString()
            => $"{OccuredDate:yyyyMMdd}-{Seq}";

        public override bool Equals(object? obj)
        {
            var target = obj as GameId;
            if (obj is null) return false;

            if (ReferenceEquals(target, this) == false)
                return true;

            return Seq.Equals(target.Seq) && OccuredDate.Equals(target.OccuredDate);
        }

        public override int GetHashCode()
            => (Seq, OccuredDate).GetHashCode();
    }
}