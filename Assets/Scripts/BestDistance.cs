using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestDistance : MonoBehaviour
{
   private Transform _transform;

   private Projector _projector;
   
   private void OnEnable()
   {
      PlayerController.OnFlying += Show;

   }

   private void OnDisable()
   {
      PlayerController.OnFlying -= Show;
   }

   private void Awake()
   {
      _transform = GetComponent<Transform>();

      _projector = GetComponent<Projector>();
   }
   
   private void Start()
   {
      Change(PlayerPrefs.GetInt("BestDistance", 0));
   }

   private void Show()
   {
      _projector.enabled = true;
   }

   private void Change(int distance)
   {
      _transform.position += new Vector3(0,0, distance);
   }
}
