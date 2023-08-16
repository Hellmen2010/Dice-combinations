using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Sound;
using UnityEngine;

namespace DiceCombinations.Code.Core.Dice
{
    public class DiceMover : IFactoryEntity
    {
        private readonly DiceView[] _playerDices;
        private readonly DiceView[] _dealerDices;
        private readonly GameConfig _config;
        private readonly ISoundService _soundService;
        private Sequence _playerDicesSpin;
        private Sequence _dealerDicesSpin;
        
        public DiceMover(DiceView[] playerDices, DiceView[] dealerDices, GameConfig config, ISoundService soundService)
        {
            _playerDices = playerDices;
            _dealerDices = dealerDices;
            _config = config;
            _soundService = soundService;
        }

        public async UniTask Spin(int[] playerCombination, int[] dealerCombination, Action<string> onPlayerDicesStop, Action<string> onDealerDicesStop)
        {
            await SpinDices(_playerDices, _playerDicesSpin, playerCombination).AsyncWaitForCompletion().AsUniTask();
            onPlayerDicesStop?.Invoke(playerCombination.Sum().ToString());
            await SpinDices(_dealerDices, _dealerDicesSpin, dealerCombination).AsyncWaitForCompletion().AsUniTask();
            onDealerDicesStop?.Invoke(dealerCombination.Sum().ToString());
        }

        private Sequence SpinDices(DiceView[] dices, Sequence sequence, int[] dicesCombination)
        {
            sequence = DOTween.Sequence();
            for (var i = 0; i < dices.Length; i++)
            {
                Vector3 angleForSide = _config.DiceData.FirstOrDefault(p => p.DiceSideValue == dicesCombination[i]).Rotation;
                sequence.Insert(_config.DelayDiceSpins * i, SpinDice(dices[i], angleForSide, _config.DiceSpinLaps));
                sequence.Insert(_config.DelayDiceSpins * i, SpinDice(dices[i], angleForSide, 1, 1, _config.DiceSpinLaps));
            }

            return sequence;
        }

        private Tween SpinDice(DiceView dice, Vector3 toAngle, int xLaps = 1, int yLaps = 1, int zLaps = 1)
        {
            _soundService.PlayEffectSound(SoundId.DiceSpin);
            toAngle = new Vector3(
                toAngle.x + 360 * xLaps, 
                toAngle.y + 360 * yLaps, 
                toAngle.z + 360 * zLaps);
            return dice.transform.DOLocalRotate(toAngle, _config.DiceSpinTime,
                RotateMode.FastBeyond360);
        }
    }
}