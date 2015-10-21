namespace Web.Common.ActionResults
{
    using System;
    using System.Web.Mvc;

    using QueryContracts.Common;

    using ServiceAgents.Common;

    using StructureMap;

    public class QueryViewResult<T> : ActionResult where T : class
    {
        private QueryParameter parameter;
        private Func<T, object> queryResultProperty;

        public QueryViewResult(QueryParameter parameter)
        {
            this.parameter = parameter;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var view = new ViewResult
                        {
                            ViewData = context.Controller.ViewData, 
                            TempData = context.Controller.TempData
                        };

            if (context.Controller.ViewData.ModelState.IsValid)
            {
                var backendClient = ObjectFactory.GetInstance<IBackendClient>();
                object objectModel = backendClient.ExecuteQuery(parameter);
                if (queryResultProperty != null)
                    objectModel = queryResultProperty(objectModel as T);
                view.ViewData.Model = objectModel;

            }
            view.ExecuteResult(context);
        }
        public QueryViewResult<T> ReturnProperty(Func<T, object> queryResultProperty)
        {
            this.queryResultProperty = queryResultProperty;
            return this;
        }
    }
}
