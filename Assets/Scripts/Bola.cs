using UnityEngine;

public class Bola : MonoBehaviour
{
    public float velocidad = 8f;
    public AudioClip sonidoRebote;
    public AudioClip sonidoGol;

    Rigidbody2D rb;
    AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

    public void SonarGol()
    {
        if (sonidoGol != null) audioSource.PlayOneShot(sonidoGol);
    }

    // Suena en cada choque fisico (paletas y paredes)
    void OnCollisionEnter2D(Collision2D col)
    {
        if (Marcador.Instance != null && Marcador.Instance.terminado) return;
        if (sonidoRebote != null) audioSource.PlayOneShot(sonidoRebote);
    }

    void FixedUpdate()
    {
        if (Marcador.Instance != null && Marcador.Instance.terminado) return;
        rb.linearVelocity = rb.linearVelocity.normalized * velocidad;
    }
}