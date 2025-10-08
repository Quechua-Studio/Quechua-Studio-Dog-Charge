using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class BasicMovement : MonoBehaviour {
  public float speed = 0.001f;
  public float jumpForce = 10f;
  public bool isGrounded = false;
  public float horizontalInput = 1f;

  // Mover objeto horizontalmente
  protected void MoveHorizontal() {
    transform.position += new Vector3(this.speed, 0f, 0f);
  }

  // Hace que el objeto salte TODO: FIX THAT
  protected void Jump() {
    if (this.isGrounded) {
      transform.position += new Vector3(0f, this.jumpForce, 0f);
    }
  }

  /// Resetea la posición del objeto, útil para respawn o reinicio
  protected void ResetPosition(Vector3 position) {
    transform.position = position;
  }

  /// Detección de colisiones para determinar si el objeto está en el suelo
  protected void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.name == "floor") {
      this.isGrounded = true;
    }
  }

  // Detección de salida de colisiones para actualizar el estado de suelo
  protected void OnCollisionExit(Collision collision) {
    if (collision.gameObject.name == "floor") {
      this.isGrounded = false;
    }
  }
}