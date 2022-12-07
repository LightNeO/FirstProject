using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryShip : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    private int maxDistancec = 34;
    [SerializeField] Sprite deathSprite;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        MoveRight();
        if (transform.position.x >= maxDistancec)
        {
            Destroy(gameObject);
        }
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Score.scoreValue += Random.Range(150,300);
            _spriteRenderer.sprite = deathSprite;

            StartCoroutine("killMisteryShip");

        }
    }

    IEnumerable killMisteryShip()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
