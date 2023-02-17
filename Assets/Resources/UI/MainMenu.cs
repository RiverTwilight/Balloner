using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private bool show = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void openDialog()
    {
        show = true;

        gameObject.SetActive(true);
    }

    public void toggleDialog()
    {
        show = !show;

        gameObject.SetActive(show);
    }

    void Update()
    {
        if (show && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);

            show = false;
        }
    }
}