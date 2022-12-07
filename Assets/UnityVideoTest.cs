using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UnityVideoTest : MonoBehaviour
{
    public VideoPlayer vPlayer;
    public bool debugFrameCount;
    

    public void clickPause(){
        vPlayer.Pause();
    }

    public void clickPlay(){
        vPlayer.Play();
    }

    void EndReached(VideoPlayer videoPlayer){
        Debug.Log("End reached!");
    }

    private void Start() {
        vPlayer.loopPointReached += EndReached;
    }

    private void Update() {
        if(debugFrameCount){
            Debug.Log("FrameTime "+vPlayer.frame);
        }
    }
}
