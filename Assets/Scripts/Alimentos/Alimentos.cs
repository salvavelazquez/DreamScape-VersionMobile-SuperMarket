using UnityEngine;

public abstract class Alimentos : MonoBehaviour
{
    protected float velocidad = 3f;
    protected Vector3 direccion = Vector3.down;
    protected int puntaje;
    protected Rigidbody rb;

    public float Velocidad
    {
        get => velocidad;
        set
        {
            velocidad = value;

            // Aplicar velocidad inmediatamente si ya existe el rigidbody
            if (rb != null)
                AplicarVelocidad();
        }
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        AplicarVelocidad();
    }

    protected void AplicarVelocidad()
    {
        if (rb != null)
            rb.linearVelocity = direccion * velocidad;
    }

    public abstract void CaerObjeto();
    public abstract void OperarPuntaje();

    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
