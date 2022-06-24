using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public bool levelPlaying;

    [Header("Gameobjects")]
    public GameObject[] asteroids;

    [Header("End Panel")]
    public GameObject endPanel;
    public TextMeshProUGUI stateTitle;
    public TextMeshProUGUI playerScoreText;

    [Header("Game Stats")]
    public int playerLives;
    public int playerScore;

    private PaddleMovement paddleMovement;
    private Ball[] balls;
    private Vector2[] ballsVelocity;

    private void Awake()
    {
        Instance = this;
        levelPlaying = false;
        paddleMovement = FindObjectOfType<PaddleMovement>();
        balls = FindObjectsOfType<Ball>();
        playerLives = 3;
        playerScore = 0;

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

    public bool ActiveAsteroids()
    {
        foreach(GameObject asteroid in asteroids)
        {
            if (asteroid.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

    public void AddScore()
    {
        switch (playerLives)
        {
            case 1:
                playerScore += 50;
                break;
            case 2:
                playerScore += 75;
                break;
            case 3:
                playerScore += 100;
                break;
        }
    }

    public void EndLevel(string winLose)
    {
        SetMovement(false);
        endPanel.SetActive(true);

        if (winLose == "win")
        {
            stateTitle.text = "You Win!";
        }
        else
        {
            stateTitle.text = "You Lose!";
        }

        playerScoreText.text = $"Score: {playerScore}";
    }

    public void CheckPlayerLives()
    {
        playerLives--;

        if (playerLives <= 0)
        {
            EndLevel("lose");
        }
    }
}
