using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public CanvasGroup gameCanvasGroup;

    public GameObject[] panels;

    private int _currentLevel;
    private float _fadeInDuration = 1.5f;
    private float _fadeOutDuration = 3.0f;

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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartMiniGame(GameType.PuzzleGame);
        }
    }

    public void StartMiniGame(GameType miniGame)
    {
        _backgroundObject.SetActive(true);
        FadeManager.Instance.FadeIn();
        StartCoroutine(ShowPanel(miniGame));
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
        //gameCanvasGroup.alpha = 1;
        //gameCanvasGroup.interactable = true;
        //gameCanvasGroup.blocksRaycasts = true;
        EnableCanvasGroup(gameCanvasGroup, true);


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

    public void OnGameCompleteBtnClick()
    {
        //Fade back to the normal view
        MakeSomeDelay(1.5f);
        FadeBack();
    }

    private void FadeBack()
    {
        FadeManager.Instance.FadeIn();

        StartCoroutine(HidePanels());
    }

    private void EnableCanvasGroup(CanvasGroup group, bool enable)
    {
        group.interactable = enable;
        group.blocksRaycasts = enable;
        group.alpha = (enable) ? 1 : 0;
    }
    public void OnCLoseBtnClick()
    {
        GameManager.Instance.FadeBack();
        print("OnCLoseBtnClick");
        _backgroundObject.SetActive(false);
    }

    private IEnumerator MakeSomeDelay(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator HidePanels()
    {
        yield return new WaitForSeconds(_fadeInDuration);
        EnableCanvasGroup(gameCanvasGroup, false);

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