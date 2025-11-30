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
        Vector3 acc = Input.acceleration;

        // Validar NaN
        if (float.IsNaN(acc.x) || float.IsNaN(acc.y) || float.IsNaN(acc.z))
            return;

        float tilt = acc.x;

        float targetX = transform.position.x + tilt * speed * Time.deltaTime;
        if (float.IsNaN(targetX))
            targetX = transform.position.x;

        float smoothX = Mathf.SmoothDamp(
            float.IsNaN(transform.position.x) ? 0 : transform.position.x,
            targetX,
            ref velocityX, smooth
        );

        Vector3 clamped = ClampToScreen(smoothX);

        transform.position = new Vector3(clamped.x, fixedY, transform.position.z);
    }

    Vector3 ClampToScreen(float xValue)
    {
        float zDist = sceneCamera.WorldToScreenPoint(transform.position).z;
        if (zDist <= 0 || float.IsNaN(zDist))
            zDist = 10f;

        Vector3 left = sceneCamera.ScreenToWorldPoint(new Vector3(0, 0, zDist));
        Vector3 right = sceneCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, zDist));

        if (float.IsNaN(xValue))
            xValue = transform.position.x;

        xValue = Mathf.Clamp(xValue, left.x + margin, right.x - margin);

        return new Vector3(xValue, 0, 0);
    }

}
