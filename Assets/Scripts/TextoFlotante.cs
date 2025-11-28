using UnityEngine;
using TMPro;

public class TextoFlotante : MonoBehaviour
{
    public float lifetime = 1f;
    public float speed = 50f;

    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // movimiento hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // desaparecer lentamente
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public void SetText(string txt, Color color)
    {
        textMesh.text = txt;
        textMesh.color = color;
    }
}
