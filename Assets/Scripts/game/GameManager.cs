using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public CanvasGroup gameCanvasGroup;
    public CanvasGroup slotCanvasGroup;

    public GameObject[] panels;

    private int _currentLevel;
    private float _fadeInDuration = 1.5f;
    private float _fadeOutDuration = 3.0f;

    [SerializeField]
    Puzzle.PuzzleGame _puzzleManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        EnableCanvasGroup(gameCanvasGroup, false);
        EnableCanvasGroup(slotCanvasGroup, true);
    }

    /// <summary>
    /// Starts selected minigame
    /// </summary>
    /// <param name="miniGame"></param>
    public void StartMiniGame(GameType miniGame)
    {
        FadeManager.Instance.FadeIn();
        StartCoroutine(ShowPanel(miniGame));
    }

    private IEnumerator ShowPanel(GameType gType)
    {
        yield return new WaitForSeconds(_fadeInDuration);
        //gameCanvasGroup.alpha = 1;
        //gameCanvasGroup.interactable = true;
        //gameCanvasGroup.blocksRaycasts = true;
        EnableCanvasGroup(gameCanvasGroup, true);
        EnableCanvasGroup(slotCanvasGroup, false);

        switch (gType)
        {
            case GameType.PuzzleGame:
                print("puzzleGame goes here");
                _puzzleManager.SetupPanels();
                _puzzleManager.StartGame();
                //_puzzleManager.puzzleComplete += OpenDoor;
                break;
            case GameType.MemoryGame:
                print("memory goes here");
                break;
            case GameType.GuessWordGame:
                print("guessWord goes here");
                break;
            default:
                break;
        }
    }
    private IEnumerator HidePanels()
    {
        yield return new WaitForSeconds(_fadeInDuration);
        EnableCanvasGroup(gameCanvasGroup, false);
        EnableCanvasGroup(slotCanvasGroup, true);
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }

    public void OnGameCompleteBtnClick()
    {
        //Fade back to the normal view
        FadeBack();
    }


    //  Call this when user want to interupt his game
    public void FadeBack()
    {
        FadeManager.Instance.FadeIn();
        
        StartCoroutine(HidePanels());
        //deactivateGameCanvas
        //setPlayerActive
        //Activate player touchInputModel
    }

    private void EnableCanvasGroup(CanvasGroup group, bool enable)
    {
        group.interactable = enable;
        group.blocksRaycasts = enable;
        group.alpha = (enable) ? 1 : 0;
    }
}

public enum GameType
{
    PuzzleGame = 0,
    MemoryGame,
    GuessWordGame
}
