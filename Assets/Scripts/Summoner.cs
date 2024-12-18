using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    public GameObject prefab;
    
    public float tiempoGeneracion = 1f;

    // Rango para modificaciones aleatorias
    public Vector3 rangoPosicionMinima = new Vector3(0.0f, -1.0f, 0.0f);
    public Vector3 rangoPosicionMaxima = new Vector3(0.0f, 1.0f, 0.0f);
   

    private void Start()
    {
        // Llama a la función de generar prefabs repetidamente
        InvokeRepeating("GenerarPrefab", 0f, tiempoGeneracion);
    }

    // Función que genera el prefab y lo modifica aleatoriamente
    private void GenerarPrefab()
    {
        GameObject objetoGenerado = Instantiate(prefab, transform.position, Quaternion.identity);

        // Modificar aleatoriamente la posición del objeto
        Vector3 posicionAleatoria = new Vector3(
            Random.Range(rangoPosicionMinima.x, rangoPosicionMaxima.x),
            Random.Range(rangoPosicionMinima.y, rangoPosicionMaxima.y),
            Random.Range(rangoPosicionMinima.z, rangoPosicionMaxima.z)
        );
        objetoGenerado.transform.position += posicionAleatoria;
    }
}