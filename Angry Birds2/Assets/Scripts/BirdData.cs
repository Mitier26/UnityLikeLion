using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData")]
public class BirdData : ScriptableObject
{
    public string birdName;
    public float weight;
    public float speed;
    public Sprite birdSprite;
    public SkillType skillType;
}

public enum SkillType
{
    Normal,
    Explosive,
    Boost,
    Split,
}