using System;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace DiceCombinations.Code.Core.UI.MainMenu
{
    public class MainMenu : MonoBehaviour, IFactoryEntity
    {
        public event Action OnPlayButtonClicked;
        public event Action OnRulesButtonClicked;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _rulesButton;
        private ISoundService _soundService;

        public void Construct(ISoundService soundService) => 
            _soundService = soundService;

        private void Start()
        {
            _playButton.onClick.AddListener(PlayButtonClicked);
            _rulesButton.onClick.AddListener(RulesButtonClicked);
        }

        private void RulesButtonClicked()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnRulesButtonClicked?.Invoke();
        }

        private void PlayButtonClicked()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnPlayButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
            _rulesButton.onClick.RemoveListener(RulesButtonClicked);
        }
    }
}