using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData")]
public class BirdData : ScriptableObject
{
    public string birdName;
    public float weight;
    public float speed;
    public Sprite birdSprite;
}
