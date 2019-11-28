using System.Collections;
using UnityEngine;

public class Location_pin : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 currentPosition;
    float runningTime;
    float direction;
    float width;
    float velocity;
    void Start()
    {
        currentPosition = transform.position;
        width = 0.02f;
        velocity = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        direction = width *  Mathf.Sin(runningTime * velocity);
       this.transform.position = new Vector3(currentPosition.x, currentPosition.y + direction , currentPosition.z);
    }
}
