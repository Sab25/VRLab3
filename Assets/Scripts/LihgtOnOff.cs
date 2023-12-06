using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LihgtOnOff : MonoBehaviour
{
    Light myLight;
    public GameObject proximityText;
    public AudioClip switchSound; // Aggiunto l'AudioClip
    private AudioSource audioSource;

    void Start()
    {
        myLight = GetComponent<Light>();
        proximityText.SetActive(false);

        // Aggiungi un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Inverti lo stato della luce
            myLight.enabled = !myLight.enabled;

            // Riproduci l'audio quando accendi o spegni la luce
            audioSource.clip = switchSound;
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Attiva il testo di prossimità
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Disabilita il testo di prossimità
            proximityText.SetActive(false);
        }
    }
}
