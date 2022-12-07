using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 30.0f;

    public System.Action destroyed;


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.destroyed != null) destroyed.Invoke();

        Destroy(gameObject);
    }
}
