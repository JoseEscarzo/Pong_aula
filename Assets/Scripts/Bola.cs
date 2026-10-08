using UnityEngine;

public class Bola : MonoBehaviour
{
    public float velocidad = 8f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Detener()
    {
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }

    public void Lanzar()
    {
        transform.position = Vector2.zero;

        if (Marcador.Instance != null && Marcador.Instance.terminado)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float dirX = Random.value < 0.5f ? -1f : 1f;
        float dirY = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(dirX, dirY).normalized * velocidad;
    }

    void FixedUpdate()
    {
        if (Marcador.Instance != null && Marcador.Instance.terminado) return;
        rb.linearVelocity = rb.linearVelocity.normalized * velocidad;
    }
}