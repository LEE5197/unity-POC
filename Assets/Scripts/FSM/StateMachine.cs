using System.Collections.Generic;

public class StateMachine<Ttarget>
{
	public Ttarget target { get; private set; }
	public Dictionary<string, BaseState<Ttarget>> stateDic { get; private set; }
	public BaseState<Ttarget> curState { get; private set; }
	public BaseState<Ttarget> prevState { get; private set; }

	public StateMachine(Ttarget target)
	{
		this.target = target;
		stateDic = new Dictionary<string, BaseState<Ttarget>>();
	}

	public void AddState(string key, BaseState<Ttarget> value)
	{
		stateDic.Add(key, value);
	}

	public void ChangeState(string key)
	{
		if (stateDic[key] == null) return;
		if (stateDic[key] == curState) return;

		curState?.Exit();
		curState = stateDic[key];
		prevState = curState;
		curState?.Enter();
	}

	public void Update()
	{
		curState?.Update();
	}
	public void FixedUpdate()
	{
		curState?.FixedUpdate();
	}
}
