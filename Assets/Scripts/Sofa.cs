using UnityEngine;

public class Sofa : MonoBehaviour
{
    private bool isSitting = false;
    private bool isPlayerDown = true;
    private Vector3 initialPosition;
    private GameObject player;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSitting = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSitting = false;
            player = null;
        }
    }

    private void Update()
    {
        if (isSitting && Input.GetKeyDown(KeyCode.X) && player != null)
        {
            isPlayerDown = !isPlayerDown;

            if (isPlayerDown)
            {
                StartCoroutine(MovePlayerTowards(initialPosition - new Vector3(0, 1.0f, 0)));
            }
            else
            {
                StartCoroutine(MovePlayerTowards(initialPosition));
            }
        }
    }

    private System.Collections.IEnumerator MovePlayerTowards(Vector3 targetPosition)
    {
        float translationDuration = 1.0f;
        float elapsedTime = 0f;
        Vector3 startPosition = player.transform.position;

        while (elapsedTime < translationDuration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / translationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition;
    }
}