using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Management.ScriptableObjects;
using System.Collections;

namespace GameUI
{
    public class DialogueUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _npcNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        private Queue<string> _dialogueQueue = new Queue<string>();
        private bool _conversationEnded;
        private float _typeSpeed = 10f;
        private bool _isTyping;
        private string _nextDialogue;
        private Coroutine _typeDialogueCoroutine;

        private const string HTML_ALPHA = "<color=#00000000>";
        private const float MAX_TYPE_TIME = 0.25f;

        public void PlayNextDialogue(ConversationScriptableObject conversation)
        {
            // if there is nothing in the queue
            if (_dialogueQueue.Count == 0)
            {
                // start the conversation
                if (!_conversationEnded)
                {
                    StartConversation(conversation);
                }
                // end the conversation
                else if (_conversationEnded && !_isTyping)
                {
                    EndConversation();
                    return;
                }
            }

            // if there is something in the queue
            if (!_isTyping)
            {
                _nextDialogue = _dialogueQueue.Dequeue();
                _typeDialogueCoroutine = StartCoroutine(TypeDialogueText(_nextDialogue));
            }
            // conversation is being type out
            else
            {
                FinishParagraphEarly();
            }

            if (_dialogueQueue.Count == 0)
            {
                _conversationEnded = true;
            }
        }

        private void StartConversation(ConversationScriptableObject conversation)
        {
            // activate conversation ui game object
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            _npcNameText.text = conversation.NPCName;

            // add dialogue to queue
            for (int i = 0; i < conversation.NPCParagraphs.Length; i++)
            {
                _dialogueQueue.Enqueue(conversation.NPCParagraphs[i]);
            }
        }

        private void EndConversation()
        {
            _dialogueQueue.Clear();

            _conversationEnded = false;

            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator TypeDialogueText(string _nextDialogue)
        {
            _isTyping = true;

            _dialogueText.text = "";

            string originalText = _nextDialogue;
            string displayedText = "";
            int alphaIndex = 0;

            foreach (char c in _nextDialogue.ToCharArray())
            {
                alphaIndex++;
                _dialogueText.text = originalText;

                displayedText = _dialogueText.text.Insert(alphaIndex, HTML_ALPHA);
                _dialogueText.text = displayedText;

                yield return new WaitForSeconds(MAX_TYPE_TIME / _typeSpeed);
            }

            _isTyping = false;
        }

        private void FinishParagraphEarly()
        {
            StopCoroutine(_typeDialogueCoroutine);
            _dialogueText.text = _nextDialogue;
            _isTyping = false;
        }
    }
}
