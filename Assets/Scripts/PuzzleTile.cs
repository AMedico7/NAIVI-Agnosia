using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTile : MonoBehaviour
{
    public int id;
    private Text tileText;
    public Image tileImage;


    public void SetId(int newId, int cellSize, Sprite puzzleSprite, int gridSize)
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

            SetImage(puzzleSprite, cellSize, gridSize);
        }

        // Check if it is the last tile
        if (newId == Mathf.Pow(SlidePuzzleController.Instance.gridSize, 2))
        {
            tileImage.enabled = false;
        }
    }


    private void SetImage(Sprite puzzleSprite, int cellSize, int gridSize)
    {   
        int row = (id - 1) / gridSize;
        int col = (id - 1) % gridSize;

        float imageWidth = puzzleSprite.rect.width;
        float imageHeight = puzzleSprite.rect.height;

        float cellWidth = imageWidth / gridSize;
        float cellHeight = imageHeight / gridSize;

        Rect rect = new Rect(col * cellWidth, (gridSize - 1 - row) * cellHeight, cellWidth, cellHeight);

        // Create a new sprite from the specified region of the original image
        Sprite tileSprite = Sprite.Create(puzzleSprite.texture, rect, new Vector2(0.5f, 0.5f), cellSize / cellWidth);

        // Set the sprite of the Image component
        tileImage.sprite = tileSprite;

        // Adjust the size of the Image component to match the cell size
        RectTransform imageRect = tileImage.GetComponent<RectTransform>();
        if (imageRect != null)
        {
            imageRect.sizeDelta = new Vector2(cellSize, cellSize);
        }
    }


    void Start()
    {
        tileText = GetComponentInChildren<Text>();
        if (tileText != null)
        {
            tileText.text = id.ToString();
            tileText.gameObject.SetActive(false);
        }


        void Start()
    {
        tileText = GetComponentInChildren<Text>();
        if (tileText != null)
        {
            tileText.text = id.ToString();

            // Set the text not active by default
            tileText.gameObject.SetActive(false);
        }
    }
    }

    public void OnTileClicked()
    {
        SlidePuzzleController.Instance.TileClicked(id);
    }


    private void SetTextVisibility(bool isVisible)
    {
        if (tileText != null)
        {
            tileText.gameObject.SetActive(isVisible);
        }
    }

    void Update()
    {
        // Toggle text
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTextVisibility(!tileText.gameObject.activeSelf);
        }
    }
}
