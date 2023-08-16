using System.Linq;
using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.GameBalance;
using DiceCombinations.Code.Core.UI.Buttons;
using DiceCombinations.Code.Core.UI.Info;
using DiceCombinations.Code.Core.UI.MainMenu;
using DiceCombinations.Code.Core.UI.Popup;
using DiceCombinations.Code.Core.UI.SoundSettings;
using DiceCombinations.Code.Data.Progress;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Data.StaticData.GameRules;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.PersistentProgress;
using DiceCombinations.Code.Services.Sound;
using DiceCombinations.Code.Services.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DiceCombinations.Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;
        private readonly ISoundService _soundService;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer, 
            IPersistentProgress progress, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _progress = progress;
            _soundService = soundService;
        }
        
        public Transform CreateRootCanvas() => 
            Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);

        public void CreateBackButton(Transform persistantRoot)
        {
            BackButton button = Object.Instantiate(_staticData.Prefabs.BackButton, persistantRoot);
            button.Construct(_soundService);
            _entityContainer.RegisterEntity(button);
        }

        public void CreateSoundSettings(Transform persistantRoot)
        {
            SoundSettingsView view = Object.Instantiate(_staticData.Prefabs.SoundSettings, persistantRoot);
            view.Construct(_soundService, _progress);
            SoundSettings settings = new SoundSettings(view, _soundService, _progress);
            view.HideSlider();
            _entityContainer.RegisterEntity(settings);
        }

        public void CreateMainMenu(Transform root)
        {
            MainMenu mainMenu = Object.Instantiate(_staticData.Prefabs.MainMenu, root);
            mainMenu.Construct(_soundService);
            _entityContainer.RegisterEntity(mainMenu);
        }

        public void CreateRules(Transform root)
        {
            RulesView view = Object.Instantiate(_staticData.Prefabs.RulesView, root);
            view.Construct(CreateRulesViews(view.GetRulesRoot));
            RulesMenu rules = new RulesMenu(view);
            rules.HideView();
            _entityContainer.RegisterEntity(rules);
        }

        private RectTransform[] CreateRulesViews(Transform rulesRoot)
        {
            PayoutData[] sortedRules = _staticData.GameConfig.PayoutRules.OrderBy(t => t.MinDicesSum).ToArray();
            RectTransform[] rectTransforms = new RectTransform[sortedRules.Length];
            for (int i = 0; i < sortedRules.Length; i++)
            {
                RuleUIView rule = Object.Instantiate(_staticData.Prefabs.RuleUIView, rulesRoot);
                rule.Construct(sortedRules[i]);
                rectTransforms[i] = rule.GetRect;
            }

            return rectTransforms;
        }

        public BalanceView CreateBalanceView(Transform root)
        {
            BalanceView view = Object.Instantiate(_staticData.Prefabs.BalanceView, root);
            view.SetText(_progress.Progress.Balance.ToString());
            _entityContainer.RegisterEntity(view);
            return view;
        }

        public BetView CreateBetView(Transform root)
        {
            BetView view = Object.Instantiate(_staticData.Prefabs.BetView, root);
            _entityContainer.RegisterEntity(view);
            return view;
        }

        public void CreateClearButton(Transform root)
        {
            ClearButton button = Object.Instantiate(_staticData.Prefabs.ClearButton, root);
            button.Construct(_soundService);
            _entityContainer.RegisterEntity(button);
        }

        public void CreateSpinButton(Transform root)
        {
            SpinButton button = Object.Instantiate(_staticData.Prefabs.SpinButton, root);
            button.Construct(_soundService);
            _entityContainer.RegisterEntity(button);
        }

        public void CreateWinPopup(Transform root)
        {
            WinPopup popup = Object.Instantiate(_staticData.Prefabs.WinPopup, root);
            popup.Construct(_soundService);
            popup.Hide();
            _entityContainer.RegisterEntity(popup);
        }

        public UIChipView[] CreateChips(Transform root)
        {
            Transform chipsRoot = Object.Instantiate(_staticData.Prefabs.ChipsRoot, root);
            UIChipView[] chips = new UIChipView[_staticData.ChipsConfig.ChipsData.Length];
            ChipData[] sortedChips = _staticData.ChipsConfig.ChipsData.OrderBy(chip => chip.Value).ToArray();
            for (var i = 0; i < sortedChips.Length; i++)
            {
                chips[i] = Object.Instantiate(_staticData.Prefabs.UIChipView, chipsRoot);
                chips[i].Construct(sortedChips[i], _soundService);
            }

            return chips;
        }

        public void CreateBalanceChanger(BalanceView balanceView, BetView betView, UIChipView[] chips,
            PlayerProgress progressProgress)
        {
            TableChipMover tableChipMover = _entityContainer.GetEntity<TableChipMover>();
            TableChipsSumView tableChipsSumView = CreateFieldChipsSumView(tableChipMover);
            UserBetBalance changer = new(balanceView, betView, chips, _progress.Progress, tableChipsSumView, tableChipMover, _staticData.GameConfig);
            _entityContainer.RegisterEntity(changer);
        }

        private TableChipsSumView CreateFieldChipsSumView(TableChipMover tableChipMover)
        {
            TableChipsSumView tableChipsSumView = new TableChipsSumView();
            _entityContainer.RegisterEntity(tableChipsSumView);
            tableChipsSumView.Construct(_staticData.ChipsConfig, tableChipMover.GetInfoChip);
            return tableChipsSumView;
        }
    }
}