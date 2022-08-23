using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private int _maxJumpCount;
    private int _jumpCount;
    private bool isCollidedWithWall;
    private float _hp;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _maxJumpCount = 2;
        _jumpCount = 0;
        isCollidedWithWall = false;
        _hp = 100.0f;
    }

    private void Update()
    {
        ProcessInput();     // 입력에 따른 상태 변환
        UpdateCollider();   // 상태에 따른 콜라이더 위치 설정
        UpdateSpeed();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 바닥에 착지하면 달리기로 변경, 점프 횟수 초기화
        if (other.gameObject.tag == "Ground")
        {
            _animator.SetTrigger("run");
            _jumpCount = 0;
        }

        // 벽과 충돌중임을 저장함
        if (other.gameObject.tag == "Wall")
        {
            _animator.SetTrigger("run");
            _jumpCount = 0;
            isCollidedWithWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            isCollidedWithWall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _hp -= 10.0f;
        Debug.Log("!");
    }

    private void ProcessInput()
    {
        // 달리는 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            // 슬라이딩
            if (Input.GetKey(KeyCode.LeftShift))
                _animator.SetBool("isSliding", true);

            // 밑으로 떨어지는 중이라면 떨어지는 애니메이션 재생
            if (_rigidbody.velocity.y < 0.0f)
                _animator.SetTrigger("fall");
        }

        // 슬라이딩 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("slide"))
        {
            // 점프
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetBool("isSliding", false);
                Jump();
            }

            // 달리기
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetBool("isSliding", false);
                _animator.SetTrigger("run");
            }
        }

        // 점프 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            // 추가 점프
            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumpCount)
                Jump();

            // 점프 애니메이션이 끝나면 떨어지는 애니메이션 재생
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                _animator.SetTrigger("fall");
        }

        // 떨어지는 중
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            // 추가 점프
            if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumpCount)
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

    private void UpdateSpeed()
    {
        // 벽에 의해 뒤로 밀렸다면, 다시 제자리로 오도록함
        if (transform.position.x < 0.0f && !isCollidedWithWall)
        {
            Vector3 pos = transform.position;
            pos.x += 3.0f * Time.deltaTime;
            pos.x = Mathf.Min(0.0f, pos.x);
            transform.position = pos;
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("jump");
        _rigidbody.velocity = new Vector2(0.0f, 17.5f);
        ++_jumpCount;
    }
}
