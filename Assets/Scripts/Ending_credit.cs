using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_credit : MonoBehaviour
{
    Vector3 currentPosition;
    float runningTime;
    float direction;
    float width;
    float velocity;
    void Start()
    {
        currentPosition = transform.position;
        width = 0.02f;
        velocity = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        direction = width * runningTime * velocity;
        this.transform.position = new Vector3(currentPosition.x, currentPosition.y + direction, currentPosition.z);
    }
}
