using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject TextCurrentScoreUI;
    public GameObject TextBestScoreUI;
    public GameObject StartText;
    public GameObject CounterText;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;

    private int currentScore = 0;
    private int bestScore = 0;
    private bool isGameOver = false;

    public AudioSource audioSource;

    public AudioClip scoreClip;
    public AudioClip deathClip;

    private bool isGameStarted = false;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        GameOverUI.SetActive(false);
        TextCurrentScoreUI.SetActive(false);
        TextBestScoreUI.SetActive(false);
        StartText.SetActive(true);
        CounterText.SetActive(false);

        // Congelar el juego al inicio
        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Detectar si el jugador toca cualquier tecla o hace clic en la pantalla
        if (!isGameStarted && (Input.anyKeyDown || Input.touchCount > 0))
        {
            StartGame();
            StartText.SetActive(false);
            CounterText.SetActive(true);
        }
    }

    private void StartGame()
    {
        // Iniciar el juego
        isGameStarted = true;
        Time.timeScale = 1f;  // Reanudar el tiempo
    }

    public void AddScore()
    {
        if (!isGameOver)
        {
            currentScore++;
            ScoreText.text = currentScore.ToString();
            audioSource.PlayOneShot(scoreClip);
        }
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            
            audioSource.PlayOneShot(deathClip);


            // Verifica y guarda el mejor puntaje
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }

            // Muestra la UI de Game Over
            GameOverUI.SetActive(true);
            TextCurrentScoreUI.SetActive(true);
            TextBestScoreUI.SetActive(true);

            BestScoreText.text = "Best Score: " + bestScore;

            // Detiene el tiempo
            Time.timeScale = 0f;
        }
    }

    public void RestartGame(float delay = 0.1f)
    {
        StartCoroutine(RestartSceneAfterDelay(delay));
    }

    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  // Pausa de 0.1 segundos
        Time.timeScale = 1f;  // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reiniciar la escena
    }
}
