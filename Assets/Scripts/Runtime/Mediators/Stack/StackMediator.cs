using Rich.Base.Runtime.Concrete.Injectable.Mediator;
using Runtime.Model.Stack;
using Runtime.Signals;
using Runtime.Views.Stack;
using UnityEngine;

namespace Runtime.Mediators.Stack
{
    public class StackMediator : MediatorLite
    {
        [Inject] public StackView StackView { get; set; }
        [Inject] public IStackModel Model { get; set; }
        [Inject] public StackSignals StackSignals { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            StackSignals.onStackFollow.AddListener(StackView.OnStackMove);
            StackSignals.onStackCollect.AddListener(StackView.OnStackCollectable);
            StackView.onInteractCollect += OnInteractCollect;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            StackSignals.onStackFollow.RemoveListener(StackView.OnStackMove);
            StackSignals.onStackCollect.RemoveListener(StackView.OnStackCollectable);
            StackView.onInteractCollect -= OnInteractCollect;
        }

        private void OnInteractCollect()
        {
            Debug.LogWarning("InteractCollect Worked");
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            StackView.SetStackData(Model.StackData.Data);
        }
    }
}