using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Puzzle
{
    public class Door : MonoBehaviour
    {
        public DoorState state = DoorState.Locked;

        public CanvasGroup cg;
        public RectTransform gamePanel;
        public RawImage tipImage;

        public Texture defaultImage;

        private Animator anim;

        [SerializeField]
        PuzzleGame manager;

        private void Start()
        {
            anim = GetComponent<Animator>();
            manager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzzleGame>();
            DebuMethod();

        }


        private void DebuMethod()
        {
            int initsize = 3;
            int gamesWon = 0;
            int currentsize = 0;            
            for (int i = 0; i < 100; i++)
            {
                currentsize = initsize + (gamesWon / initsize);// + (gamesWon % initsize);
                gamesWon++;
                Debug.Log(currentsize);

            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && state == DoorState.Locked)
            {
                cg.alpha = 1;
                Debug.Log("Player comes");
                manager.SetupPanels(gamePanel, tipImage);
                manager.StartGame();
                //manager.puzzleComplete += OpenDoor;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (state == DoorState.Opening || state == DoorState.Opened)
                {
                    tipImage.gameObject.SetActive(true);
                    //manager.puzzleComplete -= OpenDoor;
                }
                else if (state == DoorState.Locked)
                {
                    cg.alpha = 0;
                    tipImage.texture = defaultImage;
                    tipImage.gameObject.SetActive(true);
                    //manager.puzzleComplete -= OpenDoor;
                }

            }
        }

        private void OpenDoor()
        {
            Debug.Log("The Door starts opening");
            anim.SetTrigger("Open");
            state = DoorState.Opening;
        }

        public void DoorIsOpened()
        {
            state = DoorState.Opened;
        }


    }

    public enum DoorState
    {
        Locked,
        Opening,
        Opened
    }
}
