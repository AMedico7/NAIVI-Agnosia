using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RythmGameManager : MonoBehaviour
{

    public AudioSource music;
    public float beatTempo;
    private bool startPlaying;
    public BeatScroller beatScroller;


    private int difficulty;
    public GameObject backgroundObject;
    public Material[] backgroundMaterials;


    

    private float drPspeed;
    public GameObject drP;
    private PeriodicSpriteAnimation spriteAnimation;



    public int score;
    public int scoreNeeded;

    public Slider progressBar;

    public static RythmGameManager instance;

       void Start()
    {
        instance = this;   
        
        difficulty = StateNameController.rythmGameDifficulty;
        scoreNeeded = 3000 * difficulty;


        // Set the appropiate background material
        Renderer quadRenderer = backgroundObject.GetComponent<Renderer>();

        if (quadRenderer != null)
        {
            quadRenderer.material = backgroundMaterials[Mathf.Min(difficulty-1, backgroundMaterials.Length-1)];
        }

        spriteAnimation = drP.GetComponent<PeriodicSpriteAnimation>();

        drPspeed = 0.4f;

        beatScroller.beatTempo = this.beatTempo / 60f;
        beatScroller.spawnInterval = (60f / this.beatTempo);


        progressBar.minValue = 0;
        progressBar.maxValue = scoreNeeded;
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

        // Progress bar
        progressBar.value = score;


        // Win condition
        if (score >= scoreNeeded){
            StateNameController.Next();
        }
        

        // Automatically win
        if (Input.GetKeyDown(KeyCode.W))
        {
            StateNameController.Next();
        }


        // Debug

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeBackgroundMaterial();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            backgroundObject.GetComponent<ScrollingBg>().SetSpeed(backgroundObject.GetComponent<ScrollingBg>().speed * -1);
            drP.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        
    }


    public void NoteHit()
    {
        // HIT NOTE
        score += 100;
        backgroundObject.GetComponent<ScrollingBg>().SetSpeed(0.1f);
        drP.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        
    }

    public void NoteMissed()
    {
        // MISS NOTE
        score -= 50;
        score = Mathf.Max(score, 0);

        backgroundObject.GetComponent<ScrollingBg>().SetSpeed(-0.1f);
        drP.transform.eulerAngles = new Vector3(0f, 180f, 0f);

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
