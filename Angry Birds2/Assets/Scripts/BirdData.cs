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
    
    public float explosionRadius = 5f;
    public float explosionDamage = 20f;
    public GameObject explosionParticle;

    public GameObject[] blueBirds;
}

public enum SkillType
{
    Normal,
    Explosive,
    Boost,
    Split,
}