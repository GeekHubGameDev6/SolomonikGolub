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

    [SerializeField]
    private GameObject _backgroundObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);
    }
    private void Start()
    {
        _backgroundObject.SetActive(false);
        EnableCanvasGroup(gameCanvasGroup, false);
        EnableCanvasGroup(inventoryCanvasGroup, true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // GameManager.Instance.StartMiniGame(GameType.PuzzleGame);
            StartMiniGame(GameType.PuzzleGame);
        }
    }

    public void StartMiniGame(GameType miniGame)
    {
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

    internal void OnGameFinished(bool win = true)
    {
        if (win)
        {
            GameOverManager.Instance.ShowGameoverPanel(win);
        }
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
                //_memGameManager.InstantiateCards(4);
                break;
            case GameType.GuessWordGame:
                print("guessWord goes here");
                break;
            default:
                break;
        }
    }



    private void FadeBack()
    {
        StartCoroutine(HidePanels());
        FadeManager.Instance.FadeIn();
    }

    private void EnableCanvasGroup(CanvasGroup group, bool enable)
    {
        group.interactable = enable;
        group.blocksRaycasts = enable;
        group.alpha = (enable) ? 1 : 0;
    }

    public void OnGameCompleteBtnClick()
    {
        //Fade back to the normal view
        FadeBack();
        MakeSomeDelay(1.5f);
        

    }
    public void OnCLoseBtnClick()
    {
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
    GuessWordGame
}