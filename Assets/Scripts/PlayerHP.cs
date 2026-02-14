using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//Bruno Tejería
public struct Damage {
  public string Key { get; set; }
  public float Amount { get; set; }
}

//Bruno Tejería
public class PlayerHP : MonoBehaviour {
  public float CurrentHp = 1;

  private readonly Damage[] Damages = {
    new() {
      Key = "DamagingObject",
      Amount = 1f
    }
    // Aquí van mas daños que se agreguen en un futuro
  };

  //Franco García
  private void Update() {
    //Manda a matar al jugador
    if (CurrentHp <= 0) {
      Die();
    }
  }

  /* Detecta si colisiona con un objeto que haga daño
 Si colisiona llama a la función de hacer daño */
  //Bruno Tejería
  private void OnTriggerEnter2D(Collider2D other) {
    // Buscar si hay algun Damage con ese tag
    var damage = Damages.FirstOrDefault(d => other.CompareTag(d.Key));
    if (damage.Amount > 0f) {
      TakeDamage(damage.Amount);
    }
  }

  /*
   * IMPORTANTE: Mantener estos métodos publicos.
   */
  //Reduce la vida del jugador
  //Bruno Tejería
  public void TakeDamage(float amount) {
    CurrentHp -= amount;
  }

  //Mata al jugador
  //Franco García
  public void Die() {
    Destroy(gameObject);
    SceneManager.LoadSceneAsync(5);
  }
}
