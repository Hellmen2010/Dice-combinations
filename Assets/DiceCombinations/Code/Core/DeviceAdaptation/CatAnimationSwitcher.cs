using UnityEngine;

namespace DiceCombinations.Code.Core.DeviceAdaptation
{
    public class CatAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private Animator _catAnimator;
        private static readonly int IsIpad = Animator.StringToHash("isIpad");

        private void OnEnable() => 
            _catAnimator.SetBool(IsIpad, !(Screen.width < 1500));
    }
}