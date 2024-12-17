using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpanwer : MonoBehaviour
{
    public GameObject itemPrefab;

    public ItemData[] itemDatas;

    public float minSpawnTime;
    public float maxSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnItemCallback();
    }

    IEnumerator SpawnItem()
    {
        float nextRandomTime = Random.Range(minSpawnTime, maxSpawnTime);

        yield return new WaitForSeconds(nextRandomTime);

        SpawnItemCallback();
    }

    private void SpawnItemCallback()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        SpawnedItem spawnedItem = item.GetComponent<SpawnedItem>();
        spawnedItem.SetItemData(itemDatas[Random.Range(0, itemDatas.Length)]);

        // 익명함수 , 델리게이트 하나
        item.GetComponent<SpawnedItem>().OnDestroiedAction += () =>
        {
            Debug.Log("Item call");
            StartCoroutine(SpawnItem());
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
