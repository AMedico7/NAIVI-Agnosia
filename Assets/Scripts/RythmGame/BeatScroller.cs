using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool playing;

    public GameObject[] arrowPrefabs;
    public Transform[] arrowSpawnPoints;
    public float spawnInterval;

    public static BeatScroller instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        beatTempo = beatTempo / 60f;
        spawnInterval = beatTempo;
    }


    public void StartPlaying()
    {
        playing = true;
        StartCoroutine(SpawnArrowsCoroutine());
    }

    IEnumerator SpawnArrowsCoroutine()
    {
        while (playing)
        {

            int index = Random.Range(0, 4);

            GameObject arrowPrefab = arrowPrefabs[index];
            Transform spawnPoint = arrowSpawnPoints[index];

            GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);

            Coroutine moveCoroutine = StartCoroutine(MoveArrowCoroutine(arrow));

            NoteObject noteObject = arrow.GetComponent<NoteObject>();
            noteObject.SetMoveCoroutine(moveCoroutine);

            yield return new WaitForSeconds(spawnInterval);
        }
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
