using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    public class VRController : MonoBehaviour
    {
        private Hand[] _hands;
        [SerializeField] private SteamVR_Action_Boolean _menuButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
        [SerializeField] private GameObject _throwable;

        void Start()
        {
            _hands = GetComponentsInChildren<Hand>();
        }

        void Update()
        {
            foreach (var hand in _hands)
            {
                if (WasButtonReleased(_menuButton, hand))
                {
                    ToggleMenu(hand);
                }
            }
        }

        private void ToggleMenu(Hand passedInHand)
        {
            
            // toggle the menu to either show or not show
        }

        private void QuitGame()
        {
            if (something) {
                Application.Quit();
            }
        }

        private void SpawnAndAttach(Hand passedInhand)
        {
            Hand handToUse = passedInhand;
            if (passedInhand == null)
            {
                handToUse = _hands[0];
            }

            if (handToUse == null)
            {
                return;
            }
        }

        private bool WasButtonReleased(SteamVR_Action_Boolean button, Hand hand)
        {
            return button.GetStateUp(hand.handType);
        }

        private bool IsButtonDown(SteamVR_Action_Boolean button, Hand hand)
        {
            return button.GetState(hand.handType);
        }

        private bool WasButtonPressed(SteamVR_Action_Boolean button, Hand hand)
        {
            return button.GetStateDown(hand.handType);
        }
    }
}
