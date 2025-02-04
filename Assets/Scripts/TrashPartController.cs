using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPartController : MonoBehaviour
{
    public List<MaterialController> materialControllers;
    
    public void Init(PlaneColors color)
    {
        foreach (var material in materialControllers)
        {
            material.SetMaterial(color);
        }
    }
}
