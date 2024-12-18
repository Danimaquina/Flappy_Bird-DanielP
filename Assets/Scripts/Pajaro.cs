using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Pajaro : MonoBehaviour
{
    public float impulso;
    public float rotacionMaxima = 45f;
    public float velocidadRotacion = 5f;

    private Rigidbody2D rb2d;
    private bool esperandoReinicio = false;

    private GameManager gameManager; 

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Animator animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        // Buscar GameManager en la escena
        gameManager = FindObjectOfType<GameManager>(); 
    }

    private void Update()
    {
        // Detectar salto si no estamos en estado de reinicio
        if (!esperandoReinicio && (Input.anyKeyDown || Input.touchCount > 0))
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
        }

        // Rotación del objeto para simular la inclinación
        float rotacion = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Sign(rb2d.velocity.y) * rotacionMaxima, velocidadRotacion * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotacion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tubo"))
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Tocado");

            // Verificar si gameManager no es null
            if (gameManager != null)
            {
                gameManager.TriggerGameOver();
            }
            esperandoReinicio = true;

            // Llamar al reinicio con un retraso de 3 segundos
            StartCoroutine(RestartAfterDelay(3f)); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Añade Score cuando el jugador pase entre 2 tubos
        if (other.CompareTag("Paso"))
        {
            if (gameManager != null)
            {
                gameManager.AddScore();
            }
        }
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);  
        if (gameManager != null)
        {
            gameManager.RestartGame();  // Reiniciar el juego
        }
    }
}
