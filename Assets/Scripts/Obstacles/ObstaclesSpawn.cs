using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawn : MonoBehaviour
{
   public Transform spawnPoint;
   
   public List<GameObject> obstacles;

   public Vector3 prevPosition;

   private void Start()
   {
      Spawn();
   }

   private void Spawn()
   {
      prevPosition = new Vector3(0, 0, spawnPoint.position.z);
      
      for (var i = 0; i < obstacles.Count; i++)
      {
         var ob = obstacles[i].GetComponent<ObstacleController>();

         var go = Instantiate(obstacles[i], transform);
         
         go.transform.localPosition = prevPosition;
         
         prevPosition += new Vector3(0, 0, (int)ob.width);
      }
   }
}
