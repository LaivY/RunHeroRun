using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Obstacle
{
    public int type;
    public float x;
    public float y;
}

[System.Serializable]
public class Obstacles
{
    public List<Obstacle> data;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Camera _camera;
    public Player _player;
    public GameObject _stage;
    public GameObject _scoreText;
    public GameObject _progressBar;
    public int _score;
    public float _finish;

    private void Awake()
    {
        // 싱글톤
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        // 목표 위치
        Transform transform = _stage.gameObject.transform.Find("Finish");
        _finish = transform.position.x;
    }
}
