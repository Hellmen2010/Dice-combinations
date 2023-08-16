using DiceCombinations.Code.Services.EntityContainer;
using TMPro;
using UnityEngine;

namespace DiceCombinations.Code.Core.FieldInfo
{
    public class FieldDicesSum : MonoBehaviour, IFactoryEntity
    {
        [SerializeField] private TMP_Text _playerDicesSum;
        [SerializeField] private TMP_Text _dealerDicesSum;

        public void SetPlayerValue(string value) => _playerDicesSum.text = "PLAYER: " + value;
        
        public void SetDealerValue(string value) => _dealerDicesSum.text = "DEALER: " + value;

        public void ResetValues()
        {
            SetDealerValue("0");
            SetPlayerValue("0");
        }
    }
}