using UnityEngine;
using UnityEngine.InputSystem;

public class GameStop : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    private bool isPaused = false;
    private void Start()
    {
        _button.SetActive(false);
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            IWantGohome();
        }
    }

    public void IWantGohome()
    {
        isPaused = !isPaused;
        _button.SetActive(isPaused);
        Time.timeScale = !isPaused ? 1 : 0;
    }
}
