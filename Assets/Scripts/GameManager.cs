using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text finalScoreText;
    public Text bestText;
    RectTransform gameOverCard;
    AudioSource sfx;
    Renderer ringRend;
    Camera cam;
    Color ringColor;
    float ringFlash = 0f;
    float ringFlashLength = 0.25f;
    float hitStopTime = 0f;
    int score = 0;
    public bool isGameOver = false;

    float slowMotion = 0.3f;
    float panelDelay = 0.5f;

    public Color skyEnd = new Color(0.22f, 0.11f, 0.19f);
    public float skyScore = 30f;
    Color skyStart;

    void Awake()
    {
        instance = this;
        sfx = gameObject.AddComponent<AudioSource>();
        gameOverCard = gameOverPanel.transform.Find("Card") as RectTransform;
        ringRend = GameObject.Find("Cylinder").GetComponent<Renderer>();
        ringColor = ringRend.material.color;
        cam = Camera.main;
        skyStart = cam.backgroundColor;
    }

    private void Update()
    {
        scoreText.transform.localScale = Vector3.Lerp(scoreText.transform.localScale, Vector3.one, 12f * Time.unscaledDeltaTime);
        gameOverCard.localScale = Vector3.Lerp(gameOverCard.localScale, Vector3.one, 12f * Time.unscaledDeltaTime);

        ringFlashing();
        hitStopping();

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
        Play("gameover", 0.9f);
        CameraShake.instance.Shake(0.5f, 0.4f);
        Invoke("showGameOverPanel", panelDelay);
    }

    void showGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverCard.localScale = Vector3.one * 0.8f;
        finalScoreText.text = score.ToString();

        int best = PlayerPrefs.GetInt("best", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("best", score);
            bestText.text = "NEW BEST!";
        }
        else
        {
            bestText.text = "BEST " + best;
        }
    }

    public void AddScore()
    {
        if(isGameOver)return;
        score += 1;
        scoreText.text = score.ToString();
        scoreText.transform.localScale = Vector3.one * 1.3f;
        Play("score", Mathf.Min(1f + score * 0.03f, 1.6f));
        UpdateSky();
    }

    public void Play(string clipName, float pitch)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
        sfx.pitch = pitch;
        sfx.PlayOneShot(clip);
    }

    public void HitStop(float time)
    {
        if (isGameOver) return;
        Time.timeScale = 0.05f;
        hitStopTime = time;
    }

    void hitStopping()
    {
        if (hitStopTime > 0f)
        {
            hitStopTime -= Time.unscaledDeltaTime;
            if (hitStopTime <= 0f && isGameOver == false)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void FlashRing()
    {
        ringFlash = ringFlashLength;
    }

    void ringFlashing()
    {
        if (ringFlash > 0f)
        {
            ringFlash -= Time.unscaledDeltaTime;
            ringRend.material.color = Color.Lerp(ringColor, Color.white, ringFlash / ringFlashLength * 0.6f);
        }
    }

    void UpdateSky()
    {
        Color sky = Color.Lerp(skyStart, skyEnd, score / skyScore);
        cam.backgroundColor = sky;
        RenderSettings.fogColor = sky;
    }
}
