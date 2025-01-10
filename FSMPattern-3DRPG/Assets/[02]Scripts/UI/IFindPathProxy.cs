using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IFindPathProxy
{
    UniTask<Vector3[]> CalculatePath(Vector3 start, Vector3 end);
    bool IsCalculated { get; }
    float Progress { get; }
}