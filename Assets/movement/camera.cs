using UnityEngine;
using System;

public class SkiSafariCamera : MonoBehaviour {
    // Referencia al jugador
    public Transform player;

    // Configuraciones de cámara
    public float smoothSpeed = 4f;
    // Offset base desde el jugador
    public Vector3 baseOffset = new Vector3(5f, 2f, -10f);
    // Velocidad de interpolación para el zoom
    public float zoomSpeed = 0.1f;
    // Zoom mínimo cuando el jugador está detenido
    public float minZoom = 5f;
    // Zoom máximo cuando el jugador va muy rápido
    public float maxZoom = 12f;

    // Referencia al Rigidbody2D del jugador para obtener velocidad
    private Rigidbody2D playerRb;
    // Referencia a la cámara
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
        if (player == null) {
            Debug.LogWarning($"{gameObject.name} no tiene asignado el jugador.");
            return;
        }
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate() {
        if (player == null) {

            return;
        }
        ;

        // calcular velocidad horizontal
        float speed = playerRb != null ? Mathf.Abs(playerRb.linearVelocity.x) : 0f;

        // ajustar zoom según la velocidad
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, speed / 20f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed);

        // ajustar posición con offset dinámico
        Vector3 targetPos = player.position + baseOffset + new Vector3(speed * 0.2f, 0, 0);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
