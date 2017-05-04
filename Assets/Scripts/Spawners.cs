using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject[] ball;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public AudioSource audi;

    int randEnemy;

    // Use this for initialization
    void Start()
    {
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        //random spawn time between each object
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            randEnemy = Random.Range(0, 2);
            //Spawning on vector Positions(x,y,z)
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1); //Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(ball[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }

}
