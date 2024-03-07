using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void New()
    {
        StateNameController.NewGame();
    }

    public void Continue()
    {
        StateNameController.ContinueGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
