using UnityEngine;
using TMPro;

public class DebugCarrito : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public Transform carrito;

    void Update()
    {
        if (debugText == null || carrito == null)
            return;

        Vector3 pos = carrito.position;

        debugText.text =
            $"X: {pos.x:F3}\n" +
            $"Y: {pos.y:F3}\n" +
            $"Z: {pos.z:F3}\n";


        Vector3 acc = Input.acceleration;

        debugText.text =
            $"X: {pos.x:F3}\n" +
            $"Y: {pos.y:F3}\n" +
            $"Z: {pos.z:F3}\n\n" +
            $"ACC X: {acc.x:F3}\n" +
            $"ACC Y: {acc.y:F3}\n" +
            $"ACC Z: {acc.z:F3}";

    }
}
