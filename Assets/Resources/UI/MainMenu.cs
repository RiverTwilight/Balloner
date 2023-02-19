using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private bool show = false;
    public Animator dialogAnimator;

    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void openDialog()
    {
        show = true;
    }

    public void toggleDialog()
    {
        show = !show;

        Debug.Log(show);

        if (show)
        {
            dialogAnimator.Play("OpenDialog", 0);
        }
        else
        {
            dialogAnimator.Play("CloseDialog", 0);
        }
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