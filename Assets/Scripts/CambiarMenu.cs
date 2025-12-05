using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class CambiarMenu : MonoBehaviour {
  //Franco García
  public void PlayGame()
  {
    SceneManager.LoadSceneAsync(2);
  }

  //Franco García
  public void OpenOptions() {
    SceneManager.LoadSceneAsync(3);
  }
  //Franco García
  public void ViewCredits()
  {
  SceneManager.LoadSceneAsync(4);
  }
  //Franco García
  public void QuitGame()
  {
    Application.Quit();
  }
  //Franco García
  public void MainMenu()
  {
  SceneManager.LoadSceneAsync(1);
  }
}
