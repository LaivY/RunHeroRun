using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private float _offset;
    public float _scrollSpeed;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 시작한 직후(player.x < 5.0f + 6.0f), 게임이 끝났을 때는 배경을 스크롤하지 않음
        if (GameManager.instance._player.transform.position.x < 11.0f ||
            GameManager.instance._isFinished)
            return;

        _offset += _scrollSpeed * Time.deltaTime;
        _renderer.material.mainTextureOffset = new Vector2(_offset, 0.0f);
    }
}
