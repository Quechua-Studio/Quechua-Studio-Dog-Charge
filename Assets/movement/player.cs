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
  public float SlamCharge = 0f;
  private Rigidbody2D rb;
  public float DashCharge = 0f;
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  private void Update() {
    HandleInput();
    // Para resetear automático al caer al vacío
    CheckFallRespawn();
    //Recarga el Slam
    SlamCharge += 1f * Time.deltaTime;
    //
    regulateSlamCharge();
    //Recarga el Dash
    DashCharge += 0.33f * Time.deltaTime;
    //
    RegulateDash();
    //
    speed += 1f * Time.deltaTime;
  }

  private void HandleInput() {
    // Salto - IMPORTANTE: wasPressedThisFrame hace que solo salte UNA VEZ por presión
    if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && canJump &&
        jumpCharge >= 1) {
      Jump();
      jumpCharge -= 1f;
    }

    // Movimiento horizontal
    MoveHorizontal();

    // Reset manual
    if (Keyboard.current.rKey.wasPressedThisFrame) {
      ResetPosition(respawnPoint);
    }
    

    bool isSlamming = Input.GetKey(KeyCode.LeftShift);
    if (isSlamming)
    {
      Slam();
    }
    bool isDashing = Input.GetKey(KeyCode.W);
    if  (isDashing &&  DashCharge >= 0.5)
    {
      Dash();
    }
  }

  private void CheckFallRespawn() {
    // Auto-respawn si cae al vacío
    if (transform.position.y < fallThreshold) {
      ResetPosition(respawnPoint);
    }
  }

  public void Slam()
  {
    rb.AddForce(Vector2.down * 2000);
    SlamCharge -= 1;
  }

  private void regulateSlamCharge()
  {
    if (SlamCharge > 1)
    {
    SlamCharge = 1;
    }

    if (SlamCharge < 0)
    {
    SlamCharge = 0;
    }
  }

  private void Dash()
  {
    rb.AddForce(Vector2.right * 10000);
    DashCharge -= 0.1f;
  }

  private void RegulateDash() {
    if (DashCharge > 1)
    {
      DashCharge = 1;
    }
    if (DashCharge < 0)
    {
      DashCharge = 0;
    }
  }

  private void RegulateJump() {
    if (jumpCharge > 2) {
      jumpCharge = 2;
    }

    if (jumpCharge < 0) {
      jumpCharge = 0;
    }
  }
  private void OnCollisionEnter2D(Collision2D collision) {
    // Detecta si está tocando el suelo
    if (collision.gameObject.CompareTag("Ground")) {
      jumpCharge = 2;
    }
  }
}
