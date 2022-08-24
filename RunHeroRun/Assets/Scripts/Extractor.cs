using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    public GameObject _obstacles;
    public int _stage;

    private void Start()
    {
        Obstacles obstacles = new Obstacles();
        obstacles.data = new List<Obstacle>();
        for (int i = 0; i < _obstacles.transform.childCount; ++i)
        {
            Transform o = _obstacles.transform.GetChild(i);
            Obstacle obstacle = new Obstacle();
            obstacle.type = 1;
            obstacle.x = o.position.x;
            obstacle.y = o.position.y;
            obstacles.data.Add(obstacle);
        }
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/stage" + _stage + ".json", JsonUtility.ToJson(obstacles, true));
    }
}
