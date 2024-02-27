using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateNameController : MonoBehaviour
{
    public static int unlockedLevel;
    public static int destinationLevel;
    
    public static void GoToDestination()
    {

        if (destinationLevel == 1){
            SceneManager.LoadScene("SlidePuzzle");
        } 
        else {
            SceneManager.LoadScene("Map");
        }
    }
}
