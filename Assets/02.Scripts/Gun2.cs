using UnityEngine;
using UnityEngine.InputSystem;

public class Gun2 : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private GameObject _gun2;
    SpriteRenderer _spriter;
    float _angle;
    Vector3 _mouse;
    Vector3 _shot;
    private float _delay = 1f; //연사 속도
    private float _timer = 1f;
    private int _bulletCount = 18; //산탄수
    

    private void Awake()
    {
        _spriter = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        GunRotation();
        MakeBullet();
    }

    private void GunRotation()
    {
        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _angle = Mathf.Atan2(_mouse.y - transform.position.y, _mouse.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        if (_mouse.x != 0)
        {
            _spriter.flipY = _mouse.x < transform.position.x;
        }
    }

    private void MakeBullet()
    {
        if (Input.GetMouseButton(0))
        {
            if (_timer >= _delay)
            {
                for(int i = 0; i < _bulletCount; i++)
                {
                    GameObject bullet = Instantiate(_bulletPrefabs);
                    _shot = (Vector3)_mouse - transform.position;
                    bullet.transform.position = _gun2.transform.position + _shot.normalized * 1.5f; 
                    _timer = 0;
                }
                _timer = 0;
            }
        }
    }
}
