using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SophiaMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float jumpSpeed = 30f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform cannon;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip explodeSound;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myWheelCollider;
    float gravityScaleAtStart;
    bool isAlive = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myWheelCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if(!isAlive){ return; }

        Run();
        FlipSprite();
        //ClimbLadder();
        Die();
    }

    void OnMove(InputValue value){
        if(!isAlive){ return; }
        moveInput = value.Get<Vector2>();      
    }

    void OnJump(InputValue value){

        if(!isAlive){ return; }

        if(!myWheelCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }

        if(value.isPressed){                                  
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);        
        }     

    }    

    void OnFire(InputValue value){
        if(!isAlive){return;}
        Instantiate(projectile, cannon.position, transform.rotation);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
    }

    void Run(){

        Vector2 playerVelocity = new Vector2(runSpeed*moveInput.x, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
           
    }

    void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); 
        }
    }

    void ClimbLadder(){

        if(!myWheelCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y*climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;
    }

    void Die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Vines"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(explodeSound, Camera.main.transform.position);
            StartCoroutine(DisableSophia());        
        }
    }

    IEnumerator DisableSophia(){
        yield return new WaitForSeconds(0.2f);
        Destroy(myRigidBody);      
    }

}
