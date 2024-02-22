
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicSchoolController : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("SlidePuzzle");
    }


    public void CloseMap()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void GoToMap()
    {
        SceneManager.LoadSceneAsync("Map");
    }
    
}