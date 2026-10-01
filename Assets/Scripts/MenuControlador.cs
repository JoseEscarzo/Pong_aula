using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

/// <summary>
/// Menú del Pong. Jugar abre la escena "Juego" cuando ya esté en Build Settings.
/// Salir cierra el juego. Enter equivale a Jugar y Escape a Salir.
/// </summary>
public class MenuControlador : MonoBehaviour
{
    static readonly Color Cesped = new Color(0.18f, 0.48f, 0.22f);
    static readonly Color Madera = new Color(0.62f, 0.40f, 0.18f);
    static readonly Color JugarNormal = new Color(0.20f, 0.52f, 0.28f);
    static readonly Color JugarHover = new Color(0.30f, 0.66f, 0.36f);
    static readonly Color SalirNormal = new Color(0.70f, 0.26f, 0.20f);
    static readonly Color SalirHover = new Color(0.84f, 0.36f, 0.28f);

    RectTransform zonaJugar;
    RectTransform zonaSalir;
    Image imagenJugar;
    Image imagenSalir;
    Text aviso;
    bool saliendo;

    void Start()
    {
        PrepararCamara();
        PrepararEventos();
        ConstruirInterfaz();
    }

    void Update()
    {
        if (imagenJugar != null)
            imagenJugar.color = Encima(zonaJugar) ? JugarHover : JugarNormal;
        if (imagenSalir != null)
            imagenSalir.color = Encima(zonaSalir) ? SalirHover : SalirNormal;

        if (TeclaPresionada(KeyCode.Return, KeyCode.KeypadEnter, enter: true))
            Jugar();
        else if (TeclaPresionada(KeyCode.Escape, KeyCode.None, enter: false))
            Salir();
        else if (ClickIzquierdo())
        {
            if (Encima(zonaJugar))
                Jugar();
            else if (Encima(zonaSalir))
                Salir();
        }
    }

    public void Jugar()
    {
        if (saliendo)
            return;

        if (EscenaEnBuild("Juego"))
        {
            saliendo = true;
            SceneManager.LoadScene("Juego");
            return;
        }

        if (aviso != null)
            aviso.text = "JUGAR está listo. Falta agregar la escena Juego a Build Settings.";
    }

    public void Salir()
    {
        if (saliendo)
            return;
        saliendo = true;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void PrepararCamara()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            var go = new GameObject("Main Camera");
            go.tag = "MainCamera";
            cam = go.AddComponent<Camera>();
            go.AddComponent<AudioListener>();
        }

        cam.orthographic = true;
        cam.orthographicSize = 5f;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Cesped;
        cam.transform.position = new Vector3(0f, 0f, -10f);
    }

    static void PrepararEventos()
    {
        if (FindAnyObjectByType<EventSystem>() != null)
            return;

        var eventos = new GameObject("EventSystem");
        eventos.AddComponent<EventSystem>();
#if ENABLE_INPUT_SYSTEM
        eventos.AddComponent<InputSystemUIInputModule>();
#else
        eventos.AddComponent<StandaloneInputModule>();
#endif
    }

    void ConstruirInterfaz()
    {
        Font fuente = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        var canvasGo = new GameObject("MenuCanvas");
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGo.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGo.AddComponent<GraphicRaycaster>();

        var fondo = CrearImagen("Fondo", canvasGo.transform, Cesped);
        Estirar(fondo.rectTransform);

        var panel = CrearImagen("Panel", canvasGo.transform, Madera);
        var panelRt = panel.rectTransform;
        panelRt.anchorMin = panelRt.anchorMax = new Vector2(0.5f, 0.5f);
        panelRt.sizeDelta = new Vector2(720f, 820f);

        var layout = panel.gameObject.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(48, 48, 54, 40);
        layout.spacing = 22f;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        CrearTexto("Titulo", panel.transform, "PONG", fuente, 96, new Color(1f, 0.62f, 0.18f), 120f);
        CrearTexto("Subtitulo", panel.transform, "DOS JUGADORES", fuente, 32, new Color(1f, 0.94f, 0.82f), 48f);

        imagenJugar = CrearImagen("Jugar", panel.transform, JugarNormal);
        zonaJugar = imagenJugar.rectTransform;
        zonaJugar.gameObject.AddComponent<LayoutElement>().preferredHeight = 92f;
        Estirar(CrearTexto("Texto", imagenJugar.transform, "JUGAR", fuente, 42, Color.white, 92f).rectTransform);

        imagenSalir = CrearImagen("Salir", panel.transform, SalirNormal);
        zonaSalir = imagenSalir.rectTransform;
        zonaSalir.gameObject.AddComponent<LayoutElement>().preferredHeight = 92f;
        Estirar(CrearTexto("Texto", imagenSalir.transform, "SALIR", fuente, 42, Color.white, 92f).rectTransform);

        CrearTexto("Controles", panel.transform, "AZUL   W / S          ROJO   FLECHAS", fuente, 24, new Color(1f, 0.96f, 0.88f), 40f);
        aviso = CrearTexto("Aviso", panel.transform, "ENTER juega    ESCAPE sale", fuente, 22, new Color(1f, 0.9f, 0.55f), 36f);
    }

    static bool EscenaEnBuild(string nombre)
    {
        int total = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < total; i++)
        {
            string ruta = SceneUtility.GetScenePathByBuildIndex(i);
            if (Path.GetFileNameWithoutExtension(ruta) == nombre)
                return true;
        }
        return false;
    }

    static bool Encima(RectTransform zona)
    {
        if (zona == null)
            return false;
        return RectTransformUtility.RectangleContainsScreenPoint(zona, Puntero(), null);
    }

    static Vector2 Puntero()
    {
#if ENABLE_INPUT_SYSTEM
        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        return Input.mousePosition;
#else
        return Vector2.zero;
#endif
    }

    static bool ClickIzquierdo()
    {
#if ENABLE_INPUT_SYSTEM
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            return true;
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetMouseButtonDown(0);
#else
        return false;
#endif
    }

    static bool TeclaPresionada(KeyCode principal, KeyCode extra, bool enter)
    {
#if ENABLE_INPUT_SYSTEM
        Keyboard teclado = Keyboard.current;
        if (teclado != null)
        {
            if (enter && (teclado.enterKey.wasPressedThisFrame || teclado.numpadEnterKey.wasPressedThisFrame))
                return true;
            if (!enter && teclado.escapeKey.wasPressedThisFrame)
                return true;
        }
#endif
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(principal))
            return true;
        if (extra != KeyCode.None && Input.GetKeyDown(extra))
            return true;
#endif
        return false;
    }

    static Image CrearImagen(string nombre, Transform padre, Color color)
    {
        var go = new GameObject(nombre);
        go.transform.SetParent(padre, false);
        var imagen = go.AddComponent<Image>();
        imagen.color = color;
        return imagen;
    }

    static Text CrearTexto(string nombre, Transform padre, string mensaje, Font fuente, int tamano, Color color, float alto)
    {
        var go = new GameObject(nombre);
        go.transform.SetParent(padre, false);
        var texto = go.AddComponent<Text>();
        texto.font = fuente;
        texto.text = mensaje;
        texto.fontSize = tamano;
        texto.alignment = TextAnchor.MiddleCenter;
        texto.color = color;
        texto.horizontalOverflow = HorizontalWrapMode.Overflow;
        go.AddComponent<LayoutElement>().preferredHeight = alto;
        return texto;
    }

    static void Estirar(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
