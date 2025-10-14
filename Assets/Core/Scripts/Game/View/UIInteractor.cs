using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Game
{
    public class UIInteractor : MonoBehaviour
    {
        public static UIInteractor Instance;
        public Image BG;
        public Button CloseButton;
        public InteractionLayersMb[] Interactors;
        [SerializeField][Range(0, 4)] private int _interactionLayer;
        public int InteractionLayer
        {
            get => _interactionLayer;
            set
            {
                _interactionLayer = value;
                Switch();
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void OnValidate()
        {
            Switch();
        }

        public void Switch()
        {
            if (Interactors == null || Interactors.Length == 0) return;
            for (int i = 0; i < Interactors.Length; i++)
            {
                var isEnabled = i == _interactionLayer;
                Interactors[i].transform.gameObject.SetActive(isEnabled);
            }
            if (_interactionLayer < Interactors.Length)
            {
                BG.color = new Color(0, 0, 0, 0.7f);
            }
            else
            {
                BG.color = new Color(0, 0, 0, 0);
            }
        }
    }

    public enum InteractionLayerEnum
    {
        Upgrade = 0,
        ChooseWeapon = 1,
        Lose = 2,
        Win = 3,
        None = 999
    }
}