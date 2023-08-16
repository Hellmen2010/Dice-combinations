using UnityEngine;

namespace DiceCombinations.Code.Core.DeviceAdaptation
{
    public class DeviceEnvironment : MonoBehaviour
    {
        [SerializeField] private GameObject _ipadEnviroment;
        [SerializeField] private GameObject _iphoneEnviroment;

        private void Start() => SetQuality();

        private void SetQuality()
        {
            if (Screen.width >= 1200)
            {
                _ipadEnviroment.SetActive(true);
                _iphoneEnviroment.SetActive(false);
            }
            else
            {
                _ipadEnviroment.SetActive(false);
                _iphoneEnviroment.SetActive(true);
            }
        }

    }
}