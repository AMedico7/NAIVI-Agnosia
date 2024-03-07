using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LinesPuzzleController : MonoBehaviour
{
    // Start is called before the first frame update
    public static LinesPuzzleController instance;

    public GameObject itemPrefab;
    public GridLayoutGroup leftLayout;
    public GridLayoutGroup rightLayout;


    public Sprite[] leftSprites;
    public Sprite[] rightSprites;

    public int toSpawn = 3;


    private List<GameObject> leftItems;
    private List<GameObject> rightItems;


    private int matches = 0;
    private int stage = 1;
    private int matchesNeeded = 3;

    public Canvas canvas;
    void Start()
    {
        instance = this;

        leftItems = new List<GameObject>();
        rightItems = new List<GameObject>();

        // Instantiate elements on the left side
        InstantiateElements(leftLayout, leftSprites, leftItems);

        // Instantiate elements on the right side
        InstantiateElements(rightLayout, rightSprites, rightItems);
    }

    void InstantiateElements(GridLayoutGroup layoutGroup, Sprite[] sprites, List<GameObject> itemList)
    {

        for (int i = 0;  i < toSpawn; i++)
        {
           // Instantiate a new itemPrefab
            GameObject itemObject = Instantiate(itemPrefab, layoutGroup.transform);

            // Get the MatchItem component from the instantiated item
            MatchItem matchItem = itemObject.GetComponent<MatchItem>();

            // Set the sprite of the item based on the provided array
            if (matchItem != null)
            {
                matchItem.id = i;
                matchItem.canvas = canvas;

                Image image = itemObject.GetComponent<Image>();
                image.sprite = sprites[i+(stage-1)*3];
                itemList.Add(itemObject);
            }

        }

        Shuffle(itemList);

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].transform.SetSiblingIndex(i);
        }
    }

    void ResetPuzzleElements()
    {
        // Clear existing elements
        foreach (Transform child in leftLayout.transform)
        {
            MatchItem matchItem = child.GetComponent<MatchItem>();
            if (matchItem != null && matchItem.line != null)
            {
                Destroy(matchItem.line);
            }

            Destroy(child.gameObject);
        }

        foreach (Transform child in rightLayout.transform)
        {
            MatchItem matchItem = child.GetComponent<MatchItem>();
            if (matchItem != null && matchItem.line != null)
            {
                Destroy(matchItem.line);
            }

            Destroy(child.gameObject);
        }


        // Increment stage and reset matches
        stage++;
        matches = 0;


        leftItems = new List<GameObject>();
        rightItems = new List<GameObject>();
        // Instantiate new elements for the current stage
        InstantiateElements(leftLayout, leftSprites, leftItems);
        InstantiateElements(rightLayout, rightSprites, rightItems);
    }



    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void CorrectMatch()
    {
        matches ++;
        if (matches >= matchesNeeded){
            if (stage == 1)
            {
               ResetPuzzleElements();
            } 
            else 
            {
                StateNameController.Next();
            }
        }
    }
}
