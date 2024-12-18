using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubos : MonoBehaviour
{
    public float velocidadMovimiento = 1f; 

    void Update()
    {
        // Mueve el objeto ligeramente hacia la izquierda
        transform.Translate(Vector2.left * velocidadMovimiento * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("final"))
        {
            Destroy(gameObject);
        }
    }
}