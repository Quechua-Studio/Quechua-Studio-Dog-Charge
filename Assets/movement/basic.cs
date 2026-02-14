using System;
using UnityEngine;
//Bruno Tejería y Franco García
public class BasicMovement : MonoBehaviour {
  [Header("Movement Settings")] public float speed = 5f;

  public float jumpForce = 100f;
  public bool canJump;
  public Rigidbody2D rb;

  //Bruno Tejería
  protected virtual void Awake() {
    rb = GetComponent<Rigidbody2D>();
    if (rb == null) {
      Debug.LogWarning($"{gameObject.name} necesita un Rigidbody2D.");
    }
  }

  //Bruno Tejería
  private void OnCollisionEnter2D(Collision2D collision) {
    // Detecta si está tocando el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      canJump = true;
    }
  }

  //Bruno Tejería
  private void OnCollisionExit2D(Collision2D collision) {
    // Detecta si dejó de tocar el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      // PARCIALMENTE NO FUNCIONA POR UN BUG, cambiar a false
      canJump = true;
    }
  }

  // Mover objeto horizontalmente.
  // Mover constantemente hacia la dirección X positiva.

  //Bruno Tejería
  protected void MoveHorizontal() {
    rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
  }

  //
  // Salto con altura constante. Solo funciona si está en el suelo.
  //Bruno Tejería
  protected void Jump() {
    if (canJump) {
      // IMPORTANTE: Establece la velocidad Y directamente
      // Esto hace que el salto SIEMPRE sea de la misma altura
      rb.linearVelocity = new Vector2(rb.linearVelocity.x * Time.deltaTime, jumpForce);
    }
  }

  //Bruno Tejería
  /// Resetea la posición del objeto, útil para respawn o reinicio
  protected void ResetPosition(Vector3 position) {
    transform.position = position;
    rb.linearVelocity = Vector2.zero; // Detiene todo movimiento
  }

}

