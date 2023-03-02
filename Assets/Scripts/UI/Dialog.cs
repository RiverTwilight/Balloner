using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    private bool show = false;
    public Animator dialogAnimator;
    [HideLabel] public UnityEvent<string, bool> onDialogOpend;


    void Start()
    {

    }

    public void openDialog()
    {
        show = true;
    }

    [Button]
    public void toggleDialog()
    {
        show = !show;

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