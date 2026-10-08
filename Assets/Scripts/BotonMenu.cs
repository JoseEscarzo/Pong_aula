using UnityEngine;
using UnityEngine.InputSystem;

public class BotonMenu : MonoBehaviour
{
    public enum Tipo { Jugar, Salir }
    public Tipo accion;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;

        Vector2 pos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        var col = GetComponent<Collider2D>();
        if (col != null && col.OverlapPoint(pos))
        {
            if (accion == Tipo.Jugar) Marcador.Instance.Jugar();
            else Marcador.Instance.Salir();
        }
    }
}