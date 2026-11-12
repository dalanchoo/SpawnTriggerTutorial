using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class SpawnGroupTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField, Min(1)] private int totalToSpawn = 6;
    [SerializeField, Min(1)] private int maxAlive = 3;
    [SerializeField, Min(0f)] private float minDistanceFromPlayer = 3f; // new
    [SerializeField] private bool disableAfterUse = true;
    [SerializeField] private UnityEvent onTriggered;

    private readonly List<GameObject> aliveEnemies = new List<GameObject>();
    private Transform player; // new
    private int spawnedCount;
    private bool isSpawning;
    private bool hasTriggered;

    // Called when the component is first added. Makes the box a trigger automatically.
    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isSpawning) return;
        if (disableAfterUse && hasTriggered) return;

        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning($"{name}: assign an enemy prefab and at least one spawn point.", this);
            return;
        }

        player = other.transform;
        hasTriggered = true;
        isSpawning = true;
        spawnedCount = 0;
        onTriggered?.Invoke();
    }

    private void Update()
    {
        if (!isSpawning) return;

        // Destroyed objects compare equal to null in Unity, so this drops enemies the player removed.
        aliveEnemies.RemoveAll(enemy => enemy == null);

        while (aliveEnemies.Count < maxAlive && spawnedCount < totalToSpawn)
        {
            Transform point = FindFreeSpawnPoint();
            if (point == null) break; // No usable point right now. Try again next frame.
            SpawnEnemy(point);
        }

        if (spawnedCount >= totalToSpawn)
        {
            isSpawning = false;
        }
    }

    // Returns a spawn point that has no enemy on it and isn't too close to the player.
    private Transform FindFreeSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Start at a different point each time so enemies spread out.
            Transform point = spawnPoints[(spawnedCount + i) % spawnPoints.Length];
            if (point == null) continue;
            if (player != null && Vector3.Distance(point.position, player.position) < minDistanceFromPlayer) continue;
            if (IsOccupied(point)) continue;
            return point;
        }
        return null;
    }

    private bool IsOccupied(Transform point)
    {
        foreach (GameObject enemy in aliveEnemies)
        {
            if (enemy != null && Vector3.Distance(enemy.transform.position, point.position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnEnemy(Transform point)
    {
        GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);
        aliveEnemies.Add(enemy);
        spawnedCount++;
    }

    // Draws the trigger box and spawn points in the Scene view so you can see them while editing.
    private void OnDrawGizmos()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.color = new Color(1f, 0.6f, 0f);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(box.center, box.size);
            Gizmos.matrix = Matrix4x4.identity;
        }

        if (spawnPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;
            Gizmos.DrawSphere(point.position, 0.3f);
            Gizmos.DrawLine(transform.position, point.position);
        }
    }
}