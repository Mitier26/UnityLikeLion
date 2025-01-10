using UnityEngine;

public class FindPathManager : MonoBehaviour
{
    public IFindPathProxy FindPathProxy;

    private async void Start()
    {
        int random = Random.Range(0, 2);

        if (gameObject != null)
            FindPathProxy = random == 0
                ? gameObject.AddComponent<YPathProxy>()
                : gameObject.AddComponent<AStarPathProxy>();

        var result = await FindPathProxy.CalculatePath(Vector3.zero, Vector3.zero);

        foreach (var vector3 in result)
        {
            Debug.Log(vector3);
        }
    }

    void Update()
    {
        Debug.Log($"Find Path {FindPathProxy.Progress}");
    }
}