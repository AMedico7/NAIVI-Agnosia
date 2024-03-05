using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller beatScroller;


    public int difficulty = 1;
    public GameObject backgroundObject;
    public Material[] backgroundMaterials;

    public int score;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   

        // Set the appropiate background material

        Renderer quadRenderer = backgroundObject.GetComponent<Renderer>();

        if (quadRenderer != null)
        {
            quadRenderer.material = backgroundMaterials[Mathf.Min(difficulty-1, backgroundMaterials.Length-1)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.StartPlaying();

                backgroundObject.GetComponent<ScrollingBg>().speed = 0.1f;

                music.Play();
            }
        }


        if (score >= 500){
            StateNameController.GoToDestination();
        }
    }


    public void NoteHit()
    {
        Debug.Log("HIT");
        score += 100;
    }

    public void NoteMissed()
    {
        Debug.Log("MISS");
    }
}
