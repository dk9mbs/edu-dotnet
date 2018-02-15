using Microsoft.Xrm.Sdk;
using System;

namespace Mp.Crm.DBMigration
{
    public class Migration : IPlugin
    {
        public static Entity getUserAccessEntity(IOrganizationService service, ITracingService tracer, Entity srcEntity, Boolean update)
        {
            Microsoft.Xrm.Sdk.Query.QueryExpression query = new Microsoft.Xrm.Sdk.Query.QueryExpression
            {
                EntityName = "mp_useraccess",
                ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "mp_createdby", "mp_createdon", "mp_modifiedby", "mp_modifiedon" }),
                Criteria = new Microsoft.Xrm.Sdk.Query.FilterExpression
                {
                    Conditions ={
                        new Microsoft.Xrm.Sdk.Query.ConditionExpression {
                            AttributeName = "mp_useraccessid",
                            Operator = Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal,
                            Values = { srcEntity.Id }
                        }
                    }
                }
            };

            DataCollection<Entity> list = service.RetrieveMultiple(query).Entities;
            if (list.Count >= 1)
            {
                tracer.Trace($"Found in mp_useraccess:{list[0].Id}");
                return list[0];
            } else 
            {
                /*
                 * if update then get modified attributes from datenbase 
                 */
                if (update)
                {
                    tracer.Trace($"Not found in mp_useraccess return load the original entity from database!");
                    Entity entity = service.Retrieve(srcEntity.LogicalName, srcEntity.Id,
                        new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "mp_createdby", "mp_createdon", "mp_modifiedby", "mp_modifiedon" }));
                    return entity;
                } else
                {
                    tracer.Trace($"Not found in mp_useraccess return the original entity!");
                    return srcEntity;
                }

            }
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            
            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                Entity userAccessEntity = getUserAccessEntity(service, tracer, entity, context.MessageName == "Update");

                tracer.Trace(entity.LogicalName);

                if(context.MessageName == "Create")
                {
                    if (!userAccessEntity.Attributes.ContainsKey("mp_createdby")) throw new Exception("Attribute mp_createdby not exists!");
                    if (!userAccessEntity.Attributes.ContainsKey("mp_createdon")) throw new Exception("Attribute mp_createdon not exists!");
                    if (!userAccessEntity.Attributes.ContainsKey("mp_modifiedby")) throw new Exception("Attribute mp_modifiedby not exists!");
                    if (!userAccessEntity.Attributes.ContainsKey("mp_modifiedon")) throw new Exception("Attribute mp_modifiedon not exists!");

                    EntityReference createdUser = userAccessEntity.GetAttributeValue<EntityReference>("mp_createdby");
                    DateTime createdDate = userAccessEntity.GetAttributeValue<DateTime>("mp_createdon");

                    if (!(createdUser == null | createdDate == null))
                    { 
                        entity.SetValue("createdby", createdUser);
                        entity.SetValue("createdon", createdDate);
                    }

                    EntityReference modifiedUser = userAccessEntity.GetAttributeValue<EntityReference>("mp_modifiedby");
                    DateTime modifiedDate = userAccessEntity.GetAttributeValue<DateTime>("mp_modifiedon");

                    //tracer.Trace($"Modified Date:{modifiedDate}");
                    if (!(modifiedUser == null | modifiedDate == null))
                    {
                        entity.SetValue("modifiedby", modifiedUser);
                        entity.SetValue("modifiedon", modifiedDate);
                    }

                }

                if (context.MessageName == "Update")
                {

                    EntityReference modifiedUser = userAccessEntity.GetAttributeValue<EntityReference>("mp_modifiedby");
                    DateTime modifiedDate = userAccessEntity.GetAttributeValue<DateTime>("mp_modifiedon");
                    tracer.Trace($"Modified Date:{modifiedDate}");
                    if (!(modifiedUser == null | modifiedDate == null))
                    {
                        entity.SetValue("modifiedby", modifiedUser);
                        entity.SetValue("modifiedon", modifiedDate);
                    }

                }
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}
