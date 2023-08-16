using UnityEngine;

namespace DiceCombinations.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "DiceSpawnPositions", menuName = "StaticData/DiceSpawnPositions")]
    public class DiceSpawnPositions : ScriptableObject
    {
        public Vector3[] PlayerDices;
        public Vector3[] DealerDices;
    }
}