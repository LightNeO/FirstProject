using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InvadersGriid : MonoBehaviour
{
    [SerializeField] private Invaders[] invadersPrefab;
    [SerializeField] Projectile missilePrefab;

    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 8;
    [SerializeField] private float rowSpacing = 5.0f;
    [SerializeField] private AnimationCurve speed; 
    [SerializeField] private float gridMovingDistance = 1.0f;
    [SerializeField] private float missileAttackRate = 1.0f;
    [SerializeField] private GameObject congratText;
    [SerializeField] private GameObject newWaveText;
    public float speedMultiplier = 1f;
    public static bool endWave = false;

    private Vector3 _direction = Vector2.right;
    private Vector3 startPosition = new Vector3(0, 40, 0);

    public int amountKilled { get; private set; }
    public int totalInvaders => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;
    public float amountAlive => totalInvaders - amountKilled;

    void Start()
    {
        InitiateWave();
        
        InvokeRepeating("MissileAttack", missileAttackRate, missileAttackRate);
    }

    private void Update()
    {
        speed = new AnimationCurve(new Keyframe(0, 1 * speedMultiplier), new Keyframe(0.25f, 3 * speedMultiplier), new Keyframe(0.5f, 5 * speedMultiplier), new Keyframe(0.75f, 7 * speedMultiplier), new Keyframe(1.0f, 10 * speedMultiplier));
        transform.position += _direction * speed.Evaluate(percentKilled) * Time.deltaTime;
        Vector3 leftBorderPosition = leftBorder.transform.position;
        Vector3 rightBorderPosition = rightBorder.transform.position;

        foreach (Transform invaders in transform)
        {
            if (!invaders.gameObject.activeInHierarchy) continue;

            if (_direction == Vector3.right && invaders.position.x >= (rightBorderPosition.x - 3.0f)) MoveGrid();
            else if (_direction == Vector3.left && invaders.position.x <= (leftBorderPosition.x + 3.0f)) MoveGrid();
        }
        
        Debug.Log(PlayerPrefs.GetInt("TopScore"));
        Debug.Log(Score.scoreValue);
    }

    private void MoveGrid()
    {
        _direction.x *= -gridMovingDistance;
        Vector3 position = transform.position;
        position.y -= gridMovingDistance;
        transform.position = position;
    }

    private void killInvader()
    {
        amountKilled++;

        if (amountKilled >= totalInvaders)
        {
            speedMultiplier +=0.2f;
            StartCoroutine("startNewWave");
        }
    }

    private void MissileAttack()
    {
        foreach (Transform invaders in transform)
        {
            if (!invaders.gameObject.activeInHierarchy) continue;

            if (Random.value < (1.0f / (float)amountAlive))
            {
                Instantiate(missilePrefab, invaders.position, invaders.rotation);
                break;
            }
        }
    }

    private void InitiateWave()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = rowSpacing * (columns - 1);
            float height = rowSpacing * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * rowSpacing), 0);

            for (int column = 0; column < columns; column++)
            {
                Invaders invader = Instantiate(invadersPrefab[row], transform);
                invader.killed += killInvader;
                Vector3 position = rowPosition;
                position.x += column * rowSpacing;
                invader.transform.localPosition = position;
            }
        }
    }

    IEnumerator startNewWave()
    {
        congratText.SetActive(true);
        yield return new WaitForSeconds(2);
        congratText.SetActive(false);
        yield return new WaitForSeconds(2);
        newWaveText.SetActive(true);
        yield return new WaitForSeconds(2);
        newWaveText.SetActive(false);
        
        yield return new WaitForSeconds(2);
        transform.position = startPosition;
        amountKilled = 0;
        InitiateWave();
        endWave = true;
    }
}
