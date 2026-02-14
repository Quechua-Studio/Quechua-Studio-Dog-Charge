using System;
using UnityEngine;
  //Franco García
public class DogMovement : MonoBehaviour {
  public float DogSpeed = 10f;
  private Rigidbody2D rb;
  private Animator animator;
  private bool isGrounded = true;
  private bool isJumping = false;
  private bool isInJumpArea  = false;
  private bool WantsToChange = false;

  //Franco García
  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  //Franco García
  void Update() {
    //Movimiento horizontal constante
    transform.position += new Vector3(DogSpeed, 0, 0) * Time.deltaTime;

    UpdateAnimator();

    DogSpeed += 1f * Time.deltaTime;;
  }

  /*Si detecta que está dentro de "AreaDeSalto" hace un salto.
  "AreaDeSalto" debe ser un rango alrededor del obstáculo (sin colisiones) y con isTrigger true
  El rango necesita del tag para funcionar.
  El salto es automático.
  */

  //Franco García y Bruno Tejería
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("JumpingArea")) {
      isInJumpArea = true;

    }

    if (isInJumpArea) {
      Jump();
    }
  }

  //Franco García
  //Ejecuta el salto y (se supone) que cambia la animación
  void Jump() {
    rb.AddForce(Vector2.up * 1000);

  }

  //Franco García
  private void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("JumpingArea"))
    {
      isInJumpArea = false;
    }
  }

  //Bruno Tejería y Franco García
  private void OnCollisionExit2D(Collision2D collision) {
    if (collision.collider.CompareTag("Ground")) {
      isGrounded = false;
    }
  }

  //Franco García
  private void UpdateAnimator()
  {
    animator.SetBool("isInJumpArea", isInJumpArea);
    if (!isInJumpArea)
    {
      changeTheAnimation();
    }
  }

  //Franco García
  private void changeTheAnimation()
  {
    animator.SetBool("isInJumpArea", isInJumpArea);
  }
}
