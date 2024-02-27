using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    private bool hasBeenHit = false;

    private Coroutine moveCoroutine;

    void Start()
    {
        
    }

    public void SetMoveCoroutine(Coroutine coroutine)
    {
        moveCoroutine = coroutine;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                hasBeenHit = true;
                GameManager.instance.NoteHit();
                SelfDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator" && !hasBeenHit)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            BeatScroller.instance.StopCoroutine(moveCoroutine);
        }

        Destroy(gameObject);
    }
}
