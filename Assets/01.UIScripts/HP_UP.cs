using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP_UP : MonoBehaviour
{

        [Header("Health Settings")]
        public float maxHealth = 10f;         // 반피 단위 (ex. 하트 5개 = 10)
        public float currentHealth;

        [Header("Invincibility Settings")]
        public bool isInvincible = false;
        public float invincibleTime = 1f;

        [Header("Heart UI (Canvas)")]
        public Image[] heartImages;        // Canvas에 있는 하트들 (왼쪽부터 순서대로)
        public Sprite fullHeart;           // 가득 찬 하트
        public Sprite halfHeart;           // 반 하트
        public Sprite emptyHeart;          // 빈 하트

        void Start()
        {
            currentHealth = maxHealth;
            UpdateHearts();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Potion") && !isInvincible)
            {
                StartCoroutine(TakeDamageWithInvincibility());
            }
        }

        IEnumerator TakeDamageWithInvincibility()
        {
            isInvincible = true;
            TakeDamage(0.5f); // 체력 반칸 깎임
            yield return new WaitForSeconds(invincibleTime);
            isInvincible = false;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHearts();

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHearts();
        }

        void UpdateHearts()
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                float heartValue = currentHealth - (i * 2f); // 각 하트에 맞는 값 계산

                if (heartValue >= 1.5f) // 1.5 이상일 경우 가득 찬 하트
                {
                    heartImages[i].sprite = fullHeart;
                }
                else if (heartValue >= 0.5f) // 0.5 이상일 경우 반 하트
                {
                    heartImages[i].sprite = halfHeart;
                }
                else // 그 미만일 경우 빈 하트
                {
                    heartImages[i].sprite = emptyHeart;
                }
            }
        }

        void Die()
        {
            Debug.Log("플레이어 사망!");
            // TODO: 게임 오버 UI 띄우기, 재시작 등 처리
        }
    

}
