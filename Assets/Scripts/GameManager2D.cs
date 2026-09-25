using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Central game manager that tracks score, game-over state, and win state.
/// Uses a simple singleton so other scripts can access it via GameManager2D.Instance.
/// Attach this to an empty GameObject named "GameManager".
/// </summary>
public class GameManager2D : MonoBehaviour
{
    // ---- Singleton ----
    public static GameManager2D Instance { get; private set; }

    // ---- Inspector References ----

    // The dinosaur's runner script, used to stop movement.
    [SerializeField] private DinosaurRunner2D dinosaurRunner;

    // The TextMeshProUGUI that shows the current score during gameplay.
    [SerializeField] private TextMeshProUGUI scoreText;

    // The Game Over panel (should be hidden at start).
    [SerializeField] private GameObject gameOverPanel;

    // The TextMeshProUGUI on the Game Over panel that shows the final score.
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    // The You Win panel (should be hidden at start).
    [SerializeField] private GameObject youWinPanel;

    // The TextMeshProUGUI on the You Win panel that shows the final score.
    [SerializeField] private TextMeshProUGUI youWinScoreText;

    // ---- State ----

    // The number of collectibles the player has collected.
    private int score = 0;

    // True after Game Over or Win. Other scripts check this to disable actions.
    public bool IsGameEnded { get; private set; } = false;

    // ---- Unity Lifecycle ----

    private void Awake()
    {
        // Simple singleton setup.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Log errors for missing references.
        if (dinosaurRunner == null)
            Debug.LogError("GameManager2D: DinosaurRunner2D reference is not assigned!", this);
        if (scoreText == null)
            Debug.LogError("GameManager2D: Score Text reference is not assigned!", this);
        if (gameOverPanel == null)
            Debug.LogError("GameManager2D: Game Over Panel reference is not assigned!", this);
        if (gameOverScoreText == null)
            Debug.LogError("GameManager2D: Game Over Score Text reference is not assigned!", this);
        if (youWinPanel == null)
            Debug.LogError("GameManager2D: You Win Panel reference is not assigned!", this);
        if (youWinScoreText == null)
            Debug.LogError("GameManager2D: You Win Score Text reference is not assigned!", this);
    }

    private void Start()
    {
        // Make sure both end-game panels are hidden when the scene begins.
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (youWinPanel != null) youWinPanel.SetActive(false);

        // Show the initial score.
        UpdateScoreText();
    }

    // ---- Public Methods ----

    /// <summary>
    /// Add exactly 1 point to the score. Called by Collectible2D.
    /// Does nothing if the game has already ended.
    /// </summary>
    public void AddCollectible()
    {
        if (IsGameEnded) return;

        score += 1;
        UpdateScoreText();
    }

    /// <summary>
    /// Trigger the Game Over state. Called by Obstacle2D.
    /// Works only once.
    /// </summary>
    public void GameOver()
    {
        if (IsGameEnded) return;

        IsGameEnded = true;

        // Stop the dinosaur.
        if (dinosaurRunner != null)
        {
            dinosaurRunner.StopMovement();
        }

        // Show the Game Over panel with the final score.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Final Score: " + score;
        }
    }

    /// <summary>
    /// Trigger the Win state. Called by FinishTrigger2D.
    /// Works only once. Does not show the Game Over panel.
    /// </summary>
    public void Win()
    {
        if (IsGameEnded) return;

        IsGameEnded = true;

        // Stop the dinosaur.
        if (dinosaurRunner != null)
        {
            dinosaurRunner.StopMovement();
        }

        // Show the You Win panel with the final score.
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true);
        }

        if (youWinScoreText != null)
        {
            youWinScoreText.text = "Final Score: " + score;
        }
    }

    /// <summary>
    /// Reload the current scene. Connect this to RESTART button OnClick events.
    /// </summary>
    public void RestartLevel()
    {
        // Reset the singleton so the new scene creates a fresh instance.
        Instance = null;

        // Reload the currently active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ---- Private Helpers ----

    /// <summary>
    /// Update the on-screen score text.
    /// </summary>
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
