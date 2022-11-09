using Sandbox;
using SandboxEditor;

namespace SweetTrickery;

[HammerEntity]
public class TimeCandySpawner : Entity
{
	private TimeCandy _candy;
	
	[Input]
	public void SpawnCandy()
	{
		if ( !IsServer )
			return;
		
		_candy = new TimeCandy();
		_candy.Position = Position + Vector3.Up * 20;
		_candy.Spawn();
	}

	public void DeleteCandy()
	{
		if ( !IsServer )
			return;
		
		if ( _candy.IsValid() )
			_candy.Delete();
	}
}
