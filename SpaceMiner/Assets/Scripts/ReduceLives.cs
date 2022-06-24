using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceLives : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerBall" &&
            gameObject.CompareTag("Enemy"))
        {
            LevelManager.Instance.CheckPlayerLives();
        }
    }
}
