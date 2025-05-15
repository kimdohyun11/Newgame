using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5f;
    private Rigidbody2D _rigid;
    public Vector2 _moveDir;
    private Animator _animator;
    private SpriteRenderer _spriter;
    private Vector2 _mouse;
    [SerializeField]private float _dashSpeed = 2f;
    private float _dashDuration = 0.3f;
    private float _durationTimer;
    private bool _isDash = false;
    private float _dashCoolTime = 3f;
    private float _coolTimer;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriter = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _durationTimer = 0.3f;
        _coolTimer = 3f;
    }

    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>();
        PlayAnimaiton();
    }

    private void FixedUpdate()
    {
        if (_isDash == true)    
            return;

        _rigid.linearVelocity = _moveDir * _speed;
    }

    private void PlayAnimaiton()
    {
        _animator.SetFloat("move", _moveDir.magnitude);
    }

    private void Update()
    {
        _durationTimer += Time.deltaTime;
        _coolTimer += Time.deltaTime;
        if(_durationTimer > _dashDuration)
        {
            _isDash = false;
        }

        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerRotation();
        
        if (_coolTimer > _dashCoolTime)
        {
            if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            {
                Dash();
                _isDash = true;
                _durationTimer = 0f;
                _coolTimer = 0f;
            }
        }
    }

    private void PlayerRotation()
    {
        if (_mouse.x != 0)
        {
            _spriter.flipX = _mouse.x < 0;
        }
    }

    private void Dash()
    {
        _rigid.AddForce(_moveDir.normalized * _dashSpeed, ForceMode2D.Impulse);
    }
}
