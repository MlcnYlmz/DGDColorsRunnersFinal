using strange.extensions.signal.impl;
using UnityEngine;

namespace Runtime.Signals
{
    public class StackSignals
    {
        public Signal onAddStack = new Signal();
        public Signal<GameObject> onStackCollect = new Signal<GameObject>();
        public Signal onInteractionCollect = new Signal();
        public Signal<Vector2> onStackFollow = new Signal<Vector2>();
        public Signal<Vector2> onStackMover = new Signal<Vector2>();
        public Signal onInteractObstacle = new Signal();
    }
}