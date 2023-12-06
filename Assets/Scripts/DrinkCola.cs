using System.Collections;
using UnityEngine;

public class DrinkCola : MonoBehaviour
{
    public float drinkTime = 10f;
    public float rotationTimeScale = 5f;
    public float delayBeforeDisappear = 5f;
    public Transform drinkingTransform;
    public GameObject drinkableObject;
    public GameObject proximityText;
    public AudioClip drinkingSound;

    private bool isDrinking = false;
    private AudioSource audioSource;

    void Start()
    {
        proximityText.SetActive(true);

        // Aggiungi un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = drinkingSound;
        audioSource.loop = true; // Riproduci l'audio in loop
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isDrinking)
        {
            proximityText.SetActive(false);
            StartCoroutine(DrinkAnimation());
        }
    }

    IEnumerator DrinkAnimation()
    {
        isDrinking = true;

        Vector3 initialPosition = drinkableObject.transform.position;
        Quaternion initialRotation = drinkableObject.transform.rotation;

        Debug.Log("Start drinking");

        float elapsedTime = 0f;

        while (elapsedTime < drinkTime)
        {
            float percentageComplete = elapsedTime / drinkTime;

            drinkableObject.transform.position = Vector3.Lerp(initialPosition, drinkingTransform.position, percentageComplete);
            drinkableObject.transform.rotation = Quaternion.Slerp(initialRotation, drinkingTransform.rotation, percentageComplete * rotationTimeScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Attiva l'audio quando finisce di bere e resta nella posizione ruotata
        audioSource.Play();

        yield return new WaitForSeconds(delayBeforeDisappear);

        // Disattiva l'oggetto dopo un ritardo
        drinkableObject.SetActive(false);

        // Ripristina la posizione e la rotazione iniziali
        drinkableObject.transform.position = initialPosition;
        drinkableObject.transform.rotation = initialRotation;

        // Disattiva l'audio quando l'oggetto viene disattivato
        audioSource.Stop();

        isDrinking = false;
    }
}
