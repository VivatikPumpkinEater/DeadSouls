namespace Systems.UpdateSystem
{
    public interface IFixedUpdate : IActor
    {
        public void ManualFixedUpdate(float fixedDeltaTime);
    }

}