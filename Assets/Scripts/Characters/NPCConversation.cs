using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

using Management;
using Management.Interface;
using Management.ScriptableObjects;
using GameUI;
using System;


namespace Characters
{
    public class NPCConversation : MonoBehaviour, IInteractable
    {
        // TODO: modify this property to array to store multiple conversations
        [SerializeField] ConversationScriptableObject conversationData;

        private float _typeSpeed = 0.05f;
        private bool _isConversationActive;
        private bool _isTyping;
        private int _dialogueIndex;


        void Awake()
        {
            // subscribe input events
            GameplayInputManager.OnInteractKeyPress += Interact;

            _dialogueIndex = 0;
        }


        void OnDisable()
        {
            // unsubscribe event
            GameplayInputManager.OnInteractKeyPress -= Interact;
        }


        public void Interact()
        {
            if (conversationData == null) return;

            if (_isConversationActive)
            {
                // type next line
                PlayNextLine();
            }
            else
            {
                // start conversation
                StartConversation();
            }
        }


        private void StartConversation()
        {
            _isConversationActive = true;
            _dialogueIndex = 0;

            // set npc name and avatar in conversation ui
            GameplayUIManager.Instance.conversationUI.SetNPC(conversationData.NPCName, conversationData.NPCAvatar);

            // activate conversation panel
            GameplayUIManager.Instance.EnableConversationUI(true);

            // pause the game

            DisplayCurrentLine();
        }


        public void PlayNextLine()
        {
            bool skipTyping = false;

            // skip typing
            if (_isTyping)
            {
                StopAllCoroutines();

                GameplayUIManager.Instance.conversationUI.SetDialogue(conversationData.dialogueLines[_dialogueIndex]);
                _isTyping = false;

                skipTyping = true;
            }

            // clear decision
            GameplayUIManager.Instance.conversationUI.ClearDecisions();

            // check if the line is end conversation and the line is not skipped
            if (conversationData.dialogueLines.Length > _dialogueIndex &&
                conversationData.endConversationIndex.Contains(_dialogueIndex) &&
                !skipTyping)
            {
                EndConversation();
                return;
            }

            // check if have decisions and display them
            foreach (var decision in conversationData.allDecisions)
            {
                if (decision.dialogueIndex == _dialogueIndex)
                {
                    // display decisions
                    DisplayDecision(decision);
                    return;
                }
            }

            // if player skip typing current line, return
            if (skipTyping) return;

            if (++_dialogueIndex < conversationData.dialogueLines.Length)
            {
                // if there is another line, type the next line
                DisplayCurrentLine();
            }
            else
            {
                // end conversation
                EndConversation();
            }
        }


        private void EndConversation()
        {
            StopAllCoroutines();
            _isConversationActive = false;

            GameplayUIManager.Instance.conversationUI.SetDialogue("");
            GameplayUIManager.Instance.EnableConversationUI(false);

            // resume game
        }


        private IEnumerator TypeNextLine()
        {
            _isTyping = true;

            GameplayUIManager.Instance.conversationUI.SetDialogue("");

            foreach (var letter in conversationData.dialogueLines[_dialogueIndex])
            {
                GameplayUIManager.Instance.conversationUI.AddLetterToDialogue(letter);

                yield return new WaitForSeconds(_typeSpeed);
            }

            _isTyping = false;
        }


        private void DisplayDecision(ConversationDecision decisionLine)
        {
            for (int i = 0; i < decisionLine.decisions.Length; i++)
            {
                var nextIndex = decisionLine.decisions[i].nextDialogueIndex;
                var decisionType = decisionLine.decisions[i].decisionType;

                GameplayUIManager.Instance.conversationUI.CreateDecisionButtons(decisionLine.decisions[i].decisionText, () => ChooseOption(nextIndex, decisionType));
            }
        }


        private void ChooseOption(int nextIndex, DecisionType decisionType)
        {
            _dialogueIndex = nextIndex;

            GameplayUIManager.Instance.conversationUI.ClearDecisions();
            DisplayCurrentLine();

            if (decisionType == DecisionType.Talking) return;

            switch (decisionType)
            {
                case DecisionType.Buying:
                    GameplayUIManager.Instance.EnableStoreUI(true);
                    GameplayUIManager.Instance.EnableConversationUI(false);
                    break;

                case DecisionType.Selling:
                    // display sell ui
                    
                    GameplayUIManager.Instance.EnableConversationUI(false);
                    break;
            }
        }


        private void DisplayCurrentLine()
        {
            StopAllCoroutines();
            StartCoroutine(TypeNextLine());
        }
    }
}
