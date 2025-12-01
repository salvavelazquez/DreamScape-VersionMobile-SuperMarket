using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorTiempo : MonoBehaviour
{
    [Header("Pantallas de resultado")]
    public GameObject panelGanaste;
    public GameObject panelPerdiste;
    public TextMeshProUGUI textoResultadoPuntaje;

    public float tiempoInicial = 60f;
    private float tiempoRestante;
    private bool tiempoAgotado = false;

    public TextMeshProUGUI textoTiempo;
    



    void Start()
    {
        Time.timeScale = 1f;

        //Reactiva el carri en cada partida
        GameObject carrito = GameObject.FindWithTag("Carrito");
        if (carrito != null)
            carrito.SetActive(true);

        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTextoTiempo(tiempoRestante);
        }
        else if (!tiempoAgotado)
        {
            tiempoRestante = 0;
            tiempoAgotado = true;
            Debug.Log("¡El tiempo se ha agotado!");
            PausarJuego();
        }
    }

    void ActualizarTextoTiempo(float tiempo)
    {
        if (tiempo < 0)
        {
            tiempo = 0;
        }

        float minutos = Mathf.FloorToInt(tiempo / 60);
        float segundos = Mathf.FloorToInt(tiempo % 60);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void PausarJuego()
    {
        Time.timeScale = 0f;

        Debug.Log("Juego pausado. Volviendo al menú en 5 segundos...");
        StartCoroutine(VolverAlMenuDespuesDeEspera());
    }
    IEnumerator VolverAlMenuDespuesDeEspera()
    {
        MostrarResultado();
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Menu");
    }

    private void MostrarResultado()
    {
        DesactivarTextosFlotantesClone();

        int puntajeFinal = GameManager.instancia.PuntajeTotal;

        
        CarritoBehaviour carrito = FindFirstObjectByType<CarritoBehaviour>();
        if (carrito != null)
        {
            if (carrito.cartelAzucar != null)
                carrito.cartelAzucar.SetActive(false);

            if (carrito.cartelGaseosas != null)
                carrito.cartelGaseosas.SetActive(false);

            if (carrito.cartelGrasas != null)
                carrito.cartelGrasas.SetActive(false);
        }


        // Mostrar puntaje en el texto
        textoResultadoPuntaje.text = "Puntaje Final: " + puntajeFinal;

        // Ocultar ambos por seguridad
        panelGanaste.SetActive(false);
        panelPerdiste.SetActive(false);

        // Elegir panel correspondiente
        if (puntajeFinal > 0)
        {
            panelGanaste.SetActive(true);
        }
        else
        {
            panelPerdiste.SetActive(true);
        }
    }

    private void DesactivarTextosFlotantesClone()
    {
        // Buscar todos los objetos activos que sean clones de textos
        TextMeshProUGUI[] todosTextos = FindObjectsOfType<TextMeshProUGUI>();

        int clonesDesactivados = 0;

        foreach (TextMeshProUGUI texto in todosTextos)
        {
            if (texto.gameObject.activeInHierarchy && texto.gameObject.name.Contains("(Clone)"))
            {
                texto.gameObject.SetActive(false);
                clonesDesactivados++;
            }
        }

        Debug.Log($"Se desactivaron {clonesDesactivados} textos flotantes clone");
    }

}