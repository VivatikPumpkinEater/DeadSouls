namespace FSM
{
    /// <summary> Для машины состояний. Апдейт после кадра </summary>
    public interface ILateUpdateListener
    {
        /// <summary> Апдейт после кадра </summary>
        void LateUpdate();
    }
}