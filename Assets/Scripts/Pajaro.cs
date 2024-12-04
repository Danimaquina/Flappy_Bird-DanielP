using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}