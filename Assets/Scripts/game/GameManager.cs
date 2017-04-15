using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public CanvasGroup gameCanvasGroup;
    public CanvasGroup inventoryCanvasGroup;

    public GameObject[] panels;

    private int _currentLevel;
    private float _fadeInDuration = 0.5f;
    private float _fadeOutDuration = 1f;

    private int _memoryGameCards = 6;

    public int MemoryGameCards
    {
        get { return _memoryGameCards; }
        set { _memoryGameCards = value; }
    }

    /// <summary>
    /// NumberObject
    /// </summary>
    public GameObject numberObject;

    [SerializeField]
    private GameObject _backgroundObject;

    private GameState _gameState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);
    }
    private void Start()
    {
        _gameState = GameState.WaitingForGame;
        _backgroundObject.SetActive(false);
        EnableCanvasGroup(gameCanvasGroup, false);
        EnableCanvasGroup(inventoryCanvasGroup, true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // GameManager.Instance.StartMiniGame(GameType.PuzzleGame);
            StartMiniGame(GameType.MemoryGame);
        }
    }

    public void StartMiniGame(GameType miniGame)
    {
        if (_gameState == GameState.Playing)
        {
            print("doubleStart");
            return;
        }
        FadeManager.Instance.FadeIn();

        EnableFPSController(false);
        StartCoroutine(ShowPanel(miniGame));
    }
    /// <summary>
    /// starts selected minigame and takes number gameObjet as a parameter
    /// </summary>
    /// <param name="miniGame"></param>
    /// <param name="numberGameObject"></param>
    public void StartMiniGame(GameType miniGame, GameObject numberGameObject)
    {
        if (_gameState == GameState.Playing)
            return;
        numberObject = numberGameObject;
        FadeManager.Instance.FadeIn();

        EnableFPSController(false);
        StartCoroutine(ShowPanel(miniGame));
    }


    /// <summary>
    /// pass true if enable, false to disable
    /// </summary>
    /// <param name="on"></param>
    private void EnableFPSController(bool on)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = on;
        //      GameObject[] tmans=GameObject.FindGameObjectsWithTag("TouchManager");
        //      foreach (GameObject tman in tmans)
        //      {
        //          tman.SetActive(on);
        //      }
    }



    private IEnumerator ShowPanel(GameType gType)
    {
        yield return new WaitForSeconds(_fadeInDuration);
        _backgroundObject.SetActive(true);
        //gameCanvasGroup.alpha = 1;
        //gameCanvasGroup.interactable = true;
        //gameCanvasGroup.blocksRaycasts = true;
        EnableFPSController(true);
        EnableCanvasGroup(gameCanvasGroup, true);
        EnableCanvasGroup(inventoryCanvasGroup, false);
        _gameState = GameState.Playing;

        switch (gType)
        {
            case GameType.PuzzleGame:
                print("puzzleGame goes here");
                Puzzle.PuzzleGame.Instance.SetupPanels();
                Puzzle.PuzzleGame.Instance.StartGame();
                //_puzzleManager.puzzleComplete += OpenDoor;
                break;
            case GameType.MemoryGame:
                print("memory goes here");
                MemoryGame.MemoryGameManager.Instance.InstantiateCards(_memoryGameCards);
                //_memGameManager.InstantiateCards(4);
                break;
            case GameType.MatchPicture:
                print("match picture goes here");
                panels[2].SetActive(true);
                //_memGameManager.InstantiateCards(4);
                break;
            default:
                break;
        }
    }
    
    private void FadeBack()
    {
        StartCoroutine(HidePanels());
        FadeManager.Instance.FadeIn(0.5f, 0.75f);
    }

    private void EnableCanvasGroup(CanvasGroup group, bool enable)
    {
        group.interactable = enable;
        group.blocksRaycasts = enable;
        group.alpha = (enable) ? 1 : 0;
    }
    public void OnGameFinished(bool win = true)
    {        
        if (win)
        {
            GameOverManager.Instance.ShowGameoverPanel(win);
            if (numberObject != null)
            {
                numberObject.SetActive(true);
                print("other object events goes Here");
            }
            else
            {
                print("number GameObject is NULL");
            }
        }
        else
        {
            print("game lost or was closed");
            numberObject = null;
        }
    }
    public void OnCLoseBtnClick()
    {
        //Fade back to the normal view
        FadeBack();
        print("OnCLoseBtnClick");

    }


    private IEnumerator MakeSomeDelay(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator HidePanels()
    {
        yield return new WaitForSeconds(_fadeInDuration);
        EnableCanvasGroup(gameCanvasGroup, false);
        EnableCanvasGroup(inventoryCanvasGroup, true);
        _backgroundObject.SetActive(false);
        EnableFPSController(true);

        foreach (var item in panels)
        {
            item.SetActive(false);
            
        }
        _gameState = GameState.WaitingForGame;


    }
}

interface IPooler
{
    PuzzleSelection GetPooledObject();
    void DeactivateObjects();
}

public enum GameType
{
    PuzzleGame = 0,
    MemoryGame,
    MatchPicture
}

public enum GameState
{
    WaitingForGame = 0,
    Playing
}