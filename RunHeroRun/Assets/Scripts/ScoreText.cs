using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _scoreTextMesh;

    private void Awake()
    {
        _scoreTextMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _scoreTextMesh.text = GameManager.instance._score.ToString();
    }
}
