using System.Runtime.InteropServices;
using UnityEngine;

public class SpriteGaseosa : Alimentos
{
    [SerializeField] private CartAccelerometerController player;
    [SerializeField] private GameObject efecto;
    protected override void Awake()
    {
        base.Awake();
        puntaje = -10;
        player= GetComponent<CartAccelerometerController>();
    }
    public override void CaerObjeto()
    {
        rb.linearVelocity = velocidad * direccion;
    }
    public override void OperarPuntaje()
    {
        GameManager.instancia.SumarPuntos(puntaje);
    }
}
