using DG.Tweening;
using Rich.Base.Runtime.Abstract.View;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Key;
using Runtime.Signals;
using Runtime.Views.Stack;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Views.Player
{
    public class PlayerView : RichView
    {
        [Header("References")]
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confettiParticle;

        [Header("Events")]
        public UnityAction onCollectableInteraction = delegate { };
        public UnityAction onPlayerInteract = delegate { };
        public UnityAction<GameObject> onCollectableInteract = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<Transform, Transform> onStageAreaEntered = delegate { };
        public UnityAction onFinishAreaEntered = delegate { };

        [ShowInInspector] private bool isReadyToMove, isReadyToPlay;
        [ShowInInspector] private float xValue;
        [ShowInInspector] private float2 clampValues;
        [ShowInInspector] private PlayerData playerData;

        private const string collectableTag = "Collectable";
        private const string gateTag = "Gate";
        private const string groundRedTag = "GroundRed";
        private const string groundGreenTag = "GroundGreen";
        private const string groundBlueTag = "GroundBlue";

        private const float scaleCounter = 1.7f;

        [ShowInInspector] private PlayerColorTypes colorType;

        protected override void Start()
        {
            base.Start();
            var stackView = FindObjectOfType<StackView>();
        }

        public void SetPlayerData(PlayerData data)
        {
            playerData = data;
        }

        public void OnInputDragged(HorizontalInputParams horizontalInputParams)
        {
            xValue = horizontalInputParams.HorizontalValue;
            clampValues = horizontalInputParams.ClampValues;
        }

        public void OnInputReleased() => IsReadyToMove(false);

        public void OnInputTaken() => IsReadyToMove(true);

        private void FixedUpdate()
        {
            if (!isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (isReadyToMove) 
            {
                MovePlayer();
            }
            else 
            {
                StopPlayerHorizontally();
            }

        }

        private void StopPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void StopPlayerHorizontally()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, playerData.MovementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void MovePlayer()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(xValue * playerData.MovementData.SidewaysSpeed, velocity.y,
                playerData.MovementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            var position1 = rigidbody.position;
            rigidbody.position = new Vector3(Mathf.Clamp(position1.x, clampValues.x, clampValues.y),
                position1.y, position1.z);
        }

        internal void IsReadyToPlay(bool condition) => isReadyToPlay = condition;

        public void IsReadyToMove(bool condition) => isReadyToMove = condition;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered");

            if (other.gameObject.name == collectableTag)
            {
                onCollectableInteraction?.Invoke();
                Debug.Log("Collectable");
            }

            if (other.gameObject.name == gateTag)
                HandleGateCollision(other);

            if (other.CompareTag(groundRedTag))
                Debug.Log("Ground Red");

            if (other.CompareTag(groundGreenTag))
                Debug.Log("Ground Green");

            if (other.CompareTag(groundBlueTag))
                Debug.Log("Ground Blue");
        }

        private void HandleGateCollision(Collider other)
        {
            if (other.CompareTag("BlueGate")) SetPlayerColor(PlayerColorTypes.Blue);
            else if (other.CompareTag("GreenGate")) SetPlayerColor(PlayerColorTypes.Green);
            else if (other.CompareTag("RedGate")) SetPlayerColor(PlayerColorTypes.Red);
        }

        private void SetPlayerColor(PlayerColorTypes color)
        {
            if (color == PlayerColorTypes.Blue)
            {
                renderer.materials[0].color = Color.blue;
            }
            else if (color == PlayerColorTypes.Green)
            {
                renderer.materials[0].color = Color.green;
            }
            else if (color == PlayerColorTypes.Red)
            {
                renderer.materials[0].color = Color.red;
            }
        }


        internal void ScaleUpPlayer() => renderer.gameObject.transform.DOScaleX(scaleCounter, 1).SetEase(Ease.Flash);

        internal void ShowUpText()
        {
            scaleText.gameObject.SetActive(true);
            scaleText.DOFade(1, 0f).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(.65f));
            scaleText.rectTransform.DOAnchorPosY(.85f, .65f).SetRelative(true).SetEase(Ease.OutBounce).OnComplete(() =>
                scaleText.rectTransform.DOAnchorPosY(-.85f, .65f).SetRelative(true));
        }

        internal void PlayConfettiParticle() => confettiParticle.Play();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + .9f), 1.7f);
        }

        internal void OnReset()
        {
            onReset?.Invoke();
            StopPlayer();
            isReadyToMove = false;
            isReadyToPlay = false;
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
    }
}
