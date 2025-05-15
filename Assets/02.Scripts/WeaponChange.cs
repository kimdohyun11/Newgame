using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] GameObject _weaponManager;
    float _Delay;
    float _timer;

    private void Start()
    {
        _Delay = 1f;
        _timer = 1f;    
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_timer > _Delay)
        {
            if (collision.gameObject.CompareTag("Weapon"))
            {
                Weapon();   
            }
            else if (collision.gameObject.CompareTag("Weapon2"))
            {
                Weapon2();
            }
            _timer = 0f;
        }
    }

    private void Weapon()
    {
        _weaponManager.GetComponent<WeaponManager>().WeaponChange();
    }

    private void Weapon2()
    {
        _weaponManager.GetComponent<WeaponManager>().WeaponChange2();
    }
}
