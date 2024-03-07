using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;


public class SlidePuzzleController : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public Sprite[] puzzleSprites;
    public int puzzleNumber = 0;
    public PuzzleTile puzzleTilePrefab;

    private int solvedTimes = 0;

    // Singleton instance
    private static SlidePuzzleController _instance;
    public static SlidePuzzleController Instance
    {
        get
        {
            if (_instance == null)
            {
                // If the instance is null, find it in the scene
                _instance = FindObjectOfType<SlidePuzzleController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SlidePuzzleControllerSingleton");
                    _instance = singletonObject.AddComponent<SlidePuzzleController>();
                }
            }

            return _instance;
        }
    }

    public int gridSize = 3;
    private List<PuzzleTile> tiles;

    private void Start()
    {

    
    // Define sizes based on gridsize
    int spacing;
    int cellSize;


    switch (gridSize)
    {
        case 3:
            spacing = 10;
            cellSize = 280;
            break;
        case 4:
            spacing = 10;
            cellSize = 205;
            break;
        case 5:
            spacing = 10;
            cellSize = 160;
            break;

        default:
            spacing = 10;
            cellSize = 280;
            break;
    }

    gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
    gridLayoutGroup.spacing = new Vector2(spacing, spacing);

        
        tiles = new List<PuzzleTile>();

        for (int i = 0; i < gridSize * gridSize; i++)
        {
            PuzzleTile child = Instantiate(puzzleTilePrefab, gridLayoutGroup.transform);
            child.transform.SetParent(gridLayoutGroup.transform, false);
            tiles.Add(child);
            child.SetId(i + 1, cellSize, puzzleSprites[puzzleNumber], gridSize);
        }

        ShuffleList(tiles);

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].transform.SetSiblingIndex(i);
        }
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.P))
       {
        PrintTileOrder();
       }
       
       if (Input.GetMouseButtonDown(0)){
            if (IsPuzzleSolved()){

                if (solvedTimes >= 1)
                {
                    StateNameController.Next();
                }
                else
                {
                    RestartPuzzle(puzzleNumber+1, gridSize+1);
                }
            }
       }

       
    }

    private void ShuffleList<T>(List<T> list)
    {

        // TODO: FIND SHUFFLING ALOGRITHM THAT DOES NOT PRODUCE UNSOLVABLE STATES
        // FOR NOW IT IS HARDCODED
        int[] order;

        switch (gridSize)
        {
            case 3:
                order = new int[] {1, 8, 2, 9, 4, 3, 7, 6, 5};
                break;
            case 4:
                order = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 16, 13, 14, 15, 12};
                break;
            case 5:
                order = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,25,24};
                break;
            default:
                order = new int[] {1, 8, 2, 9, 4, 3, 7, 6, 5};
                break;
        }


        T[] tempArray = new T[list.Count];

        for (int i=0; i < list.Count; i++)
        {
            tempArray[i] = list[i];
        }

        for (int i=0; i < list.Count; i++)
        {
            list[i] = tempArray[order[i] -1];
        }
    }



    public void TileClicked(int clickedId){
        
        if (IsPuzzleSolved()){

            return;
        }

        if (clickedId == gridSize*gridSize)
        {
            return;
        }


        PuzzleTile clickedTile = tiles.Find(tile => tile.id == clickedId);


        int clickedIndex = tiles.IndexOf(clickedTile);
        int emptyIndex = tiles.FindIndex(tile => tile.id == gridSize*gridSize);


    
        if (IsAdjacent(clickedIndex, emptyIndex))
        {

            SwapTiles(clickedIndex, emptyIndex);

            if (IsPuzzleSolved())
            {
                tiles[tiles.Count - 1].tileImage.enabled = true;
            }
        }
    }


    private bool IsAdjacent(int index1, int index2)
    {
        int row1 = index1 / gridSize;
        int col1 = index1 % gridSize;

        int row2 = index2 / gridSize;
        int col2 = index2 % gridSize;

        return (Mathf.Abs(row1 - row2) == 1 && col1 == col2) || (Mathf.Abs(col1 - col2) == 1 && row1 == row2);
    }

    private void SwapTiles(int index1, int index2)
    {
        PuzzleTile temp = tiles[index1];
        tiles[index1] = tiles[index2];
        tiles[index2] = temp;

        tiles[index1].transform.SetSiblingIndex(index1);
        tiles[index2].transform.SetSiblingIndex(index2);
    }

    private bool IsPuzzleSolved()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].id != i+1)
            {
                return false;
            }
        }
        return true;
    }


    private void PrintAdjacentTiles(int clickedId)
    {
        PuzzleTile clickedTile = tiles.Find(tile => tile.id == clickedId);


        int clickedIndex = tiles.IndexOf(clickedTile);
        int emptyIndex = tiles.FindIndex(tile => tile.id == 9);

        List<int> adjacentTileIds = new List<int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            if (IsAdjacent(i, clickedIndex))
            {
                adjacentTileIds.Add(tiles[i].id);
            }
        }

        Debug.Log("Adjacent Tiles IDs: " + string.Join(", ", adjacentTileIds));
    }

    private void PrintTileOrder()
    {
        Debug.Log("Tile Order: " + string.Join(", ", tiles.Select(tile => tile.id)));
    }

    private void PrintSiblingIndexes()
    {
        Debug.Log("Sibling Ids: " + string.Join(", ", tiles.Select(tiles => tiles.transform.GetSiblingIndex())));
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync("Map");
    }

    public void RestartPuzzle(int newPuzzleNumber, int newGridSize)
    {
        // Clear existing tiles
        foreach (PuzzleTile tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        // Set new puzzle number and grid size
        puzzleNumber = newPuzzleNumber;
        gridSize = newGridSize;
        solvedTimes = solvedTimes+1;

        // Reinitialize puzzle with new settings
        Start();
    }
}