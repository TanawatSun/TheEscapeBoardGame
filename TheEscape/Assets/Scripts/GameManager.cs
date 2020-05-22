using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Board m_board;
    PlayerManager m_playerManager;

    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;

    bool m_hasLevelStarted = false;
    public bool HasLevelStarted { get { return m_hasLevelStarted; }set { m_hasLevelStarted = value; } }
    bool m_isGamePlaying = false;
    public bool IsGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }
    bool m_isGameOver = false;
    public bool IsGameOver { get { return m_isGameOver; } set { m_isGameOver = value; } }
    bool m_hasLevelFinihsed = false;
    public bool HasLevelFinihsed { get { return m_hasLevelFinihsed; } set { m_hasLevelFinihsed = value; } }

    [SerializeField] float delay;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_playerManager = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
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
        m_playerManager.playerInput.InputEnable = true;
        if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }
        while (!m_isGameOver)
        {
            yield return null;
        }

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
}
