using Sandbox;

namespace SweetTrickery;

public sealed partial class Player : BasePlayer
{
	private readonly ClothingContainer _clothing = new();
	[Net] public static Player Instance { get; private set; }

	public Player() { }

	public Player( Client cl ) : this()
	{
		_clothing.LoadFromClient( cl );
		Instance = this;
	}

	public override void Respawn()
	{
		Model = Model.Load( "models/citizen/citizen.vmdl" );
		CameraMode = new Camera();
		Controller = new WalkController();
		Animator = new StandardPlayerAnimator();

		base.Respawn();

		_clothing.DressEntity( this );
		Scale = .8f;
	}

	public override void Simulate( Client cl )
	{
		TickPlayerUse();

		base.Simulate( cl );
	}
}
