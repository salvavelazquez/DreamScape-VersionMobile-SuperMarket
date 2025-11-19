using UnityEngine;

public class Naranja : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = 100;
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
