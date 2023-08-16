using DiceCombinations.Code.Data.StaticData.DiceSide;
using DiceCombinations.Code.Data.StaticData.GameRules;
using UnityEngine;

namespace DiceCombinations.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public DiceSideData[] DiceData;
        public int DiceSpinLaps;
        public float DiceSpinTime;
        public float DelayDiceSpins;
        public int DiceSetSize;
        public int StartScore;
        public float PlayerWinRate;
        public float StayChance;
        public float TableChipMoveTime;
        public PayoutData[] PayoutRules;
    }
}