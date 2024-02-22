﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;


public class SlidePuzzleController : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public Image puzzleImage;

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

        puzzleImage.gameObject.SetActive(false);
        tiles = new List<PuzzleTile>();
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {

            PuzzleTile child = gridLayoutGroup.transform.GetChild(i).GetComponent<PuzzleTile>();
            tiles.Add(child);

            child.SetId(i+1);
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
    }

    private void ShuffleList<T>(List<T> list)
    {

        // TODO: FIND SHUFFLING ALOGRITHM THAT DOES NOT PRODUCE UNSOLVABLE STATES
        // FOR NOW IT IS HARDCODED

        int[] order = {1,8,2,9,4,3,7,6,5};
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

        if (clickedId == 9)
        {
            return;
        }


        PuzzleTile clickedTile = tiles.Find(tile => tile.id == clickedId);


        int clickedIndex = tiles.IndexOf(clickedTile);
        int emptyIndex = tiles.FindIndex(tile => tile.id == 9);


    
        if (IsAdjacent(clickedIndex, emptyIndex))
        {

            SwapTiles(clickedIndex, emptyIndex);

            if (IsPuzzleSolved())
            {
                tiles[tiles.Count - 1].tileImage.enabled = true;
                Debug.Log("PUZZLE SOLVED");
                puzzleImage.gameObject.SetActive(true);
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
}