using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    Rigidbody rb;
    Transform player;
    Renderer[] rends;
    Vector3 baseScale;
    float chaseForce = 10;
    float turnSpeed = 360;
    float flashTime = 0f;
    float flashLength = 0.2f;
    float stunTime = 0f;
    bool hasLanded = false;
    bool fallSoundPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rends = GetComponentsInChildren<Renderer>();
        // make the enemy able to use rb and find player
        player = GameObject.Find("Player").transform;

        baseScale = transform.localScale;
        transform.localScale = baseScale * 0.2f;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, baseScale, 10f * Time.deltaTime);
        flashing();
        if (stunTime > 0f)
        {
            stunTime -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //addforce is for pushing
        Vector3 toPlayer = player.position - transform.position;
        if (stunTime <= 0f)
        {
            rb.AddForce(toPlayer.normalized * chaseForce);
        }
        turn(toPlayer);

        if (fallSoundPlayed == false && transform.position.y < -1.5f && hasLanded)
        {
            fallSoundPlayed = true;
            GameManager.instance.Play("fall", Random.Range(0.9f, 1.1f));
        }
        if (transform.position.y < -5)
        {
            GameManager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    void turn(Vector3 toPlayer)
    {
        toPlayer.y = 0f;
        if (toPlayer.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(toPlayer);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, look, turnSpeed * Time.deltaTime));
        }
    }

    public void Hit()
    {
        flashTime = flashLength;
        stunTime = 0.4f;
        transform.localScale = new Vector3(baseScale.x * 1.4f, baseScale.y * 0.6f, baseScale.z * 1.4f);
    }

    void flashing()
    {
        if (flashTime > 0f)
        {
            flashTime -= Time.deltaTime;
            foreach (Renderer r in rends)
            {
                r.material.SetColor("_EmissionColor", Color.white * Mathf.Max(flashTime, 0f) / flashLength);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLanded == false && collision.gameObject.name == "Cylinder")
        {
            hasLanded = true;
            transform.localScale = new Vector3(baseScale.x * 1.3f, baseScale.y * 0.6f, baseScale.z * 1.3f);
            Fx.Burst(transform.position + Vector3.down * 0.9f, new Color(1f, 0.75f, 0.7f), 0.8f);
            GameManager.instance.Play("land", Random.Range(0.9f, 1.1f));
            CameraShake.instance.Shake(0.08f, 0.06f);
        }
    }
}
