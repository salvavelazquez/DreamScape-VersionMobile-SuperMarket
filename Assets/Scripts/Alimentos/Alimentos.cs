using UnityEngine;

public abstract class Alimentos : MonoBehaviour
{
    protected float velocidad = 1.2f;
    protected Vector3 direccion = Vector3.down;
    protected int puntaje;
    protected Rigidbody rb;

    public float Velocidad { get => velocidad; set => velocidad = value; }
    public Vector3 Direccion { get => direccion; set => direccion = value; }
    public int Puntaje { get => puntaje; set => puntaje = value; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public abstract void CaerObjeto();
    public abstract void OperarPuntaje();
    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
