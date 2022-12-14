using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class ItemManager : SingletonMonoBehavior<ItemManager>
{
    public GameObject Spite_Prefab;
    public GameObject Cloud_Prefab;
    public GameObject Coin_Prefab;

    public float screenWidth;
    public float screenHeight;
    public float safeScreenHeight;

    public Transform Canvas;
    public GameObject FarCloudContainer;
    public GameObject NearCloudContainer;
    public GameObject SpiteContainer;
    public GameObject CoinContainer;

    public List<Item> SpitesQueue;
    public List<Item> ItemQueue;
    private float coinSpwanStick;

    public class Item
    {
        public Transform self;
        public Vector3 itemSize;
        public bool collected = false;

        public UnityAction handleDestory;
        public ItemSet itemType;

        public Item(UnityAction handleDestory, ItemSet itemType)
        {
            this.handleDestory = handleDestory;
            this.itemType = itemType;
        }
        public Bounds CreateBounds()
        {
            return new Bounds(self.position, itemSize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        safeScreenHeight = screenHeight - 10;

        SpitesQueue = new List<Item>();
        ItemQueue = new List<Item>();
    }

    private void Update()
    {
        switch (GetComponent<GameManager>().gameStatus)
        {
            case GameStatusSet.Playing:
                SpawnCoinFlow();
                break;
            case GameStatusSet.Paused:
                if (Input.GetKey(KeyCode.Escape))
                {
                    // TODO pause menu
                }
                break;
        }
    }

    private void SpawnCoinFlow()
    {
        coinSpwanStick += Time.deltaTime;
        var gameManager = GetComponent<GameManager>();
        if (coinSpwanStick > (gameManager.speed + 2f) - gameManager.speed)
        {
            SpawnCoin();
            coinSpwanStick = 0;
        }
    }

    [Button]
    public async void SpawnSpite()
    {
        float randomX = Random.Range(0, screenWidth);

        var spiteObj = Instantiate(Spite_Prefab, new Vector3(randomX, 2700, 0), Quaternion.identity, SpiteContainer.transform);

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
    public async void SpawnCoin()
    {
        float randomX = Random.Range(5, screenWidth - 5);

        var coinObj = Instantiate(Coin_Prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, CoinContainer.transform);

        await UniTask.DelayFrame(0);

        var _coinObj = coinObj.GetComponent<CoinController>();

        if (_coinObj == null)
        {
            return;
        }

        var originalDestory = _coinObj.coinPosition.handleDestory;

        ItemQueue.Add(_coinObj.coinPosition);

        int coinIndex = ItemQueue.ToArray().Length - 1;

        _coinObj.coinPosition.handleDestory = () =>
        {
            _coinObj.coinPosition.collected = true;
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
