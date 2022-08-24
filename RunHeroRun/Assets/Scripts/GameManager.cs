using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject _obstacles;
    public GameObject _prefeb;
    public int _stage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        LoadStageData();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _obstacles.transform.Translate(-5.0f * Time.deltaTime, 0.0f, 0.0f);
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
}