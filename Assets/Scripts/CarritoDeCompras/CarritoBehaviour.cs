using UnityEngine;
using System.Collections; // Necesario para Corutinas

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

    public GameObject cartelGrasas;



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

            // Texto flotante según si es comida saludable o chatarra
            if (alimento is BananaBehaviour)
            {
                SpawnFloatingText("+10", Color.green);

            }
            else if (alimento is Hamburguesa)
            {
                SpawnFloatingText("-10", Color.red);
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

        if (cartelGrasas != null)
            cartelGrasas.SetActive(true);

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

        // Ocultar cartel
        if (cartelGrasas != null)
            cartelGrasas.SetActive(false);

        efectoActivo = false;
    }

    private void SpawnFloatingText(string mensaje, Color color)
    {
        if (floatingTextPrefab == null || canvasHUD == null)
            return;
        
        // Crear texto en el canvas
        GameObject go = Instantiate(floatingTextPrefab, canvasHUD);

        // Obtener tu script de texto flotante
        TextoFlotante ft = go.GetComponent<TextoFlotante>();
        ft.SetText(mensaje, color);

        // Posicionar cerca del carrito (convertir coordenadas a UI)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        go.transform.position = screenPos + new Vector3(0, 80, 0); // un poquito arriba
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