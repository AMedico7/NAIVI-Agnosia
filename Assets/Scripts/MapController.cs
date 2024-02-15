using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    // Level Buttons
    public Button[] levels;
    // Unlocked level

    private void Awake()
    {
        // Set all levels to not interactable not visible

        int unlockedLevel = StateNameController.unlockedLevel;

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
            levels[i].gameObject.SetActive(false);
        }

        // Set done unlocked levels to interactable
        for (int i = 0; i < unlockedLevel; i++)
        {
            levels[i].interactable = true;
        }

        // Set unlocked levels and next level visible
        for (int i = 0; i <= unlockedLevel; i++)
        {
            levels[i].gameObject.SetActive(true);
        }
    }


    public void GoTo(int levelId)   {
        StateNameController.destinationLevel = levelId;
        SceneManager.LoadScene("Transition");
    }

    public void CloseMap()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
