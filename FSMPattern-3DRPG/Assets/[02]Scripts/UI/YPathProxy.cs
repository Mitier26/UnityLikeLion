using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

public class YPathProxy : FindMonobehavior, IFindPathProxy
{
    public async UniTask<Vector3[]> CalculatePath(Vector3 start, Vector3 end)
    {
        _isCalculated = true;
        _progress = 0.0F;

        try
        {
            List<Vector3> path = new List<Vector3>();
            for (int y = 0; y < 200; ++y)
            {
                path.Add(new Vector3(0, y, 0));
                await UniTask.Delay(200);
                _progress += y / (float)200;
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