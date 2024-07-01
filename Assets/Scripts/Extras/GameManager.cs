
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static bool isPlayerControlEnabled;
    public static bool isPlayerMovementEnabled;
    [SerializeField] private Texture2D cursorTexture;

    public Character playerCharacter;
    public CharacterStats stats;

    public bool isPlayerAlive;
    public bool isBossAlive;
    private int bossCountdown;

    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(cursorTexture, new Vector2(16f,16f), CursorMode.ForceSoftware);
    }
    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
        Health.OnBossDeath += HandleOnBossDeath;
        isPlayerControlEnabled = true;
        isPlayerMovementEnabled = true;
        isPlayerAlive = true;
        isBossAlive = true;
        bossCountdown = Random.Range(2,5);
    }

    public void BossCountDown()
    {
        if (stats.level >= CharacterStats.maxLevel) { bossCountdown--; }
        Debug.Log(bossCountdown.ToString());
        if (bossCountdown == 0) { LevelManager.Instance.InitializeBossRoom(); }
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
    

