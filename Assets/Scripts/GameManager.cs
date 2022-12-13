using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Title("Movement")]
    public float speed_slow = 5f;
    public float speed_mid = 7f;
    public float speed_fast = 10f;
    public float speed = 5f;

    private ItemManager ItemManager;

    public Animator LandAnimator;
    public enum GameStatusSet
    {
        Playing,
        Initialized
    };

    public GameStatusSet gameStatus;

    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
        LandAnimator.speed = 0.5f;
        LandAnimator.Play("Land");
    }

    void Update()
    {
        speed = speed_slow;
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
