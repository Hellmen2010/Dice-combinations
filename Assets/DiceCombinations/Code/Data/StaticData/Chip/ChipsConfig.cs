using UnityEngine;

namespace DiceCombinations.Code.Data.StaticData.Chip
{
    [CreateAssetMenu(fileName = "ChipsConfig", menuName = "StaticData/ChipsConfig")]
    public class ChipsConfig : ScriptableObject
    {
        public ChipData[] ChipsData;
    }
}