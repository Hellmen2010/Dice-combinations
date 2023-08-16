using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiceCombinations.Code.Services.EntityContainer
{
    public class EntityContainer : IEntityContainer, IDisposable
    {
        private readonly Dictionary<Type, IFactoryEntity> _entities = new Dictionary<Type, IFactoryEntity>(10);

        public void RegisterEntity<TEntity>(TEntity entity) where TEntity : class, IFactoryEntity
        {
            if (_entities.ContainsKey(typeof(TEntity)))
                ReplaceEntityWithDispose(entity);
            else
                _entities.Add(typeof(TEntity), entity);
        }

        public TEntity GetEntity<TEntity>()
        {
            _entities.TryGetValue(typeof(TEntity), out IFactoryEntity entity);
            return (TEntity) entity;
        }

        public void Dispose()
        {
            foreach (var entity in _entities.Values)
                TryDisposeEntity(entity);
        }

        private void ReplaceEntityWithDispose<TEntity>(TEntity entity) where TEntity : class, IFactoryEntity
        {
            IFactoryEntity replaceEntity = _entities[typeof(TEntity)];
            TryDisposeEntity(replaceEntity);
            _entities[typeof(TEntity)] = entity;
        }

        private void TryDisposeEntity(object entity)
        {
            if (entity is IDisposable disposableEntity) 
                disposableEntity.Dispose();
        }
    }
}