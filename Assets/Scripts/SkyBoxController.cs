using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkyBoxController : MonoBehaviour
{
    public List<SkyBox> skyBoxes;

    public List<SkyBox> skyBoxes2;

    public bool is2d;

    private List<int> indexes;
    
    private void Awake()
    {
        indexes = new List<int>(){0,1,2,3,4};
        
        Change();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Change();
        }
    }

    public void ChangeMode()
    {
        is2d = !is2d;
        
        Change();
    }

    private void Change()
    {
        var index = indexes[Random.Range(0, indexes.Count)];

        indexes.Remove(index);
        
        if(indexes.Count == 0)
            indexes = new List<int>(){0,1,2,3,4};

        if (is2d)
        {
            RenderSettings.skybox = skyBoxes2[index].material;

            RenderSettings.fogColor = skyBoxes2[index].fogColor;
        }
        else
        {
            RenderSettings.skybox = skyBoxes[index].material;

            RenderSettings.fogColor = skyBoxes[index].fogColor;
        }
    }
}


[System.Serializable]
public class SkyBox
{
    public Material material;

    public Color fogColor;
}
