using System.Collections;
using DiceCombinations.Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace DiceCombinations.Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}