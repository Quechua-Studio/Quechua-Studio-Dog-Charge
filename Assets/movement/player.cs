
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

/**
 * Movimiento específico del jugador.
 * Hereda de BasicMovement y añade controles de teclado.
 */
public class PlayerMovement : BasicMovement {
    public Vector3 respawnPoint = Vector3.zero;
    public float fallThreshold;

    void Update() {
        HandleInput();
        // Para resetear automático al caer al vacío
        CheckFallRespawn();
    }

    private void HandleInput() {
        // Salto - IMPORTANTE: wasPressedThisFrame hace que solo salte UNA VEZ por presión
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && this.canJump) {
            this.Jump();
        }

        // Movimiento horizontal
        this.MoveHorizontal();

        // Reset manual
        if (Keyboard.current.rKey.wasPressedThisFrame) {
            this.ResetPosition(respawnPoint);
        }
    }

    private void CheckFallRespawn() {
        // Auto-respawn si cae al vacío
        if (transform.position.y < fallThreshold) {
            this.ResetPosition(respawnPoint);
        }
    }
}

