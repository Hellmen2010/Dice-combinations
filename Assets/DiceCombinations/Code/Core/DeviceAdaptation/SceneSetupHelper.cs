using UnityEngine;

namespace DiceCombinations.Code.Core.DeviceAdaptation
{
    public class SceneSetupHelper : MonoBehaviour
    {
        [SerializeField] private GameObject _object;

        private void Awake() => _object.SetActive(false);
    }
}