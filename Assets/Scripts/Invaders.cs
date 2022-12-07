using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Sprite[] invadersSprites;
    [SerializeField] private float animationSpeed = 2f;
    private int frameCount;
    private SpriteRenderer _spriteRenderer;
    public System.Action killed;
    [SerializeField] private int invaderNumber;
    [SerializeField] private Sprite deathSprite;
    private BoxCollider2D _spriteBoxCollider;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteBoxCollider = GetComponent<BoxCollider2D>();

        InvokeRepeating("ChangeSprite", animationSpeed, animationSpeed);
    }

    private void ChangeSprite()
    {
        frameCount++;

        if (frameCount >= invadersSprites.Length)
        {
            frameCount = 0;
        }

        _spriteRenderer.sprite = invadersSprites[frameCount];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            InvokeRepeating("showDeathSprite", 0.001f, 0.001f);
            _spriteBoxCollider.enabled = false;
            StartCoroutine(killInvader());

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("BottomBorder"))
        {
            Player.isDead = true;
        }



    }
    private void showDeathSprite()
    {
        _spriteRenderer.sprite = deathSprite;
    }

    IEnumerator killInvader()
    {
        yield return new WaitForSeconds(1);

        switch (invaderNumber)
        {
            case 1:
                Score.scoreValue += 10;
                break;
            case 2:
                Score.scoreValue += 20;
                break;
            case 3:
                Score.scoreValue += 30;
                break;
            case 4:
                Score.scoreValue += 40;
                break;
            case 5:
                Score.scoreValue += 50;
                break;
            case 6:
                Score.scoreValue += 60;
                break;
            case 7:
                Score.scoreValue += 70;
                break;
            case 8:
                Score.scoreValue += 80;
                break;
            case 9:
                Score.scoreValue += 90;
                break;
            case 10:
                Score.scoreValue += 100;
                break;
        }
        killed.Invoke();
        Destroy(gameObject);


    }
}
