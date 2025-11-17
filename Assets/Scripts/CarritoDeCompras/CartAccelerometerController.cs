using UnityEngine;

public class CartAccelerometerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;          // sensibilidad del acelerómetro
    public float smooth = 0.1f;       // suavizado del movimiento

    [Header("Límites de pantalla")]
    public Camera sceneCamera;
    public float margin = 0.5f;       // margen para no salir del borde

    private float fixedY;
    private float velocityX;

    void Start()
    {
        fixedY = transform.position.y;

        if (sceneCamera == null)
            sceneCamera = Camera.main;
    }

    void Update()
    {
        // 1) Leer acelerómetro (solo X)
        float tilt = Input.acceleration.x;   // -1 a 1

        // 2) Convertir a movimiento horizontal
        float targetX = transform.position.x + tilt * speed * Time.deltaTime;

        // 3) Suavizado (opcional)
        float smoothX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smooth);

        // 4) Limitar a los bordes visibles
        Vector3 clamped = ClampToScreen(smoothX);

        // 5) Mover el carrito
        transform.position = new Vector3(clamped.x, fixedY, transform.position.z);
    }

    Vector3 ClampToScreen(float xValue)
    {
        float zDist = sceneCamera.WorldToScreenPoint(transform.position).z;

        // Bordes del mundo en base a la pantalla
        Vector3 left = sceneCamera.ScreenToWorldPoint(new Vector3(0, 0, zDist));
        Vector3 right = sceneCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, zDist));

        xValue = Mathf.Clamp(xValue, left.x + margin, right.x - margin);

        return new Vector3(xValue, 0, 0);
    }
}
