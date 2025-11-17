using UnityEngine;

public class CarritoBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Alimentos alimento = other.GetComponent<Alimentos>();
        if (alimento != null)
        {
            alimento.OperarPuntaje();     
            alimento.DestruirObjeto();    
        }
    }

}
