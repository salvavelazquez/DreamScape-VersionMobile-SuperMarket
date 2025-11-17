using UnityEngine;

public class Destructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Alimentos alimentos = other.GetComponent<Alimentos>();
        if (alimentos != null)
        {
            alimentos.DestruirObjeto();
        }
    }
}
