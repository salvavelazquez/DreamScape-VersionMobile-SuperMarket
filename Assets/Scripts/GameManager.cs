using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    private int puntajeTotal = 0;
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoPutajeFinal;

    public int PuntajeTotal { get => puntajeTotal; set => puntajeTotal = value; }

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SumarPuntos(int puntos)
    {
        PuntajeTotal += puntos;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoPuntaje.text = "Puntaje: " + PuntajeTotal.ToString();
        textoPutajeFinal.text = "Tu Puntaje Final es: " + puntajeTotal.ToString();   
    }
}