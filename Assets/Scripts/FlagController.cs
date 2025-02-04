using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private Transform _transform;
    
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

        Show();
    }
    
    private void Show()
    {
        var pos = new Vector3(
            PlayerPrefs.GetFloat("_X", transform.position.x),
            PlayerPrefs.GetFloat("_Y", transform.position.y + 50),
            PlayerPrefs.GetFloat("_Z", transform.position.z));
        
        RaycastHit hit;
        if (PlayerPrefs.GetInt("IsRaycast", 0) == 1)
        {
            if (Physics.Raycast(pos, -Vector3.up, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                pos.y = hit.point.y;
                _transform.position = pos;
            
                _transform.up = hit.normal;
            }
        }
        else
        {
            _transform.position = new Vector3(
                PlayerPrefs.GetFloat("_X", transform.position.x),
                PlayerPrefs.GetFloat("_Y", transform.position.y),
                PlayerPrefs.GetFloat("_Z", transform.position.z));
        }
    }
}
