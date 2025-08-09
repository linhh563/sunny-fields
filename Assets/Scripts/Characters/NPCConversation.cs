using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

using Management;
using Management.Interface;
using Management.ScriptableObjects;
using GameUI;
using Unity.VisualScripting;


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
            ClearDecisions();

            // check if the line is end conversation and the line is not skipped
            if (conversationData.dialogueLines.Length > _dialogueIndex &&
                conversationData.endConversationIndex.Contains(_dialogueIndex) &&
                !skipTyping)
            {
                EndConversation();
                return;
            }

            // check if have decisions and display them
            foreach (var decision in conversationData.decisions)
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


        private void ClearDecisions()
        {
            foreach (Transform child in GameplayUIManager.Instance.conversationUI.decisionPanel)
            {
                ObjectPoolManager.ReturnObjectToPool(child.gameObject);
            }
        }


        private void DisplayDecision(ConversationDecision decisionLine)
        {
            for (int i = 0; i < decisionLine.decisions.Length; i++)
            {
                var nextIndex = decisionLine.nextDialogueIndex[i];
                CreateDecisionButtons(decisionLine.decisions[i], () => ChooseOption(nextIndex, decisionLine.decisionTypes[i]));
            }
        }


        private void ChooseOption(int index, DecisionType decisionType)
        {
            _dialogueIndex = index;

            switch (decisionType)
            {
                case DecisionType.Talking:
                    DisplayCurrentLine();
                    break;

                case DecisionType.Buying:
                    GameplayUIManager.Instance.EnableStoreUI(true);
                    break;

                case DecisionType.Selling:
                    // enable sell ui
                    break;

                default:
                    break;
            }

            ClearDecisions();
            DisplayCurrentLine();
        }


        private void DisplayCurrentLine()
        {
            StopAllCoroutines();
            StartCoroutine(TypeNextLine());
        }


        // create and set up decision buttons
        private void CreateDecisionButtons(string decisionText, UnityEngine.Events.UnityAction buttonEvent)
        {
            var decisionButtonPrefab = GameplayUIManager.Instance.conversationUI.decisionButtonPrefab;
            var decisionPanel = GameplayUIManager.Instance.conversationUI.decisionPanel;

            GameObject decisionButton = ObjectPoolManager.SpawnObject(decisionButtonPrefab, decisionPanel);
            decisionButton.GetComponentInChildren<TMP_Text>().SetText(decisionText);
            decisionButton.GetComponent<Button>().onClick.AddListener(buttonEvent);
        }
    }
}
