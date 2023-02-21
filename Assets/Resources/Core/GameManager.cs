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

    [Title("Speed Index Preset")]
    public int speed_slow;
    public int speed_mid;
    public int speed_fast;

    [Title("Status")]
    public GameStatusSet gameStatus;
    [ReadOnly] public float speed;
    public float speedIndex;
    public int baseSpeed;
    [ReadOnly] public bool isLevelChanging = false;
    public int currentScore;
    public int currentHeight;
    private float timeStick;

    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            // Set target FPS to 90 for mobile devices
            Application.targetFrameRate = 90;
        }
        else
        {
            // Set target FPS to 300 for desktop devices
            Application.targetFrameRate = 300;
        }
    }

    void Update()
    {
        CurrentScoreComp.text = "Score: " + currentScore;

        switch (gameStatus)
        {
            case GameStatusSet.Initialized:
                if (Input.GetKey(KeyCode.Escape))
                {
                    Application.Quit();
                }
                break;
            case GameStatusSet.Playing:
                timeStick += Time.deltaTime;
                if (timeStick > 1.0)
                {
                    int intSpeed = (int)speed;
                    currentHeight += intSpeed / 4;
                    timeStick = 0;
                }
                UpdateSpeed();
                if (Input.GetKey(KeyCode.Escape))
                {
                    gameStatus = GameStatusSet.Paused;
                    // TODO display pause menu
                }
                break;
            case GameStatusSet.Paused:
                if (Input.GetKey(KeyCode.Escape))
                {
                    // TODO close pause menu
                }
                break;
        }
    }
    private void UpdateSpeed()
    {
        if (currentScore <= 10)
        {
            speedIndex = speed_slow;
        }
        else if (currentScore <= 50 && !isLevelChanging)
        {
            SpeedUp(speed_mid);
        }
        else if (currentScore > 80 && speedIndex < speed_fast)
        {
            SpeedUp(speed_fast);
        }
        speed = baseSpeed * speedIndex;
    }

    private async void SpeedUp(int target)
    {
        isLevelChanging = true;
        while (speedIndex < target)
        {
            speedIndex += 0.05f;
            await UniTask.Delay(500);
        }
        isLevelChanging = false;
    }

    public void UpdateScore(int delta)
    {
        currentScore += delta;
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

        await UniTask.Delay(1000);

        while (true)
        {
            ItemManager.SpawnCloud();
            int randomDelay = Random.Range(3000, 4000);
            randomDelay /= (int)speedIndex;
            await UniTask.Delay(randomDelay);
        }
    }

    public void HandlePause()
    {
        Time.timeScale = 0f; // Pause game
        Debug.Log("User pressed pause button");
    }
}
