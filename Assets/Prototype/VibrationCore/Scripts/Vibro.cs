using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Prototype.Vibration
{
    [System.Serializable]
    public enum TVibration
    {
        [Tooltip("Already created earlier")]
        Common,

        [Tooltip("Created independently")]
        Custom
    }

    public sealed class Vibro : MonoBehaviour
    {
        [Header("Vibration type (own or created)")]
        public TVibration type;

        [Header("The type of vibration created")]
        public HapticTypes hapticTypes;

        [Header("Patterns")]
        public long[] pattern;

        [Header("Amplitudes")]
        public int[] amplitudes;

        public void Trigger()
        {
            if (!VibrationSettings.IsVibroEnabled())
                return;

            if (type == TVibration.Common)
            {
                MMVibrationManager.Haptic(hapticTypes);
            }
            else
            {
                if (MMVibrationManager.Android())
                {
                    MMVibrationManager.AndroidVibrate(pattern, amplitudes, -1);
                }
                else
                {
                    MMVibrationManager.iOSTriggerHaptics(hapticTypes, false);
                }
            }
        }
    }
}
