using TMPro;
using UnityEngine;

namespace DiceCombinations.Code.Core.UI.Info
{
    public class UIInfo : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _valueText;

        public void SetText(string value) => _valueText.text = value;
    }
}