using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private EnergyItem energy;
    public Sprite laserOnSprite;
    public Sprite laserOffSprite;
    public Sprite laserOffEnergyOffSprite;

    public float toggleInterval = 0.5f;
    public float rotationSpeed = 0.0f;

    private bool isLaserOn = true;
    private float timeUntilNextToggle;

    private Collider2D laserCollider;
    private SpriteRenderer laserRenderer;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextToggle = toggleInterval;

        laserCollider = gameObject.GetComponent<Collider2D>();
        laserRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energy.isHaveEnergy)
        {
            this.GetComponent<AudioSource>().enabled = true;
            Debug.Log("Have Energy");
            timeUntilNextToggle -= Time.deltaTime;

            if (timeUntilNextToggle <= 0)
            {
                isLaserOn = !isLaserOn;

                laserCollider.enabled = isLaserOn;

                if (isLaserOn)
                {
                    laserRenderer.sprite = laserOnSprite;
                }
                else
                {
                    laserRenderer.sprite = laserOffSprite;
                }

                timeUntilNextToggle = toggleInterval;
            }

            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isLaserOn = false;
            this.GetComponent<AudioSource>().enabled = false;
            laserCollider.enabled = isLaserOn;
            laserRenderer.sprite = laserOffEnergyOffSprite;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(TagManager.PLAYER_TAG))
        {
            collision.GetComponent<PlayerAbility>().isPlayerDied = true;
            Debug.Log("Player die");
        }
    }
}
