using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    GameStop gameStop;
    private void Awake()
    {
        gameStop = FindFirstObjectByType<GameStop>();
    }
    public void Restart()
    {
        gameStop.IWantGohome();
    }
}
