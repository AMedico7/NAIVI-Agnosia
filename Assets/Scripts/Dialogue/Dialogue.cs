using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        DialogueManager.instance.SetCharacterImage(lines[index]);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach ( char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            DialogueManager.instance.SetCharacterImage(lines[index]);
            StartCoroutine(TypeLine());
        } else 
        {
            DialogueManager.instance.SetCharacterImage("");
            gameObject.SetActive(false);
            // Go to the next step
            DialogueManager.instance.inDialogue = false;
        }
    }
}
