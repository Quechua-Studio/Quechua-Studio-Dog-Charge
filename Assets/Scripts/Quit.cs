using UnityEngine;

public class Quit : MonoBehaviour
{
    //Franco garcía
    void Update() {
      bool WantsToQuit = Input.GetKeyDown(KeyCode.Escape);
      if (WantsToQuit)
      {
        QuitGame();
      }
    }
    //Franco García
    public void QuitGame()
    {
      Application.Quit();
    }
}
