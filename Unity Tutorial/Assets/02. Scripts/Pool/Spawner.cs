using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    
    public float maxTime = 3f;
    public float timer = 0f;

    void CreateMonster()
    {
        timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            timer = 0f;
            
            // int randomIndex = Random.Range(0, 3);
            
            GameObject monsterObj = Instantiate(spawnPrefabs[0], this.transform);
            monsterObj.transform.position = this.transform.position;
        }
    }
}