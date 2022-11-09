using Sandbox;

namespace SweetTrickery;

[GameResource("Clothing Description", "stcloth", "Clothing Decription", Icon = "iron")]
public sealed class ClothingDescription : GameResource
{
	[ResourceType( "clothing" )]
	public Clothing[] Clothing { get; set; }
}
