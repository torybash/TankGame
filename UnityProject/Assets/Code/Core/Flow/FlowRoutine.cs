using System.Collections;

namespace TankGame.Flow
{
	public class FlowRoutine
	{
		public IFlow flow;
		public Routine routine;

		public FlowRoutine(IFlow flow)
		{
			this.flow = flow;
			routine = new Routine();
			routine.Start(flow.Flow());
			flow.Entered();
		}
	}


}