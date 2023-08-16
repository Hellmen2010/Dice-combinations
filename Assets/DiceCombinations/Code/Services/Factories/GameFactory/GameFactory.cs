using DiceCombinations.Code.Core.Chip;
using DiceCombinations.Code.Core.Dice;
using DiceCombinations.Code.Core.FieldInfo;
using DiceCombinations.Code.Core.GameBalance;
using DiceCombinations.Code.Services.EntityContainer;
using DiceCombinations.Code.Services.Sound;
using DiceCombinations.Code.Services.StaticData;
using UnityEngine;
using UnityEngine.Pool;

namespace DiceCombinations.Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;

        public GameFactory(IStaticData staticData, IEntityContainer entityContainer, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _soundService = soundService;
        }

        public void CreateDices()
        {
            Transform root = new GameObject().transform;
            var playerDices = CreateDiceSet(_staticData.GameConfig.DiceSetSize, root, _staticData.DiceSpawnPositions.PlayerDices);
            var dealerDices = CreateDiceSet(_staticData.GameConfig.DiceSetSize, root, _staticData.DiceSpawnPositions.DealerDices);
            DiceMover diceMover = new DiceMover(playerDices, dealerDices, _staticData.GameConfig, _soundService);
            _entityContainer.RegisterEntity(diceMover);
        }

        public void CreateGameplayCalculator()
        {
            RoundCalculations calculator = new RoundCalculations(_staticData.GameConfig);
            _entityContainer.RegisterEntity(calculator);
        }

        public void CreateTableChips()
        {
            ObjectPool<TableChipView> pool = new ObjectPool<TableChipView>(() => Object.Instantiate(_staticData.Prefabs.TableChip),
                tableChip => { },
                tableChip => { tableChip.Hide();},
                tableChip => { },
                false, 10, 20);
            TableChipView infoTableChip = Object.Instantiate(_staticData.Prefabs.InfoTableChip);
            TableChipMover tableChipMover =
                new TableChipMover(pool, infoTableChip, _staticData.GameConfig.TableChipMoveTime);
            _entityContainer.RegisterEntity(tableChipMover);
        }

        public void CreateFieldDicesSumInfo()
        {
            FieldDicesSum dicesSum = Object.Instantiate(_staticData.Prefabs.FieldDicesSum);
            _entityContainer.RegisterEntity(dicesSum);
        }

        private DiceView[] CreateDiceSet(int amount, Transform root, Vector3[] spawnPositions)
        {
            DiceView[] diceSet = new DiceView[amount];
            for (int i = 0; i < amount; i++)
            {
                diceSet[i] = CreateDice(root, spawnPositions[i]);
                diceSet[i].transform.eulerAngles = _staticData.GameConfig.DiceData[Random.Range(0, _staticData.GameConfig.DiceData.Length)].Rotation;
            }
            return diceSet;
        }

        private DiceView CreateDice(Transform root, Vector3 atPos) => 
            Object.Instantiate(_staticData.Prefabs.Dice, atPos, Quaternion.identity , root);
    }
}