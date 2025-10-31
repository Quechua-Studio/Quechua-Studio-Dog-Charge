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

}
