using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private ItemManager ItemManager;
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
    private float timeStick;
    private float coinSpwanStick;

    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
    }

    void Update()
    {
        CurrentScoreComp.text = "Score: " + currentScore;

        SpawnCoinWork();

        switch (gameStatus)
        {
            case GameStatusSet.Initialized:
                if (Input.GetKey(KeyCode.Escape))
                {
                    Application.Quit();
                }
                break;
            case GameStatusSet.Playing:
                UpdateSpeed();
                break;
            case GameStatusSet.Paused:
                if (Input.GetKey(KeyCode.Escape))
                {
                    // TODO pause menu
                }
                break;
        }
    }

    private void SpawnCoinWork()
    {
        coinSpwanStick += Time.deltaTime;
        if (coinSpwanStick > (speed_fast + 2f) - speed)
        {
            ItemManager.SpawnCoin();
            coinSpwanStick = 0;
        }

    }

    private void UpdateSpeed()
    {
        if (currentScore <= 10)
        {
            speed = speed_slow;
        }
        else if (currentScore <= 100)
        {
            speed = speed_mid;
        }
        else
        {
            speed = speed_fast;
        }
    }

    public void UpdateScore(int delta)
    {
        currentScore += delta;
    }

    private void FixedUpdate()
    {
        timeStick += Time.deltaTime;
        if (timeStick > 1.0)
        {
            int intSpeed = (int)speed;
            if (gameStatus == GameStatusSet.Playing)
            {
                currentHeight += intSpeed / 4;
            }
            timeStick = 0;
            Debug.Log("currentHeight: " + currentHeight);
        }
    }

    public void ResetGame()
    {
        currentHeight = 0;
        currentScore = 0;
        gameStatus = GameStatusSet.Initialized;
    }

    async public void StartGame()
    {
        gameStatus = GameStatusSet.Playing;

        while (true)
        {
            ItemManager.SpawnSpite();
            ItemManager.SpawnCloud();
            int randomDelay;
            if (speed <= speed_slow)
            {
                randomDelay = Random.Range(3000, 4000);
            }
            else if (speed <= speed_mid)
            {
                randomDelay = Random.Range(2000, 3000);
            }
            else
            {
                randomDelay = Random.Range(1000, 2000);
            }
            await UniTask.Delay(randomDelay);
        }
    }
}
