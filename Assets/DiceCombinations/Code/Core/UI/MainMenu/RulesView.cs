using UnityEngine;

namespace DiceCombinations.Code.Core.UI.MainMenu
{
    public class RulesView : MonoBehaviour
    {
        [SerializeField] private Transform _rulesRoot;
        [SerializeField] private GameObject _animatedObject;
        public RectTransform[] RulesRects { get; private set; }

        public void Construct(RectTransform[] rulesRects) => RulesRects = rulesRects;
        
        public void Hide()
        {
            _animatedObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void Show() => gameObject.SetActive(true);

        public void ShowAnimation() => _animatedObject.SetActive(true);

        public Transform GetRulesRoot => _rulesRoot;
    }
}