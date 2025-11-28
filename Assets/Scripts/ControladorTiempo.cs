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
        

        int puntajeFinal = GameManager.instancia.PuntajeTotal;

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

}