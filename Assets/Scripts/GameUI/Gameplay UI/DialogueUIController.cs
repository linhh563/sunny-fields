using UnityEngine;
using TMPro;
using Management.ScriptableObjects;
using System.Collections;
using UnityEngine.UI;
using Management;

namespace GameUI
{
    public class DialogueUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _npcNameText;
        [SerializeField] private Image _npcAvatar;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Transform _decisionPanel;
        
        public GameObject decisionButtonPrefab { get; private set;}


        public TMP_Text dialogueText { get => _dialogueText; set => _dialogueText = value; }
        public Transform decisionPanel { get => _decisionPanel; set => _decisionPanel = value; }

        void Awake()
        {
            decisionButtonPrefab = Resources.Load<GameObject>("Prefabs/DecisionButton");

            CheckPropertiesValue();
        }


        public void SetNPC(string name, Sprite avatar)
        {
            _npcNameText.SetText(name);
            _npcAvatar.sprite = avatar;
        }


        public void SetDialogue(string dialogue)
        {
            _dialogueText.SetText(dialogue);
        }


        public void AddLetterToDialogue(char letter)
        {
            _dialogueText.text += letter;
        }


        private void CheckPropertiesValue()
        {
            if (_decisionPanel == null ||
                _npcNameText == null ||
                _npcAvatar == null ||
                _dialogueText == null ||
                decisionButtonPrefab == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
                return;
            }

        }
    }
}
