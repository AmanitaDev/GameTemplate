using UnityEngine;
using VContainer;

namespace GameTemplate.Scripts.Systems.MVC
{
    public abstract class BaseController<TModel, TView> : MonoBehaviour, IController
        where TModel : IModel
        where TView : IView
    {
        [Inject] protected TModel Model { get; private set; }
        [Inject] protected TView View { get; private set; }

        protected virtual void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            // Override in child class
        }

        protected virtual void OnModelChanged()
        {
            // Override in child class to update view
        }

        public virtual void Dispose()
        {
            
        }

        protected virtual void OnDestroy()
        {
            Dispose();
        }
    }
}