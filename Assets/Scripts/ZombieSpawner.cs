using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Префаб зомби")]
    public GameObject zombiePrefab;
    [Tooltip("Точка спавна")]
    public Transform spawnPoint;
    [Tooltip("Интервал спавна в секундах")]
    public float spawnInterval = 5f;
    [Tooltip("Максимальное количество зомби на сцене (0 — без лимита)")]
    public int maxZombies = 0;

    private int currentZombies = 0;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // ждем первой итерации
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            // если введен лимит — проверяем
            if (maxZombies == 0 || currentZombies < maxZombies)
            {
                SpawnOne();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnOne()
    {
        var go = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        currentZombies++;

        // подписываемся на событие смерти, чтобы уменьшить счетчик
        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            // когда зомби умирает, вычитаем его из currentZombies
            StartCoroutine(TrackDeath(enemy));
        }
    }

    private IEnumerator TrackDeath(Enemy enemy)
    {
        // ждем пока Enemy.isDead станет true и объект будет уничтожен
        while (!enemy.isDead)
            yield return null;

        // даем время на Destroy(gameObject,3f) внутри Enemy
        yield return new WaitForSeconds(3f);
        currentZombies = Mathf.Max(0, currentZombies - 1);
    }
}
