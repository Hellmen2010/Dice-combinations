using System;
using UnityEngine;

namespace DiceCombinations.Code.Core.DeviceAdaptation
{
    public class DeviceRules : MonoBehaviour
    {
        [SerializeField] private RectTransform _header;
        [SerializeField] private RectTransform _rules;
        [SerializeField] private Animator _catAnimator;
        private static readonly int IsIpad = Animator.StringToHash("isIpad");

        private void Start()
        {
            if (Screen.width < 1200) return;
            _header.anchoredPosition = new Vector2(_header.anchoredPosition.x, -140);
            _rules.anchoredPosition = new Vector2(_rules.anchoredPosition.x, -240);
        }
    }
}