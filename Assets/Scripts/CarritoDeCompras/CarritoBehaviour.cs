using UnityEngine;
using System.Collections; // Necesario para Corutinas

public class CarritoBehaviour : MonoBehaviour
{
    // VARIABLES PARA EL EFECTO
    [Header("Efectos de Gaseosa")]
    [SerializeField] private ParticleSystem efectoVisualGaseosa; 
    [SerializeField] private float duracionEfecto = 2.0f;
    [SerializeField] private float velocidadBoost = 7.5f;

    // VARIABLES PRIVADAS
    private CartAccelerometerController playerController;
    private float velocidadNormal;
    private int contadorGaseosa = 0;

    void Awake()
    {
        // Obtenemos el controlador de velocidad UNA SOLA VEZ
        playerController = GetComponentInParent<CartAccelerometerController>();
        if (playerController != null)
        {
            velocidadNormal = playerController.speed; // Guardamos la velocidad normal
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Alimentos alimento = other.GetComponent<Alimentos>();
        if (alimento != null)
        {
            alimento.OperarPuntaje();
            alimento.DestruirObjeto();

            if (alimento is Coquita|| alimento is SpriteGaseosa)
            {
                contadorGaseosa++;
                Debug.Log("Gaseosa recogida! Contador: " + contadorGaseosa);

                if (contadorGaseosa >= 3)
                {
                    Debug.Log("¡MUCHA GASEOSA! Iniciando efecto...");
                    contadorGaseosa = 0;
                    StartCoroutine(ActivarEfectoGaseosa());
                }
            }
        }
    }

    private IEnumerator ActivarEfectoGaseosa()
    {
        playerController.speed = velocidadBoost; // Aumenta velocidad
        efectoVisualGaseosa.gameObject.SetActive(true);
        Debug.Log($"velocidad:{playerController.speed}");
        yield return new WaitForSeconds(duracionEfecto);
        Debug.Log("Efecto terminado. Volviendo a la normalidad.");
        playerController.speed = velocidadNormal; // Regresa a velocidad normal
        efectoVisualGaseosa.gameObject.SetActive(false);
    }
}