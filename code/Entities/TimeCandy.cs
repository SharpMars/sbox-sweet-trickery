using Sandbox;
using Sandbox.Component;

namespace SweetTrickery;

public class TimeCandy : ModelEntity
{
	public override void Spawn()
	{
		Model = Model.Load( "models/candy.vmdl" );
		Tags.Add( "trigger" );
		SetupPhysicsFromAABB( PhysicsMotionType.Keyframed, Model.PhysicsBounds.Mins, Model.PhysicsBounds.Maxs );
		RenderColor = Color.Random;
		EnableTouch = true;
		Rotation = Rotation.FromRoll( -15f );
		base.Spawn();
	}

	public override void Touch( Entity other )
	{
		base.Touch( other );

		if ( other is not Player )
			return;

		if ( IsClient )
			Sound.FromWorld( "eat", Position );
		
		SweetTrickery.TimeCandyPickedUp();

		if ( !IsServer )
			return;
		
		Delete();
	}
	
	[Event.Tick]
	private void Tick()
	{
		var glow = Components.GetOrCreate<Glow>();
		glow.ObscuredColor = RenderColor;
		glow.Enabled = true;
		
		if ( !IsServer )
			return;
		
		Rotation = Rotation.RotateAroundAxis( Vector3.Up, 100f * Time.Delta );
	}
}
