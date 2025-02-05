using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCountUI : MonoBehaviour
{
    [SerializeField] Text scoreText;

    #region Subscribe
    private void OnEnable()
    {
        PlayerController._OnTrashCountChanged += OnTrashCountChanged;
    }
    private void OnDisable()
    {
        PlayerController._OnTrashCountChanged -= OnTrashCountChanged;
    }
    #endregion

    private void Start()
    {
        scoreText.text = "0";
    }
    private void OnTrashCountChanged(int trashCount)
    {
        scoreText.text = trashCount.ToString();
    }
}
