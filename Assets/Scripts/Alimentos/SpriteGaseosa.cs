using UnityEngine;

public class SpriteGaseosa : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = -10;
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
