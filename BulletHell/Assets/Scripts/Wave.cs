using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<float> enemyPercentages;
    public List<GameObject> spawnLocations;
    public int time, maxNumberOfEnemies, numberOfEnemies;
    public float timeBetweenSpawns;
    public bool hasBoss;
    public GameObject boss;
}
