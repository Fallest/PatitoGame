using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void QuitGame()
  {
    Debug.Log("Game quit");
    Application.Quit();
  }
}
