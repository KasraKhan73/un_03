using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WidthType
{
   Single = 12,
   Double = 24
}

public class ObstacleController : MonoBehaviour
{
   public WidthType width;
}
