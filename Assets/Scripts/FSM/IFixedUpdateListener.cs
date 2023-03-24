namespace FSM
{
    /// <summary> Для машины состояний. Апдейт после кадра </summary>
    public interface IFixedUpdateListener
    {
        /// <summary> Апдрейт каждый кадр </summary>
        /// <param name="fixedDeltaTime"> Разница времени между кадрами </param>
        void FixedUpdate(float fixedDeltaTime);
    }

}