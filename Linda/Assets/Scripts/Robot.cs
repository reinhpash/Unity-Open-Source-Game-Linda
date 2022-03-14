using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    Animator anim;
    [SerializeField] private float speed = 1.5f;
    private Transform target;
    [SerializeField] private GameObject player;
    public float minDistance = 1.2f;
    private GameObject targetRobot;
    private Collider2D coll;
    public bool imGonnaDestroyed = false;

    public enum State
    {
        Default,
        Friend,
        Attack
    }

    public State _state;
    [SerializeField] private EnergyItem energy;

    // Start is called before the first frame update
    void Start()
    {
        coll = this.GetComponent<Collider2D>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
        _state = State.Default;
    }

    private void Update()
    {
        if (!energy.isHaveEnergy)
        {
            _state = State.Friend;
            Physics2D.IgnoreCollision(coll, player.GetComponent<BoxCollider2D>(), true);
            anim.SetBool("isFriend", true);
        }
        else
        {
            _state = State.Default;
            Physics2D.IgnoreCollision(coll, player.GetComponent<BoxCollider2D>(), false);
            anim.SetBool("isFriend", false);
        }

        if (_state == State.Default)
        {
            if (target == null)
            {

            }
            else if (target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                //Movement(target);
            }
        }

        if (_state == State.Friend)
        {
            if (target == null || !target.CompareTag(TagManager.Robot_TAG))
            {
                if (Vector2.Distance(transform.position, player.transform.position) > minDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
            }
            else if (target != null && !target.CompareTag(TagManager.PLAYER_TAG))
            {
                targetRobot = target.gameObject;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }

    }

    void Movement(Transform targetTransform)
    {
        Vector3 direction = (targetTransform.transform.position - this.transform.position).normalized;
        rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state == State.Default)
        {
            if (collision.CompareTag(TagManager.PLAYER_TAG))
            {
                Debug.Log("Player");
                target = collision.gameObject.transform;
            }
        }

        if (_state == State.Friend)
        {
            if (collision.CompareTag(TagManager.Robot_TAG))
            {
                Debug.Log("Robot");
                target = collision.gameObject.transform;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_state == State.Default)
        {
            if (collision.transform.CompareTag(TagManager.PLAYER_TAG))
            {
                collision.transform.GetComponent<PlayerAbility>().isPlayerDied = true;
            }
        }
        if (_state == State.Friend)
        {
            if (collision.transform.CompareTag(TagManager.Robot_TAG))
            {
                Debug.Log("Robot Destroy " + targetRobot.name);
                target = collision.gameObject.transform;
                collision.gameObject.GetComponent<Robot>().imGonnaDestroyed = true;
                SoundManager.instance.PlayFX(SoundManager.instance.RobotOlumFx);
                Destroy(collision.gameObject, 0.5f);
                Destroy(this.gameObject,0.5f);
            }
        }
    }

    void ApplyGravity()
    {
        transform.Translate(9.81f * Time.deltaTime * Vector2.down);
    }
}
