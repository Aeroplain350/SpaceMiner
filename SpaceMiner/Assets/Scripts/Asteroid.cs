using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBall") &&
            gameObject.layer == LayerMask.NameToLayer("EnemyAsteroids"))
        {
            gameObject.SetActive(false);

            LevelManager.Instance.AddScore();
            AudioManager.Instance.Play("Explosion");
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyBall") &&
            gameObject.layer == LayerMask.NameToLayer("PlayerAsteroids"))
        {
            gameObject.SetActive(false);
            AudioManager.Instance.Play("Explosion");
        }

        if (!LevelManager.Instance.ActiveAsteroids())
        {
            LevelManager.Instance.EndLevel("win");
        }
    }
}
