using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawHandler : MonoBehaviour
{
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite friendSprite;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    [SerializeField] private EnergyItem energy;
    enum States
    {
        friend,
        enemy
    }
    private States state;
    // Start is called before the first frame update
    void Start()
    {
        state = States.enemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!energy.isHaveEnergy)
        {
            state = States.friend;
        }
        else
        {
            state = States.enemy;
        }

        if (state == States.enemy)
        {
            left.GetComponent<SpriteRenderer>().sprite = enemySprite;
            right.GetComponent<SpriteRenderer>().sprite = enemySprite;
        }
        if (state == States.friend)
        {
            left.GetComponent<SpriteRenderer>().sprite = friendSprite;
            right.GetComponent<SpriteRenderer>().sprite = friendSprite;
        }

        RotateChain();
    }

    void RotateChain()
    {
        Vector3 zAxis = new Vector3(0, 0, 1);
        left.transform.RotateAround(left.transform.position, zAxis, 300* Time.deltaTime);
        right.transform.RotateAround(right.transform.position, zAxis, 300 * Time.deltaTime);
    }


}
