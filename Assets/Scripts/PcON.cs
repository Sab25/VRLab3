using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcSwitch : MonoBehaviour
{
    public GameObject pcOn;
    public GameObject pcOff;
    public GameObject proximityText;
    public AudioClip startUpSound;
    private AudioSource audioSource;

    private bool isOn = false;
    private bool isPlayerNear = false;

    void Start()
    {
        TogglePCState(isOn);
        proximityText.SetActive(false);

        // Inizializza l'AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOn)
            {
                // Avvia l'audio di accensione quando premi "E" e il PC è spento
                audioSource.clip = startUpSound;
            }
            else

            // Avvia l'audio
            audioSource.Play();

            // Cambia lo stato del PC
            TogglePCState(!isOn);
        }
    }

    void TogglePCState(bool newState)
    {
        isOn = newState;
        pcOn.SetActive(isOn);
        pcOff.SetActive(!isOn);
        proximityText.SetActive(!isOn);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOn && other.CompareTag("Player"))
        {
            isPlayerNear = true;
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOn && other.CompareTag("Player"))
        {
            isPlayerNear = false;
            proximityText.SetActive(false);
        }
    }
}
