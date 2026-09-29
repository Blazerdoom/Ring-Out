using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text finalScoreText;
    int score = 0;
    public bool isGameOver = false;

    float slowMotion = 0.3f;
    float panelDelay = 0.5f;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }
    }

    public void gameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = slowMotion;
        Invoke("showGameOverPanel", panelDelay);
    }

    void showGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = score.ToString();
    }

    public void AddScore()
    {
        if(isGameOver)return;
        score += 1;
        scoreText.text = score.ToString();
    }
}
