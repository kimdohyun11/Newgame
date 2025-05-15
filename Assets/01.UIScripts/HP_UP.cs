using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP_UP : MonoBehaviour
{

        [Header("Health Settings")]
        public float maxHealth = 10f;         // ���� ���� (ex. ��Ʈ 5�� = 10)
        public float currentHealth;

        [Header("Invincibility Settings")]
        public bool isInvincible = false;
        public float invincibleTime = 1f;

        [Header("Heart UI (Canvas)")]
        public Image[] heartImages;        // Canvas�� �ִ� ��Ʈ�� (���ʺ��� �������)
        public Sprite fullHeart;           // ���� �� ��Ʈ
        public Sprite halfHeart;           // �� ��Ʈ
        public Sprite emptyHeart;          // �� ��Ʈ

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
            TakeDamage(0.5f); // ü�� ��ĭ ����
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
                float heartValue = currentHealth - (i * 2f); // �� ��Ʈ�� �´� �� ���

                if (heartValue >= 1.5f) // 1.5 �̻��� ��� ���� �� ��Ʈ
                {
                    heartImages[i].sprite = fullHeart;
                }
                else if (heartValue >= 0.5f) // 0.5 �̻��� ��� �� ��Ʈ
                {
                    heartImages[i].sprite = halfHeart;
                }
                else // �� �̸��� ��� �� ��Ʈ
                {
                    heartImages[i].sprite = emptyHeart;
                }
            }
        }

        void Die()
        {
            Debug.Log("�÷��̾� ���!");
            // TODO: ���� ���� UI ����, ����� �� ó��
        }
    

}
