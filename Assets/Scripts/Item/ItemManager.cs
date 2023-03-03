using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;

public class ItemManager : SingletonMonoBehavior<ItemManager>
{
    [Title("Prefabs")]
    public GameObject Spite_Prefab;
    public GameObject Cloud_Prefab;
    public GameObject Coin_Prefab;
    public GameObject Magnet_Prefab;

    [Title("Device Specefication")]
    public float screenWidth;
    public float screenHeight;
    public float safeScreenHeight;

    public Transform Canvas;
    public GameObject FarCloudContainer;
    public GameObject NearCloudContainer;
    public GameObject SpiteContainer;
    public GameObject CollectableItemContainer;

    private SyncItemFactory collectableSpawnFlow;
    private SyncItemFactory spiteSpawnFlow;
    public List<BoundedItem> SpitesQueue;
    public List<BoundedItem> ItemQueue;

    public class SyncItemFactory
    {
        private float coinSpawnStick = -3f;
        public Action onSpawned;
        public float offset;

        public SyncItemFactory(Action SpawnFunction, float SpawnIntervalOffset)
        {
            onSpawned = SpawnFunction;
            offset = SpawnIntervalOffset;
        }

        public void SpawnSyncItem(GameManager gameManager)
        {
            coinSpawnStick += Time.deltaTime;

            if (coinSpawnStick > (50f + offset) / gameManager.speed)
            {
                onSpawned?.Invoke();
                coinSpawnStick = 0;
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
        spiteSpawnFlow = new SyncItemFactory(SpawnSpite, 0);
    }

    private void Update()
    {
        switch (GameManager.Instance.gameStatus)
        {
            case GameStatusSet.Playing:
                GameManager gameManager = GameManager.Instance;

                spiteSpawnFlow.SpawnSyncItem(gameManager);
                collectableSpawnFlow.SpawnSyncItem(gameManager);
                break;
        }
    }

    [Button]
    public async void SpawnSpite()
    {
        float randomX = Random.Range(0, screenWidth);

        var spiteObj = Instantiate(Spite_Prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, SpiteContainer.transform);

        await UniTask.DelayFrame(0);

        var _spiteObj = spiteObj.GetComponent<SpliteGenerator>();

        if (_spiteObj == null)
        {
            return;
        }

        var originalDestory = _spiteObj.spitePosition.handleDestory;

        _spiteObj.spitePosition.handleDestory = () =>
        {
            originalDestory();
            SpitesQueue.RemoveAt(0);
        };

        SpitesQueue.Add(_spiteObj.spitePosition);
    }

    [Button]
    public async void SpawnCollectableItem()
    {
        float randomX = Random.Range(5, screenWidth - 5);

        int randomIndex = Random.Range(0, 100);

        MoveableItem _collectableObj;

        if (randomIndex < 70)
        {
            GameObject collectableObj = Instantiate(Coin_Prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, CollectableItemContainer.transform) as GameObject ;
             _collectableObj = collectableObj.GetComponent<CoinController>();
        }
        else
        {
            GameObject collectableObj = Instantiate(Magnet_Prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, CollectableItemContainer.transform) as GameObject ;
             _collectableObj = collectableObj.GetComponent<MagnetController>();
        }

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
