using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pajaro : MonoBehaviour
{
    public float impulso;  
    public float rotacionMaxima = 45f;  
    public float velocidadRotacion = 5f;
    public GameObject GameOver;
    public GameObject info;
    public TextMeshProUGUI Score;

    private int ScoreValue;
    private Rigidbody2D rb2d;
    private bool esperandoReinicio = false;

    public AudioSource audio;
    public AudioClip clip;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  
        ScoreValue = 0;
        GameOver.SetActive(false);
        info.SetActive(false);
    }

    void Update()
    {
        Score.text = ScoreValue.ToString();

        // Detectar salto si no estamos en estado de reinicio
        if (!esperandoReinicio && Input.anyKeyDown)
        {
            // Aplica una fuerza hacia arriba en el Rigidbody2D
            rb2d.velocity = Vector2.zero;  
            rb2d.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
        }

        // Rotación del objeto para simular la inclinación
        float rotacion = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Sign(rb2d.velocity.y) * rotacionMaxima, velocidadRotacion * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotacion);

        // Detectar reinicio cuando estamos esperando
        if (esperandoReinicio && Input.anyKeyDown)
        {
            StartCoroutine(ReiniciarEscenaConRetraso(0.1f));
            esperandoReinicio = false; // Evita múltiples llamadas al Coroutine
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto con el que colisionamos tiene el tag "tubo"
        if (other.CompareTag("tubo"))
        {
            // Congela el tiempo en el juego
            Time.timeScale = 0f;

            GameOver.SetActive(true);
            info.SetActive(true);

            esperandoReinicio = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Paso"))
        {
            ScoreValue++;
            audio.PlayOneShot(clip);
        }
    }

    private IEnumerator ReiniciarEscenaConRetraso(float tiempoDeEspera)
    {
        // Espera el tiempo dado, pero sin detener el contador global
        yield return new WaitForSecondsRealtime(tiempoDeEspera);

        // Reiniciar la escena actual
        Time.timeScale = 1f; // Restaura el tiempo del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena
    }
}
