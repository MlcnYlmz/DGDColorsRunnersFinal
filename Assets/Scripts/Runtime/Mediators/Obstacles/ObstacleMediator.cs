using Rich.Base.Runtime.Concrete.Injectable.Mediator;
using Runtime.Model.Stack;
using Runtime.Signals;
using Runtime.Views.Obstacle;
using Runtime.Views.Stack;
using UnityEngine;

namespace Runtime.Mediators.Stack
{
    public class ObstacleMediator : MediatorLite
    {
        [Inject] public  ObstacleView ObstacleView { get; set; }
        [Inject] public ObstacleSignals ObstacleSignals { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            ObstacleSignals.onColorObstacle.AddListener(ObstacleView.);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            ObstacleSignals.onColorObstacle.RemoveListener(ObstacleView.);

        }

        private void OnInteractCollect()
        {
            Debug.LogWarning("InteractCollect Worked");
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            //StackView.SetStackData(Model.StackData.Data);
        }
    }
}