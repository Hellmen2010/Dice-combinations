using TMPro;
using UnityEngine;

namespace DiceCombinations.Code.Core.Chip
{
    public class TableChipView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SpriteRenderer _sprite;

        public void Construct(UIChipView uiChip)
        {
            SetText(uiChip.Value.ToString());
            SetSprite(uiChip.GetSprite);
            transform.position = Camera.main.ScreenToWorldPoint(uiChip.transform.position);
        }
        
        public void SetText(string value) => _text.text = value;

        public void SetSprite(Sprite sprite) => _sprite.sprite = sprite;

        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);
    }
}