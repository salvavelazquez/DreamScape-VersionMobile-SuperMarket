using UnityEngine;

public class PapasLays : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = -30;
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
