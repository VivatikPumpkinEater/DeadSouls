using System;

namespace Systems
{
public abstract class BaseSystem : IDisposable
    {
        // public virtual void Setup(World world) {}
        public abstract void AddActor(IActor actor);
        public abstract void RemoveActor(IActor actor);
        public abstract void Dispose();
    }

    public abstract class BaseSystem<T> : BaseSystem
        where T : IActor
    {
        public override void AddActor(IActor actor)
        {
            if (actor is not T typedActor)
                return;

            OnActorAdded(typedActor);
        }

        public override void RemoveActor(IActor actor)
        {
            if (actor is not T typedActor)
                return;

            OnActorRemoved(typedActor);
        }

        protected virtual void OnActorAdded(T actor)
        {
        }

        protected virtual void OnActorRemoved(T actor)
        {
        }

        public override void Dispose()
        {
        }
    }

    public abstract class BaseSystem<T, T1> : BaseSystem<T>
        where T : IActor
        where T1 : IActor
    {
        public override void AddActor(IActor actor)
        {
            base.AddActor(actor);

            if (actor is not T1 typedActor)
                return;

            OnActorAdded(typedActor);
        }

        public override void RemoveActor(IActor actor)
        {
            base.RemoveActor(actor);

            if (actor is not T1 typedActor)
                return;

            OnActorRemoved(typedActor);
        }

        protected virtual void OnActorAdded(T1 actor)
        {
        }

        protected virtual void OnActorRemoved(T1 actor)
        {
        }
    }

    public abstract class BaseSystem<T, T1, T2> : BaseSystem<T, T1>
        where T : IActor
        where T1 : IActor
        where T2 : IActor
    {
        public override void AddActor(IActor actor)
        {
            base.AddActor(actor);

            if (actor is not T2 typedActor)
                return;

            OnActorAdded(typedActor);
        }

        public override void RemoveActor(IActor actor)
        {
            base.RemoveActor(actor);

            if (actor is not T2 typedActor)
                return;

            OnActorRemoved(typedActor);
        }

        protected virtual void OnActorAdded(T2 actor)
        {
        }

        protected virtual void OnActorRemoved(T2 actor)
        {
        }
    }
    
    public abstract class BaseSystem<T, T1, T2, T3> : BaseSystem<T, T1, T2>
        where T : IActor
        where T1 : IActor
        where T2 : IActor
        where T3 : IActor
    {
        public override void AddActor(IActor actor)
        {
            base.AddActor(actor);

            if (actor is not T3 typedActor)
                return;

            OnActorAdded(typedActor);
        }

        public override void RemoveActor(IActor actor)
        {
            base.RemoveActor(actor);

            if (actor is not T3 typedActor)
                return;

            OnActorRemoved(typedActor);
        }

        protected virtual void OnActorAdded(T3 actor)
        {
        }

        protected virtual void OnActorRemoved(T3 actor)
        {
        }
    }
    
    public abstract class BaseSystem<T, T1, T2, T3, T4> : BaseSystem<T, T1, T2, T3>
        where T : IActor
        where T1 : IActor
        where T2 : IActor
        where T3 : IActor
        where T4 : IActor
    {
        public override void AddActor(IActor actor)
        {
            base.AddActor(actor);

            if (actor is not T4 typedActor)
                return;

            OnActorAdded(typedActor);
        }

        public override void RemoveActor(IActor actor)
        {
            base.RemoveActor(actor);

            if (actor is not T4 typedActor)
                return;

            OnActorRemoved(typedActor);
        }

        protected virtual void OnActorAdded(T4 actor)
        {
        }

        protected virtual void OnActorRemoved(T4 actor)
        {
        }
    }

}