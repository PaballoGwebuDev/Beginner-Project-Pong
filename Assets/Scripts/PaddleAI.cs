using UnityEngine;

public class PaddleAI : Bounding
{
    // Start is called before the first frame update
    bool startMoving = false;
    BallMovement ballMovement;
    Vector3 target;
    float speed = 5.0f;
    public Collider2D AICollider;
    void Start()
    {
        Bounding myBounding = new Bounding();
        EventBroker.KickStartAi += EventBroker_KickStartAi;
        ballMovement = FindObjectOfType<BallMovement>();
        AICollider = gameObject.GetComponent<Collider2D>(); //you actually have to instantiate colliders like this to get them to work inGame. 


    }

    private void EventBroker_KickStartAi()
    {
        startMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving == true)
        {
            target = new Vector3(-6.77f, ballMovement.ballsYPosition);
            Vector3 moveHere = target - new Vector3(transform.position.x, transform.position.y, 0.0f);

            if (transform.position.y <= UpPaddleBounds.y)
                transform.Translate((moveHere * Time.deltaTime * speed));
            else
                transform.position = UpPaddleBounds;
            //
            if (transform.position.y >= LowPaddleBounds.y)
                transform.Translate((moveHere * Time.deltaTime * speed));
            else
                transform.position = LowPaddleBounds;



        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventBroker.callAIPaddleHit();
    }
}
