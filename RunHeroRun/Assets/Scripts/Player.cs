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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _maxJumpCount = 2;
        _jumpCount = 0;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 착지하면 달리기로 변경, 점프 횟수 초기화
        _animator.SetTrigger("run");
        _jumpCount = 0;
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

    private void Jump()
    {
        _animator.SetTrigger("jump");
        _rigidbody.velocity = new Vector2(0.0f, 20.0f);
        ++_jumpCount;
    }
}
