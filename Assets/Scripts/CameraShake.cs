using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    Vector3 startPos;
    float shakeTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this; // script that got attacted is refer to "this" this is making sure that it applies only to this object
        startPos = transform.position; // capture the current position of the camera
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0f)
        {
            transform.position = startPos + Random.insideUnitSphere * 0.2f;
            shakeTime -= Time.deltaTime;
        }

        else
        {
            transform.position = startPos;
        }
    }

    public void Shake()
    {
        shakeTime = 0.15f;
    }
}
