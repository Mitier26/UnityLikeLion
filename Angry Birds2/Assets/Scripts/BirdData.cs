using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData")]
public class BirdData : ScriptableObject
{
    public string birdName;
    public float weight;
    public float speed;
    public Sprite birdSprite;
    public SkillType skillType;
    public bool isTriangle;
    public Vector2 offset;
    public float radius;
}

public enum SkillType
{
    Normal,
    Explosive,
    Boost,
    Split,
}