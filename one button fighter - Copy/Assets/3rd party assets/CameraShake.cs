using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//credit to FTVS https://gist.github.com/ftvs/5822103

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    public float minDuration = .01f;
    public float maxDuration = 0.2f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float minShakeAmount = 0.5f;
    public float maxShakeAmount = 1.0f;


    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void impact(float impactStrengthPercent)
    {
        shakeAmount = Mathf.Lerp(minShakeAmount, maxShakeAmount, impactStrengthPercent);
        shakeDuration = Mathf.Lerp(minDuration, maxDuration, impactStrengthPercent);
    }
}
