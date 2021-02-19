using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    public class VRController : MonoBehaviour
    {
        private Hand[] _hands;
        [SerializeField] private Text _countText;
        [SerializeField] private Text _winText;
        [SerializeField] private SteamVR_Action_Boolean _actionButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
        [SerializeField] private SteamVR_Action_Boolean _grabButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        [SerializeField] private GameObject _throwable;

        private int _count = 0;
        private int _pickupCount = 0;

        void Start()
        {
            _pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
            SetCountText();
            _winText.text = "";
            _hands = GetComponentsInChildren<Hand>();
        }

        void Update()
        {
            foreach (var hand in _hands)
            {
                if (WasButtonReleased(_actionButton, hand) && IsButtonDown(_grabButton, hand))
                {
                    SpawnAndAttach(hand);
                }
            }
        }

        public void IncrementCount()
        {
            _count++;
            SetCountText();
        }

        private void SetCountText()
        {
            _countText.text = $"Count: {_count}";
            if (_count >= _pickupCount)
            {
                _winText.text = "You Win!";
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

            GameObject prefabObject = Instantiate(_throwable) as GameObject;
            handToUse.AttachObject(prefabObject, GrabTypes.Pinch);
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
