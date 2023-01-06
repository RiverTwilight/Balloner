using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class GameManager : SingletonMonoBehavior<MonoBehaviour>
{
    private ItemManager ItemManager;
    public Animator LandAnimator;
    public Text CurrentScoreComp;

    [Title("Movement")]
    public float speed_slow = 8f;
    public float speed_mid = 10f;
    public float speed_fast = 12f;

    [Title("Status")]
    public GameStatusSet gameStatus;
    public float speed = 5f;
    public int currentScore;
    public int currentHeight;
    public float timeStick;

    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
        LandAnimator.speed = 0.5f;
        LandAnimator.Play("Land");
    }

    void Update()
    {
        if (currentScore < 100)
        {
            speed = speed_slow;
        }
        else if (currentScore < 200)
        {
            speed = speed_mid;
        }
        else
        {
            speed = speed_fast;
        }
    }

    private void FixedUpdate()
    {
        timeStick += Time.deltaTime;
        if (timeStick > 1.0)
        {
            int intSpeed = (int)speed;
            if (gameStatus == GameStatusSet.Playing)
            {
                currentHeight += intSpeed / 2;
            }
            timeStick = 0;
            Debug.Log("currentHeight: " + currentHeight);
        }
    }

    async public void StartGame()
    {
        gameStatus = GameStatusSet.Playing;

        while (true)
        {
            ItemManager.SpawnSpite();
            ItemManager.SpawnCloud();
            ItemManager.SpawnCoin();
            int randomDelay;
            if (speed == speed_slow)
            {
                randomDelay = Random.Range(9000, 12000);
            }
            else if (speed == speed_mid)
            {
                randomDelay = Random.Range(6000, 9000);
            }
            else
            {
                randomDelay = Random.Range(3000, 6000);
            }
            await UniTask.Delay(randomDelay);
        }
    }
}
