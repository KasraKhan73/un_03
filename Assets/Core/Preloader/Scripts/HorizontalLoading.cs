using System.Collections;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Preloader
{
    [DefaultExecutionOrder(-100)]
    public class HorizontalLoading : MonoBehaviour
    {
        private Slider progressSlider;

        private const float InitialProgressValue = 0.25f;
        private const float ProgressSpeed = 0.8f;

        private void Start()
        {
            InitializeProgressSlider();
            
            if (progressSlider != null)
            {
                StartCoroutine(LoadProgress());
            }
        }

        private void InitializeProgressSlider()
        {
            progressSlider = GetComponentInChildren<Slider>();
            
            if (progressSlider == null)
            {
                Debug.LogError("Slider not found in children!");
                
                return;
            }

            SetInitialProgressValue(InitialProgressValue);
        }

        private void SetInitialProgressValue(float value)
        {
            progressSlider.value = value;
        }

        private IEnumerator LoadProgress()
        {
            while (progressSlider.value < progressSlider.maxValue)
            {
                UpdateProgressValue();
                
                yield return null;
            }

            OnLoadingComplete();
        }

        private void UpdateProgressValue()
        {
            progressSlider.value += ProgressSpeed * Time.deltaTime;
        }

        private void OnLoadingComplete()
        {
            SceneLoader.Instance.SwitchToScene(SceneLoader.Instance.mainScene);
        }
    }
}