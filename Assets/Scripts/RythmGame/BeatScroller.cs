﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool playing;

    public GameObject[] arrowPrefabs;
    public Transform[] arrowSpawnPoints;
    public float spawnInterval;
    private int spawns;

    public static BeatScroller instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        beatTempo = beatTempo / 60f;
        spawnInterval = beatTempo;
    }


    public void StartPlaying(int difficulty)
    {
        playing = true;
        StartCoroutine(SpawnArrowsCoroutine());
        spawns = difficulty;
    }

    IEnumerator SpawnArrowsCoroutine()
    {
        while (playing)
        {

            List<int> usedIndexes = new List<int>();

            for (int i=0; i<spawns; i++)
            {
                int index = GetUniqueRandomIndex(usedIndexes);

                GameObject arrowPrefab = arrowPrefabs[index];
                Transform spawnPoint = arrowSpawnPoints[index];

                GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);

                Coroutine moveCoroutine = StartCoroutine(MoveArrowCoroutine(arrow));

                NoteObject noteObject = arrow.GetComponent<NoteObject>();
                noteObject.SetMoveCoroutine(moveCoroutine);

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }


    int GetUniqueRandomIndex(List<int> usedIndexes)
    {
        int index;

        // Keep generating a random index until it is unique
        do
        {
            index = Random.Range(0, 4);
        } while (usedIndexes.Contains(index));

        return index;
    }

    IEnumerator MoveArrowCoroutine(GameObject arrow)
    {
        while (playing && arrow != null && arrow.activeSelf)
        {

            arrow.transform.Translate(Vector3.down * beatTempo * Time.deltaTime, Space.World);
            yield return null;
        }
    }    
    // Update is called once per frame
    void Update()
    {
    }
}
