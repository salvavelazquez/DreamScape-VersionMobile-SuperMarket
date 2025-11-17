using UnityEngine;

public class Pizza : Alimentos
{
    protected override void Awake()
    {
        base.Awake();
        puntaje = +100;
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
