using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject _stage;

    private void Start()
    {
        
    }

    private void Update()
    {
        _stage.transform.Translate(-5.0f * Time.deltaTime, 0.0f, 0.0f);
    }
}
