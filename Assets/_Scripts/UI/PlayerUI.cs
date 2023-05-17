using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private VisualElement _root;
        private Label _velocity;
        private Label _mode;

        void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _velocity = _root.Q<Label>("shipVelocity");
            _mode = _root.Q<Label>("shipMode");
        }

        void Update()
        {
            _velocity.text = Spaceship.Kpm.ToString("F0");
            if (Spaceship.ComputerMovement)
                _mode.text = "Linear";
            else
                _mode.text = "Inertia";
        }
    }
}
