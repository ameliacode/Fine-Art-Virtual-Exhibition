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
        direction = width * Mathf.Sin(runningTime * velocity);
        this.transform.position = new Vector3(currentPosition.x, currentPosition.y + direction, currentPosition.z);
    }

    public void goFAVE()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.LIGHT_EMPIRE, GameManager.SoundState.LIGHT_EMPIRE);
    }

    public void goListeningRoom()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.LISTENING_ROOM);
    }

    public void goPersonalValue()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.PERSONAL_VALUE);
    }

    public void goLightMan()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.LIGHT_EMPIRE_MAN);
    }

    public void goPyreneeCatsle()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.PYRENEE_CATSLE);
    }
}
