using DiceCombinations.Code.Data.StaticData.GameRules;
using TMPro;
using UnityEngine;

namespace DiceCombinations.Code.Core.UI.Info
{
    public class RuleUIView : MonoBehaviour
    {
        public RectTransform GetRect => _rect;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TMP_Text _text;

        public void Construct(PayoutData data)
        {
            _text.text = data.MinDicesSum != data.MaxDicesSum
                ? $"{data.MinDicesSum} - {data.MaxDicesSum} POINTS IS {data.PayMulti - 1} : 1"
                : $"{data.MaxDicesSum} POINTS IS {data.PayMulti - 1} : 1";
        }
    }
}