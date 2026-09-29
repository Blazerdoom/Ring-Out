using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    Rigidbody rb;
    Transform player;
    float chaseForce = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // make the enemy able to use rb and find player 
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //addforce is for pushing
        Vector3 toPlayer = player.position - transform.position;
        rb.AddForce(toPlayer.normalized * chaseForce);
        if (transform.position.y < -5)
        {
            GameManager.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
