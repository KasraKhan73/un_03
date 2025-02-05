using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [Header("maxTrashCountOnRoad")]
    [SerializeField]
    private int maxCount = 1;
    
    [SerializeField] private List<GameObject> trashes;
    
    private List<Trash> trashList = new List<Trash>();

    private void Start()
    {
        CreatedTrashes();
    }

    private void AddInTrashList(Trash trash)
    {
        trashList.Add(trash);
        trash.SetRoadParent(this);
    }

    public void RemoveFromTrashList(Trash trash)
    {
        trashList.Remove(trash);
    }

    private void OnDisable()
    {
        foreach (var t in trashList)
        {
            Destroy(t.gameObject);
        }

        trashList.Clear();
    }
    
    private void CreatedTrashes()
    {
        if(trashes.Count == 0) return;
        
        for (var i = 0; i < Random.Range(1, maxCount); i++)
        {
            var spawnPos = new Vector3(Random.Range(transform.position.x - 3, transform.position.x + 3), 1.01f, Random.Range(transform.position.z - 14, transform.position.z + 14));

            AddInTrashList(Instantiate(trashes[Random.Range(0, trashes.Count)], spawnPos, Quaternion.identity).GetComponent<Trash>());
        }
    }
}
