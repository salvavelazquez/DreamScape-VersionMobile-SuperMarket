using UnityEngine;

public class Cereal : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = 40;
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
