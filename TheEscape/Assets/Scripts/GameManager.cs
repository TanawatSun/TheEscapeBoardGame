using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public enum Turn{

    Player,
    Enemy
}

public class GameManager : MonoBehaviour
{
    Board m_board;
    PlayerManager m_playerManager;
    EnemyMovement m_enemy;

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    public UnityEvent loseLevelEvent;

    List<EnemyManager> m_enemies;
    Turn m_currenturn = Turn.Player;

    public Turn Currenturn { get { return m_currenturn; } }


    bool m_hasLevelStarted = false;
    public bool HasLevelStarted { get { return m_hasLevelStarted; }set { m_hasLevelStarted = value; } }
    bool m_isGamePlaying = false;
    public bool IsGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }
    bool m_isGameOver = false;
    public bool IsGameOver { get { return m_isGameOver; } set { m_isGameOver = value; } }
    bool m_hasLevelFinihsed = false;
    public bool HasLevelFinihsed { get { return m_hasLevelFinihsed; } set { m_hasLevelFinihsed = value; } }

    [SerializeField] float delay;
    [SerializeField] float playerMoveDelay;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_playerManager = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        m_enemy = Object.FindObjectOfType<EnemyMovement>().GetComponent<EnemyMovement>();
        EnemyManager[] enemy = GameObject.FindObjectsOfType<EnemyManager>() as EnemyManager[];
        m_enemies = enemy.ToList();  
    }
    private void Start()
    {
        if (m_playerManager != null && m_board != null)
        {
            StartCoroutine("RunGameLoop");
        }
    }

    IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

    IEnumerator StartLevelRoutine()
    {
        if (setupEvent != null)
        {
            setupEvent.Invoke();
        }
        m_playerManager.playerInput.InputEnable = false;
        while (!m_hasLevelStarted)
        {
            yield return null;
        }
        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }

    }
    IEnumerator PlayLevelRoutine()
    {
        m_isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        
        if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }
        yield return new WaitForSeconds(playerMoveDelay);
        m_playerManager.playerInput.InputEnable = true;
        while (!m_isGameOver)
        {
            yield return null;
            m_isGameOver = IsWin();  
            
        }
        Debug.Log("Win");
    }

    public void LoseLevel()
    {
        StartCoroutine(LoseLevelRoutine());
    }

    IEnumerator LoseLevelRoutine()
    {
        m_isGameOver = true;
        yield return new WaitForSeconds(2f);
        if (loseLevelEvent != null)
        {
            loseLevelEvent.Invoke();
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EndLevelRoutine()
    {
        m_playerManager.playerInput.InputEnable = false;
        if (endLevelEvent != null)
        {
            endLevelEvent.Invoke();
        }
        while (!m_hasLevelFinihsed)
        {
            yield return null;
        }
        // or load next level
        ReStartLevel();
    }

    void ReStartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel()
    {
        m_hasLevelStarted = true;
    }

    bool IsWin()
    {
        if (m_board.PlayerNode != null)
        {
            return (m_board.PlayerNode == m_board.GoalNode);
        }
        return false;
    }

    public void UpdateTurn()
    {
        if (m_currenturn == Turn.Player && m_playerManager != null)
        {
            if (m_playerManager.IsTurnComplete)
            {
                PlayEnemyTurn();
            }
        }
        else if(m_currenturn == Turn.Enemy)
        {
            if (IsEnemyTurnComplete())
            {
                PlayPlayerTurn();
            }

        }
    }

    void PlayPlayerTurn()
    {
        m_currenturn = Turn.Player;
        m_playerManager.IsTurnComplete = false;
    }
    void PlayEnemyTurn()
    {
        m_currenturn = Turn.Enemy;
        foreach (EnemyManager enemy in m_enemies)
        {
            if (enemy!=null)
            {
                enemy.IsTurnComplete = false;
                enemy.PlayTurn();
            }
        }
    }

    bool IsEnemyTurnComplete()
    {
        foreach(EnemyManager enemy in m_enemies)
        {
            if (!enemy.IsTurnComplete)
            {
                return false;
            }
        }
        return true;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level-2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Level-1");
    }
}
