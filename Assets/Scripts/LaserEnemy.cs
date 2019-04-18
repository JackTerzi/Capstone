using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour {

    Vector2 moveDir, aimDir;
    public float moveSpeed;
    List<RaycastHit2D> rayData = new List<RaycastHit2D>();
    RaycastHit2D rC;
    Rigidbody2D rb;
    bool shouldAim, shouldMove;
    float shootTimer;


    public LineRenderer lr;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

       rayData.Add(Physics2D.Raycast(gameObject.transform.position, Vector2.left, 100, LayerMask.GetMask("Walls")));
       rayData.Add(Physics2D.Raycast(gameObject.transform.position, Vector2.right, 100, LayerMask.GetMask("Walls")));
        rayData.Add(Physics2D.Raycast(gameObject.transform.position, Vector2.up, 100, LayerMask.GetMask("Walls")));
        rayData.Add(Physics2D.Raycast(gameObject.transform.position, Vector2.down, 100, LayerMask.GetMask("Walls")));
        float f = Mathf.Infinity;
        Debug.Log(LayerMask.GetMask("Walls"));
        foreach (var ray in rayData)
        {
            if(ray.distance < f)
            {
                f = ray.distance;
                rC = ray;
            }
        }
        moveDir = rC.point;
        Debug.Log(moveDir);
        shouldMove = true;

    }
    
    // Update is called once per frame
    void Update () {
        if (shouldAim)
        {
            aimDir = Manager.me.player.transform.position - transform.position; 
            
            shouldAim = false;
        }
        else
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                lr.SetPosition(0, transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir);
                if ( hit )
                {
                    if (hit.collider)
                    {
                        lr.SetPosition(1, hit.point);
                    }
                }

            }
        }
    }



    private void FixedUpdate()
    {
        if (shouldMove)
        {
            rb.MovePosition(transform.position + transform.right * moveSpeed * Time.fixedDeltaTime);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask("Walls"))
        {
            shouldMove = false;
            shouldAim = true;
        }
    }

}

