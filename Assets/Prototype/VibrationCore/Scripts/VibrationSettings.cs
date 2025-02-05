using UnityEngine;

namespace Prototype.Vibration
{

    public class VibrationSettings : MonoBehaviour
    {
        private static bool actual;
        
        private static bool vibroON = true;
        
        public static void UpdateSettings()
        {
            if (actual) return;
            
            vibroON = PlayerPrefs.GetInt("vibro_on", 1) != 0;
            actual = true;
        }
        
        public static bool IsVibroEnabled()
        {
            return vibroON;
        }
        
        public static void EnableVibro(bool param)
        {
            vibroON = param;
            PlayerPrefs.SetInt("vibro_on", vibroON == false ? 0 : 1);
            SaveSettings();
        }
        
        private static void SaveSettings()
        {
            PlayerPrefs.Save();
        }
    }
}
