
using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    float speed = 5;
    float directionSpeed = 8;
    float dashPower = 5;
    float knockPower = 20;
    float dashCD = 0.5f   ;
    bool canDash = true;
    Vector3 smoothDirection;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dash();
        if(transform.position.y < -5)
        {
            GameManager.instance.gameOver();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move();
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

    private void dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            canDash = false;
            rb.AddForce(direction.normalized * dashPower, ForceMode.Impulse);
            Invoke("resetDash", dashCD);
        }
            
    }
    void resetDash()
    {
        canDash = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(canDash == false && collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromMe = collision.transform.position - transform.position;
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.AddForce(awayFromMe.normalized * knockPower, ForceMode.Impulse);
            CameraShake.instance.Shake();
        }
    }
}
