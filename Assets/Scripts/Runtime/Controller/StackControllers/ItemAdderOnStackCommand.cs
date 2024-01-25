using Runtime.Signals;
using strange.extensions.command.impl;

namespace Runtime.Controller.StackControllers
{
    public class ItemAdderOnStackCommand : Command
    {
        [Inject] private StackSignals StackSignals { get; set; }
        
        public override void Execute()
        {
            StackSignals.onInteractionCollect.Dispatch();
        }
    }
}