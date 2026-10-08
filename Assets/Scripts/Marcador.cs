using UnityEngine;
using UnityEngine.SceneManagement;

public class Marcador : MonoBehaviour
{
    public static Marcador Instance;
    public int puntosAzul;
    public int puntosRojo;
    public int puntosParaGanar = 5;
    public float segundosAntesMenu = 3f;

    public bool terminado = true; // true = no se juega

    public SpriteRenderer numeroAzul;
    public SpriteRenderer numeroRojo;
    public Sprite[] digitos;

    public GameObject ganoAzul;
    public GameObject ganoRojo;
    public GameObject panelMenu;
    public Bola bola;

    void Awake() { Instance = this; }

    void Start()
    {
        ganoAzul.SetActive(false);
        ganoRojo.SetActive(false);
        panelMenu.SetActive(true);
        terminado = true;
        Actualizar();
        bola.Detener();
    }

    public void Punto(bool paraRojo)
    {
        if (terminado) return;

        if (paraRojo) puntosRojo++; else puntosAzul++;
        Actualizar();

        if (puntosAzul >= puntosParaGanar) Terminar(ganoAzul);
        else if (puntosRojo >= puntosParaGanar) Terminar(ganoRojo);
    }

    void Terminar(GameObject cartel)
    {
        terminado = true;
        bola.Detener();
        cartel.SetActive(true);
        Invoke("MostrarMenu", segundosAntesMenu);
    }

    void MostrarMenu()
    {
        ganoAzul.SetActive(false);
        ganoRojo.SetActive(false);
        panelMenu.SetActive(true);
    }

    void Actualizar()
    {
        numeroAzul.sprite = digitos[Mathf.Min(puntosAzul, digitos.Length - 1)];
        numeroRojo.sprite = digitos[Mathf.Min(puntosRojo, digitos.Length - 1)];
    }

    public void Jugar()
    {
        puntosAzul = 0;
        puntosRojo = 0;
        Actualizar();
        panelMenu.SetActive(false);
        terminado = false;
        bola.Lanzar();
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}