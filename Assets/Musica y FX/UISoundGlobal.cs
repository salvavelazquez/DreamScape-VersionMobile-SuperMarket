using UnityEngine;

public class UISoundGlobal : MonoBehaviour
{
    public static UISoundGlobal instancia;

    public AudioSource audioSource;
    public AudioClip sonidoClick;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void PlayClick()
    {
        if (instancia != null && instancia.audioSource != null)
            instancia.audioSource.PlayOneShot(instancia.sonidoClick);
    }
}
