using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    public float moveTime = 5f;
    public Transform moveDestination;
    public AudioClip movingSound;
    public GameObject proximityTextPrefab;

    private bool isMoving = false;
    private AudioSource audioSource;
    private GameObject proximityText;

    void Start()
    {
        // Aggiungi un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Crea il testo di prossimità come un GameObject
        proximityText = Instantiate(proximityTextPrefab, transform.position, Quaternion.identity);
        proximityText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMoving)
        {
            StartCoroutine(MoveObjectAnimation());
        }

        if (isMoving && Input.GetKeyDown(KeyCode.V))
        {
            // Disattiva il testo di prossimità quando viene premuto il pulsante
            proximityText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Attiva il testo di prossimità quando il giocatore si avvicina all'oggetto
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disattiva il testo di prossimità quando il giocatore si allontana dall'oggetto
            proximityText.SetActive(false);
        }
    }

    IEnumerator MoveObjectAnimation()
    {
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        // Attiva l'audio quando inizia il movimento
        PlaySound(movingSound);

        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            float percentageComplete = elapsedTime / moveTime;

            transform.position = Vector3.Lerp(initialPosition, moveDestination.position, percentageComplete);
            transform.rotation = Quaternion.Slerp(initialRotation, moveDestination.rotation, percentageComplete);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Imposta isMoving a false quando il movimento è completato
        isMoving = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
