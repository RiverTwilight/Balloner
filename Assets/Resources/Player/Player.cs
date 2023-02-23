using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    [Title("Touch")]
    [ReadOnly] private Touch theTouch;
    [ReadOnly] private Vector2 touchStartPosition, touchEndPosition;

    public GameObject ballon;
    public Bounds ballonBounds;
    public GameManager gameManager;
    public RectTransform ballonTransform;
    public Vector2 ballonSize;

    public Animator shadowAnim;
    public float safeAreaHeight;

    void Start()
    {
        gameManager = GameObject.Find("Context").GetComponent<GameManager>();
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 315, 0);

        Animator ballonerAnim = gameObject.GetComponent<Animator>();
        ballonerAnim.Play("Floating");
        shadowAnim.Play("ShadowFloating");

        ballonTransform = GetComponent<RectTransform>();
        ballonSize = ballonTransform.rect.size;

        safeAreaHeight = Screen.height * 0.9f;
    }

    void Update()
    {
        switch (gameManager.gameStatus)
        {
            case GameStatusSet.Playing:
                Movement();
                CheckCollidation();
                CheckCollectableItems();
                break;
        }
    }

    void CheckCollectableItems()
    {
        var transform = GetComponent<RectTransform>();
        var collider = GetComponent<BoxCollider2D>();

        ballonBounds = new Bounds(collider.bounds.center * 2, collider.bounds.size * 2);

        List<ItemManager.Item> queue = ItemManager.Instance?.ItemQueue.Where(i => i.self != null).ToList();
        int queueLength = queue.ToArray().Length;

        for (int i = 0; i < queueLength; i++)
        {
            var item = queue[i];
            var spiteBounds = item.CreateBounds();
            var actualSpiteBounds = new Bounds(spiteBounds.center * 2, spiteBounds.size);
            //Debug.Log($"Spite{spite} [center: {spiteBounds.center} size: {spiteBounds.size}]");
            if (ballonBounds.Intersects(actualSpiteBounds))
            {
                Debug.Log("Collected");

                if (PlayerPrefs.GetInt("EnableSound") == 1)
                {
                    AudioManager.PlaySoundEffect(item.itemType);
                }

                switch (item.itemType)
                {
                    case ItemSet.Coin_1:
                        gameManager.UpdateScore(1);
                        break;
                }

                item.handleDestory();
            }
            if (queue.ToArray().Length - 1 == i && item.collected)
            {
                ItemManager.Instance?.ItemQueue.RemoveAt(i);
            }
        }
    }

    void CheckCollidation()
    {
        var transform = GetComponent<RectTransform>();
        var collider = GetComponent<BoxCollider2D>();

        ballonBounds = new Bounds(collider.bounds.center * 2, collider.bounds.size * 2);

        //Debug.Log($"Ballon [center: {ballonBounds.center} size: {ballonBounds.size}]");

        List<ItemManager.Item> queue = ItemManager.Instance?.SpitesQueue.Where(i => i.self != null).ToList();

        queue.ForEach(spite =>
        {
            var spiteBounds = spite.CreateBounds();
            var actualSpiteBounds = new Bounds(spiteBounds.center * 2, spiteBounds.size);
            //Debug.Log($"Spite{spite} [center: {spiteBounds.center} size: {spiteBounds.size}]");
            if (ballonBounds.Intersects(actualSpiteBounds))
            {
                Debug.Log("Colided");
                AudioManager.StopBackgroundMusic();
                gameManager.HandleDeath();
                gameManager.ResetGame();
                ItemManager.Instance.SpitesQueue.Clear();
                ItemManager.Instance.ItemQueue.Clear();
                SceneManager.LoadScene("Main");
            }
        });
    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchEndPosition.y;

                Debug.Log(touchEndPosition.y);

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    // direction = "Tapped";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y) && touchEndPosition.y < safeAreaHeight)
                {
                    Vector3 v = transform.localPosition;

                    //Debug.Log($"Touch: {touchEndPosition.x}");
                    //Debug.Log($"Screen: {Screen.width}");
                    //Debug.Log($"Ballon: {transform.localPosition.x}");
                    if (touchEndPosition.x <= Screen.width - 10 && touchEndPosition.x >= 10)
                    {
                        transform.localPosition = new Vector3((touchEndPosition.x - (Screen.width / 2)) * 2, v.y, v.z);
                    }
                }
            }
        }
    }
}
