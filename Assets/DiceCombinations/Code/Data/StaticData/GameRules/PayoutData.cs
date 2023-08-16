using System;

namespace DiceCombinations.Code.Data.StaticData.GameRules
{
    [Serializable]
    public class PayoutData
    {
        public int MinDicesSum;
        public int MaxDicesSum;
        public int PayMulti;
        public int EventWeight;
    }
}