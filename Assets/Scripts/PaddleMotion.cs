using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMotion : MonoBehaviour
{

    private float upPaddleBounds = 4.45f;
    private float lowPaddleBounds = -4.98f;

    bool goingUp = false;
    bool goingDown = false;
    float playerSide = 12.82f; //placeholder for direction of the paddle for methods
    Transform paddleTransform;
    //Encapsulating vertical game screen bounds
    public float UpPaddleBounds
    {
        get
        {
            return upPaddleBounds;
        }
    }
    public float LowPaddleBounds
    {
        get
        {
            return lowPaddleBounds;
        }
    }



    private float verticalInput;
    public float speed = 2.0f;
    public Collider2D paddleCollider;

    // Start is called before the first frame update
    void Start()
    {

        paddleCollider = gameObject.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()

    {
        paddleTransform = gameObject.transform;
        verticalInput = Input.GetAxis("Vertical");
        MoveController(paddleTransform);
    }

    protected virtual void MoveController(Transform whichPaddle)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (whichPaddle.position.y <= UpPaddleBounds)
            {
                whichPaddle.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
            }
            else
            {
                whichPaddle.localPosition = new Vector2(playerSide, UpPaddleBounds);
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (whichPaddle.position.y >= LowPaddleBounds)
            {
                whichPaddle.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
            }
            else
            {
                whichPaddle.localPosition = new Vector2(playerSide, LowPaddleBounds);
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        EventBroker.CallPlayerPaddleHit(); //Try using a parameterized call to send the paddle's location.

    }

}
