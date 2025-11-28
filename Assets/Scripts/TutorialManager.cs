using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject panelControles;
    public GameObject panelAlimentos;

    void Start()
    {
        MostrarPanelControles();
    }

    public void MostrarPanelControles()
    {
        panelControles.SetActive(true);
        panelAlimentos.SetActive(false);
    }

    public void MostrarPanelAlimentos()
    {
        panelControles.SetActive(false);
        panelAlimentos.SetActive(true);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}