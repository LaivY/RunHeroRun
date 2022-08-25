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

    private void Start()
    {
        
    }

    private void Update()
    {
        _offset += _scrollSpeed * Time.deltaTime;
        _renderer.material.mainTextureOffset = new Vector2(_offset, 0.0f);
    }
}
