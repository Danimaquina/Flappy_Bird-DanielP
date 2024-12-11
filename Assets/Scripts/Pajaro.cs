using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pajaro : MonoBehaviour
{
    public float impulso;  
    public float rotacionMaxima = 45f;  
    public float velocidadRotacion = 5f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Obtiene el Rigidbody2D del objeto
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Aplica una fuerza hacia arriba en el Rigidbody2D
            rb2d.velocity = Vector2.zero;  
            rb2d.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
        }

        // Rotación del objeto para simular la inclinación
        float rotacion = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Sign(rb2d.velocity.y) * rotacionMaxima, velocidadRotacion * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotacion);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto con el que colisionamos tiene el tag "tubo"
        if (other.CompareTag("tubo"))
        {
            // Congela el tiempo en el juego
            Time.timeScale = 0f;
        
            // Espera unos segundos (3 segundos en este caso) y luego reinicia la escena
            StartCoroutine(ReiniciarEscenaConRetraso(2f));
        }
    }

    private IEnumerator ReiniciarEscenaConRetraso(float tiempoDeEspera)
    {
        // Espera el tiempo dado, pero sin detener el contador global
        yield return new WaitForSecondsRealtime(tiempoDeEspera);
    
        // Reiniciar la escena actual
        Time.timeScale = 1f; // Restaura el tiempo del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarga la escena
    }

}