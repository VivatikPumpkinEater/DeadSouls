namespace Systems.UpdateSystem
{
    public interface ILateUpdate : IActor
    {
        public void ManualLateUpdate();
    }

}