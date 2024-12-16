using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Setting")]
    [Tooltip("How fast should the texture scroll")]
    public float scrollSpeed;

    [Header("Reference")]
    public MeshRenderer meshRenderer;

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(GameManager.Instance.CalculateGameSpeed() /20 * Time.deltaTime, 0);
    }
}
