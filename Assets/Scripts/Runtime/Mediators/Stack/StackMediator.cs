using DG.Tweening;
using Rich.Base.Runtime.Concrete.Injectable.Mediator;
using Runtime.Model.Player;
using Runtime.Model.Stack;
using Runtime.Signals;
using Runtime.Views.Player;
using Runtime.Views.Pool;
using Runtime.Views.Stack;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Mediators.Stack
{
    public class StackMediator : MediatorLite
    {
        
        [Inject] public StackView StackView { get; set; }
        
        [Inject] public IStackModel model { get; set; }
        
        [Inject] public StackSignals StackSignals { get; set; }
        

        public override void OnRegister()
        {
            base.OnRegister();
            StackSignals.onInteractionCollectable.AddListener(StackView.OnInteractionCollectable);
        }
        public override void OnRemove()
        {
            base.OnRemove();
            StackSignals.onInteractionCollectable.RemoveListener(StackView.OnInteractionCollectable);
        }

        private void OnInteractCollect()
        {
            Debug.LogWarning("Interaction Work");
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            StackView.SetStackData(model.StackData.Data);
        }
    }
}