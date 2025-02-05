using UnityEngine;
using UnityEngine.UI;

namespace Prototype.Vibration
{
    public class SwitchVibration : MonoBehaviour
    {
        public Sprite enable;

        public Sprite disable;
        
        private Image bttn;

        private void Awake()
        {
            bttn = GetComponent<Image>();
        }

        private void Start()
        {
            UpdateBttn();
        }

        public void SetEnableVibro()
        {
            VibrationSettings.EnableVibro(!VibrationSettings.IsVibroEnabled());

            UpdateBttn();
        }

        private void UpdateBttn()
        {
            Debug.Log(VibrationSettings.IsVibroEnabled());

            if (enable == null || disable == null)
            {
                var clr = VibrationSettings.IsVibroEnabled()?  Color.green : Color.red;

                bttn.color = clr;
            }
            else
            {
                bttn.sprite = VibrationSettings.IsVibroEnabled() ? enable : disable;
            }
        }
    }
}
