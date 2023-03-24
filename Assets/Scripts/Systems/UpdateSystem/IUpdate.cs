namespace Systems.UpdateSystem
{
    public interface IUpdate : IActor
    {
        public void ManualUpdate(float deltaTime);
    }

}