using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public GameObject interactText;
    public AudioClip songClip;
    private AudioSource audioSource;
    private bool isPlayerNear = false;

    void Start()
    {
        interactText.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = songClip;
        audioSource.loop = true; // Imposta su true se vuoi che la canzone si ripeta
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.G))
        {
            interactText.SetActive(false);
            ToggleMusic();
        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactText.SetActive(false);
        }
    }

    void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}