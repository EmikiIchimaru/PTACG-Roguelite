
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Texture2D cursorTexture;
    protected override void Awake()
    {
        base.Awake();
        Cursor.SetCursor(cursorTexture, new Vector2(16f,16f), CursorMode.ForceSoftware);
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
    

