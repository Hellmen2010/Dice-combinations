using System;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.StaticData.GameRules;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiceCombinations.Code.Core.UI.Popup
{
    public class WinPopup : MonoBehaviour, IFactoryEntity
    {
        public event Action OnCloseButtonClicked;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _text;
        private ISoundService _soundService;

        public void Construct(ISoundService soundService) => 
            _soundService = soundService;

        private void Start() => _closeButton.onClick.AddListener(CloseButtonClicked);

        private void CloseButtonClicked()
        {
            OnCloseButtonClicked?.Invoke();
            Hide();
        }

        public void SetText(RoundResult result)
        {
            _text.text = result.Result switch
            {
                Result.Win => $"YOU WON\n{result.WinAmount}",
                Result.Lose => "YOU LOSE",
                Result.Stay => "STAY",
                _ => _text.text
            };
        }

        public void Show()
        {
            _soundService.PlayEffectSound(SoundId.WinPopup);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);

        private void OnDestroy() => _closeButton.onClick.RemoveListener(CloseButtonClicked);
    }
}