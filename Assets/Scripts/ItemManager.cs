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

    public List<SpitePosition> SpitesQueue;

    public class SpitePosition
    {
        //public Vector2 leftTop;
        //public Vector2 rightTop;
        //public Vector2 rightBottom;
        //public Vector2 leftBottom;

        public Transform self;
        public Vector3 spiteGroupSize;

        public UnityAction handleDestory;

        public SpitePosition(UnityAction handleDestory)
        {
            this.handleDestory = handleDestory;
        }

        public Bounds CreateBounds()
        {
            return new Bounds(self.position, spiteGroupSize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        safeScreenHeight = screenHeight - 10;

        SpitesQueue = new List<SpitePosition>();
    }

    [Button]
    public async void SpawnSpite()
    {
        float randomX = Random.Range(0, screenWidth);

        var spiteObj = Instantiate(Spite_Prefab, new Vector3(randomX, 2500, 0), Quaternion.identity, SpiteContainer.transform);

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
    public void SpawnCoin()
    {
        float randomX = Random.Range(-50, screenWidth + 50);

        Instantiate(Coin_Prefab, new Vector3(randomX, 3000, 0), Quaternion.identity, CoinContainer.transform);
    }
    [Button]
    public void SpawnCloud()
    {
        float randomX = Random.Range(-50, screenWidth + 50);

        int distance = Random.Range(0, 50);

        if (distance <= 40)
        {

            Instantiate(Cloud_Prefab, new Vector3(randomX, 3500, 0), Quaternion.identity, FarCloudContainer.transform);
        }
        else
        {
            Instantiate(Cloud_Prefab, new Vector3(randomX, 3500, 0), Quaternion.identity, NearCloudContainer.transform);
        }

    }
}
