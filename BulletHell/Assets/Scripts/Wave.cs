using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<int> enemyRatios;
    public List<GameObject> spawnLocations;
    public int time, maxNumberOfEnemies, numberOfEnemies;
    public bool hasBoss;
    public GameObject boss;
}
