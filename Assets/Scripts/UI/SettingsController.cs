using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public Animator logoAnimator;
    public Animator panelAnimator;
    private bool isSettingOpen;

    //void Start()
    //{

    //}

    //void Update()
    //{
    //}

    public void TogglePanel() {
        if (isSettingOpen) {
            ShowLogo();
            HidePanel();
        }
        else {
            HideLogo();
            ShowPanel();
        }
        isSettingOpen = !isSettingOpen;
    }

    public void HidePanel() {
        panelAnimator.Play("SlideOutToLeft");
    }

    public void ShowPanel() {
        panelAnimator.Play("SlideInFromLeft");
    }

    public void HideLogo()
    {
        logoAnimator.Play("SlideOutToRight"); // Starts the animation from the beginning
    }

    public void ShowLogo()
    {

        logoAnimator.Play("SlideInFromRight"); // Starts the animation from the end
    }
}
