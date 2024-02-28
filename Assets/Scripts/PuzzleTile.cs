using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTile : MonoBehaviour
{
    public int id;
    private Text tileText;
    public Image tileImage;


    public void SetId(int newId, float cellSize)
    {
        id = newId;
        tileText = GetComponentInChildren<Text>();
        tileImage = GetComponentInChildren<Image>();

        if (tileText != null)
        {
            tileText.text = id.ToString();

            RectTransform textRect = tileText.GetComponent<RectTransform>();
            if (textRect != null)
            {
                textRect.sizeDelta = new Vector2(cellSize, cellSize);
            }
        }


        if (tileImage != null)
        {
            RectTransform imageRect = tileImage.GetComponent<RectTransform>();
            if (imageRect != null)
            {
                imageRect.sizeDelta = new Vector2(cellSize, cellSize);
            }
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
