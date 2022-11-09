using Sandbox;
using SandboxEditor;

namespace SweetTrickery;

[HammerEntity, Model(Archetypes = ModelArchetype.animated_model), SupportsSolid]
public sealed partial class HouseDoor : KeyframeEntity, IUse
{
	private float _originalYaw;
	private float _finalYaw;
	[Net] private bool _isOpening { get; set; } = false;
	[Net] private bool _shouldClose { get; set; } = false;
	[Net] private TimeSince _timeSinceOpened { get; set; } = 0f;
	[Net] private float _openingProgress { get; set; } = 0f;
	[Property] public EntityTarget ParticleSystem { get; set; }

	private const float Speed = .01f;
	
	public override void Spawn()
	{
		base.Spawn();

		SetupPhysicsFromModel( PhysicsMotionType.Keyframed );
		_originalYaw = Rotation.Yaw();
		_finalYaw = _originalYaw + 90;
		if( _finalYaw >= 360 ) _finalYaw -= 360;
	}

	public bool OnUse( Entity user )
	{
		if ( _isOpening == false && _shouldClose == false )
		{
			_isOpening = true;
		}
		return true;
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	[Event.Tick.Server]
	private void Tick()
	{
		ParticleSystem.GetTarget<ParticleSystemEntity>().IsActive = SweetTrickery.Instance.CurrentDoor == this;

		if ( _timeSinceOpened >= 2 && _shouldClose )
			_isOpening = true;
		
		if ( !_isOpening )
			return;

		if ( !_shouldClose )
		{
			if ( _openingProgress < 1 )
			{
				_timeSinceOpened = 0f;
				_openingProgress += Time.Delta;
			}
			else
			{
				Rotation = Rotation.FromYaw( _finalYaw );
				_isOpening = false;
				_shouldClose = true;
		
				if ( SweetTrickery.Instance.CurrentDoor != this )
					return;
		
				ushort score;
		
				if ( SweetTrickery.LastDoorOpen > 30 )
				{
					score = 30;
				}
				else if ( SweetTrickery.LastDoorOpen > 20 )
				{
					score = 60;
				}
				else if ( SweetTrickery.LastDoorOpen > 10 )
				{
					score = 90;
				}
				else
				{
					score = 120;
				}
			
				SweetTrickery.LastDoorOpen = 0;
				Sound.FromScreen( "candy" );
				SweetTrickery.Instance.Score += score;
				SweetTrickery.ChooseNewDoor();
			}
		}
		else
		{
			if ( _openingProgress > 0 )
			{
				_openingProgress -= Time.Delta;
			}
			else
			{
				Rotation = Rotation.FromYaw( _originalYaw );
				_isOpening = false;
				_shouldClose = false;
			}
		}
		
		Rotation = Rotation.FromYaw( _originalYaw.LerpTo( _finalYaw, _openingProgress ) );
	}
}
