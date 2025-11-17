using UnityEngine;

public class pera : Alimentos
{
     protected override void Awake()
    {
        base.Awake();
        puntaje = +20;
    }
    public override void CaerObjeto()
    {
        rb.linearVelocity = Time.fixedDeltaTime * velocidad * direccion;
    }
    public override void OperarPuntaje()
    {
        GameManager.instancia.SumarPuntos(puntaje);
    }
}
