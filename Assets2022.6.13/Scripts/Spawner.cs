using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public float spawndelay = 2f;
    public float spawntime = 5f;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawndelay, spawntime);
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemies.Length);
        Instantiate(enemies[index], transform.position, transform.localRotation);
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
