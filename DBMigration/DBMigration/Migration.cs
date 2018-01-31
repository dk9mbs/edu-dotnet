using Microsoft.Xrm.Sdk;
using System;

namespace Mp.Crm.DBMigration
{
    public class Migration : IPlugin
    {
        #region Secure/Unsecure Configuration Setup
        private string _secureConfig = null;
        private string _unsecureConfig = null;

        public Migration(string unsecureConfig, string secureConfig)
        {
            _secureConfig = secureConfig;
            _unsecureConfig = unsecureConfig;
        }
        #endregion
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {

                Entity entity = (Entity)context.InputParameters["Target"];
                var calculatedEntity = new Entity(entity.LogicalName, entity.Id);
                tracer.Trace(entity.LogicalName);
                
                EntityReference createdUser = entity.GetAttributeValue<EntityReference>("mp_createdby");
                DateTime createdDate = entity.GetAttributeValue<DateTime>("mp_createdon");

                EntityReference modifiedUser = entity.GetAttributeValue<EntityReference>("mp_modifiedby");
                DateTime modifiedDate = entity.GetAttributeValue<DateTime>("mp_modifiedon");

                if (createdUser == null | createdDate == null)
                {
                    tracer.Trace("no create user or date in override field!!!");
                } else
                {
                    entity.SetValue("createdby", createdUser);
                    entity.SetValue("createdon", createdDate);
                    entity.SetValue("mp_createdby", null);
                    entity.SetValue("mp_createdon", null);
                }

                if (modifiedUser == null | modifiedDate == null)
                {
                    tracer.Trace("no create user or date in override field!!!");
                }
                else
                {
                    entity.SetValue("modifiedby", modifiedUser);
                    entity.SetValue("modifiedon", modifiedDate);
                    entity.SetValue("mp_modifiedby", null);
                    entity.SetValue("mp_modifiedon", null);
                }

            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
