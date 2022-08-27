using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private bool _isOnGround;
    private bool _isJumped;
    private int _jumpCount;
    private int _maxJumpCount;
    private float _hp;
    private float _maxhp;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _isOnGround = false;
        _isJumped = false;
        _jumpCount = 0;
        _maxJumpCount = 2;
        _hp = _maxhp = 100.0f;
    }

    private void Update()
    {
        ProcessInput();     // 입력 처리
        UpdateCollider();   // 콜라이더 위치 설정

        // 목표 지점에 도착했으면 idle 애니메이션 재생
        if (_isOnGround && GameManager.instance._isFinished && !_animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _animator.SetTrigger("idle");
        }
    }

    private void FixedUpdate()
    {
        // 목표 지점을 넘어갈 수 없게 설정
        if (transform.position.x < GameManager.instance._finishLine)
            _rigidbody.velocity = new Vector2(5.0f, _rigidbody.velocity.y);
        else
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);

        // 점프
        if (_isJumped)
        {
            _isJumped = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 17.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 바닥에 착지하면 달리기로 변경, 점프 횟수 초기화
        if (other.gameObject.CompareTag("Ground"))
        {
            _animator.SetTrigger("run");
            _jumpCount = 0;
            _isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            OnDamaged();
        else if (other.gameObject.CompareTag("Finish"))
            GameManager.instance._isFinished = true;
    }

    private void ProcessInput()
    {
        // 달리는 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.F))
                Jump();

            // 슬라이딩
            if (Input.GetKey(KeyCode.J))
                _animator.SetBool("isSliding", true);

            // 밑으로 떨어지는 중이라면 떨어지는 애니메이션 재생
            if (!_isOnGround && _rigidbody.velocity.y < 0.0f)
            {
                _animator.SetTrigger("fall");
                ++_jumpCount;
            }
        }

        // 슬라이딩 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("slide"))
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.F))
            {
                _animator.SetBool("isSliding", false);
                Jump();
            }

            // 달리기
            if (!Input.GetKey(KeyCode.J))
            {
                _animator.SetBool("isSliding", false);
                _animator.SetTrigger("run");
            }
        }

        // 점프 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            // 추가 점프
            if (Input.GetKeyDown(KeyCode.F) && _jumpCount < _maxJumpCount)
                Jump();

            // 점프 애니메이션이 끝나면 떨어지는 애니메이션 재생
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                _animator.SetTrigger("fall");
        }

        // 떨어지는 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            // 추가 점프
            if (Input.GetKeyDown(KeyCode.F) && _jumpCount < _maxJumpCount)
                Jump();
        }
    }

    private void UpdateCollider()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            _collider.offset = new Vector2(0.3f, -0.17f);
            _collider.size = new Vector2(0.73f, 1.85f);
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            _collider.offset = new Vector2(0.3f, 0.1f);
            _collider.size = new Vector2(0.73f, 1.85f);
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            _collider.offset = new Vector2(0.3f, -0.17f);
            _collider.size = new Vector2(0.73f, 1.85f);
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("slide"))
        {
            _collider.offset = new Vector2(0.12f, -0.75f);
            _collider.size = new Vector2(1.6f, 0.65f);
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("jump");
        _isJumped = true;
        ++_jumpCount;
    }

    public void OnGainGem(Gem gem)
    {
        GameManager.instance._score += gem._score;
        Destroy(gem.gameObject);
    }

    public void OnDamaged()
    {
        _hp -= 10.0f;
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Invoke("OnDamagedExit", 1.5f);
    }

    public void OnDamagedExit()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public float GetHp()
    {
        return _hp;
    }

    public float GetMaxHp()
    {
        return _maxhp;
    }
}
