using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private ItemManager ItemManager;
    // Start is called before the first frame update
    void Start()
    {
        ItemManager = GetComponent<ItemManager>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void StartGame()
    {
        ItemManager.SpawnSpite();

        //while (true)
        //{
        //    ItemManager.SpawnSpite();
        //    //ItemManager.SpawnCloud();
        //    //ItemManager.SpawnCoin();
        //    await UniTask.Delay(8000);
        //}
    }
}
