using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    public GameObject _text;
    public GameObject _icon;

    private void Update()
    {
        float textWidth = _text.GetComponent<TextMeshProUGUI>().preferredWidth;
        Vector3 iconPos = _text.transform.position;
        iconPos.x -= 45.0f;
        iconPos.x -= textWidth * 20.0f / 2.0f;
        _icon.transform.position = iconPos;
    }
}
