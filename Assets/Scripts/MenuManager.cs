using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public Button quitButton;

    void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameState.GameOver:
                ShowGameOverMenu();
                break;
            default:
                HideAllMenus();
                break;
        }
    }

    void ShowMainMenu()
    {
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
    }

    void ShowGameOverMenu()
    {
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    void HideAllMenus()
    {
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
}