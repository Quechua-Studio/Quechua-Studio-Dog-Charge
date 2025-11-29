using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Movimiento específico del jugador.
 * Hereda de BasicMovement y añade controles de teclado.
 */
public class PlayerMovement : BasicMovement {
  public Vector3 respawnPoint = Vector3.zero;
  public float fallThreshold;
  private float jumpCharge;
  public float jumpChargeAmount = 1;

  private void Update() {
    HandleInput();
    // Para resetear automático al caer al vacío
    CheckFallRespawn();
  }

  private void HandleInput() {
    // Salto - IMPORTANTE: wasPressedThisFrame hace que solo salte UNA VEZ por presión
    if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && canJump && jumpCharge >= 1) {
      Jump();
      jumpCharge = 0;
    }

    // Movimiento horizontal
    MoveHorizontal();

    // Reset manual
    if (Keyboard.current.rKey.wasPressedThisFrame) {
      ResetPosition(respawnPoint);
    }


    jumpCharge += jumpChargeAmount *  Time.deltaTime;


  }

  private void CheckFallRespawn() {
    // Auto-respawn si cae al vacío
    if (transform.position.y < fallThreshold) {
      ResetPosition(respawnPoint);
    }
  }
}
