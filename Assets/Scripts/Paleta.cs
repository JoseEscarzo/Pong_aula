using UnityEngine;

public class Paleta : MonoBehaviour
{
    public float velocidad = 10f;
    public float limite = 3.5f;
    public KeyCode teclaArriba = KeyCode.W;
    public KeyCode teclaAbajo = KeyCode.S;

    void Update()
    {
        if (Marcador.Instance != null && Marcador.Instance.terminado) return;

        float mov = 0f;
        if (Input.GetKey(teclaArriba)) mov = 1f;
        if (Input.GetKey(teclaAbajo)) mov = -1f;

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + mov * velocidad * Time.deltaTime, -limite, limite);
        transform.position = pos;
    }
}