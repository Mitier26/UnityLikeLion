using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathProxy : FindMonobehavior, IFindPathProxy
{
    public async UniTask<Vector3[]> CalculatePath(Vector3 start, Vector3 end)
    {
        _isCalculated = true;
        _progress = 0.0F;

        try
        {
            List<Vector3> path = new List<Vector3>();
            for (int x = 0; x < 100; ++x)
            {
                path.Add(new Vector3(x, 0, 0));
                await UniTask.Delay(100);
                _progress += x / (float)100;
            }

            _isCalculated = false;

            return path.ToArray();
        }
        finally
        {
            _isCalculated = false;
            _progress = 0.0f;
        }
    }

    public bool IsCalculated => _isCalculated;
    public float Progress => _progress;
}