
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private Texture2D cursorTexture;

    public bool isPlayerAlive;

    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(cursorTexture, new Vector2(16f,16f), CursorMode.ForceSoftware);
    }
    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
        isPlayerAlive = true;
        
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

}
    

