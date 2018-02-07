using Microsoft.Xrm.Sdk;
using System;

namespace Mp.Crm.DBMigration
{
    public class Migration : IPlugin
    {
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

                if(context.MessageName == "Create")
                {
                    if (!entity.Attributes.ContainsKey("mp_createdby")) throw new Exception("Attribute mp_createdby not exists!");
                    if (!entity.Attributes.ContainsKey("mp_createdon")) throw new Exception("Attribute mp_createdon not exists!");
                    if (!entity.Attributes.ContainsKey("mp_modifiedby")) throw new Exception("Attribute mp_modifiedby not exists!");
                    if (!entity.Attributes.ContainsKey("mp_modifiedon")) throw new Exception("Attribute mp_modifiedon not exists!");

                    EntityReference createdUser = entity.GetAttributeValue<EntityReference>("mp_createdby");
                    DateTime createdDate = entity.GetAttributeValue<DateTime>("mp_createdon");

                    if (!(createdUser == null | createdDate == null))
                    { 
                        entity.SetValue("createdby", createdUser);
                        entity.SetValue("createdon", createdDate);
                    }
                }

                if (context.MessageName == "Update")
                {
                    Entity src = service.Retrieve(entity.LogicalName, entity.Id,
                        new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "mp_modifiedby", "mp_modifiedon", "mp_modifiedonbehalfby" }));

                    EntityReference modifiedUser = src.GetAttributeValue<EntityReference>("mp_modifiedby");
                    DateTime modifiedDate = src.GetAttributeValue<DateTime>("mp_modifiedon");
                    EntityReference modifiedonbehalfby = src.GetAttributeValue<EntityReference>("mp_modifiedonbehalfby");

                    if (!(modifiedUser == null | modifiedDate == null))
                    {
                        entity.SetValue("modifiedby", modifiedUser);
                        entity.SetValue("modifiedon", modifiedDate);
                    }

                    entity.SetValue("modifiedonbehalfby", modifiedonbehalfby);
                }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
