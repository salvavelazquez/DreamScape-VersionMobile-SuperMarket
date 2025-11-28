using UnityEngine;

public class FloatUI : MonoBehaviour
{
    public float speed = 1f;        // Velocidad del movimiento
    public float distance = 20f;    // Distancia en píxeles que se mueve arriba y abajo

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition; // Guardamos posición inicial
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * distance;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
