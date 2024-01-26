using Runtime.Signals;
using strange.extensions.command.impl;

namespace Runtime.Controller.Obstacle
{
    public class ObstacleCommand : Command
    {
        [Inject] public ObstacleSignals ObstacleSignals { get; set; }
        public override void Execute()
        {
            
        }
    }
}