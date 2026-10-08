using UnityEngine;

public class Gol : MonoBehaviour
{
    public bool puntoParaRojo;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (!otro.CompareTag("Bola")) return;
        if (Marcador.Instance.terminado) return;

        Bola bola = otro.GetComponent<Bola>();
        bola.SonarGol();
        Marcador.Instance.Punto(puntoParaRojo);
        bola.Lanzar();
    }
}