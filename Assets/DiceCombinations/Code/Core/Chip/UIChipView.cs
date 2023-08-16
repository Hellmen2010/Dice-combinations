using System;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiceCombinations.Code.Core.Chip
{
    public class UIChipView : MonoBehaviour
    {
        public event Action<UIChipView> OnChipClicked;
        public int Value { get; private set; }
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textValue;
        private ISoundService _soundService;

        public void Construct(ChipData chipData, ISoundService soundService)
        {
            _soundService = soundService;
            _button.image.sprite = chipData.Sprite;
            Value = chipData.Value;
            _textValue.text = chipData.Value.ToString();
        }

        private void Start() => _button.onClick.AddListener(ChipClicked);

        private void ChipClicked()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnChipClicked?.Invoke(this);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(ChipClicked);

        public Sprite GetSprite => _button.image.sprite;
    }
}