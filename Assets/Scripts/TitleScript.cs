using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{

    private void Awake()
    {
        if (PlayerPrefs.HasKey("title"))
            this.gameObject.SetActive(false);
        else
            PlayerPrefs.SetString("title", "true");
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("hide", 2.0f);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
