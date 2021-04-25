using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float waveStartDelay = 3.0f;

    public int enemiesInWave = 10;

    public float delayBetweenEnemies = 0.3f;

    public float difficultyIncreasePerWave = 0.1f;

    public GameObject enemyPrefab;
    
    public List<MovementPattern> powerBillPatterns;

    public List<MovementPattern> waterBillPatterns;

    public static float difficulty = 1.0f;

    private List<List<MovePoint>> finalPatterns = new List<List<MovePoint>>();

    [System.Serializable]
    public class MovementPattern
    {
        public Enemy.Type type;
        public List<MovePointInspector> movePoints;

        public List<MovePoint> Init(bool flipX, bool flipY)
        {
            List<MovePoint> points = new List<MovePoint>();

            foreach (MovePointInspector point in movePoints)
            {
                Vector3 pos = point.Init(flipX, flipY);    
                points.Add(new MovePoint(pos, point.moveDuration, type));
            }

            return points;
        }
    }

    [System.Serializable]
    public class MovePointInspector
    {
        public Transform targetPoint;
        public float moveDuration;

        public Vector3 Init(bool flipX, bool flipY)
        {
            float xMultiplier = (flipX) ? -1.0f : 1.0f;
            float yMultiplier = (flipY) ? -1.0f : 1.0f;
            return new Vector3(xMultiplier * targetPoint.position.x, yMultiplier * targetPoint.position.y, targetPoint.position.z);
        }
    }

    private void Awake()
    {
        foreach (MovementPattern pattern in powerBillPatterns)
        {
            finalPatterns.Add(pattern.Init(false, false));
            finalPatterns.Add(pattern.Init(true, false));
            finalPatterns.Add(pattern.Init(false, true));
            finalPatterns.Add(pattern.Init(true, true));
        }

        foreach (MovementPattern pattern in waterBillPatterns)
        {
            finalPatterns.Add(pattern.Init(false, false));
            finalPatterns.Add(pattern.Init(true, false));
            finalPatterns.Add(pattern.Init(false, true));
            finalPatterns.Add(pattern.Init(true, true));
        }

        difficulty = 1.0f;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies ()
    {
        yield return new WaitForSeconds(waveStartDelay);

        List<MovePoint> pattern = GetRandomPattern();
        int numEnemies = (int)(enemiesInWave * difficulty);
        float spawnDelay = delayBetweenEnemies / difficulty;

        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemy(pattern);
            yield return new WaitForSeconds(spawnDelay);
        }

        difficulty += difficultyIncreasePerWave;

        StartCoroutine(SpawnEnemies());
    }

    private List<MovePoint> GetRandomPattern ()
    {
        return finalPatterns[Random.Range(0, finalPatterns.Count)];
    }

    private void SpawnEnemy(List<MovePoint> points)
    {
        Enemy enemy = Instantiate(enemyPrefab, points[0].targetPoint, Quaternion.identity, null).GetComponent<Enemy>();
        enemy.Init(points, points[0].enemyType);
    }
}

public class MovePoint
{
    public Vector3 targetPoint;
    public float moveDuration;
    public Enemy.Type enemyType;

    public MovePoint(Vector3 point, float duration, Enemy.Type type)
    {
        targetPoint = point;
        moveDuration = duration;
        enemyType = type;
    }
}