using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Management;
using Management.ScriptableObjects;

namespace GameUI
{
    public class DialogueUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _npcNameText;
        [SerializeField] private Image _npcAvatar;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Transform _decisionPanel;

        public GameObject decisionButtonPrefab { get; private set; }


        public TMP_Text dialogueText { get => _dialogueText; set => _dialogueText = value; }
        public Transform decisionPanel { get => _decisionPanel; set => _decisionPanel = value; }

        void Awake()
        {
            decisionButtonPrefab = Resources.Load<GameObject>("Prefabs/DecisionButton");

            CheckPropertiesValue();
        }


        // create and set up decision buttons
        public void CreateDecisionButtons(string decisionText, UnityEngine.Events.UnityAction buttonEvent)
        {
            var decisionButtonPrefab = GameplayUIManager.Instance.conversationUI.decisionButtonPrefab;
            var decisionPanel = GameplayUIManager.Instance.conversationUI.decisionPanel;

            GameObject decisionButton = ObjectPoolManager.SpawnObject(decisionButtonPrefab, decisionPanel);
            
            decisionButton.GetComponentInChildren<TMP_Text>().SetText(decisionText);
            decisionButton.GetComponent<Button>().onClick.AddListener(buttonEvent);
        }


        public void ClearDecisions()
        {
            foreach (Transform child in GameplayUIManager.Instance.conversationUI.decisionPanel)
            {
                var button = child.GetComponent<Button>();
                button.onClick.RemoveAllListeners();

                ObjectPoolManager.ReturnObjectToPool(child.gameObject);
            }
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
