using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTile : MonoBehaviour
{
    public int id;
    private Text tileText;
    private Image tileImage;

    private void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileImage = GetComponent<Image>();
    }

    public void SetId(int newId)
    {
        id = newId;

        Text tileText = GetComponentInChildren<Text>();
        if (tileText != null)
        {
            tileText.text = id.ToString();
        }
    }

    void Start()
    {
        Text tileText = GetComponentInChildren<Text>();
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
