using System.Collections;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
    public float openTime = 5f;
    public float rotationTimeScale = 5f;
    public float delayBeforeClose = 5f;
    public Transform openTransform;
    public GameObject cabinetObject;
    public GameObject proximityText;
    public AudioClip openingSound;

    private bool isOpening = false;
    private AudioSource audioSource;
    private bool isPlayerNear = false;

    void Start()
    {
        proximityText.SetActive(false);

        // Aggiungi un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.V) && !isOpening)
        {
            proximityText.SetActive(false);
            StartCoroutine(OpenAnimation());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            proximityText.SetActive(false);
        }
    }

    IEnumerator OpenAnimation()
    {
        isOpening = true;

        Vector3 initialPosition = cabinetObject.transform.position;
        Quaternion initialRotation = cabinetObject.transform.rotation;

        Debug.Log("Start opening");

        // Attiva l'audio quando inizia l'apertura
        PlaySound(openingSound);

        float elapsedTime = 0f;

        while (elapsedTime < openTime)
        {
            float percentageComplete = elapsedTime / openTime;

            cabinetObject.transform.position = Vector3.Lerp(initialPosition, openTransform.position, percentageComplete);
            cabinetObject.transform.rotation = Quaternion.Slerp(initialRotation, openTransform.rotation, percentageComplete * rotationTimeScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeClose);

        // Ripristina la posizione e la rotazione iniziali
        cabinetObject.transform.position = initialPosition;
        cabinetObject.transform.rotation = initialRotation;

        isOpening = false;
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
