using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet2 : MonoBehaviour
{
    private float _speed = 12f;
    private Vector3 _moveDir;
    private GameObject _firePos;
    Vector2 _mousePos;
    private float _angle;
    private float _lifeTime = 0.4f; //ÃÑ¾Ë »ýÁ¸ ½Ã°£
    private float _spawnTime;
    private float _spreadAngle = 40; //ÅºÆÛÁü °¢µµ
    private Vector3 _rayDirection;
    Vector3 _spreadDirection;

    private void Start()
    {
        Shot();
        BulletRotation();
    }

    private void FixedUpdate()
    {
        transform.position += _spreadDirection.normalized * _speed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Time.time - _spawnTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void Shot()
    {
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _firePos = GameObject.Find("Gun2_1");
        _rayDirection = ((Vector3)_mousePos - _firePos.transform.position).normalized;
        float _randomAngle = Random.Range(-_spreadAngle / 2f, _spreadAngle / 2f);
        _spreadDirection = Quaternion.Euler(0, 0, _randomAngle) * _rayDirection;
        _spawnTime = Time.time;
    }

    private void BulletRotation()
    {
        _angle = Mathf.Atan2(_mousePos.y - _firePos.transform.position.y, _mousePos.x - _firePos.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
