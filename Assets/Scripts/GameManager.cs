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

    async public void StartGame()
    {
        gameStatus = GameStatusSet.Playing;

        while (true)
        {
            ItemManager.SpawnSpite();
            ItemManager.SpawnCloud();
            ItemManager.SpawnCoin();
            await UniTask.Delay(10000);
        }
    }
}
