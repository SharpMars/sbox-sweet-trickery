using Sandbox;
using Sandbox.UI;

namespace SweetTrickery.UI;

public sealed class MainMenuHud : HudEntity<RootPanel>
{
	public MainMenuHud()
	{
		RootPanel.SetTemplate( "UI/MainMenu/MainMenuHud.html" );
		SetThumbnail();

		About.Instance.Style.Display = DisplayMode.None;
		ScoreShow.Instance.Style.Display = DisplayMode.None;
	}
	
	private async void SetThumbnail()
	{
		var pgk = await Package.Fetch( Global.MapName, true );
		if ( pgk == null ) return;
		RootPanel.Style.SetBackgroundImage( pgk.Background );
	}
}
