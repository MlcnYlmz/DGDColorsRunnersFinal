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
        #region Public Variables
        
        #endregion
        
        #region Serialized Variables

        [SerializeField] private GameObject collectableStickMan;
        [SerializeField] private int numberOfCollectablesToSpawn = 10;

        #endregion

        #region Private Variables

        private StackData _data;
        private List<GameObject> _collectableStack = new List<GameObject>();
        private List<GameObject> _inactiveCollectables = new List<GameObject>();

        [Inject] public StackSignals StackSignals { get; set; }
        
        #endregion

        protected override void Start()
        {
            base.Start();
            PrepareCollectableStickMen(numberOfCollectablesToSpawn);
        }

        public void SetStackData(StackData stackData)
        {
            _data = stackData;
        }

        private void PrepareCollectableStickMen(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject collectable = Instantiate(collectableStickMan, transform);
                collectable.SetActive(false);
                _inactiveCollectables.Add(collectable);
            }
        }

        internal void OnInteractionCollectable()
        {
#if UNITY_EDITOR
            Debug.Log("Collectable Worked");
#endif

            if (_inactiveCollectables.Count > 0)
            {
                GameObject inactiveCollectable = _inactiveCollectables[0];
                _inactiveCollectables.RemoveAt(0);

                AddStack(inactiveCollectable);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("No collectables.");
#endif
            }
        }

        private void AddStack(GameObject collectableGameObject)
        {
            collectableGameObject.transform.SetParent(transform);

            if (_collectableStack.Count > 0)
            {
                Vector3 newPos = _collectableStack[_collectableStack.Count - 1].transform.localPosition;
                newPos.z += 1.0f;
                collectableGameObject.transform.localPosition = newPos;
            }
            else
            {
                collectableGameObject.transform.localPosition = new Vector3(0, 1f, 0.335f);
            }

            _collectableStack.Add(collectableGameObject);
            collectableGameObject.SetActive(true);
        }
    }
}
