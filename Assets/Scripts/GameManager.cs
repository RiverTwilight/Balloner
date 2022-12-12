using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private ItemManager ItemManager;
    public enum GameStatusSet
    {
        Playing,
        Initialized
    };

    public GameStatusSet gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
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
            //ItemManager.SpawnCoin();
            await UniTask.Delay(10000);
        }
    }
}
