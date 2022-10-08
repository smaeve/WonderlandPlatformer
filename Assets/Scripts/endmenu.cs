using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endmenu : MonoBehaviour
{
  public void RestartButton()
  {
      SceneManager.LoadScene("SampleScene");
  }
  public void ExitButton()
  {
      SceneManager.LoadScene("menu");
  }
}
