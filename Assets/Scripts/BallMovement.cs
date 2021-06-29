using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BallMovement : MonoBehaviour

{
    #region Field Declarations

 
    [SerializeField] float ballSpeed = 3.0f;
    [SerializeField] float aiReactPoint = 2.07f; //currently -1 in the inspector(which reacts okay)
    
    Rigidbody2D playerRb;
    //used to manipulate the direction of the ball bounces
    bool playerPaddleHit = false; 
    bool aIPaddleHit = false;
    bool noHit;

    public float ballsYPosition; 
    //screen bounds
    public static float leftLimit = 12.82f;
    public static float rightLimit = -6.77f;
    public static float upLimit = 4.82f;
    public static float lowLimit = -5.32f;
    //
    Vector3 target;
    Vector2 paddleLocation;
    Vector2 aIPaddleLocation;
    PaddleMotion paddleMotion;
    PaddleAI paddleAI;

    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI aIScoreText;
    [SerializeField] GameObject[] uIPrefabs;

    int playerScore = 0;
    int aIScore = 0;
    

    #endregion

    private void Awake()
        
    {
        playerRb = GetComponent<Rigidbody2D>();
        noHit = true;
        

    }
    private void Update()
    {
        TrackPlayerScore();
    }
    void Start()
    {

        RestartBall("start");
        EventBroker.PlayerPaddleHit += EventBroker_PlayerPaddleHit;
        EventBroker.AIPaddleHit += EventBroker_AIPaddleHit;
        paddleMotion = FindObjectOfType<PaddleMotion>();
        paddleAI = FindObjectOfType<PaddleAI>();
    }


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
    
        
        paddleLocation = paddleMotion.paddleCollider.transform.position;
        aIPaddleLocation = paddleAI.AICollider.transform.position;
        //Collisions with paddles.
        if (other.gameObject.CompareTag("Player"))
        {
            aIPaddleHit = false;
            DecidePaddleBounce(paddleLocation, rightLimit);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            playerPaddleHit = false;
            DecidePaddleBounce(aIPaddleLocation, leftLimit);
        }

        //collisions with screen bounds.
        else if (other.gameObject.CompareTag("Up limit"))
        {
            DecideBoundBounce(lowLimit);
        }
        else
        {
            DecideBoundBounce(upLimit);
        }

    }
    void DecidePaddleBounce(Vector2 paddleLocationVector, float horizontalLimit)
    {
        noHit = false;
        if (paddleLocationVector.y == playerRb.position.y)
            playerRb.AddForce(new Vector2(horizontalLimit, playerRb.position.y).normalized * ballSpeed, ForceMode2D.Impulse);
        else if (playerRb.position.y > paddleLocationVector.y)
            playerRb.AddForce(new Vector2(horizontalLimit, upLimit).normalized * ballSpeed, ForceMode2D.Impulse);
        else
            playerRb.AddForce(new Vector2(horizontalLimit, lowLimit).normalized * ballSpeed, ForceMode2D.Impulse);
    }
    void DecideBoundBounce(float verticalBound)
    {
        if (noHit == true)
        {
            if (playerRb.position.x <= 3.580f)
            {
                playerRb.AddForce(new Vector2(rightLimit, verticalBound).normalized * ballSpeed, ForceMode2D.Impulse);
            }
            else
            {
                playerRb.AddForce(new Vector2(leftLimit, verticalBound).normalized * ballSpeed, ForceMode2D.Impulse);
            }
        }
        if (playerPaddleHit == true)
            playerRb.AddForce(new Vector2(rightLimit, verticalBound).normalized * ballSpeed, ForceMode2D.Impulse);
        else
            playerRb.AddForce(new Vector2(leftLimit, verticalBound).normalized * ballSpeed, ForceMode2D.Impulse);

    }

    void RestartBall(string identifier)
    {
        playerRb.velocity = Vector2.zero;
        playerRb.transform.position = new Vector2(3.58f, 2.17f);
        if (identifier == "start")
        {
            target = new Vector2(Random.Range(rightLimit, leftLimit), Random.Range(lowLimit, upLimit));
        }
        else if(identifier == "player") { target = new Vector2(rightLimit, Random.Range(lowLimit, upLimit)); }
        else { target = new Vector2(leftLimit, Random.Range(lowLimit, upLimit)); }
            
        playerRb.AddForce((target - playerRb.transform.position).normalized * ballSpeed, ForceMode2D.Impulse);
    }
    void TrackPlayerScore()
    {
        if ((playerScore < 3)& (aIScore < 3))
        {

            if (playerRb.position.x < rightLimit)
            {
                RestartBall("player");
                playerScore++;
                playerScoreText.text = playerScore.ToString();
                
            }
            else if (playerRb.position.x > leftLimit)
            {
                RestartBall("ai");
                aIScore++;
                aIScoreText.text = aIScore.ToString();
                
            }


        }
        else
        {
            //Trigger gameOver
            gameObject.SetActive(false);
           DeclareWinner();
            for (int i = 0; i < (uIPrefabs.Length-3);i++)
            {
                uIPrefabs[i].SetActive(true);
            }
            
        }
    }

    void DeclareWinner()
    {
        uIPrefabs[3].SetActive(true);
        if (aIScore > playerScore)
        {
            uIPrefabs[5].SetActive(true);
        }
        else
        {
            uIPrefabs[4].SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if ((playerPaddleHit == true)|(noHit == true))
        {
            if(playerRb.position.x <= aiReactPoint)
            {
                EventBroker.CallKickStartAi();
                ballsYPosition = playerRb.position.y;
            }
        }
    }

}
