using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSpawner : MonoBehaviour
{
    public static event Action<Road> _OnCreatedRoad;

    [SerializeField] private List<GameObject> Roads;
    
    [SerializeField] private float SpawnDistance = 30;

    [SerializeField] private GameObject SlingShot;

    [SerializeField] private int maxRoads = 7;

    [SerializeField] private int countRoads = 4;
    
    [SerializeField] private Road LastRoad;
    
    #region Subscribe
    private void OnEnable()
    {
        RoadPoint._OnPlayerEntered += OnPlayerEntered;

        PlayerController.OnPlayerisStoped += CreateSlingShot;
    }
    private void OnDisable()
    {
        RoadPoint._OnPlayerEntered -= OnPlayerEntered;
        
        PlayerController.OnPlayerisStoped -= CreateSlingShot;
    }
    #endregion

    private void CreateSlingShot(Transform plane)
    {
        var slingshot = Instantiate(SlingShot, plane.position + new Vector3 (0,0,-0.6f), Quaternion.identity).GetComponent<SlingshotController>();
        
        slingshot.Init(plane);
    }

    private void OnPlayerEntered(GameObject roadToDestroy)
    {
        countRoads++;

        if (countRoads > maxRoads) return;

        if (countRoads == maxRoads)
        {
            LastRoad = Instantiate(Roads[1], LastRoad.transform.position + new Vector3(0, 0, SpawnDistance),
                Quaternion.identity).GetComponent<Road>();

            _OnCreatedRoad?.Invoke(LastRoad);

            Destroy(roadToDestroy);
        }
        else
        {
            LastRoad = Instantiate(Roads[0], LastRoad.transform.position + new Vector3(0, 0, SpawnDistance),
                Quaternion.identity).GetComponent<Road>();

            _OnCreatedRoad?.Invoke(LastRoad);

            Destroy(roadToDestroy);
        }
    }
}
