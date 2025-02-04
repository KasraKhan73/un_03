using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour
{
   public GameObject part;

   public bool isShowing;

   public void Show()
   {
      part.SetActive(true);

      isShowing = true;
   }

   public void Hide()
   {
      part.SetActive(false);

      isShowing = false;
   }
}
