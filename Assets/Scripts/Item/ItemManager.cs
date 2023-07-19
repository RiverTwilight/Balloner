using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;

public class ItemManager : SingletonMonoBehavior<ItemManager>
{
    [System.Serializable]
    public struct SpawnableItem
    {
        public GameObject prefab;
        public int probability;
    }

    [Title("Item Prefabs")]
    public List<GameObject> Obstacle_Prefabs;
    public GameObject Cloud_Prefab;
    public List<SpawnableItem> collectableItems;

    [Title("Device Specefication")]
    [ShowInInspector] private float screenWidth;
    [ShowInInspector] private float screenHeight;
    [ShowInInspector] private float safeScreenHeight;

    [Title("Containers")]
    public Transform Canvas;
    public GameObject FarCloudContainer;
    public GameObject NearCloudContainer;
    public GameObject SpiteContainer;
    public GameObject CollectableItemContainer;

    private SyncItemFactory collectableSpawnFlow;
    private SyncItemFactory spiteSpawnFlow;
    private SyncItemFactory cloudSpawnFlow;
    public List<BoundedItem> SpitesQueue;
    public List<BoundedItem> ItemQueue;

    public class SyncItemFactory
    {
        private float spawnStick = -3f;
        public Action onSpawned;
        public float offset;

        public SyncItemFactory(Action SpawnFunction, float SpawnIntervalOffset)
        {
            onSpawned = SpawnFunction;
            offset = SpawnIntervalOffset;
        }

        public void SpawnSyncItem(GameManager gameManager)
        {
            spawnStick += Time.deltaTime;

            if (spawnStick > (45f + offset) / gameManager.speed)
            {
                onSpawned?.Invoke();
                spawnStick = 0;
            }
        }
    }

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        safeScreenHeight = screenHeight - 10;

        SpitesQueue = new List<BoundedItem>();
        ItemQueue = new List<BoundedItem>();

        collectableSpawnFlow = new SyncItemFactory(SpawnCollectableItem, -6f);
        spiteSpawnFlow = new SyncItemFactory(SpawnObstacle, 0);
        cloudSpawnFlow = new SyncItemFactory(SpawnCloud, 2f);
    }

    private void Update()
    {
        switch (GameManager.Instance.gameStatus)
        {
            case GameStatusSet.Playing:
                GameManager gameManager = GameManager.Instance;

                spiteSpawnFlow.SpawnSyncItem(gameManager);
                collectableSpawnFlow.SpawnSyncItem(gameManager);
                cloudSpawnFlow.SpawnSyncItem(gameManager);
                break;
        }
    }

    [Button]
    public async void SpawnObstacle()
    {
        float randomX = Random.Range(0, screenWidth);

        int randomIndex = Random.Range(0, Obstacle_Prefabs.Count);

        var spiteObj = Instantiate(Obstacle_Prefabs[randomIndex], new Vector3(randomX, 3000, 0), Quaternion.identity, SpiteContainer.transform);

        await UniTask.DelayFrame(0);

        var _spiteObj = spiteObj.GetComponent<MoveableItem>();

        if (_spiteObj == null)
        {
            return;
        }

        var originalDestory = _spiteObj._item.handleDestory;

        _spiteObj._item.handleDestory = () =>
        {
            originalDestory();
            SpitesQueue.RemoveAt(0);
        };

        SpitesQueue.Add(_spiteObj._item);
    }

    [Button]
    public async void SpawnCollectableItem()
    {
        float randomX = Random.Range(5, screenWidth - 5);

        // Calculating total probability
        int totalProbability = 0;
        foreach (var item in collectableItems)
        {
            totalProbability += item.probability;
        }

        int randomIndex = Random.Range(0, totalProbability);

        GameObject collectableObj = null;
        int currentSum = 0;
        foreach (var item in collectableItems)
        {
            currentSum += item.probability;
            if (randomIndex < currentSum)
            {
                collectableObj = Instantiate(item.prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, CollectableItemContainer.transform) as GameObject;
                break;
            }
        }

        if (collectableObj == null)
        {
            return;
        }

        MoveableItem _collectableObj = collectableObj.GetComponent<MoveableItem>();

        await UniTask.DelayFrame(0);

        if (_collectableObj == null)
        {
            return;
        }

        var originalDestory = _collectableObj._item.handleDestory;

        ItemQueue.Add(_collectableObj._item);

        int coinIndex = ItemQueue.ToArray().Length - 1;

        _collectableObj._item.handleDestory = () =>
        {
            _collectableObj._item.collected = true;
            originalDestory();
        };
    }

    [Button]
    public void SpawnCloud()
    {
        float randomX = Random.Range(-50, screenWidth + 50);

        int distance = Random.Range(0, 100);

        if (gameObject.GetComponent<GameManager>().currentHeight >= 150)
        {
            if (distance <= 60)
            {

                GameObject cloud = Instantiate(Cloud_Prefab, new Vector3(randomX, 3500, 0), Quaternion.identity, FarCloudContainer.transform);
                cloud.GetComponent<CloudController>().SetFarCloud(true);
            }
            else
            {
                // Magnifiy near cloud, and force alpha to 0.9.
                GameObject cloud = Instantiate(Cloud_Prefab, new Vector3(randomX, 3500, 0), Quaternion.identity, NearCloudContainer.transform);
                cloud.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
                cloud.GetComponent<CanvasGroup>().alpha = 0.9f;
                cloud.GetComponent<CloudController>().SetFarCloud(false);
            }
        }
    }
}
