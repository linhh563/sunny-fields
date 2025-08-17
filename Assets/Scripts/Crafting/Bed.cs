using System;
using System.Collections;
using Management;
using Management.Interface;
using UnityEngine;


namespace Crafting
{
    public class Bed : MonoBehaviour, IInteractable
    {
        private bool _canInteract;


        void Start()
        {
            _canInteract = false;

            GameplayInputManager.OnInteractKeyPress += Interact;
        }


        void OnDisable()
        {
            GameplayInputManager.OnInteractKeyPress -= Interact;
        }


        public void Interact()
        {
            if (!_canInteract) return;

            ChangeDay();
        }


        private void ChangeDay()
        {
            // display ui change next day

            TimeManager.ChangeNextDay();
        }


        private IEnumerator ChangeDayAnimation()
        {
            
            yield return new WaitForSeconds(2);
        }


        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _canInteract = true;
            }
        }


        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _canInteract = false;
            }
        }
    }
}
