using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite friendSprite;
    SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftTarget, rightTarget;
    bool moveRight = true;
    bool playerOnPlatform;
    bool isUsable = false;
    bool move;
    public int speed = 2;
    public float playerSpeed = 3.5f;
    Vector2 _nextPos;

    private GameObject player;
    public enum State
    {
        Default,
        Energy,
        PlayerLeft
    }

    private State _state;
    [SerializeField] private EnergyItem energy;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
        if (moveRight == true)
        {
            _nextPos = rightTarget.position;
        }
        else
        {
            _nextPos = leftTarget.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (energy.isHaveEnergy)
        {
            _state = State.Default;
            sr.sprite = defaultSprite;
            MoveBetweenTargets();
        }

        if (!energy.isHaveEnergy)
        {
            _state = State.Energy;
            sr.sprite = friendSprite;
        }

        if (_state == State.Energy)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerOnPlatform)
            {
                SoundManager.instance.PlayFX(SoundManager.instance.platformSesi);
                isUsable = !isUsable;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_state == State.Energy)
        {
            if (isUsable && playerOnPlatform)
            {
                move = true;
                player.transform.SetParent(transform);
                PlayerMovement1 movement = player.GetComponent<PlayerMovement1>();
                movement.enabled = false;
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                playerRb.bodyType = RigidbodyType2D.Kinematic;
                //Disable player movement
                EnergyMovement();
            }
            else if (!isUsable)
            {
                move = false;
                //Give Player Control back
                player.transform.SetParent(null);
                PlayerMovement1 movement = player.GetComponent<PlayerMovement1>();
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                playerRb.bodyType = RigidbodyType2D.Dynamic;
                movement.enabled = true;
            }

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state == State.Default)
        {
            //Make Player Child Of The Platform
            if (collision.transform.CompareTag(TagManager.PLAYER_TAG))
            {
                collision.transform.SetParent(transform);
            }
        }

        if (_state == State.Energy)
        {
            if (collision.CompareTag(TagManager.PLAYER_TAG))
            {
                Debug.Log("Can Usable");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_state == State.Energy)
        {
            if (collision.transform.CompareTag(TagManager.PLAYER_TAG))
            {
                playerOnPlatform = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_state == State.Energy)
        {
            if (collision.transform.CompareTag(TagManager.PLAYER_TAG))
            {
                Debug.Log("SDHJFGBHJSDF");
                playerOnPlatform = false;
            }
        }

        if (_state == State.Default)
        {
            //Make Player Child Of The Platform
            if (collision.CompareTag(TagManager.PLAYER_TAG))
            {
                collision.transform.SetParent(null);
            }
        }
    }

    private void MoveBetweenTargets()
    {
        if (transform.position == rightTarget.position)
        {
            _nextPos = leftTarget.position;
        }
        if (transform.position == leftTarget.position)
        {
            _nextPos = rightTarget.position;
        }

        transform.position = Vector3.MoveTowards(transform.position,_nextPos,speed * Time.deltaTime);
    }

    private void EnergyMovement()
    {
        if (move)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float moveByX = x * playerSpeed;
            float moveByY = y * playerSpeed;

            rb.velocity = new Vector2(moveByX, moveByY);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }
}
