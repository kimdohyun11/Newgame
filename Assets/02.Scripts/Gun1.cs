using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Gun1 : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefabs;
    [SerializeField] private GameObject _gun1;
    SpriteRenderer _spriter;
    float _angle;
    Vector2 _mouse;
    Vector3 _shot;
    bool _canFire = true;
    [SerializeField] private float _reloadTime = 0.3f;
    private GameObject[] _bulletPool;
    private int _poolSize = 10;

    private void Awake()
    {
        _spriter = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _bulletPool = new GameObject[_poolSize];

        for(int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefabs);
            _bulletPool[i] = bullet;
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_canFire)
                MakeBullet();
        }

        GunRotation();
    }

    private void MakeBullet()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = _bulletPool[i];
            if (!bullet.activeSelf)
            {
                _shot = (Vector3)_mouse - transform.position;
                bullet.transform.position = _gun1.transform.position + _shot.normalized;
                bullet.SetActive(true);

                break;
            }
        }

        StartCoroutine(ReloadCoroutine());
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

    private IEnumerator ReloadCoroutine()
    {
        _canFire = false;
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }
}
