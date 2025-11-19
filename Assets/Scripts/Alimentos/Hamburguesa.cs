using UnityEngine;

public class Hamburguesa : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = -20;
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
