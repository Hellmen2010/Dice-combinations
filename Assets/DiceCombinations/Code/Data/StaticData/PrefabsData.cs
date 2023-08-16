using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.Dice;
using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.UI.Buttons;
using DiceCombinations.Code.Core.UI.Info;
using DiceCombinations.Code.Core.UI.MainMenu;
using DiceCombinations.Code.Core.UI.Popup;
using DiceCombinations.Code.Core.UI.SoundSettings;
using UnityEngine;

namespace DiceCombinations.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "StaticData/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        [Header("SceneObjects")]
        public DiceView Dice;
        public FieldDicesSum FieldDicesSum;
        public TableChipView TableChip;
        public TableChipView InfoTableChip;

        [Header("UI")]
        public Transform RootCanvasPrefab;
        public BackButton BackButton;
        public SoundSettingsView SoundSettings;
        public MainMenu MainMenu;
        public RulesView RulesView;
        public BalanceView BalanceView;
        public BetView BetView;
        public ClearButton ClearButton;
        public SpinButton SpinButton;
        public WinPopup WinPopup;
        public Transform ChipsRoot;
        public UIChipView UIChipView;
        public RuleUIView RuleUIView;
    }
}