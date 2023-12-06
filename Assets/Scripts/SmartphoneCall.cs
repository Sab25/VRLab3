using System.Collections;
using UnityEngine;

public class SmartphoneCall : MonoBehaviour
{
    public float callTime = 0.0001f;
    public float rotationTimeScale = 5f;
    public float delayBeforeReset = 5f; // Aggiunto il tempo di attesa prima di tornare alla posizione iniziale
    public Transform phoneTransform;
    public GameObject smartphoneObject;
    public GameObject proximityText;
    private bool isAnswer = false;
    private PickPhone pickPhoneScript;

    void Start(){
        proximityText.SetActive(true);
        pickPhoneScript = FindObjectOfType<PickPhone>(); // Trova il componente PickPhone all'avvio
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isAnswer)
        {
            proximityText.SetActive(false);
            StartCoroutine(CallAnimation());
            pickPhoneScript.StopRingtone(); // Chiamare StopRingtone quando si risponde
        }
    }

    IEnumerator CallAnimation()
    {
        isAnswer = true;

        Vector3 initialPosition = smartphoneObject.transform.position;
        Quaternion initialRotation = smartphoneObject.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < callTime)
        {
            float percentageComplete = elapsedTime / callTime;

            smartphoneObject.transform.position = Vector3.Lerp(initialPosition, phoneTransform.position, percentageComplete);
            smartphoneObject.transform.rotation = Quaternion.Slerp(initialRotation, phoneTransform.rotation, percentageComplete * rotationTimeScale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeReset);

        
        smartphoneObject.transform.position = initialPosition;
        smartphoneObject.transform.rotation = initialRotation;

        isAnswer = false;
    }
}
