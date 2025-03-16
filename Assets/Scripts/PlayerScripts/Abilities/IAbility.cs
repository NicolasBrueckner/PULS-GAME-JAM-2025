public interface IAbility
{
	bool IsActive{ get; }
	void ChangeActivityStatus( bool isActive );
}