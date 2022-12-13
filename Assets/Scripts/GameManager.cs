using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
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

    // Update is called once per frame
    //void Update()
    //{

    //}

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
