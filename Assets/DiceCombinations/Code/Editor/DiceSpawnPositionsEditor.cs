using System.Linq;
using DiceCombinations.Code.Data.StaticData;
using UnityEditor;
using UnityEngine;

namespace DiceCombinations.Code.Editor
{
    [CustomEditor(typeof(DiceSpawnPositions))]
    public class DiceSpawnPositionsEditor :UnityEditor.Editor
    {
        private const string PLAYER_DICE = "PlayerDice";
        private const string DEALER_DICE = "DealerDice";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("CollectPlayerDices")) Collect(PLAYER_DICE);
            if (GUILayout.Button("CollectDealerDices")) Collect(DEALER_DICE);
        }

        private void Collect(string tag)
        {
            DiceSpawnPositions spawnPositions = (DiceSpawnPositions)target;
            Vector3[] positions = GameObject.FindGameObjectsWithTag(tag)
                .Select(pos => pos.transform.position)
                .OrderBy(pos => pos.x)
                .ToArray();
            
            switch (tag)
            {
                case PLAYER_DICE:
                    spawnPositions.PlayerDices = positions;
                    break;
                case DEALER_DICE:
                    spawnPositions.DealerDices = positions;
                    break;
            }
            EditorUtility.SetDirty(spawnPositions);
        }
    }
}