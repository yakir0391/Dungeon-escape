using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private Player _player;
    public int gems = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (collision.tag == "Player")
        {
            _player.AddGems(gems);
            Destroy(this.gameObject);
        }
    }
}
