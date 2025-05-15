using System.Collections;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    enum _State { Patrol, Chase, Attack }
    _State _currentstate = _State.Patrol;

    [SerializeField] private int _maxHP = 10300;
    private int _currentHP;
    [SerializeField] private float _patrolRange = 3f;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _chaseRange = 13f;
    [SerializeField] private float _attackRange = 1.5f;
    private Rigidbody2D _rb;

    private AIPath _aiPath;
    private AIDestinationSetter _destinationSetter;
    private Vector2 _origin;
    private Transform _player;

    private Animator _anim; // 애니메이터 컴포넌트

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        _anim = GetComponent<Animator>(); // 애니메이터 연결
    }

    private void Start()
    {
        _origin = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(PatrolRoutine());
        _currentHP = _maxHP;
    }

    private void Update()
    {
        float _dist = Vector2.Distance(transform.position, _player.position);

        // 애니메이션 이동 처리 (속도 기반)
        bool isWalking = _aiPath.desiredVelocity.magnitude > 0.1f;
        _anim.SetBool("Walk", isWalking);


        switch (_currentstate)
        {
            case _State.Patrol:
                if (_dist < _chaseRange)
                {
                    StopAllCoroutines();
                    _currentstate = _State.Chase;
                }
                break;

            case _State.Chase:
                if (_dist < _attackRange)
                {
                    _currentstate = _State.Attack;
                    _aiPath.canMove = false;
                    _anim.SetFloat("Walk", 0); // 멈춤
                }
                else if (_dist > _chaseRange)
                {
                    _currentstate = _State.Patrol;
                    _aiPath.canMove = false;
                    StartCoroutine(PatrolRoutine());
                }
                else
                {
                    _aiPath.canMove = true;
                    _destinationSetter.target = _player;
                }
                break;

            case _State.Attack:
                if (_dist > _attackRange)
                {
                    _currentstate = _State.Chase;
                }
                else
                {
                    _aiPath.canMove = false;
                    _anim.SetTrigger("Attack"); // 공격 애니메이션 트리거
                    Debug.Log("공격!");
                }
                break;
        }

        // 좌우 반전 (이동 방향)
        if (_aiPath.desiredVelocity.x != 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(_aiPath.desiredVelocity.x), 1, 1);
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (_currentstate == _State.Patrol)
        {
            Vector2 _randomOffset = new Vector2(
                Random.Range(-_patrolRange, _patrolRange),
                Random.Range(-_patrolRange, _patrolRange)
            );

            Vector2 patrolTarget = _origin + _randomOffset;

            GameObject tempTarget = new GameObject("TempPatrolTarget");
            tempTarget.transform.position = patrolTarget;
            _destinationSetter.target = tempTarget.transform;
            _aiPath.canMove = true;

            while (Vector2.Distance(transform.position, patrolTarget) > 0.2f)
            {
                yield return null;
            }

            Destroy(tempTarget);
            _aiPath.canMove = false;

            yield return new WaitForSeconds(1f);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        Debug.Log($" {damage}!: {_currentHP}");

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("사망");
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(1);
        }
    }
}