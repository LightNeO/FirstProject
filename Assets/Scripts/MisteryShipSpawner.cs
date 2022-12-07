using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryShipSpawner : MonoBehaviour
{
    [SerializeField] private GameObject misteryShip;
    private int StartDelay = 15;
    private int SpawnDelay = 30;
    void Start()
    {
        InvokeRepeating("SpawnMisteryShip", StartDelay ,SpawnDelay);
        
    }

    void Update()
    {
        if (InvadersGriid.endWave == true)
        {
            CancelInvoke();
            InvadersGriid.endWave = false;
            InvokeRepeating("SpawnMisteryShip", StartDelay, SpawnDelay);
        }
    }

    private void SpawnMisteryShip()
    {
        Instantiate(misteryShip, transform.position, transform.rotation);
    }
}
