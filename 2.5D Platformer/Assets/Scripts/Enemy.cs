using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy *", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [Space]
    [Header("Health System")]
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 1;
    [Space]
    [Header("Movement")]
    [SerializeField] private float speed = 5;

    public void Setup(EnemyMovement enemyMovement, MeshFilter meshFilter, MeshRenderer meshRenderer)
    {
        enemyMovement.speed = speed;

        meshFilter.mesh = mesh;
        meshRenderer.material = material;
    }
}
