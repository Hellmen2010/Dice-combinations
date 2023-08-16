using System.Linq;
using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Data.StaticData.Chip;
using DiceCombinations.Code.Services.EntityContainer;
using UnityEngine;

namespace DiceCombinations.Code.Core.FieldInfo
{
    public class TableChipsSumView : IFactoryEntity
    {
        public int CurrentBet { get; private set; }
        
        private TableChipView _infoChip;
        private ChipsConfig _chipsConfig;

        public void Construct(ChipsConfig chipsConfig, TableChipView infoChip)
        {
            _chipsConfig = chipsConfig;
            _infoChip = infoChip;
        }

        public void UpdateValue(int chipValue)
        {
            CurrentBet += chipValue;
            UpdateView(CurrentBet);
        }

        public void ResetSum()
        {
            CurrentBet = 0;
            UpdateView(0);
        }

        private void UpdateView(int chipValue)
        {
            _infoChip.SetText(chipValue.ToString());
            _infoChip.SetSprite(GetSprite());
            if (CurrentBet == 0)
                _infoChip.Hide();
            else
                _infoChip.Show();
        }

        private Sprite GetSprite()
        {
            ChipData chipData = _chipsConfig.ChipsData.OrderByDescending(t => t.Value).FirstOrDefault(t => CurrentBet >= t.Value);
            return chipData == null ? _chipsConfig.ChipsData[0].Sprite : chipData.Sprite;
        }
    }
}