using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void New()
    {
        StateNameController.unlockedLevel = 1;
        SceneManager.LoadSceneAsync("Map");
    }

    public void Continue()
    {
        StateNameController.unlockedLevel = 2;
        SceneManager.LoadSceneAsync("Map");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
