using System.Collections.Generic;
using UnityEngine;


// Generador de terreno procedural infinito.
// Genera chunks adelante del jugador y destruye los que quedan atrás.
public class TerrainGenerator : MonoBehaviour {
    [Header("References")]
    [Tooltip("Transform del jugador a seguir")]
    public Transform player;

    [Tooltip("Prefab del chunk de terreno")]
    public GameObject chunkPrefab;

    [Header("Generation Settings")]
    [Tooltip("Cantidad de chunks a generar al inicio")]
    public int chunksPreload = 6;

    [Tooltip("Ancho de cada chunk en unidades")]
    public float chunkWidth = 16f;

    [Tooltip("Distancia adelante del jugador para generar nuevos chunks")]
    public float spawnOffset = 20f;

    [Tooltip("Distancia atrás del jugador para destruir chunks")]
    public float destroyOffset = 20f;

    [Header("Terrain Variation")]
    [Tooltip("Variación vertical del terreno")]
    public float heightVariation = 1f;

    [Tooltip("¿Activar variación de altura?")]
    public bool enableHeightVariation = true;

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private float lastChunkX = 0f;
    private bool isInitialized = false;

    void Start() {
        InitializeTerrain();
    }

    void Update() {
        if (!isInitialized || player == null) {
            return;
        }

        GenerateChunksAhead();
        RemoveChunksBehind();
    }


    // Inicializa el terreno generando los chunks iniciales
    private void InitializeTerrain() {
        if (player == null) {
            Debug.LogError("TerrainGenerator: ¡Player no asignado! Asigna el Transform del jugador en el Inspector.");
            return;
        }

        if (chunkPrefab == null) {
            Debug.LogError("TerrainGenerator: ¡ChunkPrefab no asignado! Asigna un prefab de chunk en el Inspector.");
            return;
        }

        // Empieza generando chunks desde antes de la posición del jugador
        lastChunkX = player.position.x - (chunkWidth * (chunksPreload / 2f));

        // Pre-genera los chunks iniciales
        for (int i = 0; i < chunksPreload; i++) {
            SpawnChunk();
        }

        isInitialized = true;
        Debug.Log($"TerrainGenerator: {chunksPreload} chunks generados exitosamente.");
    }


    // Genera nuevos chunks adelante del jugador
    private void GenerateChunksAhead() {
        // Genera chunks si el jugador se acerca al último chunk
        while (player.position.x + spawnOffset > lastChunkX) {
            SpawnChunk();
        }
    }

    // Remueve chunks que quedaron muy atrás del jugador
    private void RemoveChunksBehind() {
        // Destruye chunks que están muy atrás del jugador
        while (activeChunks.Count > 0) {
            GameObject frontChunk = activeChunks.Peek();

            // Verifica si el chunk está muy atrás
            if (frontChunk != null && frontChunk.transform.position.x + chunkWidth < player.position.x - destroyOffset) {
                Destroy(activeChunks.Dequeue());
            } else {
                break; // Sale del loop si el chunk aún está cerca
            }
        }
    }


    // Genera un nuevo chunk de terreno
    private void SpawnChunk() {
        lastChunkX += chunkWidth;

        // Calcula la posición Y con variación opcional
        float yPos = enableHeightVariation ? Random.Range(-heightVariation, heightVariation) : 0f;
        Vector3 position = new Vector3(lastChunkX, yPos, 0f);

        // Instancia el chunk
        GameObject newChunk = Instantiate(chunkPrefab, position, Quaternion.identity, transform);
        newChunk.name = $"Chunk_{activeChunks.Count}";

        activeChunks.Enqueue(newChunk);
    }


    // Limpia todos los chunks activos (útil para reiniciar)
    public void ClearAllChunks() {
        while (activeChunks.Count > 0) {
            GameObject chunk = activeChunks.Dequeue();
            if (chunk != null) {
                Destroy(chunk);
            }
        }
        lastChunkX = 0f;
        isInitialized = false;
    }


    // Reinicia el generador de terreno
    public void ResetTerrain() {
        ClearAllChunks();
        InitializeTerrain();
    }

    // Dibuja gizmos en el editor para visualizar el sistema
    void OnDrawGizmos() {
        if (player == null) return;

        // Zona de spawn (verde)
        Gizmos.color = Color.green;
        Vector3 spawnZone = new Vector3(player.position.x + spawnOffset, player.position.y, 0f);
        Gizmos.DrawWireSphere(spawnZone, 2f);

        // Zona de destrucción (rojo)
        Gizmos.color = Color.red;
        Vector3 destroyZone = new Vector3(player.position.x - destroyOffset, player.position.y, 0f);
        Gizmos.DrawWireSphere(destroyZone, 2f);
    }
}
