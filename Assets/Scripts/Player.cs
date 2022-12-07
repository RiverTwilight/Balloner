using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;

    public Text currentScore;

    public float speed = 2f;
    public float speed_middlm = 2.5f;
    public float speed_fast = 3f;

    public GameObject ballon;
    public Bounds ballonBounds;
    public ItemManager itemManager;

    async void Start()
    {
        var rect = GetComponent<RectTransform>().rect;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckCollidation();
    }

    void CheckCollidation()
    {
        var rect = GetComponent<RectTransform>().rect;

        //ballonBounds = new Bounds(
        //    transform.position,
        //    rect.size
        //); 

        ballonBounds = GetComponent<BoxCollider2D>().bounds;

        Debug.Log($"Ballon [center: {ballonBounds.center} size: {ballonBounds.size}]");

        ItemManager.Instance?.SpitesQueue.ForEach(spite =>
        {
            if (spite == null) { Debug.Log("ASDFASDF"); return; }
            var spiteBounds = spite.CreateBounds();
            Debug.Log($"Spite{spite} [center: {spiteBounds.center} size: {spiteBounds.size}]");
            if (spiteBounds.Intersects(ballonBounds))
            {
                Debug.Log("Fxxxxk");
                //ItemManager.Instance.SpitesQueue.Clear();
                //SceneManager.LoadScene("SampleScene");
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

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    // direction = "Tapped";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    Vector3 v = transform.localPosition;

                    //Debug.Log($"Touch: {touchEndPosition.x}");
                    //Debug.Log($"Screen: {Screen.width}");
                    //Debug.Log($"Ballon: {transform.localPosition.x}");
                    if (touchEndPosition.x <= Screen.width - 50 && touchEndPosition.x >= 50)
                    {
                        transform.localPosition = new Vector3((touchEndPosition.x - (Screen.width / 2)) * 2, v.y, v.z);
                    }
                    else
                    {
                        if (touchEndPosition.x > Screen.width - 100)
                        {
                            transform.localPosition = new Vector3((Screen.width / 2 - 50) * 2, v.y, v.z);
                        }
                        else
                        {
                            transform.localPosition = new Vector3((-(Screen.width / 2) + 50) * 2, v.y, v.z);

                        }
                    }
                }
            }
        }
    }
}
