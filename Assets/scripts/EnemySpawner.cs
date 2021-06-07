using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private float SpawnRadius = 3.0f;
    // Start is called before the first frame update
    void Awake() {
        Vector3 spwanLocation = new Vector3(this.gameObject.transform.position.x + SpawnRadius * Random.Range(0f, SpawnRadius) * (Random.Range(0, 2) *2 -1), 0f , this.gameObject.transform.position.z + SpawnRadius * Random.Range(0f, SpawnRadius) * (Random.Range(0, 2) *2 -1));
        Instantiate(this.EnemyPrefab, spwanLocation, Quaternion.identity);
    }
}
