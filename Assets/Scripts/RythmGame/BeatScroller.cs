using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public float spawnInterval;

    public bool playing;

    public GameObject[] arrowPrefabs;
    public Transform[] arrowSpawnPoints;
    private int spawns;

    public static BeatScroller instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }


    public void StartPlaying(int difficulty)
    {
        playing = true;

        spawns = Mathf.Max(Mathf.Min(difficulty,3), 1);


        StartCoroutine(SpawnArrowsCoroutine());
    }

    IEnumerator SpawnArrowsCoroutine()
    {
        while (playing)
        {
            List<int> usedIndexes = new List<int>();
            
            int arrowsToSpawn = Random.Range(1, spawns + 1);

            for (int i=0; i<arrowsToSpawn; i++)
            {
                int index = GetUniqueRandomIndex(usedIndexes);
                usedIndexes.Add(index);

                GameObject arrowPrefab = arrowPrefabs[index];
                Transform spawnPoint = arrowSpawnPoints[index];

                GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);

                Coroutine moveCoroutine = StartCoroutine(MoveArrowCoroutine(arrow));

                NoteObject noteObject = arrow.GetComponent<NoteObject>();
                noteObject.SetMoveCoroutine(moveCoroutine);

                
            }


            int beatsToWait = Random.value > 0.66f ? 1 : 2;

            yield return new WaitForSeconds(beatsToWait*spawnInterval);
        }
    }


    int GetUniqueRandomIndex(List<int> usedIndexes)
    {
        int index;

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
