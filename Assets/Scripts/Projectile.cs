using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    Rigidbody2D myRigidBody;
    [SerializeField] float projectileSpeed = 1f;
    SophiaMovement player;
    float xSpeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<SophiaMovement>();   
        xSpeed = player.transform.localScale.x * projectileSpeed;     
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);        
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);    
    }

}
