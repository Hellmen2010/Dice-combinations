using System;
using DiceCombinations.Code.Data.Enums;
using UnityEngine;

namespace DiceCombinations.Code.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }
}