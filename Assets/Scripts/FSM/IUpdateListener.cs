namespace FSM
{
    /// <summary> Для машины состояний. Апдейт после кадра </summary>
    public interface IUpdateListener
    {
        /// <summary> Апдрейт каждый кадр </summary>
        /// <param name="deltaTime"> Разница времени между кадрами </param>
        void Update(float deltaTime);
    }

}