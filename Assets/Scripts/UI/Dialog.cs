using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    private bool show = false;
    public Animator dialogAnimator;
    [HideLabel] public UnityEvent<string, bool> onDialogOpend;

    public GameObject GameStarter;

    void Start()
    {

    }

    public void OpenDialog()
    {
        show = true;
        GameStarter.SetActive(false);
        dialogAnimator.Play("OpenDialog", 0);
    }

    public void CloseDialog() {
        Debug.Log("Close");
        show = false;
        GameStarter.SetActive(true);
        dialogAnimator.Play("CloseDialog", 0);
    }

    [Button]
    public void ToggleDialog()
    {
        if (show)
        {
            CloseDialog();
        }
        else
        {
            OpenDialog();
        }
    }

    void Update()
    {
        if (show && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);

            CloseDialog();
        }
    }
}