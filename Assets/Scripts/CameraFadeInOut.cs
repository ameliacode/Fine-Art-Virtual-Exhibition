using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeInOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fadeOutAni(0.0f);
    }

    void OnUpdateOpacity(float nextValue)
    {
        if (gameObject.transform.GetComponent<Image>() != null)
        {
            Image TargetImage = this.gameObject.GetComponent<Image>();
            TargetImage.color = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, nextValue);
        }
    }

    public void fadeOutAni(float delay)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "delay", delay, "easetype", iTween.EaseType.easeInOutCubic, "onupdate", "OnUpdateOpacity", "time", 1.0f, "oncomplete", "fadeAniComplete"));
    }


    public void fadeInAni(float delay)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0.0f, "to", 1.0f, "delay", delay, "easetype", iTween.EaseType.easeInOutCubic, "onupdate", "OnUpdateOpacity", "time", 1.0f, "oncomplete", "fadeAniComplete"));
    }

    float CurrentOpacity()
    {
        if (gameObject.transform.GetComponent<Image>() != null)
            return gameObject.transform.GetComponent<Image>().color.a;
        else
            return 0.0f;

    }

    void fadeAniComplete()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
