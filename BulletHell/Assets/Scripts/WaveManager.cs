using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    private int waveNumber;
    public float timeBetweenWaves;
    private Wave currentWave;
    private float timer;
    private float spawnTimer;
    private bool isInWave;
    [SerializeField]
    Text waveText, timerText;
    [SerializeField]
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        timer = 0f;
        spawnTimer = 0f;
        isInWave = true;
        currentWave = waves[waveNumber];
        waveText.text = string.Format("Wave: {0}", waveNumber + 1);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //remove enemies from boss wave  && !currentWave.hasBoss
        if (isInWave && FindObjectsOfType<Enemy>().Length < currentWave.maxNumberOfEnemies && spawnTimer > currentWave.timeBetweenSpawns && currentWave.numberOfEnemies > 0)
        {
            //spawn enemy
            SpawnEnemy();
            spawnTimer = 0;
            currentWave.numberOfEnemies--;
        }
        if(isInWave && currentWave.hasBoss && FindObjectsOfType<Boss>().Length == 0)
        {
            GameObject spawnPoint = currentWave.spawnLocations[0];
            Instantiate(boss, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        //check end wave
        if(isInWave)
        {
            timerText.text = string.Format("Time: {0}", (int)(currentWave.time - timer));
            spawnTimer += Time.deltaTime;
            CheckIsWaveOver();
        }
        else if(timer > timeBetweenWaves)
        {
            FindObjectOfType<ShipController>().RestoreToMaxHealth();
            timer = 0;
            waveNumber++;
            isInWave = true;
            if(waveNumber < waves.Count)
            {

                waveText.text = string.Format("Wave: {0}", waveNumber + 1);
                currentWave = waves[waveNumber];
            }
        }
    }
    private void CheckIsWaveOver()
    {
        isInWave = !(timer > currentWave.time || (currentWave.numberOfEnemies <= 0 && FindObjectsOfType<Enemy>().Length == 0));
        if(!isInWave)
        {
            timer = 0;
            spawnTimer = 0;
            if(FindObjectsOfType<Enemy>().Length!=0)
            {
                foreach (Enemy e in FindObjectsOfType<Enemy>())
                {
                    Destroy(e.gameObject);
                }
            }
        }

    }
    private void SpawnEnemy()
    {
        GameObject spawnPoint = currentWave.spawnLocations[Random.Range(0, currentWave.spawnLocations.Count)];
        GameObject enemy = currentWave.enemyList[Choose(currentWave.enemyPercentages)];
        Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
    int Choose(List<float> probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Count; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Count - 1;
    }
}
