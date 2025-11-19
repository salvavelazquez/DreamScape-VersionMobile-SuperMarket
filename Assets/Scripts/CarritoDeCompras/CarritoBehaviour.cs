using UnityEngine;
using System.Collections; // Necesario para Corutinas

public class CarritoBehaviour : MonoBehaviour
{
    // VARIABLES PARA EL EFECTO DE GASEOSA
    [Header("Efectos de Gaseosa")]
    [SerializeField] private ParticleSystem efectoVisualGaseosa;
    [SerializeField] private float duracionEfectoGaseosa = 2.0f; // Duración de la Aceleración
    [SerializeField] private float velocidadBoost = 7.5f;

    // VARIABLES PARA EL EFECTO GRASOSO
    [Header("Efectos de Pizza (Grasoso)")]
    [SerializeField] private ParticleSystem efectoVisualGrasoso;
    [SerializeField] private float duracionEfectoGrasoso = 3.0f; // Duración de la Ralentización
    [SerializeField] private float velocidadSlow = 2.0f; // La nueva velocidad lenta

    // VARIABLES PRIVADAS
    private CartAccelerometerController playerController;
    private float velocidadNormal;
    private int contadorGaseosa = 0;
    private int contadorPizza = 0; // NUEVO CONTADOR PARA LA PIZZA
    private bool efectoActivo = false; // Bandera para evitar efectos simultáneos

    void Awake()
    {
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

            // Lógica para el efecto de GASEOSA
            if (alimento is Coquita || alimento is SpriteGaseosa)
            {
                contadorGaseosa++;
                contadorPizza = 0; // Reinicia el contador opuesto
                Debug.Log("Gaseosa recogida! Contador: " + contadorGaseosa);

                if (contadorGaseosa >= 3 && !efectoActivo)
                {
                    Debug.Log("¡MUCHA GASEOSA! Iniciando efecto de Aceleración...");
                    contadorGaseosa = 0;
                    StopCoroutine(nameof(ActivarEfectoGrasoso)); // Previene/detiene el efecto contrario
                    StartCoroutine(ActivarEfectoGaseosa());
                }
            }
            // Lógica para el efecto de PIZZA (GRASOSO)
            else if (alimento is Pizza|| alimento is PapasLays || alimento is Hamburguesa || alimento is pancho)
            {
                contadorPizza++; // Incrementa el contador de Pizza
                contadorGaseosa = 0; // Reinicia el contador opuesto
                Debug.Log("Pizza recogida! Contador: " + contadorPizza);

                if (contadorPizza >= 3 && !efectoActivo) // NUEVA CONDICIÓN DE UMBRAL
                {
                    Debug.Log("¡MUCHA PIZZA! Iniciando efecto de Ralentización...");
                    contadorPizza = 0;
                    StopCoroutine(nameof(ActivarEfectoGaseosa)); // Previene/detiene el efecto contrario
                    StartCoroutine(ActivarEfectoGrasoso());
                }
            }
        }
    }

    // Corrutina para el efecto de Aceleración (Gaseosa)
    private IEnumerator ActivarEfectoGaseosa()
    {
        efectoActivo = true;
        playerController.speed = velocidadBoost;
        efectoVisualGaseosa.gameObject.SetActive(true);

        Debug.Log($"Velocidad temporalmente: {playerController.speed}");
        yield return new WaitForSeconds(duracionEfectoGaseosa);

        Debug.Log("Efecto de Gaseosa terminado. Volviendo a la normalidad.");
        playerController.speed = velocidadNormal;
        efectoVisualGaseosa.gameObject.SetActive(false);
        efectoActivo = false;
    }

    // Corrutina para el efecto de Ralentización (Grasoso)
    private IEnumerator ActivarEfectoGrasoso()
    {
        efectoActivo = true;
        playerController.speed = velocidadSlow; // Aplica la ralentización

        if (efectoVisualGrasoso != null)
        {
            efectoVisualGrasoso.gameObject.SetActive(true);
        }

        Debug.Log($"Velocidad temporalmente REDUCIDA: {playerController.speed}");
        yield return new WaitForSeconds(duracionEfectoGrasoso);

        Debug.Log("Efecto Grasoso terminado. Volviendo a la normalidad.");
        playerController.speed = velocidadNormal; // Vuelve a la velocidad normal

        if (efectoVisualGrasoso != null)
        {
            efectoVisualGrasoso.gameObject.SetActive(false);
        }
        efectoActivo = false;
    }
}