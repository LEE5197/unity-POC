
public class PlayerFSM
{
    public IState curstate { get; private set; }
	public IState prevstate { get; private set; }
	public PlayerFSM()
	{
		curstate = null;
		prevstate = null;
	}
    
    public void changeState(IState nextState)
	{
		if (nextState == null) return;
		if (curstate == nextState) return;

		curstate?.Exit();
		if (curstate == null) prevstate = nextState;
		else prevstate = curstate;
		curstate = nextState;
		curstate.Enter();
	}

}
