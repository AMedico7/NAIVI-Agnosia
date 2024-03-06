using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public GameObject background;
    private Dialogue dialogue;
    private Image backgroundImage;

    public Image character;
    private Sprite characterSprite;

    public Sprite[] backgrounds;

    public Sprite[] characters;  // Dr. P, Señora. P, Dr. Sacks, Alumno

    public bool inDialogue;

    public static DialogueManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
        // Set dialogue and dialogue lines
        dialogue = dialogueBox.GetComponent<Dialogue>();
        backgroundImage = background.GetComponent<Image>();

        backgroundImage.sprite = backgrounds[StateNameController.location];
        SetDialogueLines(StateNameController.lines);

        characterSprite = character.GetComponent<Sprite>();

        inDialogue = true;
    }


    public void Update() {
    
        if(Input.GetMouseButtonDown(0) && !inDialogue){
            StateNameController.ExitDialogue();
        }
    }

    public void SetDialogueLines(string[] newLines)
    {
        dialogue.lines = newLines;
        dialogue.index = 0;
    }

    public void SetCharacterImage(string line)
    {
        int characterIndex = -1;

        if (line.StartsWith("Dr. P"))
        {
            characterIndex = 0;
        } 
        else if (line.StartsWith("Señora P"))
        {
            characterIndex = 1;
        }
        else if (line.StartsWith("Dr. S"))
        {
            characterIndex = 2;
        }
        else if (line.StartsWith("Alumno"))
        {
            characterIndex = 3;
        }

        if (characterIndex != -1 && characterIndex < characters.Length)
        {   
            character.gameObject.SetActive(true);
            characterSprite = characters[characterIndex];
        }
        else
        {
            character.gameObject.SetActive(false);
            characterSprite = null; // Set to null if it is an unknown character
        }
        character.sprite = characterSprite;
    }
}
