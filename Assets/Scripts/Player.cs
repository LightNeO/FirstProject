using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private Projectile laser;
    [SerializeField] GameObject[] joystickPrefabs;
    [SerializeField] GameObject[] buttonsPrefabs;
    [SerializeField] GameObject[] livesPrefabs;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private int lives = 3;
    private bool _laserActive;
    private int maxDistance = 32;

    public Sprite[] playerSprites;
    private int frameNumber;
    private SpriteRenderer _spriteRenderer;
    public static bool isDead = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        lives = 3;

    }
    void Update()
    {
        if (transform.position.x > maxDistance)
        {
            transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -maxDistance)
        {
            transform.position = new Vector3(-maxDistance, transform.position.y, transform.position.z);
        }
        MovePlayer();

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
            buttonsPrefabs[0].gameObject.SetActive(false);
            buttonsPrefabs[1].gameObject.SetActive(true);
        }
        else
        {
            buttonsPrefabs[0].gameObject.SetActive(true);
            buttonsPrefabs[1].gameObject.SetActive(false);
        }

        if (isDead)
        {
            killPlayer();
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(horizontalInput, 0);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        transform.Translate(movementDirection * magnitude * speed * Time.deltaTime);

        if (horizontalInput < 0)
        {
            joystickPrefabs[0].gameObject.SetActive(false);
            joystickPrefabs[1].gameObject.SetActive(true);
            joystickPrefabs[2].gameObject.SetActive(false);
        }
        else if (horizontalInput > 0)
        {
            joystickPrefabs[0].gameObject.SetActive(false);
            joystickPrefabs[1].gameObject.SetActive(false);
            joystickPrefabs[2].gameObject.SetActive(true);
        }
        else
        {
            joystickPrefabs[0].gameObject.SetActive(true);
            joystickPrefabs[1].gameObject.SetActive(false);
            joystickPrefabs[2].gameObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        if (!_laserActive && !isDead)
        {
            Projectile projectile = Instantiate(laser, transform.position, transform.rotation);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }
    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private void hitPlayer()
    {
        if (lives == 3)
        {
            lives--;
            livesPrefabs[2].SetActive(false);
        }
        else if (lives == 2)
        {
            lives--;
            livesPrefabs[1].SetActive(false);
        }
        else if (lives == 1)
        {
            killPlayer();
        }
    }

    private void killPlayer()
    {
        lives = 0;
        livesPrefabs[0].SetActive(false);

        frameNumber = 1;

        _spriteRenderer.sprite = playerSprites[frameNumber];

        speed = 0;
        isDead = true;
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GAMEOVER
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            hitPlayer();
        }
    }
}
