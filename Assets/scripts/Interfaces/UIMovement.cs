using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour {
  public int speed = 10;
    // Tambien se usa en la cámara
    void Update()
    {
      transform.position += new Vector3(speed, 0, 1) * Time.deltaTime;

    }
    //Tendria que heredar la posicion de Y del jugador
}
