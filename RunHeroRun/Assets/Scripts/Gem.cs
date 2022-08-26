using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int _score;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance._player.OnGainGem(this);
    }
}
