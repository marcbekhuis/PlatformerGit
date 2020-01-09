using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy *", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [SerializeField] private Vector3 size;
    [Space]
    [Header("Health System")]
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask player;
    [Space]
    [SerializeField] private Vector3 damageCheckSize;
    [SerializeField] private Vector3 damageCheckOffset;
    [Space]
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask ground;
    [Space]
    [SerializeField] private Vector3 groundCheckSize;
    [SerializeField] private Vector3 groundCheckOffsetLeft;
    [SerializeField] private Vector3 groundCheckOffsetRight;
    [Space]
    [SerializeField] private Vector3 wallCheckSize;
    [SerializeField] private Vector3 wallCheckOffsetLeft;
    [SerializeField] private Vector3 wallCheckOffsetRight;

    public void Setup(EnemyMovement enemyMovement, MeshFilter meshFilter, MeshRenderer meshRenderer, GameObject enemy, EnemyHealth enemyHealth)
    {
        enemy.transform.localScale = size;
        enemy.name = name;

        enemyMovement.speed = speed;
        enemyMovement.ground = ground;

        enemyMovement.groundCheckSize = groundCheckSize;
        enemyMovement.groundCheckOffsetLeft = groundCheckOffsetLeft;
        enemyMovement.groundCheckOffsetRight = groundCheckOffsetRight;


        enemyMovement.wallCheckSize = wallCheckSize;
        enemyMovement.wallCheckOffsetLeft = wallCheckOffsetLeft;
        enemyMovement.wallCheckOffsetRight = wallCheckOffsetRight;

        meshFilter.mesh = mesh;
        meshRenderer.material = material;

        enemyHealth.wallCheckSize = wallCheckSize;
        enemyHealth.wallCheckOffsetLeft = wallCheckOffsetLeft;
        enemyHealth.wallCheckOffsetRight = wallCheckOffsetRight;
        enemyHealth.health = health;
        enemyHealth.damage = damage;
        enemyHealth.player = player;
        enemyHealth.damageCheckOffset = damageCheckOffset;
        enemyHealth.damageCheckSize = damageCheckSize;
    }
}
