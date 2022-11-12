using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<int> enemyAmount;
    public List<GameObject> spawnLocations;
    public int time, maxNumberOfEnemies;
    public bool hasBoss;
    public GameObject boss;
}
