using UnityEngine;

public class Marcador : MonoBehaviour
{
    public static Marcador Instance;
    public int puntosAzul;
    public int puntosRojo;

    void Awake() { Instance = this; }

    public void Punto(bool paraRojo)
    {
        if (paraRojo) puntosRojo++; else puntosAzul++;
        Debug.Log("Azul " + puntosAzul + " - Rojo " + puntosRojo);
    }
}