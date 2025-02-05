using MoreMountains.NiceVibrations;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Vibration
{
    [System.Serializable]
    public class HapticTypeClass
    {
        [Header("Name (to be accessible)")]
        public string name;

        [Header("Patterns")]
        public long[] pattern;

        [Header("Amplitudes")]
        public int[] amplitudes;

        [Header("Looping")]
        public bool repeat;
    }

    public class VibrationManager : MonoBehaviour
    {
        public static VibrationManager Instance;
        
        [Header("List of own vibration types")]
        public List<HapticTypeClass> hapticTypes;

        protected void OnDisable()
        {
            MMVibrationManager.iOSReleaseHaptics();
        }

        protected void Awake()
        {
            Instance = this;
            
            MMVibrationManager.iOSInitializeHaptics();
        }

        public void TriggerNoDefault(string _name)
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            if (MMVibrationManager.Android())
            {
                var _hapticTypes = hapticTypes.Find((x) => x.name == _name);
                if (_hapticTypes != null)
                {
                    MMVibrationManager.AndroidVibrate(_hapticTypes.pattern, _hapticTypes.amplitudes, _hapticTypes.repeat ? 1 : -1);
                }
            }
            else
            {
                TriggerDefault();
            }
        }

        public void TriggerNoDefault(int _id)
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            if (MMVibrationManager.Android())
            {
                HapticTypeClass _hapticTypes = hapticTypes[_id];
                if (_hapticTypes != null)
                {
                    MMVibrationManager.AndroidVibrate(_hapticTypes.pattern, _hapticTypes.amplitudes, _hapticTypes.repeat ? 1 : -1);
                }
            }
            else
            {
                TriggerDefault();
            }
        }
        public void TriggerDefault()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

#if UNITY_IOS || UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }

        public void TriggerVibrate()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Vibrate();
        }

        public void TriggerSelection()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.Selection);
        }

        public void TriggerSuccess()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.Success);
        }

        public void TriggerWarning()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.Warning);
        }

        public void TriggerFailure()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.Failure);
        }

        public void TriggerLightImpact()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        public void TriggerMediumImpact()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        public void TriggerHeavyImpact()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}
