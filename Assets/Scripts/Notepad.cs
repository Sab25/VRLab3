using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepad : MonoBehaviour
{
    public GameObject NotepadON;
    public GameObject NotepadOff;
    public GameObject proximityText;
    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOn = false;
    private AudioSource audioSource;

    void Start()
    {
        ToggleNotepadState(isOn);
        proximityText.SetActive(false);

        // Aggiungi un componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleNotepadState(!isOn);

            // Riproduci l'audio in base allo stato del quaderno
            if (isOn)
            {
                audioSource.clip = openSound;
            }
            else
            {
                audioSource.clip = closeSound;
            }

            audioSource.Play();
        }
    }

    void ToggleNotepadState(bool newState)
    {
        isOn = newState;
        NotepadON.SetActive(isOn);
        NotepadOff.SetActive(!isOn);
        proximityText.SetActive(!isOn);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOn && other.CompareTag("Player"))
        {
            proximityText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOn && other.CompareTag("Player"))
        {
            proximityText.SetActive(false);
        }
    }
}
