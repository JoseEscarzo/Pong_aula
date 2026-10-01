using UnityEngine;
using UnityEngine.SceneManagement;

public class Marcador : MonoBehaviour
{
    public static Marcador Instance;
    public int puntosAzul;
    public int puntosRojo;
    public int puntosParaGanar = 5;
    public bool terminado;

    public SpriteRenderer numeroAzul;
    public SpriteRenderer numeroRojo;
    public Sprite[] digitos;

    public GameObject ganoAzul;
    public GameObject ganoRojo;

    void Awake() { Instance = this; }

    void Start() { Actualizar(); }

    void Update()
    {
        // R reinicia la partida al terminar
        if (terminado && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Punto(bool paraRojo)
    {
        if (terminado) return;

        if (paraRojo) puntosRojo++; else puntosAzul++;
        Actualizar();

        if (puntosAzul >= puntosParaGanar)
        {
            terminado = true;
            ganoAzul.SetActive(true);
        }
        else if (puntosRojo >= puntosParaGanar)
        {
            terminado = true;
            ganoRojo.SetActive(true);
        }
    }

    void Actualizar()
    {
        numeroAzul.sprite = digitos[Mathf.Min(puntosAzul, digitos.Length - 1)];
        numeroRojo.sprite = digitos[Mathf.Min(puntosRojo, digitos.Length - 1)];
    }
}