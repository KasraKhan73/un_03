using Prototype.AudioCore;
using UnityEngine;
using UnityEngine.UI;

public class BttnClick : MonoBehaviour
{
    private Button _bttn;

    private void Awake()
    {
        _bttn = GetComponent<Button>();
        
        _bttn.onClick.AddListener(Click);
    }

    private void Click()
    {
        AudioController.PlaySound("ButtonClick", StreamGroup.FX);
    }
}
