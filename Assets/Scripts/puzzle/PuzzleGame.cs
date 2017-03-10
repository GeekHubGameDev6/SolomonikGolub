using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
   public delegate void PuzzleGameComple();

    public class PuzzleGame : MonoBehaviour
    {
        public int openedDoors;
        private const int initSize = 3;
        private PuzzleLogic logic;
        public int size;
        private Rect[,] puzzlesParts;
        [SerializeField]
        private PuzzleSelection[,] puzzles;

        public PuzzleSelection puzzlePrefab;
        public Texture[] puzzlePictures;
        private Texture _puzzlePicture;
        public RectTransform gamePanel;

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
        public RawImage tipImage;
        private bool showTip;
        public float currentTime = 0;

        public PuzzleGameComple puzzleComplete;

        private void Start()
        {
            openedDoors = 0;
            puzzleComplete += GameOver;
            gameOverPanel.SetActive(false);
            //Debug.Log(panel.rect.width + "panel scale X");
            //StartGame();
        }

        public void StartGame()
        {
            size = initSize + openedDoors / initSize;
            showTip = false;
            currentTime = 0;
            gameOverPanel.SetActive(false);
            tipImage.gameObject.SetActive(false);
            logic = new PuzzleLogic(size);

            //CropSprite();

            InitPictures();

            logic.Start();

            for (int i = 0; i < size * size * 30; i++)
            {
                logic.ShiftRandom();
            }

            Refresh();
        }
        public void SetupPanels(RectTransform gamePanel, RawImage tipImage)
        {
            _puzzlePicture = GetPuzzlePicture();
            this.tipImage = tipImage;
            this.gamePanel = gamePanel;
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
                    PuzzleSelection newImg = Instantiate(puzzlePrefab);
                    newImg.transform.localPosition = new Vector2(x * w + offsetX, y * h + offsetY);

                    newImg.transform.SetParent(gamePanel, false);
                    newImg.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
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

        private Texture GetPuzzlePicture()
        {
            return puzzlePictures[UnityEngine.Random.Range(0, puzzlePictures.Length)];
        }



        //private void CropSprite()
        //{
        //    sprites = new Sprite[size, size];
        //    for (int x = 0; x < size; x++)
        //    {
        //        for (int y = 0; y < size; y++)
        //        {
        //            sprites[x, y] = GetImagePart(x, y);
        //        }
        //    }
        //}
        //private Sprite GetImagePart(int x, int y)
        //{
        //    Texture2D sp = sprite.texture;
        //    int w, h;
        //    w = (int)panel.rect.width / size;
        //    h = (int)panel.rect.height / size;
        //    Rect spriteRect = sprite.textureRect;
        //    Bitmap bitmap = Properties.Resources.frog;
        //    int w, h;
        //    w = bitmap.Width / size;
        //    h = bitmap.Height / size;
        //    Rectangle crop_part = new Rectangle(x * w, y * h, w, h);
        //    Bitmap target = new Bitmap(w, h);
        //    Graphics g = Graphics.FromImage(target);
        //    g.DrawImage(bitmap,
        //        new Rectangle(0, 0, target.Width, target.Height),
        //        crop_part, GraphicsUnit.Pixel);
        //    return target;
        //    return null;
        //}

        private void OnClick(string sender)
        {
            int position = Convert.ToInt16(sender);
            Debug.Log(sender + " sender " + position + " position");
            logic.Shift(position);

            Refresh();
            if (logic.CheckNumbers())
            {
                Debug.Log("You won!!");
                puzzleComplete.Invoke(); //GameOver();
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

        private void GameOver()
        {
            openedDoors++;
            ShowAll();
            //gameOverPanel.SetActive(true);
            tipImage.gameObject.SetActive(false);
        }

        public void Restart()
        {
            StartGame();
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
