using Sandbox;

namespace SweetTrickery;

public sealed class Camera : CameraMode
{
	private Angles orbitAngles;
	private float orbitDistance = 150;
	private float orbitHeight = 0;

	public override void Update()
	{
		if ( Local.Pawn is not AnimatedEntity pawn )
			return;

		Position = pawn.Position;
		Vector3 targetPos;

		var center = Position + Vector3.Up * (72 * .8f);

		Position = center;
		Rotation = Rotation.FromAxis( Vector3.Up, 4 ) * Input.Rotation;

		var distance = 130.0f * pawn.Scale;
		targetPos = Position + Input.Rotation.Right * ((pawn.CollisionBounds.Maxs.x + 15) * pawn.Scale);
		targetPos += Input.Rotation.Forward * -distance;
		
		var tr = Trace.Ray( Position, targetPos )
			.WithAnyTags( "solid" )
			.Ignore( pawn )
			.Radius( 8 )
			.Run();

		Position = tr.EndPosition;

		FieldOfView = 70;

		Viewer = null;
	}
}
