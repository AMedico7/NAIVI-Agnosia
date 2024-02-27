using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmButtonController : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            spriteRenderer.sprite = pressedImage;
        }

        if(Input.GetKeyUp(key))
        {
            spriteRenderer.sprite = defaultImage;
        }
    }
}
