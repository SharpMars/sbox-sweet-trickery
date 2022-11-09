using Sandbox;
using Sandbox.UI;

namespace SweetTrickery.UI.Playing;

public class Jumpscare : Panel
{
	private float _saturationAmount = 100;
	private float _scaleAmount = 100;
	private float _timeSinceJumpscare;
	private bool _once;
	
	public Jumpscare()
	{
		Style.Width = Length.Percent( 100 );
		Style.SetBackgroundImage( "/ui/terry_scary.png" );
		Style.Display = DisplayMode.None;
	}

	public override void Tick()
	{
		if ( Style.Display == DisplayMode.Flex )
		{
			_timeSinceJumpscare += Time.Delta;

			Style.FilterSaturate = Length.Percent(_saturationAmount);
			Style.FilterContrast = Length.Percent(_saturationAmount);
			Style.BackgroundSizeX = Length.Percent(_scaleAmount);
			Style.BackgroundPositionX = Length.Percent(50 - _scaleAmount / 2);
			Style.BackgroundSizeY = Length.Percent(_scaleAmount);
			Style.BackgroundPositionY = Length.Percent(60 - _scaleAmount / 2);
			_saturationAmount += 1f;
			_scaleAmount += 1f;
			
			if ( !_once )
			{
				Sound.FromScreen( "creepy_sound" );
				_once = true;
			}
			
			if ( _timeSinceJumpscare > 4 )
			{
				Delete( true );
			}
		}
		
		base.Tick();
	}
}
