using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        LIGHT_EMPIRE,
        LIGHT_EMPIRE_MAN,
        PERSONAL_VALUE,
        LISTENING_ROOM,
        PYRENEE_CATSLE,
        NONE
    };

    public enum SoundState
    {
        TITLE,
        MOVE,
        DOOR,
        UNMUTESOUND,
        MUTESOUND,
        NONE
    };

    float deltaTime = 0.0f;
    bool isPaused = false;

    private GameState currentGameState = GameState.NONE;

    private GameState previousGameState = GameState.NONE;

    public GameState nextGameState = GameState.NONE;

    private SoundState currentSoundState = SoundState.NONE;

    public delegate void OnGameStateChangeHandler();
    public delegate void OnSoundStateChangeHandler();

    // Event to call when game/sound state changes.
    public event OnGameStateChangeHandler OnGameStateChange;
    public event OnSoundStateChangeHandler OnSoundStateChange;

#if UNITY_ANDROID

    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;
    AndroidJavaObject toast;

    void ShowToast(string message)
    {
        currentActivity.Call
        (
            "runOnUiThread",
            new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");

                AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);

                toast = Toast.CallStatic<AndroidJavaObject>
                (
                    "makeText",
                    context,
                    javaString,
                    Toast.GetStatic<int>("LENGTH_SHORT")
                );

                toast.Call("show");
            })
         );
    }

    public void CancelToast()
    {
        currentActivity.Call("runOnUiThread",
            new AndroidJavaRunnable(() =>
            {
                if (toast != null) toast.Call("cancel");
            }));
    }

#endif

    public override void Awake()
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
#endif

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ChangeState(GameState.LIGHT_EMPIRE, SoundState.TITLE);
    }

    void Update()
    {

#if UNITY_ANDROID
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                { // if game is not yet paused, ESC will pause it
                    ShowToast("'뒤로' 버튼을 한 번 더 누르면 종료됩니다.");
                    isPaused = true;
                    Invoke("PausedBack", 3.0f);
                }
                else
                { // if game is paused and ESC is pressed, it's the second press. QUIT
                    Application.Quit();
                }

            }
        }
#endif
    }

    void PausedBack()
    {
        isPaused = false;
    }
   
    public void ChangeState(GameState gameState)
    {
        this.SetPreviousGameState(currentGameState);
        this.SetCurrentGameState(gameState);
    }

    public void ChangeState(GameState gameState, SoundState soundState)
    {
        this.ChangeState(gameState);
        this.ChangeState(soundState);
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }

    public GameState GetPreviousGameState()
    {
        return previousGameState;
    }

    private void SetCurrentGameState(GameState gameState)
    {
        this.currentGameState = gameState;

        if (OnGameStateChange != null)
        {
            OnGameStateChange();
        }
    }

    private void SetPreviousGameState(GameState gameState)
    {
        this.previousGameState = gameState;
    }

    public void ChangeState(SoundState soundState)
    {
        this.SetSoundState(soundState);
    }

    public SoundState GetCurrentSoundState()
    {
        return currentSoundState;
    }

    private void SetSoundState(SoundState soundState)
    {
        this.currentSoundState = soundState;

        if (OnSoundStateChange != null)
        {
            OnSoundStateChange();
        }
    }

    public static void EnableColliders2D(GameObject go, bool state)
    {
        foreach (var collider in go.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = state;
        }
    }

    public static void EnableColliders(GameObject go, bool state)
    {
        foreach (var collider in go.GetComponentsInChildren<Collider>())
        {
            collider.enabled = state;
        }
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // print("pause");
            // we are in background
        }
        else
        {
            // print("resume");
            // we are in foreground again.
        }
    }
}
