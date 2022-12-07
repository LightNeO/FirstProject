using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryShipSpawner : MonoBehaviour
{
    [SerializeField] private GameObject misteryShip;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMisteryShip", 10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMisteryShip()
    {
        Instantiate(misteryShip, transform.position, transform.rotation);
    }
}
