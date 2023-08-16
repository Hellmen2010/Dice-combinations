using System;
using UnityEngine;

namespace DiceCombinations.Code.Core.DeviceAdaptation
{
    public class DeviceCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Start()
        {
            if (Screen.width >= 1200) 
                _camera.orthographicSize = 8;
        }
    }
}