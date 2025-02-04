using Extension;
using UnityEngine;
using UnityEngine.UI;
using Prototype.SceneLoaderCore.Helpers;

namespace Prototype
{
    public class GoToScene : MonoBehaviour
    {
        [Header("Next Scene")]
        [Scene]
        public string target;

        public void LoadScene()
        {
            SceneLoader.Instance.SwitchToScene(target);
        }
    }
}
