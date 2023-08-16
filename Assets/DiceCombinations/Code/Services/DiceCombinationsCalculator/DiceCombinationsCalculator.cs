using DiceCombinations.Code.Data.Enums;
using DiceCombinations.Code.Data.StaticData;
using DiceCombinations.Code.Data.StaticData.GameRules;
using UnityEngine;

namespace DiceCombinations.Code.Services.DiceCombinationsCalculator
{
    public class DiceCombinationsCalculator : IDiceCombinationsCalculator
    {
        private readonly int _diceSides;
        private readonly int _dicesInGame;

        public DiceCombinationsCalculator(GameConfig config)
        {
            _diceSides = config.DiceData.Length;
            _dicesInGame = config.DiceSetSize;
        }
        
        public void GetRoundDiceCombinations(RoundResult roundResult, out int[] playerDices, out int[] dealerDices)
        {
            playerDices = GetDicesCombination(roundResult.DicesSum);
            dealerDices = GetDicesCombination(GetDealerDicesSum(roundResult));
        }

        private int GetDealerDicesSum(RoundResult roundResult)
        {
            int sum = roundResult.Result switch
            {
                Result.Win => Random.Range(_dicesInGame, roundResult.DicesSum),
                Result.Stay => roundResult.DicesSum,
                Result.Lose => Random.Range(roundResult.DicesSum + 1, _diceSides * _dicesInGame + 1),
                _ => 0
            };
            return sum;
        }

        private int[] GetDicesCombination(int dicesSum)
        {
            int[] combination = new int[_dicesInGame];
            int delta;

            for (int i = 0; i < combination.Length; i++)
            {
                int minDiceValue = 1;
                int maxDiceValue = _diceSides + 1;

                delta = (combination.Length - i) * 6 - dicesSum;
                if (delta < _diceSides) 
                    minDiceValue = 6 - delta;

                delta = (combination.Length - 1 - i) * 1 + _diceSides;
                if (delta > dicesSum) 
                    maxDiceValue = (_diceSides + 1) - (delta - dicesSum);

                combination[i] = Random.Range(minDiceValue, maxDiceValue);
                dicesSum -= combination[i];
            }
            
            return combination;
        }
    }
}