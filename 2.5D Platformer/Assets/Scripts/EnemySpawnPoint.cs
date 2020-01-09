using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = new GameObject();
        spawnedEnemy.transform.position = this.transform.position;

        spawnedEnemy.AddComponent<BoxCollider>();
        spawnedEnemy.AddComponent<Rigidbody>();

        EnemyMovement enemyMovement = spawnedEnemy.AddComponent<EnemyMovement>();
        EnemyHealth enemyHealth = spawnedEnemy.AddComponent<EnemyHealth>();

        MeshRenderer meshRenderer = spawnedEnemy.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = spawnedEnemy.AddComponent<MeshFilter>();

        enemy.Setup(enemyMovement, meshFilter, meshRenderer, spawnedEnemy, enemyHealth);
    }
}
