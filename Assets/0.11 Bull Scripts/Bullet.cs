using System.Collections;
using System.Drawing;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private float _speed = 12f;
    private Vector3 _moveDir;
    private GameObject _firePos;
    Vector2 _mousePos;
    private float _angle;
    private float _lifeTime = 1.5f; //총알 생존 시간

    private void OnEnable()
    {
        Shot();
        BulletRotation();
    }

    private void FixedUpdate()
    {
        transform.position += _moveDir.normalized * _speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Shot()
    {
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _firePos = GameObject.Find("Gun1_1");
        _moveDir = (Vector3)_mousePos - (Vector3)_firePos.transform.position;
        StartCoroutine(LifeTime());
    }

    private void BulletRotation()
    {
        _angle = Mathf.Atan2(_mousePos.y - _firePos.transform.position.y, _mousePos.x - _firePos.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }
}
