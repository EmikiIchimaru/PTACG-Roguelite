
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static bool isGamePlaying;
    public static bool isPlayerControlEnabled;
    public static bool isPlayerMovementEnabled;
    public static bool isCheatingAllowed;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Camera2D camera2D;

    public Character playerCharacter;
    public CharacterStats stats;

    public bool isPlayerAlive;
    public bool isBossAlive;
    private int bossCountdown;
    private int cheatCounter;

    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(cursorTexture, new Vector2(16f,16f), CursorMode.ForceSoftware);
    }

    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
        Health.OnBossDeath += HandleOnBossDeath;
        isGamePlaying = true;
        isPlayerControlEnabled = true;
        isPlayerMovementEnabled = true;
        isPlayerAlive = true;
        isBossAlive = true;
        isCheatingAllowed = false;
        cheatCounter = 0;
        bossCountdown = Random.Range(2,5);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isCheatingAllowed)
        {
            cheatCounter++;
            if (cheatCounter > 5) { isCheatingAllowed = true; }
        }

        if (Input.GetKeyDown(KeyCode.O) && isCheatingAllowed)
        {
            bossCountdown = -999;
            LevelManager.Instance.InitializeBossRoom();
        }
    }

    public void BossCountDown()
    {
        if (stats.level >= CharacterStats.maxLevel) { bossCountdown--; }
        Debug.Log(bossCountdown.ToString());
        if (bossCountdown == 0) { LevelManager.Instance.InitializeBossRoom(); }
    }

    public void CameraBossRoom(Transform target)
    {
        //isPlayerControlEnabled = false;
        //isPlayerMovementEnabled = false;
        
        camera2D.StartBossPanSequence(target);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void HandleOnPlayerDeath()
    {
        if (isPlayerAlive && isGamePlaying) 
        { 
            isPlayerAlive = false;
            isGamePlaying = false;
            UIManager.Instance.ShowDefeatScreen();
        }
        
    }
    private void HandleOnBossDeath()
    {
        if (isBossAlive && isGamePlaying) 
        { 
            isBossAlive = false;
            isGamePlaying = false;
            while (BossBehaviour.spawns.Count > 0)
            {
                GameObject tempvar = BossBehaviour.spawns[0];
                Destroy(tempvar);
                BossBehaviour.spawns.RemoveAt(0);
            }
            
            UIManager.Instance.ShowVictoryScreen();
        }
        
    }

}
    

