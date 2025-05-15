using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private void Awake()
    {
        _playerStatus = FindFirstObjectByType<PlayerStatus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<IItem>(out IItem item))
        {
            bool isbuyitem = false;
            int price = item.GetItemData().Price, coin = _playerStatus.Coin;
            if (collision.CompareTag("Stend"))
            {
                isbuyitem = true;
                if (coin < price) return;
            }
            if (!item.Use(gameObject)) return;
            collision.gameObject.SetActive(false);
            Destroy((MonoBehaviour)item);
            if (isbuyitem) _playerStatus.Coin -= price;
        }
    }
}
