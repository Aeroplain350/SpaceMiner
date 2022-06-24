using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool levelPlaying;

    private PaddleMovement paddleMovement;
    private Ball[] balls;
    private Vector2[] ballsVelocity;

    private void Awake()
    {
        levelPlaying = false;
        paddleMovement = FindObjectOfType<PaddleMovement>();
        balls = FindObjectsOfType<Ball>();

        ballsVelocity = new Vector2[balls.Length];
    }

    public void StartLevel()
    {
        SetMovement(true);

        // set after to start initial random velocity
        levelPlaying = true;
    }

    public void PauseLevel()
    {
        SetMovement(false);
    }

    public void ResumeLevel()
    {
        if (!levelPlaying) return;

        SetMovement(true);
    }

    public void SetMovement(bool allowMovement)
    {
        paddleMovement.moveable = allowMovement;

        for(int i = 0; i < balls.Length; i++)
        {
            balls[i].moveable = allowMovement;

            if (allowMovement)
            {
                if (!levelPlaying)
                {
                    balls[i].StartMovement();
                }
                else
                {
                    balls[i].rb.velocity = ballsVelocity[i];
                }
            }
            else
            {
                // caches ball's velocity for when resuming game
                ballsVelocity[i] = balls[i].rb.velocity;
                balls[i].rb.velocity = Vector2.zero;
            }
        }
    }
}
