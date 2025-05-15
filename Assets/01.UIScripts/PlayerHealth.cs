using System.Collections;
using UnityEngine;
using UnityEngine.UI; // ✅ UI 사용하려면 필요!

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public SpriteRenderer[] heartImages;       // ⬅️ UnityEngine.UI.Image로 변경
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isInvincible = false;
    public float invincibleTime = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
            //StartCoroutine(TakeDamageWithInvincibility());
        }
    }

    //IEnumerator TakeDamageWithInvincibility()
    //{
    //    isInvincible = true;
    //    TakeDamage(1);
    //    yield return new WaitForSeconds(invincibleTime);
    //    isInvincible = false;
    //}

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
                heartImages[i].sprite = fullHeart;
            else
                heartImages[i].sprite = emptyHeart;
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
        // 사망 처리
    }
}
