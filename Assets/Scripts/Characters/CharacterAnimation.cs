using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void PlayAnimation(string animationName)
        {
            _animator.Play(animationName);
        }
    }
}

