using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float _progressBarWidth;
    private Player _player;
    private float _finish;

    private void Start()
    {
        _player = GameManager.instance._player;
        _finish = GameManager.instance._finishLine;
    }

    private void Update()
    {
        float xPos = Mathf.Lerp(-_progressBarWidth / 2.0f, _progressBarWidth / 2.0f, _player.transform.position.x / _finish);
        transform.localPosition = new Vector3(xPos, 0.0f, 0.0f);
    }
}
