using System.Collections.Generic;
using Rich.Base.Runtime.Abstract.View;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Views.Stack
{
    public class StackView : RichView
    {
        public UnityAction onInteractCollect = delegate { };

        [SerializeField] internal GameObject collectableStickMan;

        internal StackData _data;
        internal List<GameObject> _collectableStack = new List<GameObject>();

        [Inject] public StackSignals StackSignals { get; set; }

        public void SetStackData(StackData stackData)
        {
            _data = stackData;
        }
        public void MoveStack(float directionX, List<GameObject> collectableStack)
        {
            float direct = Mathf.Lerp(collectableStack[0].transform.localPosition.x, directionX, _data.LerpSpeed);
            collectableStack[0].transform.localPosition = new Vector3(direct, 1f, 0.335f);
            StackLerp(collectableStack);
        }
        internal void OnStackMove(Vector2 direction)
        {
            transform.position = new Vector3(0, transform.position.y, direction.y - 1f);
            if (transform.childCount > 0)
            {
                MoveStack(direction.x, _collectableStack);
            }
        }
        private void StackLerp(List<GameObject> collectableStack)
        {
            for (int i = 1; i < collectableStack.Count; i++)
            {
                Vector3 pos = collectableStack[i].transform.localPosition;
                pos.x = collectableStack[i - 1].transform.localPosition.x;
                float direct = Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _data.LerpSpeed);
                collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }

        internal void OnStackCollectable(GameObject collectableGameObject)
        {
            Debug.Log("Collectable Worked");
            AddStack(collectableGameObject);
        }

        private void AddStack(GameObject collectableGameObject)
        {
            Transform stackTransform = transform;
            if (_collectableStack.Count > 0)
            {
                Vector3 newPos = _collectableStack[^1].transform.localPosition + Vector3.forward * _data.CollectableOffsetInStack;
                collectableGameObject.transform.SetParent(stackTransform);
                collectableGameObject.transform.localPosition = newPos;
            }
            else
            {
                collectableGameObject.transform.SetParent(stackTransform);
                collectableGameObject.transform.localPosition = new Vector3(0, 1f, 0.335f);
            }

            _collectableStack.Add(collectableGameObject);
        }
    }
}
