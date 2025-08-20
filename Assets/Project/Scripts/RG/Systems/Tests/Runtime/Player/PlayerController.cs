using UnityEngine;
using RG.Systems.Input;
using UtilitiesLibrary.Validation.Attributes;

namespace RG.Systems.Tests.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Header("Input"), Tooltip("A Scriptable Object with the player actions"), Required(LogToConsoleIfNullOrEmpty = true, LogType = LogType.Error)]
        private BaseInputReaderScriptableObject _input;
        private PlayerMovement _movement;
        private PlayerCamera _camera;
        private PlayerInteractor _interactor;
        [SerializeField] private int @int;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _camera = GetComponent<PlayerCamera>();
            _interactor = GetComponent<PlayerInteractor>();

            _input.EnablePlayerActions();
        }

        private void Update()
        {
            //Move player
        }
    }


    public class PlayerInteractor: MonoBehaviour
    {
    }

    public class PlayerCamera : MonoBehaviour
    {
    }

    public class PlayerMovement : MonoBehaviour
    {
    }
}
