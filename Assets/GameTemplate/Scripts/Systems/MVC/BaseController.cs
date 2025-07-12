using UnityEngine;

namespace GameTemplate.Scripts.Systems.MVC
{
    public abstract class BaseController<TModel, TView> : IController
        where TModel : IMVCModel
        where TView : IMVCView
    {
        protected TModel model;
        protected TView view;

        protected BaseController(TModel model, TView view)
        {
            this.model = model;
            this.view = view;
        }

        public virtual void Initialize()
        {
            model.Initialize();
            view.Show();
        }
    }
}