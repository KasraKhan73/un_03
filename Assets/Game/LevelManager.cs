using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // Масив кнопок рівнів

    void Start()
    {
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1); // Кількість відкритих рівнів
        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0); // Кількість завершених рівнів

        // Налаштовуємо кожну кнопку рівня
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Отримуємо внутрішні елементи кнопки
            Button button = levelButtons[i];
            Transform checkmark = button.transform.Find("Checkmark"); // Галочка
            Transform lockIcon = button.transform.Find("Lock");      // Замочок
            Text levelText = button.GetComponentInChildren<Text>();  // Номер рівня

            // Ініціалізація видимості
            checkmark.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(false);
            levelText.text = (i + 1).ToString();

            if (i + 1 <= unlockedLevels) // Якщо рівень відкритий
            {
                button.interactable = true; // Активна кнопка
                if (i + 1 <= completedLevels) // Якщо рівень пройдений
                {
                    checkmark.gameObject.SetActive(true); // Показуємо галочку
                }
            }
            else // Якщо рівень заблокований
            {
                button.interactable = false; // Заблокована кнопка
                lockIcon.gameObject.SetActive(true); // Показуємо замочок
            }
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        // Оновлюємо стан завершеного рівня
        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0);
        if (levelIndex > completedLevels)
        {
            PlayerPrefs.SetInt("CompletedLevels", levelIndex);
        }

        // Відкриваємо наступний рівень
        UnlockNextLevel();
    }

    public void UnlockNextLevel()
    {
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        if (unlockedLevels < levelButtons.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels + 1);
        }
    }
}
