using UnityEngine;

public class PaddleAI : PaddleMotion
{

    bool startMoving = false;
    public float aISpeed;

    BallMovement ballMovement;
    Vector3 target;
    float speedMultiplier;

    float aISide = -6.77f;
    float aIVerticalInput;
    public static Transform aIPaddleTransform;

    public Collider2D AICollider;
    public static bool player2AI;
    void Start()
    {
        player2AI = true;
        EventBroker.KickStartAi += EventBroker_KickStartAi;
        ballMovement = FindObjectOfType<BallMovement>();
        AICollider = gameObject.GetComponent<Collider2D>();
        if(GameManager.PlayerAI == false)
        {
            speedMultiplier = 1.0f;

        }
        else
        {
            speedMultiplier = 2.0f;
        }
        aISpeed = speed * speedMultiplier;

    }

    private void EventBroker_KickStartAi()
    {
        startMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        aIPaddleTransform = gameObject.transform;
        if (GameManager.PlayerAI & startMoving)
        {
            MoveAIController();
        }
        else if (GameManager.PlayerAI == false)
        {
            aIVerticalInput = Input.GetAxis("Vertical");
            MoveController(aIPaddleTransform);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventBroker.callAIPaddleHit();
    }
    private void MoveAIController()
    {
        //base.MoveController();
        target = new Vector3(-6.77f, ballMovement.ballsYPosition);
        Vector3 moveHere = target - new Vector3(aIPaddleTransform.position.x, aIPaddleTransform.position.y);

        if (aIPaddleTransform.position.y <= UpPaddleBounds)
            aIPaddleTransform.Translate((moveHere * Time.deltaTime * aISpeed));
        else
            aIPaddleTransform.position = new Vector2(aISide, UpPaddleBounds);
        //
        if (aIPaddleTransform.position.y >= LowPaddleBounds)
            aIPaddleTransform.Translate((moveHere * Time.deltaTime * aISpeed));
        else
            aIPaddleTransform.position = new Vector2(aISide, LowPaddleBounds);

    }

    protected override void MoveController(Transform whichPaddle)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (whichPaddle.position.y <= UpPaddleBounds)
            {
                whichPaddle.Translate(Vector2.up * Time.deltaTime * aISpeed * aIVerticalInput);
            }
            else
            {
                whichPaddle.localPosition = new Vector2(aISide, UpPaddleBounds);
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (whichPaddle.position.y >= LowPaddleBounds)
            {
                whichPaddle.Translate(Vector2.up * Time.deltaTime * aISpeed * aIVerticalInput);
            }
            else
            {
                whichPaddle.localPosition = new Vector2(aISide, LowPaddleBounds);
            }

        }
    }
    //private void InputLogger2()
    //{
    //    //Upward motion
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        aIGoingUp = true;
    //        aIGoingDown = false;
    //    }
    //    //Downward motion
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        aIGoingDown = true;
    //        aIGoingUp = false;
    //    }
    //}
}
