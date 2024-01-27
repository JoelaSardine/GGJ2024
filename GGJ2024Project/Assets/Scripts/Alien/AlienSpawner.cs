using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private Transform StartPoint, WaitPoint, OutPoint;
    [SerializeField] private GameObject AlienPrefab;

    private Alien currentAlien;

    private void Update()
    {
        if (currentAlien == null)
        {
            currentAlien = Instantiate(AlienPrefab).GetComponent<Alien>();
            currentAlien.Init(StartPoint, WaitPoint, OutPoint);
        }
    }
}
