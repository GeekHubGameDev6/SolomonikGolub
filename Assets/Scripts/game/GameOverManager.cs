using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public static GameOverManager Instance { get; set; }
    [SerializeField]
    private GameObject _gameoverPanel;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);

        if (_gameoverPanel.activeInHierarchy)
            _gameoverPanel.SetActive(false);
    }

    public void ShowGameoverPanel(bool win, float delayBeforePanelShows = 1.25f)
    {
        if (win)
        {
            StartCoroutine(ShowPanel(delayBeforePanelShows));
        }
    }

    private IEnumerator ShowPanel(float time)
    {
        yield return new WaitForSeconds(time);
        _gameoverPanel.SetActive(true);
    }
}
