using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    //public delegate void PuzzleGameComple();

    public class PuzzleGame : MonoBehaviour
    {
        public static PuzzleGame Instance { get; set; }

        public int openedDoors;
        private const int INIT_SIZE = 3;
        private PuzzleLogic logic;
        public int size;
        private Rect[,] puzzlesParts;
        [SerializeField]
        private PuzzleSelection[,] puzzles;

        public PuzzleSelection puzzlePrefab;

        public Texture[] puzzlePictures;                // Array of pictures        
        private Texture _puzzlePicture;                 // Current Selected Picture;

        // public Canvas Objects
        public RectTransform gamePanel;
        public RawImage tipImage;
        public GameObject gameOverPanel;

        public Texture PuzzlePicture
        {
            get
            {
                if (_puzzlePicture == null)
                    _puzzlePicture = GetPuzzlePicture();
                return _puzzlePicture;
            }
        }

        //Tips
        public float timeToShowTip;
        private bool showTip;
        public float currentTime = 0;

        public GameObject pooler;
        private IPooler _puzzlePooler;

        //public PuzzleGameComple puzzleComplete;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != null && Instance != this)
                Destroy(this.gameObject);

        }
        private void Start()
        {
            _puzzlePooler = pooler.GetComponent<IPooler>();
            openedDoors = 0;
            //puzzleComplete += GameOver;
            gameOverPanel.SetActive(false);
            gamePanel.gameObject.SetActive(false);
            gamePanel.sizeDelta = new Vector2(gamePanel.rect.height, gamePanel.sizeDelta.y);
            //Debug.Log(panel.rect.width + "panel scale X");
            //StartGame();
        }

        public void StartGame()
        {
            gamePanel.gameObject.SetActive(true);
            size = INIT_SIZE + openedDoors / INIT_SIZE;
            showTip = false;
            currentTime = 0;
            gameOverPanel.SetActive(false);
            tipImage.gameObject.SetActive(false);
            logic = new PuzzleLogic(size);

            InitPictures();

            logic.Start();

            for (int i = 0; i < size * size * 30; i++)
            {
                logic.ShiftRandom();
            }
            Refresh();
        }

        private void InitPictures()
        {
            if (_puzzlePicture == null)
                _puzzlePicture = GetPuzzlePicture();

            float w, h;
            w = gamePanel.rect.width / size;  // 512 P.width / size = 128
            h = gamePanel.rect.height / size;

            float offsetX, offsetY;
            offsetX = w / 2;    // 128/2 = 64
            offsetY = h / 2;

            puzzles = new PuzzleSelection[size, size];
            puzzlesParts = new Rect[size, size];

            //        4x4
            //  0.3|1.3|2.3|3.3
            //  0.2|1.2|2.2|3.2
            //  0.1|1.1|2.1|3.1
            //  0.0|1.0|2.0|3.0

            Vector2 offset;
            Vector2 m_scale = new Vector2(1.0f / size, 1.0f / size);


            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    PuzzleSelection newImg = _puzzlePooler.GetPooledObject();
                    newImg.gameObject.SetActive(true);  //Instantiate(puzzlePrefab);
                    newImg.transform.localPosition = new Vector2(x * w + offsetX, y * h + offsetY);

                    newImg.transform.SetParent(gamePanel, false);
                    newImg.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
                    newImg.transform.rotation = gamePanel.rotation;
                    newImg.name = (y * size + x).ToString();
                    newImg.GetComponent<Button>().onClick.AddListener(() => { this.OnClick(newImg.name); });

                    puzzles[x, y] = newImg;
                    offset = new Vector2(x * m_scale.x, y * m_scale.y);
                    puzzles[x, y].AssignImage(m_scale, offset);

                    puzzlesParts[x, y] = puzzles[x, y].PuzzlePart;

                    //images[x, y].CreatePuzzlePiece(size);
                }
            }
        }
        public void SetupPanels(RectTransform gamePanel, RawImage tipImage)
        {
            _puzzlePicture = GetPuzzlePicture();
            this.tipImage = tipImage;
            this.gamePanel = gamePanel;
            tipImage.texture = _puzzlePicture;

        }

        public void SetupPanels()
        {
            _puzzlePicture = GetPuzzlePicture();
            tipImage.texture = _puzzlePicture;
        }

        private void Refresh(bool show = false)
        {
            for (int position = 0; position < size * size; position++)
            {
                int number = logic.GetNumber(position);
                puzzles[position % size, position / size].image.texture = (number >= 0) ? _puzzlePicture : ((!show) ? null : _puzzlePicture);

                if (number >= 0)
                    puzzles[position % size, position / size].PuzzlePart = puzzlesParts[number % size, number / size];
            }
        }



        //Change logic here!!
        private Texture GetPuzzlePicture()
        {
            return puzzlePictures[UnityEngine.Random.Range(0, puzzlePictures.Length)];
        }

        private void OnClick(string sender)
        {
            int position = Convert.ToInt16(sender);
            //Debug.Log(sender + " sender " + position + " position");
            logic.Shift(position);

            Refresh();

            if (logic.CheckNumbers())
            {
                Debug.Log("You won!!");
                print("fire GetNumberEventHere");
                GameOver(true);
                //puzzleComplete.Invoke(); //GameOver();
            }
        }

        private void ShowAll()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    puzzles[x, y].image.texture = _puzzlePicture;
                    puzzles[x, y].PuzzlePart = puzzlesParts[x, y];
                }
            }
        }

        private void GameOver(bool win=false)
        {
            openedDoors++;
            _puzzlePooler.DeactivateObjects();
            ShowAll();
            gameOverPanel.SetActive(true);
            GameManager.Instance.OnGameFinished(win);
            tipImage.gameObject.SetActive(false);
        }

        public void Restart()
        {
            StartGame();
        }
        public void OnCloseBtnCLick()
        {
            GameOver(false);
            print("OnCloseBtnClick");
        }
        public void OnInfoBtnClick()
        {
            print("OnInfoBtnClick");
        }
        public void ShowTip()
        {
            if (!showTip)
            {
                currentTime = Time.time;
                showTip = true;
            }
            Debug.Log(string.Format("Curr time: {0}, Time.time: {1}", currentTime, Time.time));
        }

        private void Update()
        {
            if (showTip)
            {
                if (currentTime + timeToShowTip > Time.time)
                {
                    tipImage.gameObject.SetActive(true);
                }
                else
                {
                    tipImage.gameObject.SetActive(false);
                    showTip = false;
                }
            }

        }
    }
}
