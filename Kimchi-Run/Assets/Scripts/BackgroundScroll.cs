using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Setting")]
    [Tooltip("How fast should the texture scroll")]
    public float scrollSpeed;

    [Header("Reference")]
    public MeshRenderer meshRenderer;
}
