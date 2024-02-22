using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    public int destination;
    void Awake()
    {
        destination = StateNameController.destinationLevel;

        
    }

    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.S))
        {

            if (destination == 1){
                Debug.Log("MUSIC");
                SceneManager.LoadScene("SlidePuzzle");
            }

            Debug.Log("NOT A VALID DESTINATION");
        }
    }
}
