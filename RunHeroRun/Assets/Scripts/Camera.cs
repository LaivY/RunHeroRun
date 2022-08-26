using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GameManager.instance._player;
    }

    private void Update()
    {
        // 플레이어를 따라가도록
        Vector3 pos = _player.transform.position;
        pos.x += 5.0f;
        pos.x = Mathf.Max(6.0f, pos.x);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
