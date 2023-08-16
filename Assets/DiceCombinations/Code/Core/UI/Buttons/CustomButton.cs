using System;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace DiceCombinations.Code.Core.UI.Buttons
{
    public class CustomButton : MonoBehaviour
    {
        public event Action OnButtonClicked;
        
        [SerializeField] protected Button _button;
        private ISoundService _soundService;

        public virtual void Construct(ISoundService soundService) => 
            _soundService = soundService;

        protected void Start() => _button.onClick.AddListener(ButtonClicked);

        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);

        protected void ButtonClicked()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnButtonClicked?.Invoke();
        }

        protected void OnDestroy() => _button?.onClick.RemoveListener(ButtonClicked);
    }
}