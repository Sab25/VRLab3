using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float openTime = 20f;
    public float rotationTimeScale = 5f;
    public float delayBeforeReset = 2f; 
    public Transform openTransform;  // Cambiato il nome da doorTransform a openTransform
    public GameObject doorObject;  // Cambiato il nome da openableObject a doorObject
    public GameObject proximityText;
    public AudioClip openingSound;

    private bool isOpening = false;
    private AudioSource audioSource;
    private bool isPlayerNear = false;

    void Start()
    {
        proximityText.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {  
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X) && !isOpening)
        {
            proximityText.SetActive(false);
            StartCoroutine(OpenAnimation());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOpening && other.CompareTag("Player"))
        {
            isPlayerNear = true;
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOpening && other.CompareTag("Player"))
        {
            isPlayerNear = false;
            proximityText.SetActive(false);
        }
    }

    IEnumerator OpenAnimation()
    {
        isOpening = true;

        Vector3 initialPosition = doorObject.transform.position;
        Quaternion initialRotation = doorObject.transform.rotation;

        PlaySound(openingSound);

        float elapsedTime = 0f;

        while (elapsedTime < openTime)
        {
            float percentageComplete = elapsedTime / openTime;

            doorObject.transform.position = Vector3.Lerp(initialPosition, openTransform.position, percentageComplete);
            doorObject.transform.rotation = Quaternion.Slerp(initialRotation, openTransform.rotation, percentageComplete * rotationTimeScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeReset);

        doorObject.transform.position = initialPosition;
        doorObject.transform.rotation = initialRotation;

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
