using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : Bounding

{
    #region Field Declarations

    //Declaring fields outside methods allow you to use them across the whole class, wheras inside methods only allows you to use them within those methods.
    [SerializeField] float ballSpeed = 3.0f;
    [SerializeField] float aiReactPoint = 2.07f; //currently -1 in the inspector(which reacts okay)
    //[SerializeField] float impulseReduction = 0.05f;
    Rigidbody2D playerRb;
    bool playerPaddleHit = false; //used to manipulate the direction of the ball bounces
    bool aIPaddleHit = false;
    public float ballsYPosition; 
    //screen bounds
    public static float leftLimit = 12.82f;
    public static float rightLimit = -6.77f;
    public static float upLimit = 4.82f;
    public static float lowLimit = -5.32f;
    //
    Vector3 target;
    PaddleMotion paddleMotion;
    PaddleAI paddleAI;

    #endregion

    private void Awake()
        
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRb.transform.position = new Vector2(3.58f, 2.17f);
    }

    void Start()
    {
        Bounding myBounding = new Bounding();
        //target = new Vector3(Random.Range(rightLimit, leftLimit), Random.Range(lowLimit,upLimit));
        target = new Vector2(leftLimit, Random.Range(lowLimit, upLimit));
        playerRb.AddForce((target - playerRb.transform.position).normalized * ballSpeed, ForceMode2D.Impulse);
        EventBroker.PlayerPaddleHit += EventBroker_PlayerPaddleHit;
        EventBroker.AIPaddleHit += EventBroker_AIPaddleHit;
        //Class References
        paddleMotion = FindObjectOfType<PaddleMotion>();
        paddleAI = FindObjectOfType<PaddleAI>();
        

      

    }

    #region Event Methods
    private void EventBroker_AIPaddleHit()
    {
        aIPaddleHit = true;
    }

    private void EventBroker_PlayerPaddleHit()
    {
        playerPaddleHit = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    
        
        Vector2 paddleLocation = paddleMotion.paddleCollider.transform.position;
        Vector2 aIPaddleLocation = paddleAI.AICollider.transform.position;
        //Collisions with paddles.
        if (other.gameObject.CompareTag("Player"))
        {
            aIPaddleHit = false;
            if (paddleLocation.y == playerRb.position.y)
                playerRb.AddForce(new Vector2(rightLimit, playerRb.position.y).normalized * ballSpeed, ForceMode2D.Impulse);
            else if (playerRb.position.y > paddleLocation.y)
                playerRb.AddForce(new Vector2(rightLimit, upLimit).normalized * ballSpeed, ForceMode2D.Impulse);
            else //if(playerRb.position.y < paddleLocation.y)
                playerRb.AddForce(new Vector2(rightLimit, lowLimit).normalized * ballSpeed, ForceMode2D.Impulse);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            playerPaddleHit = false;
            if (aIPaddleLocation.y == playerRb.position.y)
                playerRb.AddForce(new Vector2(leftLimit, playerRb.position.y).normalized * ballSpeed, ForceMode2D.Impulse);
            else if (playerRb.position.y > aIPaddleLocation.y)
                playerRb.AddForce(new Vector2(leftLimit, upLimit).normalized * ballSpeed, ForceMode2D.Impulse);
            else
                playerRb.AddForce(new Vector2(leftLimit, lowLimit).normalized * ballSpeed, ForceMode2D.Impulse);
        }

        //collisions with screen bounds.
        else if (other.gameObject.CompareTag("Up limit"))
        {
            if (playerPaddleHit == true)
                playerRb.AddForce(new Vector2(rightLimit, lowLimit).normalized * ballSpeed, ForceMode2D.Impulse);
            else
                playerRb.AddForce(new Vector2(leftLimit, lowLimit).normalized * ballSpeed, ForceMode2D.Impulse);
        }
        else
        {
            if (playerPaddleHit == true)
                playerRb.AddForce(new Vector2(rightLimit, upLimit).normalized * ballSpeed, ForceMode2D.Impulse);
            else
                playerRb.AddForce(new Vector2(leftLimit, upLimit).normalized * ballSpeed, ForceMode2D.Impulse);
        }

    }
    #endregion
    private void FixedUpdate()
    {
        if (playerPaddleHit == true)
        {
            if(playerRb.position.x <= aiReactPoint)
            {
                EventBroker.CallKickStartAi();
                ballsYPosition = playerRb.position.y;
            }
        }
    }

}
