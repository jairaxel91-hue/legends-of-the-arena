using UnityEngine;
using UnityEngine.SceneManagement;
using LegendsOfTheArena.Characters;

namespace LegendsOfTheArena.Core
{
    public sealed class MatchManager : MonoBehaviour
    {
        [SerializeField] private FighterHealth fighterOne;
        [SerializeField] private FighterHealth fighterTwo;
        [SerializeField, Min(1)] private int roundsToWin = 2;
        private int winsOne;
        private int winsTwo;
        private bool roundEnded;

        private void OnEnable()
        {
            if (fighterOne != null) fighterOne.KnockedOut += OnKnockout;
            if (fighterTwo != null) fighterTwo.KnockedOut += OnKnockout;
        }

        private void OnDisable()
        {
            if (fighterOne != null) fighterOne.KnockedOut -= OnKnockout;
            if (fighterTwo != null) fighterTwo.KnockedOut -= OnKnockout;
        }

        private void OnKnockout(FighterHealth defeated)
        {
            if (roundEnded) return;
            roundEnded = true;
            if (defeated == fighterOne) winsTwo++; else winsOne++;
            Debug.Log(winsOne >= roundsToWin ? "PLAYER 1 WINS THE MATCH" :
                winsTwo >= roundsToWin ? "PLAYER 2 WINS THE MATCH" :
                "ROUND OVER — press R to continue");
        }

        private void Update()
        {
            if (!roundEnded || !Input.GetKeyDown(KeyCode.R)) return;
            if (winsOne >= roundsToWin || winsTwo >= roundsToWin)
            {
                winsOne = winsTwo = 0;
            }
            if (fighterOne != null) fighterOne.RestoreFullHealth();
            if (fighterTwo != null) fighterTwo.RestoreFullHealth();
            roundEnded = false;
        }

        public void Configure(FighterHealth one, FighterHealth two)
        {
            fighterOne = one;
            fighterTwo = two;
        }
    }
}
