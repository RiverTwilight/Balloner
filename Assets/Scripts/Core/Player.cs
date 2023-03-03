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
    public RectTransform ballonTransform;
    public Vector2 ballonSize;
    public Animator shadowAnim;
    public float safeAreaHeight;

    [Title("Self Component")]
    private BoxCollider2D boxColider;
    private CapsuleCollider2D capsuleColider;
    private RectTransform rectTransform;

    void Awake()
    {
        boxColider = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
        capsuleColider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 315, 0);

        Animator ballonerAnim = gameObject.GetComponent<Animator>();
        ballonerAnim.Play("Floating");
        shadowAnim.Play("ShadowFloating");

        ballonSize = rectTransform.rect.size;

        safeAreaHeight = Screen.height * 0.9f;
    }

    void Update()
    {
        switch (GameManager.Instance.gameStatus)
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
        ballonBounds = new Bounds(boxColider.bounds.center, boxColider.bounds.size);

        List<BoundedItem> queue = ItemManager.Instance?.ItemQueue.Where(i => i.self != null).ToList();
        int queueLength = queue.ToArray().Length;

        for (int i = 0; i < queueLength; i++)
        {
            var item = queue[i];
            var itemBound = item.CreateBounds();
            if (ballonBounds.Intersects(itemBound))
            {
                if (PlayerPrefs.GetInt("EnableSound") == 1)
                {
                    AudioManager.PlaySoundEffect(item.itemType);
                }

                switch (item.itemType)
                {
                    case ItemSet.Coin_1:
                        GameManager.Instance.UpdateScore(1);
                        break;
                    case ItemSet.Coin_10:
                        GameManager.Instance.UpdateScore(10);
                        break;
                    case ItemSet.Magnent:
                        // TODO magnet effect
                        Debug.Log("Get Magnet");
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
        ballonBounds = new Bounds(boxColider.bounds.center, boxColider.bounds.size);

        //Debug.Log($"Ballon [center: {ballonBounds.center} size: {ballonBounds.size}]");

        List<BoundedItem> queue = ItemManager.Instance?.SpitesQueue.Where(i => i.self != null).ToList();

        queue.ForEach(spite =>
        {
            var spiteBounds = spite.CreateBounds();
            // Debug.Log($"Ballon Position: {boxColider.bounds.center}");
            // Debug.Log($"[center: {spiteBounds.center} size: {spiteBounds.size}]");
            if (ballonBounds.Intersects(spiteBounds))
            {
                Debug.Log("Colided");
                AudioManager.StopBackgroundMusic();
                GameManager.Instance.HandleDeath();
                GameManager.Instance.ResetGame();
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
        else
        {
            float moveSpeed = 1900f * GameManager.Instance.speedIndex;
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 currentPosition = transform.position;
            currentPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
            currentPosition.x = Mathf.Clamp(currentPosition.x, 10, Screen.width - 10);
            transform.position = currentPosition;
        }
    }
}
