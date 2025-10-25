using UnityEngine;

public class SkiSafariCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 3f;
    public Vector3 baseOffset = new Vector3(5f, 2f, -10f);
    public float zoomSpeed = 0.1f;
    public float minZoom = 5f;
    public float maxZoom = 12f;

    private Rigidbody2D playerRb;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (player != null)
            playerRb = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (player == null) return;

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
