using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPhone : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject SmartphoneOnPlayer;
    public AudioClip ringtoneSound;
    private AudioSource audioSource;

    public bool isPickedUp = false; // Ora questa variabile è pubblica
    private bool isPlayerNear = false;

    private Renderer objectRenderer;

    void Start()
    {
        SmartphoneOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
        objectRenderer = GetComponent<Renderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = ringtoneSound;
        audioSource.loop = false;
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !isPickedUp)
        {
            PickUp();
        }
        else if (isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        isPickedUp = true;
        objectRenderer.enabled = false;
        SmartphoneOnPlayer.SetActive(true);
        PickUpText.SetActive(false);

        audioSource.loop = true;
        audioSource.Play();
    }

    private void Drop()
    {
        isPickedUp = false;
        objectRenderer.enabled = true;
        SmartphoneOnPlayer.SetActive(false);
        PickUpText.SetActive(false);

        audioSource.Stop();
        audioSource.loop = false;
        Debug.Log("Oggetto riposizionato");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            PickUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            PickUpText.SetActive(false);
        }
    }

    public void StopRingtone()
    {
        audioSource.Stop();
    }
}
