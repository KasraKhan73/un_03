using Prototype.AudioCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SBabchuk
{
    [DisallowMultipleComponent]
    [AddComponentMenu( "Audio/Click Sound", 100 )]
    public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
    {
        public static void Play()
        {
            AudioController.PlaySound("choose_crystal");
        }

        void IPointerClickHandler.OnPointerClick( PointerEventData eventData )
        {
            Play();
        }
    }
}
