using Assets.Scripts;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpriteGaseosa : Alimentos,IEventoVisual
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
        rb.linearVelocity = Time.fixedDeltaTime * velocidad * direccion;
    }
    public override void OperarPuntaje()
    {
        GameManager.instancia.SumarPuntos(puntaje);
        Contador += 1;
        if (Contador== 3)
        {
            EfectoJugador();
            EfectoVisual();
        }
    }

    public void TemporizarEfecto()
    {
        throw new System.NotImplementedException();
    }

    public void EfectoJugador()
    {
        player.speed = 5.5f;
        float tiempo = 2f;
        if (Time.time > tiempo)
        {
            player.speed = 5.5f;
        }
    }

    public void EfectoVisual()
    {
        efecto.SetActive(true);
        float tiempo = 2f;
        if (Time.time>tiempo)
        {
            efecto.SetActive(false);
        }
    }
}
