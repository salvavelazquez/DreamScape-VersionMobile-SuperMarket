using UnityEngine;
using System.Collections; // Necesario para Corutinas
using TMPro;


public class CarritoBehaviour : MonoBehaviour
{
    // VARIABLES PARA EL EFECTO DE GASEOSA
    [Header("Efectos de Gaseosa")]
    [SerializeField] private ParticleSystem efectoVisualGaseosa;
    [SerializeField] private float duracionEfectoGaseosa = 3.0f; // Duración de la Aceleración
    [SerializeField] private float velocidadBoost = 40f;

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

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;
    public Transform canvasHUD;

    [Header("PowerUp: Mucha Azúcar")]
    public GameObject cartelAzucar;
    private int contadorCereal = 0;
    private bool azucarActiva = false;

    [Header("Sonidos")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoBoost;
    [SerializeField] private AudioClip sonidoSquish;


    public GameObject cartelGrasas;
    public GameObject cartelGaseosas;




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

      
            // Si tiene tag Saludable +20
            if (other.CompareTag("Fruta"))
            {
                
                SpawnFloatingText("+20", Color.green, other.transform.position);
            }

            // Si tiene tag Chatarra -10
            else if (other.CompareTag("Chatarra"))
            {
                SpawnFloatingText("-10", Color.red, other.transform.position);
            }

            if (alimento is Cereal)
            {
                contadorCereal++;

                if (contadorCereal >= 3 && !azucarActiva)
                {
                    contadorCereal = 0;
                    StartCoroutine(ActivarMuchaAzucar());
                }
            }



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
        if (cartelGaseosas != null)
            cartelGaseosas.SetActive(true);
        playerController.speed = velocidadBoost;
        efectoVisualGaseosa.gameObject.SetActive(true);

        Debug.Log($"Velocidad temporalmente: {playerController.speed}");

        if (audioSource != null && sonidoBoost != null)
        {
            audioSource.clip = sonidoBoost;
            audioSource.loop = true;
            audioSource.Play();
        }


        yield return new WaitForSeconds(duracionEfectoGaseosa);

        Debug.Log("Efecto de Gaseosa terminado. Volviendo a la normalidad.");
        playerController.speed = velocidadNormal;
        efectoVisualGaseosa.gameObject.SetActive(false);
        // Ocultar cartel
        if (cartelGaseosas != null)
            cartelGaseosas.SetActive(false);
        
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        efectoActivo = false;
    }

    // Corrutina para el efecto de Ralentización (Grasoso)
    private IEnumerator ActivarEfectoGrasoso()
    {
        efectoActivo = true;

        if (cartelGrasas != null)
            cartelGrasas.SetActive(true);

        playerController.speed = velocidadSlow; // Aplica la ralentización

        if (efectoVisualGrasoso != null)
        {
            efectoVisualGrasoso.gameObject.SetActive(true);
        }

        Debug.Log($"Velocidad temporalmente REDUCIDA: {playerController.speed}");

        if (audioSource != null && sonidoSquish != null)
        {
            audioSource.clip = sonidoSquish;
            audioSource.loop = true;
            audioSource.Play();
        }

        yield return new WaitForSeconds(duracionEfectoGrasoso);

        Debug.Log("Efecto Grasoso terminado. Volviendo a la normalidad.");
        playerController.speed = velocidadNormal; // Vuelve a la velocidad normal

        if (efectoVisualGrasoso != null)
        {
            efectoVisualGrasoso.gameObject.SetActive(false);
        }

        // Ocultar cartel
        if (cartelGrasas != null)
            cartelGrasas.SetActive(false);

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        efectoActivo = false;
    }

    private void SpawnFloatingText(string mensaje, Color color, Vector3 worldPos)
    {
        if (floatingTextPrefab == null || canvasHUD == null)
            return;

        // Crear texto
        GameObject go = Instantiate(floatingTextPrefab, canvasHUD);

        // Obtener el TMP
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = mensaje;
            tmp.color = color;
        }

        // Posición en pantalla
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        go.transform.position = screenPos;

        // Destruir en 1 segundos
        Destroy(go, 1f);
    }



    private IEnumerator ActivarMuchaAzucar()
    {
        azucarActiva = true;

        // Mostrar cartel
        if (cartelAzucar != null)
            cartelAzucar.SetActive(true);

        // Aumentar velocidad de caída → A todos los alimentos
        GeneradorAlimentos gen = FindFirstObjectByType<GeneradorAlimentos>();
        if (gen != null)
            gen.SetVelocidadAlimentos(18f); // velocidad nueva

        yield return new WaitForSeconds(10f);

        // Volver a la normalidad
        if (gen != null)
            gen.SetVelocidadAlimentos(3f); // velocidad normal

        if (cartelAzucar != null)
            cartelAzucar.SetActive(false);

        azucarActiva = false;
    }

}