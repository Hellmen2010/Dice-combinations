using System.Linq;
using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.GameRules;
using DiceCombinations.Code.Services.EntityContainer;
using UnityEngine;

namespace DiceCombinations.Code.Core.GameBalance
{
    public class RoundCalculations : IFactoryEntity
    {
        private readonly float _playerWinRate;
        private readonly PayoutData[] _payoutRules;
        private readonly float _stayChance;
        private readonly int _minDicesSum;
        private readonly int _maxDicesSum;

        public RoundCalculations(GameConfig config)
        {
            _playerWinRate = config.PlayerWinRate;
            _stayChance = config.StayChance;
            _payoutRules = config.PayoutRules.OrderBy(a => a.MinDicesSum).ToArray();
            _minDicesSum = config.DiceSetSize;
            _maxDicesSum = config.DiceSetSize * config.DiceData.Length;
        }

        public RoundResult CalculateRoundResult()
        {
            RoundResult result = new RoundResult
            {
                Result = GetSpinResult()
            };

            switch (result.Result)
            {
                case Result.Lose:
                    result.DicesSum = Random.Range(_minDicesSum, _maxDicesSum);
                    return result;
                case Result.Stay:
                    result.DicesSum = Random.Range(_minDicesSum, _maxDicesSum + 1);
                    result.WinMulti = 1;
                    return result;
            }

            int rand = Random.Range(0, GetPayoutEventsSum(result.Result));
            int currentSum = 0;
            for (int i = _payoutRules.Length - 1; i >= 0; i--)
            {
                currentSum += _payoutRules[i].EventWeight;
                if (rand > currentSum) continue;
                result.WinMulti = _payoutRules[i].PayMulti;
                result.DicesSum = Random.Range(_payoutRules[i].MinDicesSum, _payoutRules[i].MaxDicesSum + 1);
                break;
            }
            
            return result;
        }

        private int GetPayoutEventsSum(Result spinResult)
        {
            return spinResult == Result.Lose
                ? _payoutRules.SkipLast(1).Sum(a => a.EventWeight)
                : _payoutRules.Sum(a => a.EventWeight);
        }

        public int CalculateRoundWin(int bet, int winMulti) => bet * winMulti;

        private Result GetSpinResult()
        {
            float rand = Random.Range(0, 1.0f);
            if (rand < _playerWinRate) return Result.Win;
            if (rand < _playerWinRate + _stayChance) return Result.Stay;
            return Result.Lose;
        }
    }
}