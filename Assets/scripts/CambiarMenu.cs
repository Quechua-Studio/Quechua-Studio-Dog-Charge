using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class CambiarMenu : MonoBehaviour {
  public void PlayGame()
  {
    SceneManager.LoadSceneAsync(0);
  }

  public void OpenOptions() {
    SceneManager.LoadSceneAsync(2);
  }

  public void ViewCredits()
  {
  SceneManager.LoadSceneAsync(1);
  }
  public void QuitGame()
  {
    Application.Quit();
  }

  public void MainMenu()
  {
  SceneManager.LoadSceneAsync(3);
  }
}
