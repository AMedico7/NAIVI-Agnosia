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




    private float drPspeed;
    public GameObject drP;
    private PeriodicSpriteAnimation spriteAnimation;

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

        spriteAnimation = drP.GetComponent<PeriodicSpriteAnimation>();

        drPspeed = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.StartPlaying(difficulty);

                backgroundObject.GetComponent<ScrollingBg>().speed = 0.1f;
                spriteAnimation.ChangeAnimationSpeed(drPspeed);

                music.Play();
            }
        }



        // Win condition
        if (score >= 50000){
            StateNameController.GoToDestination();
        }

        

        // Debug

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeBackgroundMaterial();
        }
        
    }


    public void NoteHit()
    {
        Debug.Log("DRPSPEED"+drPspeed);
        score += 100;
    }

    public void NoteMissed()
    {
        Debug.Log("MISS");
    }


    private void SetBackgroundMaterial()
    {
        Renderer quadRenderer = backgroundObject.GetComponent<Renderer>();

        if (quadRenderer != null)
        {
            quadRenderer.material = backgroundMaterials[Mathf.Min(difficulty-1, backgroundMaterials.Length-1)];
        }
    }

    private void ChangeBackgroundMaterial()
    {
        // Change background material (cycling through materials)
        difficulty++;
        if (difficulty > backgroundMaterials.Length)
        {
            difficulty = 1;
        }

        SetBackgroundMaterial();
    }
}
