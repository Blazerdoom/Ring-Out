using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    Vector3 startPos;
    float shakeTime = 0f;
    float shakePower = 0.2f;
    Camera cam;
    float startFov;
    float fovKick = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this; // script that got attacted is refer to "this" this is making sure that it applies only to this object
        startPos = transform.position; // capture the current position of the camera
        cam = GetComponent<Camera>();
        startFov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0f)
        {
            transform.position = startPos + Random.insideUnitSphere * shakePower;
            shakeTime -= Time.unscaledDeltaTime;
        }

        else
        {
            transform.position = startPos;
        }

        fovKick = Mathf.Lerp(fovKick, 0f, 8f * Time.unscaledDeltaTime);
        cam.fieldOfView = startFov + fovKick;
    }

    public void Shake()
    {
        Shake(0.15f, 0.2f);
    }

    public void Shake(float time, float power)
    {
        if (shakeTime > 0f && power < shakePower) return;
        shakeTime = time;
        shakePower = power;
    }

    public void Kick(float amount)
    {
        fovKick = amount;
    }
}
