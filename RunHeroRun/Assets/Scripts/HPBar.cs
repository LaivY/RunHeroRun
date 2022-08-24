using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Image _image;
    private float _deltaHp;
    private float _hp;
    public Player _player;
    public float _decreaseSpeed;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _hp = _player.GetHp();
    }

    private void Update()
    {
        float hp = _player.GetHp();
        if (_hp != hp)
        {
            _deltaHp += _hp - hp;
            _hp = hp;
        }

        if (_deltaHp > 0.0f)
        {
            _deltaHp = Mathf.Max(0.0f, _deltaHp - _decreaseSpeed * Time.deltaTime);
            _image.fillAmount = (_hp + _deltaHp) / _player.GetMaxHp();
        }
    }
}
