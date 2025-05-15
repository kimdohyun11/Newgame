using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    public void RsetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}
