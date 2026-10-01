using UnityEngine;

public class Bola : MonoBehaviour
{
    public float velocidad = 8f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Lanzar();
    }

    public void Lanzar()
    {
        transform.position = Vector2.zero;
        float dirX = Random.value < 0.5f ? -1f : 1f;
        float dirY = Random.Range(-0.5f, 0.5f);
        rb.linearVelocity = new Vector2(dirX, dirY).normalized * velocidad;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * velocidad;
    }
}