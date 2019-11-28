using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    private GameManager gameManager;

    public override void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameStateChange += OnGameStateChangeHandler;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGameStateChangeHandler()
    {
        //Debug.Log ("Current Game State " + gameManager.GetCurrentGameState());

        switch (gameManager.GetCurrentGameState())
        {
            case GameManager.GameState.LIGHT_EMPIRE:
                SceneManager.LoadScene("FAVE");
                break;
            case GameManager.GameState.LIGHT_EMPIRE_MAN:
                SceneManager.LoadScene("LIGHT_EMPIRE_MAN");
                break;
            case GameManager.GameState.LISTENING_ROOM:
                SceneManager.LoadScene("LISTENING_ROOM");
                break;
            case GameManager.GameState.PERSONAL_VALUE:
                SceneManager.LoadScene("PERSONAL_VALUE");
                break;
            case GameManager.GameState.PYRENEE_CATSLE:
                SceneManager.LoadScene("PYRENEE_CATSLE");
                break;
            default:
                break;

        }

        this.DestoryPreviousMenu();
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
}