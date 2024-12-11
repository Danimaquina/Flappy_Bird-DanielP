using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubos : MonoBehaviour
{
    public float velocidadMovimiento = 1f; // Velocidad a la que se mueve el tubo

    void Update()
    {
        // Mueve el objeto ligeramente hacia la izquierda
        transform.Translate(Vector2.left * velocidadMovimiento * Time.deltaTime);
    }

    // Este método se llama cuando el objeto colisiona con otro objeto con un collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        // Verifica si el objeto con el que colisionamos tiene el tag "final"
        if (other.CompareTag("final"))
        {
            Destroy(gameObject);
        }
    }
}