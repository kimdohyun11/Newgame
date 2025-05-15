using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public int maxHealth = 10; 
    public float currentHealth;

    public Image[] heartImages; 
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); 
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
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
            float heartValue = currentHealth - (i * 2);

            if (heartValue >= 2)
            {
                heartImages[i].sprite = fullHeart;
            }
            else if (heartValue >= 1)
            {
                heartImages[i].sprite = halfHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
        // 사망 처리 추가
    }
}

