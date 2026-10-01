using UnityEngine;

public class Gol : MonoBehaviour
{
    public bool puntoParaRojo;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (!otro.CompareTag("Bola")) return;
        Marcador.Instance.Punto(puntoParaRojo);
        otro.GetComponent<Bola>().Lanzar();
    }
}