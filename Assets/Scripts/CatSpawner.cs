using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> cats;
    [SerializeField] private List<GameObject> shelves;
    [SerializeField] private bool spawn;

    [SerializeField, Range(0,1)] private float spawnDelayForEachShelf = 0.5f;
    private List<bool> shelfBool;
    private float spawnTimer;
    
    void Start()
    {
        shelfBool = new List<bool>();
        for (var i = 0; i < shelves.Count; i++)
        {
            shelfBool.Add(false);
        }
        spawn = false;
        spawnTimer = GameManager.Instance.CatDelay;
    }

    private void Update()
    {
        if (Random.value > spawnTimer) spawn = true;
        if (!spawn) return;
        var index = PickShelf();
        SpawnCat(index);
        spawn = false;
        spawnTimer -= 0.01f;
    }

    private int PickShelf()
    {
        for (int i = 0; i < shelves.Count; i++)
        {
            shelfBool[i] = IsShelfEmpty(shelves[i]);
        }
        var indices = new List<int>();
        for (var i = 0; i < shelfBool.Count; i++)
        {
            if (!shelfBool[i])
            {
                indices.Add(i);
            }
        }

        if (indices.Count == 0) return -1;
        var rand = Random.Range(0, indices.Count);
        return indices[rand];
    }

    private void SpawnCat(int index)
    {
        if (index == -1) return;
        shelfBool[index] = true;
        var spawnTransform = shelves[index].transform.GetChild(Random.Range(0, 2));
        var rand = Random.Range(0, cats.Count);
        Instantiate(cats[rand], spawnTransform);
        StartCoroutine(ShelfReset(index, spawnDelayForEachShelf));
    }

    bool IsShelfEmpty(GameObject shelf)
    {
        for (int i = 0; i < shelf.transform.childCount; i++)
        {
            var shelfChild = shelf.transform.GetChild(i);
            var empty = shelfChild.childCount != 0;
            if (empty) return empty;
        }
        return false;
    }

    private IEnumerator ShelfReset(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        shelfBool[index] = false;
    }
}


