using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateNameController : MonoBehaviour
{
    public static int unlockedLevel = 0;
    public static int currentPosition = 0;
    public static int destinationLevel;
    
    public static void GoToDestination()
    {

        if (destinationLevel == 0){
            SceneManager.LoadScene("SlidePuzzle");
        } 
        else {
            SceneManager.LoadScene("Map");
        }
    }

    public static void GoToLevel(int levelId)
    {
        destinationLevel = levelId;
        currentPosition = levelId;
        SceneManager.LoadScene("RythmGame");
    }
}
