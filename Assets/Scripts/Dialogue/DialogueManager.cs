using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public int location = 0;
    public GameObject dialogueBox;
    public GameObject background;
    private Dialogue dialogue;
    private Image backgroundImage;


    public Sprite[] backgrounds;
    public static DialogueManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
        // Set dialogue and dialogue lines
        dialogue = dialogueBox.GetComponent<Dialogue>();
        backgroundImage = background.GetComponent<Image>();
    }

    public void SetDialogueLines(string[] newLines)
    {
        dialogue.lines = newLines;
        dialogue.index = 0;
    }
}
