using System.Threading;
using UnityEngine;

namespace Character
{
    public class BodyController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;

        private CancellationTokenSource _cts;

        public CapsuleCollider Collider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<CapsuleCollider>();

                return _collider;
            }
        }

        protected Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();

                return _rigidbody;
            }
        }

        public float ColliderRadius => Collider.radius;

        public Vector3 Position => transform.position;
        public float Mass => _rigidbody.mass;

        public void ResetVelocity()
        {
            Rigidbody.velocity = new(0, Rigidbody.velocity.y, 0);
            Rigidbody.angularVelocity = Vector3.zero;
        }

        public void EnablePhysics(bool value)
        {
            Rigidbody.isKinematic = !value;
        }

        public void EnableCollider(bool value)
        {
            Collider.enabled = value;
        }

        public void AddForce(Vector3 force)
        {
            Rigidbody.AddForce(force, ForceMode.Impulse);
        }
        
        public void SetRotation(Quaternion quaternion)
        {
            Rigidbody.MoveRotation(quaternion);
        }
        
        public void LookAt(Vector3 target, float rotationSpeed)
        {
            var targetDirection = target - transform.position;
            targetDirection.y = 0;
            
            var lookRotation = Quaternion.LookRotation(targetDirection);
            var nextRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
            
            SetRotation(nextRotation);
        }

    }
}