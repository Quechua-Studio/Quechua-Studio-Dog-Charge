using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class BasicMovement : MonoBehaviour {
  [Header("Movement Settings")]
  public float speed = 5f;
  public float jumpForce = 5f;
  public bool canJump = false;
  public Rigidbody2D rb;

  protected virtual void Awake() {
    rb = GetComponent<Rigidbody2D>();
    if (rb == null) {
      Debug.LogWarning($"{gameObject.name} necesita un Rigidbody2D.");
    }
  }

  // Mover objeto horizontalmente.
  // Mover constantemente hacia la dirección X positiva.
  protected void MoveHorizontal() {
    rb.linearVelocity = new Vector2(this.speed, rb.linearVelocity.y);

  }

  //
  // Salto con altura constante. Solo funciona si está en el suelo.
  //
  protected void Jump() {
    if (this.canJump) {
      // IMPORTANTE: Establece la velocidad Y directamente
      // Esto hace que el salto SIEMPRE sea de la misma altura
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
  }

  /// Resetea la posición del objeto, útil para respawn o reinicio
  protected void ResetPosition(Vector3 position) {
    transform.position = position;
    rb.linearVelocity = Vector2.zero; // Detiene todo movimiento

  }

  private void OnCollisionEnter2D(Collision2D collision) {
    // Detecta si está tocando el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      this.canJump = true;
    }
  }

  private void OnCollisionExit2D(Collision2D collision) {
    // Detecta si dejó de tocar el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      this.canJump = false;
    }
  }
}