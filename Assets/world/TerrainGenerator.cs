using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject chunkPrefab;
    public int chunksPreload = 6;
    public float chunkWidth = 16f;
    public float spawnOffset = 10f;

    Queue<GameObject> activeChunks = new Queue<GameObject>();
    float lastChunkX = 0f;

    void Start()
    {
        // Pre-generate chunks
        lastChunkX = -chunkWidth;
        for (int i = 0; i < chunksPreload; i++)
            SpawnChunk();
    }

    void Update()
    {
        // spawn ahead of player
        if (player.position.x + spawnOffset > lastChunkX)
            SpawnChunk();

        // remove chunks behind player
        if (activeChunks.Count > 0)
        {
            var front = activeChunks.Peek();
            if (front.transform.position.x + chunkWidth < player.position.x - 20f)
            {
                Destroy(activeChunks.Dequeue());
            }
        }
    }

    void SpawnChunk()
    {
        lastChunkX += chunkWidth;
        Vector3 pos = new Vector3(lastChunkX, Random.Range(-1f, 1f), 0f);
        var go = Instantiate(chunkPrefab, pos, Quaternion.identity, transform);
        // Optionally randomize tile sprites / obstacles inside chunk via script on chunk prefab
        activeChunks.Enqueue(go);
    }
}
