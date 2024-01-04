using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrawfisSoftware.TempleRun
{
    internal class InputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputAsset;

        private LeftRightJumpSlide _inputActions;
        private InputAction _leftAction;
        private InputAction _rightAction;

        private void Start()
        {
            _inputActions = new LeftRightJumpSlide();
            _leftAction = _inputActions.Player.Left;
            _leftAction.Enable();
            _leftAction.performed += LeftAction_performed;
            _rightAction = _inputActions.Player.Right;
            _rightAction.Enable();
            _rightAction.performed += RightAction_performed;
        }

        private void LeftAction_performed(InputAction.CallbackContext obj)
        {
            _leftAction.Disable();
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.LeftTurnRequested, this, null);
            StartCoroutine(EnableAfterDelay(_leftAction));
        }

        private void RightAction_performed(InputAction.CallbackContext obj)
        {
            _leftAction.Disable();
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.RightTurnRequested, this, null);
            StartCoroutine(EnableAfterDelay(_leftAction));
        }

        private IEnumerator EnableAfterDelay(InputAction actionToEnable)
        {
            yield return new WaitForSeconds(Blackboard.Instance.GameConfig.InputCoolDownForTurns);
            actionToEnable.Enable();
        }
    }
}