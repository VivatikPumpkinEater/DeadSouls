using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Systems.UpdateSystem
{
    public class UpdateSystem : BaseSystem<IUpdate, IFixedUpdate, ILateUpdate>
    {
        private readonly List<IUpdate> _updateActors = new();
        private readonly List<IFixedUpdate> _fixedUpdateActors = new();
        private readonly List<ILateUpdate> _lateUpdateActors = new();

        private float _updateFactor = 1;

        public static float DeltaTime { private set; get; }
        public static float FixedDeltaTime { private set; get; }

        public UpdateSystem()
        {
            UnityEventsListener.ApplicationFixedUpdate += OnFixedUpdate;
            UnityEventsListener.ApplicationUpdate += OnUpdate;
        }

        private void OnFixedUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Q))
                return;
#endif

            FixedDeltaTime = Time.fixedDeltaTime * _updateFactor;

            for (var index = 0; index < _fixedUpdateActors.Count; index++)
            {
                var actor = _fixedUpdateActors[index];
                actor.ManualFixedUpdate(FixedDeltaTime);
            }
        }

        private void OnUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Q))
                return;
#endif
            if (Mathf.Approximately(0.0f, _updateFactor))
                return;

            DeltaTime = Time.deltaTime * _updateFactor;

            for (var index = 0; index < _updateActors.Count; index++)
            {
                var actor = _updateActors[index];
                actor.ManualUpdate(DeltaTime);
            }

            DOTween.ManualUpdate(DeltaTime, DeltaTime);

            for (var index = 0; index < _lateUpdateActors.Count; index++)
            {
                var actor = _lateUpdateActors[index];
                actor.ManualLateUpdate();
            }
        }

        protected override void OnActorAdded(IUpdate actor) => _updateActors.Add(actor);

        protected override void OnActorRemoved(IUpdate actor) => _updateActors.Remove(actor);

        protected override void OnActorAdded(IFixedUpdate actor) => _fixedUpdateActors.Add(actor);

        protected override void OnActorRemoved(IFixedUpdate actor) => _fixedUpdateActors.Remove(actor);

        protected override void OnActorAdded(ILateUpdate actor) => _lateUpdateActors.Add(actor);

        protected override void OnActorRemoved(ILateUpdate actor) => _lateUpdateActors.Remove(actor);

        public void ChangeUpdateFactor(float value, bool isAnimated, float duration)
        {
            if (!isAnimated)
            {
                _updateFactor = value;
                return;
            }

            DOTween.To(() => _updateFactor, target => _updateFactor = target, value, duration)
                .SetTarget(_updateFactor);
        }

        public override void Dispose()
        {
            base.Dispose();

            UnityEventsListener.ApplicationFixedUpdate -= OnFixedUpdate;
            UnityEventsListener.ApplicationUpdate -= OnUpdate;
        }
    }
}