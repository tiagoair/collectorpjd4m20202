using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Fields

    public static GameManager Instance;

    public bool IsGameOver
    {
        get => _isGameOver;
        set => _isGameOver = value;
    }

    public bool GameStarted => _gameStarted;

    #endregion

    #region Private Fields

    private bool _isGameOver;

    private bool _gameStarted;

    #endregion
    
    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckGameOver();
    }

    #endregion

    #region Private Methods

    private void CheckGameOver()
    {
        if (_isGameOver && _gameStarted)
        {
            _gameStarted = false;
            Time.timeScale = 0f;
            Observer.OnEnableDeathText();
        }
    }

    #endregion

    #region Public Methods

    public void ResetGame()
    {
        if (_isGameOver)
        {
            _isGameOver = false;
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        _gameStarted = true;
    }

    #endregion
}
