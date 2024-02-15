using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    public int difficulty;
    void Awake()
    {
        difficulty = StateNameController.destinationLevel;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("SlidePuzzle");
        }
    }
}
