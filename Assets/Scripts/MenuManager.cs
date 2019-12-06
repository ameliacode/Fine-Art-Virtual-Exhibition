using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    private GameManager gameManager;
    public GameObject Player;
    public GameObject fadeCanvas;
    public Transform[] Cameras;
    public override void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameStateChange += OnGameStateChangeHandler;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGameStateChangeHandler()
    {
        //Debug.Log ("Current Game State " + gameManager.GetCurrentGameState());

        switch (gameManager.GetCurrentGameState())
        {
            case GameManager.GameState.LIGHT_EMPIRE:
                cameraSet(0);
                Player.GetComponent<VRAutowalk>().enabled = true;
                SceneManager.LoadScene("01.FAVE");
                break;
            case GameManager.GameState.LISTENING_ROOM:
                cameraSet(1);
                Player.GetComponent<VRAutowalk>().enabled = false;
                SceneManager.LoadScene("02.LISTENING_ROOM");
                break;
            case GameManager.GameState.PERSONAL_VALUE:
                cameraSet(2);
                Player.GetComponent<VRAutowalk>().enabled = false;
                SceneManager.LoadScene("03.PERSONAL_VALUE");
                break;
            case GameManager.GameState.LIGHT_EMPIRE_MAN:
                cameraSet(3);
                Player.GetComponent<VRAutowalk>().enabled = false;
                SceneManager.LoadScene("04.LIGHT_EMPIRE_MAN");
                break;
            case GameManager.GameState.PYRENEE_CATSLE:
                cameraSet(4);
                Player.GetComponent<VRAutowalk>().enabled = false;
                SceneManager.LoadScene("05.PYRENEES_CASTLE");
                break;
            case GameManager.GameState.ENDING:
                Player.GetComponent<VRAutowalk>().enabled = false;
                SceneManager.LoadScene("06.ENDING");
                Invoke("restart", 20.0f);
                break;
            default:
                break;

        }

        this.DestoryPreviousMenu();
    }

    public void cameraSet(int n)
    {
        if (fadeCanvas != null)
            Instantiate(fadeCanvas.gameObject, this.transform);
        Player.transform.position = new Vector3(Cameras[n].position.x, Cameras[n].position.y, Cameras[n].position.z);
    }

    IEnumerator GameInit(string load)
    {
        yield return Instantiate(Resources.Load(load));
        this.DestoryPreviousMenu();
    }

    public void DestoryPreviousMenu()
    {
        GameObject menuObject = null;

        switch (gameManager.GetPreviousGameState())
        {
            case GameManager.GameState.LIGHT_EMPIRE:
                //    menuObject = GameObject.FindGameObjectWithTag("Intro");
                break;
            default:
                //Debug.Log("Previous Game State : " + gameManager.GetPreviousGameState());
                break;
        }

        if (menuObject != null)
        {
            Destroy(menuObject);
            Resources.UnloadUnusedAssets();
        }
    }

    public void restart()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.ChangeState(GameManager.GameState.LIGHT_EMPIRE, GameManager.SoundState.LIGHT_EMPIRE);
    }
}