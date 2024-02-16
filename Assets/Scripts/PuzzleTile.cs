using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTile : MonoBehaviour
{
    public int id;
    private Text tileText;
    private Image tileImage;


    public void SetId(int newId)
    {
        id = newId;
        tileText = GetComponentInChildren<Text>();
        tileImage = GetComponentInChildren<Image>();

        if (tileText != null)
        {
            tileText.text = id.ToString();
        }


        // Check if it is the last tile
        if (newId == Mathf.Pow(SlidePuzzleController.Instance.gridSize, 2))
        {
            tileImage.enabled = false;
        }
    }

    void Start()
    {
        tileText = GetComponentInChildren<Text>();
        if (tileText != null)
        {
            tileText.text = id.ToString();
        }
    }

    public void OnTileClicked()
    {
        SlidePuzzleController.Instance.TileClicked(id);
    }
}
