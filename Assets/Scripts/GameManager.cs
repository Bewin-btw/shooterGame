using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Score & Win Condition")]
    public int scoreToWin = 5;
    private int currentScore = 0;

    [Header("UI Elements")]
    public GameObject victoryUI;     // Panel с текстом «Вы выжили!» + score
    public Text scoreText;           // Текст, где показывается счёт
    public GameObject defeatUI;      // Panel с текстом «Проигрыш»

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
    }

    public void AddScore(int amount = 1)
    {
        currentScore += amount;
        UpdateScoreText();

        if (currentScore >= scoreToWin)
            Win();
    }

    public void Lose()
    {
        // Разблокировать курсор
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        defeatUI.SetActive(true);
    }

    private void Win()
    {
        // Разблокировать курсор
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        victoryUI.SetActive(true);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}/{scoreToWin}";
    }
}
