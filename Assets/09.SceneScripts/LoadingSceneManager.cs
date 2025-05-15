using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public Image loadingFillImage;     // ���� �ε� �� (Fill Ÿ�� �̹���)
    public float loadDuration = 3f;    // �ε� �ð� (��)

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
