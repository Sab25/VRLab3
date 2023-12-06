using System.Collections;
using UnityEngine;

public class PickCola : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject proximityText;
    public AudioClip pickupSound;

    private bool isVisible = false;
    private bool isPlayerNearby = false;
    private AudioSource audioSource;

    void Start()
    {
        targetObject.SetActive(false);
        proximityText.SetActive(false);

        // Aggiungi l'AudioSource e configuralo
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Verifica se il giocatore è nelle vicinanze prima di eseguire l'azione
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(PickupRoutine());
        }
    }

    IEnumerator PickupRoutine()
    {
        // Riproduci il suono prima di far apparire l'oggetto
        if (pickupSound != null)
        {
            audioSource.clip = pickupSound;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // Aspetta per un po' prima di far apparire l'oggetto
        yield return new WaitForSeconds(0.3f);

        // Fai apparire o sparire l'oggetto
        isVisible = !isVisible;
        targetObject.SetActive(isVisible);

        if (isVisible)
        {
            proximityText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isVisible && other.CompareTag("Player"))
        {
            proximityText.SetActive(true);
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isVisible && other.CompareTag("Player"))
        {
            proximityText.SetActive(false);
            isPlayerNearby = false;
        }
    }
}
