using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public RectTransform dashBar;
    float speed = 5;
    float directionSpeed = 8;
    float dashPower = 5;
    float knockPower = 20;
    float dashCD = 0.5f   ;
    float turnSpeed = 720;
    float dashTimer = 0;
    int knockCount = 0;
    bool canDash = true;
    Vector3 smoothDirection;
    Vector3 direction;
    Vector3 startScale;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startScale = transform.localScale;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        dash();
        squashBack();
        dashBarFill();
        anim.SetBool("isMoving", smoothDirection.sqrMagnitude > 0.05f);
        if(transform.position.y < -5)
        {
            GameManager.instance.gameOver();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        turn();
    }

    private void move()
    {
        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        smoothDirection = Vector3.MoveTowards(smoothDirection, direction, directionSpeed*Time.deltaTime);
        rb.MovePosition(rb.position + smoothDirection * speed * Time.deltaTime);
    }

    private void turn()
    {
        if (smoothDirection.sqrMagnitude > 0.01f)
        {
            Quaternion look = Quaternion.LookRotation(smoothDirection);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, look, turnSpeed * Time.deltaTime));
        }
    }

    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            canDash = false;
            knockCount = 0;
            dashTimer = dashCD;
            rb.AddForce(direction.normalized * dashPower, ForceMode.Impulse);
            Invoke("resetDash", dashCD);

            transform.localScale = new Vector3(startScale.x * 0.75f, startScale.y * 0.85f, startScale.z * 1.5f);
            Fx.Burst(transform.position + Vector3.down * 0.9f, Color.white, 0.8f);
            GameManager.instance.Play("dash", 1f);
            CameraShake.instance.Kick(4f);
        }

    }
    void resetDash()
    {
        canDash = true;
    }

    void squashBack()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, 10f * Time.deltaTime);
    }

    void dashBarFill()
    {
        if (dashBar == null) return;
        dashTimer = Mathf.Max(dashTimer - Time.deltaTime, 0f);
        dashBar.localScale = new Vector3(1f - dashTimer / dashCD, 1f, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(canDash == false && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromMe = collision.transform.position - transform.position;
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.AddForce(awayFromMe.normalized * knockPower, ForceMode.Impulse);
            CameraShake.instance.Shake(0.2f, 0.35f);

            collision.gameObject.GetComponent<EnemyChase>().Hit();
            Fx.Burst(collision.GetContact(0).point, new Color(1f, 0.85f, 0.4f), 1.2f);
            GameManager.instance.HitStop(0.06f);
            GameManager.instance.FlashRing();
            GameManager.instance.Play("knock", 1f + knockCount * 0.15f);
            knockCount += 1;
        }
    }
}
