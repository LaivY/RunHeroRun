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
    public GameObject _obstacles;
    public GameObject _prefeb;
    public GameObject _scoreText;
    public int _stage;
    public int _score;
    private TextMeshProUGUI _scoreTextMesh;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        LoadStageData();
        _scoreTextMesh = _scoreText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector3 pos = _player.transform.position;
        pos.x += 5.0f;
        pos.x = Mathf.Max(6.0f, pos.x);
        pos.y = _camera.transform.position.y;
        pos.z = _camera.transform.position.z;
        _camera.transform.position = pos;

        _scoreTextMesh.text = _score.ToString();
    }

    private void LoadStageData()
    {
        TextAsset text = Resources.Load<TextAsset>("stage" + _stage);
        Obstacles walls = JsonUtility.FromJson<Obstacles>(text.ToString());
        foreach (Obstacle w in walls.data)
        {
            GameObject tmp = Instantiate(_prefeb);
            tmp.transform.position = new Vector3(w.x, w.y, 0.0f);
            tmp.transform.SetParent(_obstacles.transform);
        }
    }

    public void OnPlayerGetGem(Gem gem)
    {
        _score += gem.score;
        Destroy(gem.gameObject);
    }
}
