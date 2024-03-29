﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    // Level Buttons
    public Button[] levels;
    public GameObject drP;
    private Vector3 offset;
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
        for (int i = 0; i <= unlockedLevel; i++)
        {
            levels[i].interactable = true;
        }

        // Set unlocked levels and next level visible
        for (int i = 0; i <= unlockedLevel+1; i++)
        {
            if (levels.Length >= i+1)
            {
                levels[i].gameObject.SetActive(true);
            }
        }

    }


    public void Update() {
        if (StateNameController.currentPosition >= 0 && StateNameController.currentPosition < levels.Length)
        {
            // Get the position of the level associated with currentPos
            Vector3 levelPosition = levels[StateNameController.currentPosition].transform.position;

            // Set the position of the DrP GameObject to the level's position
            drP.transform.position = levelPosition + new Vector3(-3.5f, 35.0f, 0.0f);
        }
    }

    public void GoTo(int levelId)   {
        StateNameController.Next();
    }

    public void CloseMap()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
