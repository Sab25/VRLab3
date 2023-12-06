using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBook : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject BookOnPlayer;

    private bool isPickedUp = false;
    private bool isPlayerNear = false;

    private Renderer objectRenderer;  // Aggiunto il riferimento al renderer

    void Start()
    {
        BookOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
        objectRenderer = GetComponent<Renderer>();  // Ottieni il riferimento al renderer
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            PickUp();
        }
        else if (isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        isPickedUp = true;
        objectRenderer.enabled = false;  // Disabilita solo il renderer
        BookOnPlayer.SetActive(true);
        PickUpText.SetActive(false);
    }

    private void Drop()
    {
        isPickedUp = false;
        objectRenderer.enabled = true;  // Riabilita il renderer
        BookOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
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
}
