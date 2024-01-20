namespace SpaceInvacers
{
	interface IDestructible
	{
		bool Activo { get; set; }
		void Destruir();
	}
}
