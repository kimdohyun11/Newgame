using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public Image loadingFillImage;     // 안쪽 로딩 바 (Fill 타입 이미지)
    public float loadDuration = 3f;    // 로딩 시간 (초)

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / loadDuration);
        loadingFillImage.fillAmount = progress;

        if (progress >= 1f)
        {
            SceneManager.LoadScene("PlayScene");
        }
    }
}
