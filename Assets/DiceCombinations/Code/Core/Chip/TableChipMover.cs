using System;
using DG.Tweening;
using DiceCombinations.Code.Services.EntityContainer;
using UnityEngine;
using UnityEngine.Pool;

namespace DiceCombinations.Code.Core.Chip
{
    public class TableChipMover : IFactoryEntity
    {
        public TableChipView GetInfoChip => _infoChip;
        private readonly ObjectPool<TableChipView> _pool;
        private readonly TableChipView _infoChip;
        private readonly float _moveTime;

        public TableChipMover(ObjectPool<TableChipView> pool, TableChipView infoChip, float moveTime)
        {
            _pool = pool;
            _infoChip = infoChip;
            _moveTime = moveTime;
        }
        
        public void MoveTableChipAnimated(UIChipView chip, Action onChipMoved = null)
        {
            TableChipView tableChip = _pool.Get();
            tableChip.Construct(chip);
            tableChip.Show();
            MoveChip(tableChip, _infoChip.transform.position, _moveTime, onChipMoved);
        }

        private void MoveChip(TableChipView tableChip, Vector3 toPos, float moveTime, Action onChipMoved)
        {
            tableChip.transform.DOMove(toPos, moveTime).OnComplete(() =>
            {
                onChipMoved();
                _pool.Release(tableChip);
            });
        }
    }
}