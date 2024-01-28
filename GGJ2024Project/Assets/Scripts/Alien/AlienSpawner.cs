using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private Transform StartPoint, WaitPoint, OutPoint;
    [SerializeField] private GameObject AlienPrefab;
    [SerializeField] private List<AlienDefinition> Definitions;

    private Alien currentAlien;

    private void Update()
    {
        if (currentAlien == null)
        {
            GameObject go = Instantiate(AlienPrefab);
            currentAlien = go.GetComponent<Alien>();
            currentAlien.AlienDef = Definitions[Random.Range(0, Definitions.Count - 1)];
            currentAlien.Init(StartPoint, WaitPoint, OutPoint);
        }
    }
}
