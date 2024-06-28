
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static bool isPlayerEnabled;
    [SerializeField] private Texture2D cursorTexture;

    public bool isPlayerAlive;
    public bool isBossAlive;

    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(cursorTexture, new Vector2(16f,16f), CursorMode.ForceSoftware);
    }
    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
        Health.OnBossDeath += HandleOnBossDeath;
        isPlayerEnabled = true;
        isPlayerAlive = true;
        isBossAlive = true;
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void HandleOnPlayerDeath()
    {
        if (isPlayerAlive) 
        { 
            isPlayerAlive = false;
            UIManager.Instance.ShowDefeatScreen();
        }
        
    }
    private void HandleOnBossDeath()
    {
        if (isBossAlive) 
        { 
            isBossAlive = false;
            UIManager.Instance.ShowVictoryScreen();
        }
        
    }

}
    

