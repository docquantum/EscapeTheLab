using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    public class VRController : MonoBehaviour
    {
        public GameObject menu;
        private Hand[] _hands;
        [SerializeField] private SteamVR_Action_Boolean _menuButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");

        void Start()
        {
            _hands = GetComponentsInChildren<Hand>();
        }

        void Update()
        {
            foreach (var hand in _hands)
            {
                if (WasButtonReleased(_menuButton, hand) && menu.activeSelf == false)
                {
                    ToggleMenu(hand);
                } 
                else if (WasButtonReleased(_menuButton, hand) && menu.activeSelf == true)
                {
                    ManageMenu(hand);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        private void ToggleMenu(Hand passedInHand)
        {
            menu.SetActive(true);
        }

        private void ManageMenu(Hand passedInHand)
        {
            if (passedInHand == _hands[0]) {
                Application.Quit();
            }
            else if (passedInHand == _hands[1])
            {
                menu.SetActive(false);
            }
        }

        /*private void SpawnAndAttach(Hand passedInhand)
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
        }*/

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
