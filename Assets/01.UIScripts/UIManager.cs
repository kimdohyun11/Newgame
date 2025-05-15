using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void PlayBtnClick()
    {
        SceneManager.LoadScene("LodingScene");   
    }
    public void ExitBtnClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
