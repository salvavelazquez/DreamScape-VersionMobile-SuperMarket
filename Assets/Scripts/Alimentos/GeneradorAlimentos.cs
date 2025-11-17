using System.Collections;
using UnityEngine;

public class GeneradorAlimentos : MonoBehaviour
{
    public GameObject[] prefabsAlimentos; // Asignale los prefabs en el inspector
    private float intervalo = 0.7f;
    private int intervaloEntero;
    [SerializeField]private Transform puntoA;
    [SerializeField] private Transform puntoB;
    [SerializeField] private float velocidad;
    void Start()
    {
        StartCoroutine(GenerarAlimentosPeriodicamente());
    }
    // Update is called once per frame
    private void Update()
    {
        float t = (Mathf.Cos(Time.time * velocidad) + 1f) / 2f; // Oscila entre 0 y 1
        transform.position = Vector3.Lerp(puntoA.position, puntoB.position, t);
    }
    public void GenerarAlimento()
    {
        int index = Random.Range(0, prefabsAlimentos.Length);
        GameObject nuevoAlimento = Instantiate(prefabsAlimentos[index], transform.position, Quaternion.identity);

        Alimentos alimento = nuevoAlimento.GetComponent<Alimentos>();
        alimento.CaerObjeto();
    }
    IEnumerator GenerarAlimentosPeriodicamente()
    {
        while (true)
        {
            GenerarAlimento();
            intervaloEntero = Random.Range(7, 11);
            intervalo = intervaloEntero / 10f;
            yield return new WaitForSeconds(intervalo);
        }
    }

}
