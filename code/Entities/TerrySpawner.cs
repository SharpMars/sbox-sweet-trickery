using Sandbox;
using SandboxEditor;

namespace SweetTrickery;

[HammerEntity, Model(Model = "models/citizen/citizen.vmdl", Archetypes = ModelArchetype.animated_model)]
public sealed partial class TerrySpawner : AnimatedEntity
{
	[Property, Net, ResourceType( "stcloth" )] public string Clothing { get; set; }
	
	[Property, Net, FGDType( "sequence" )] string DefaultAnimation { set; get; }

	public override void Spawn()
	{
		var clothingContainer = new ClothingContainer();
		var clothingDescription = ResourceLibrary.Get<ClothingDescription>(Clothing);
		foreach ( var cloth in clothingDescription.Clothing )
		{
			clothingContainer.Clothing.Add( cloth );
		}

		clothingContainer.DressEntity( this );

		CurrentSequence.Name = DefaultAnimation;
		
		base.Spawn();
	}
}
