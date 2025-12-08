using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    MainMenu,
    Start,
    Prediction,
    Dodge,
    Shoot,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;

    public Follow cameraFollow;
    public PlayerController player;
    public ShootBullet shooter;
    private Vector3[] bulletTargets;
    private LineRenderer[] bulletPaths;
    private float animationStartTime;
    private float currentAnimationLength;

    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    void Awake()
    {
        instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        print("Game State changed to: " + newState.ToString());
        switch (newState)
        {
            case GameState.MainMenu:
                // Handle main menu state
                break;
            case GameState.Start:
                // Handle start state
                handleStart();
                break;
            case GameState.Prediction:
                // Handle prediction state
                handlePrediction();
                break;
            case GameState.Dodge:
                // Handle dodge state
                handleDodge();
                break;
            case GameState.Shoot:
                // Handle shoot state
                handleShoot();
                break;
            case GameState.GameOver:
                // Handle game over state
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public void UpdateGameStateInt(int newState)
    {
        UpdateGameState((GameState)newState);
    }

    private void handleStart()
    {
        cameraFollow.enabled = true; // enable camera follow
        player.setIdleFalse(); // set animator boolean idle false
    }

    private void handlePrediction()
    {
        bulletTargets = new Vector3[6];
        bulletPaths = new LineRenderer[6];
        for (int i = 0; i < 6; i++)
        {
            bulletTargets[i] = shooter.GetRandomShootTarget();
            bulletPaths[i] = shooter.bulletPath(bulletTargets[i]);
        }
        UpdateGameStateInt((int)GameState.Dodge);
    }

    private void handleDodge()
    {
        Time.timeScale = 0.5f;
        // wait for player input
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 3f && player.inputReady && player.isAlive)
        {
            yield return null; // wait for next frame
        }

        // play animation for 0.75 seconds
        animationStartTime = Time.time;
        currentAnimationLength = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 2f;
        while (Time.time - animationStartTime < 1.5f && player.isAlive)
        {
            yield return null; // wait for next frame
        }

        // destroy bullet paths
        for (int i = 0; i < bulletPaths.Length; i++)
        {
            if (bulletPaths[i] != null)
            {
                Destroy(bulletPaths[i].gameObject);
            }
        }

        UpdateGameStateInt((int)GameState.Shoot);
    }

    private void handleShoot()
    {
        // set timescale to 1
        Time.timeScale = 1f;

        // shoot bullets
        for (int i = 0; i < 6; i++)
        {
            StartCoroutine(shootDelay(i));
        }

        // wait for animation to finish
        StartCoroutine(WaitForAnimation());

        // proceed to prediction phase again
        UpdateGameStateInt((int)GameState.Prediction);
    }

    private IEnumerator shootDelay(int i) {
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            yield return null; // wait for next frame
        }
        shooter.Shoot(bulletTargets[i]);
    }

    private IEnumerator WaitForAnimation() {
        while (Time.time - animationStartTime < currentAnimationLength + 2f && player.isAlive)
        {
            yield return null; // wait for next frame
        }
        bulletPaths = null;
        bulletTargets = null;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


