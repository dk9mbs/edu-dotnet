using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;

using Microsoft.Crm.Sdk.Samples;

namespace edu_D365_Discovery.Examples
{
    class CRUDOptions
    {
        public static IOrganizationService createOrganisationProxy()
        {
            //var connString = "https://Url=d365_url; Username=username; Password=password; authtype=Office365";
            //CrmServiceClient conn = new Microsoft.Xrm.Tooling.Connector.CrmServiceClient(connString);
            //var service = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

            ServerConnection serverConnect = new ServerConnection();
            ServerConnection.Configuration config = serverConnect.GetServerConfiguration();
            IOrganizationService service = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(config);

            return service;
        }

        public static void update(IOrganizationService service) {
            /*
             * here we are changing an account 
            */
            Entity account = service.Retrieve("account", new Guid("6AB8D765-2F07-E811-A957-000D3A2AC06D"),
                new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "accountid", "address1_city" }));
            Console.WriteLine(account.GetAttributeValue<string>("address1_city"));
            account.Attributes["address1_city"] = "Bad";
            update(service, account);
        }

        public static void create(IOrganizationService service) {
            /*
             * Now we are createing an account 
            */
            Entity account = account = new Entity("account");
            account.Attributes["mp_createdon"] = null;
            account.Attributes["mp_createdby"] = null;
            account.Attributes["mp_modifiedon"] = null;
            account.Attributes["mp_modifiedby"] = null;
            account.Attributes["name"] = "Webservice";
            account.Attributes["accountid"] = service.Create(account);
            update(service, account);
        }

        public static DataCollection<Entity> getOpportunityProducts(IOrganizationService service, Guid opportunityGuid) {
            Microsoft.Xrm.Sdk.Query.QueryExpression query = new Microsoft.Xrm.Sdk.Query.QueryExpression 
            { 
                EntityName = "opportunityproduct", 
                ColumnSet=new Microsoft.Xrm.Sdk.Query.ColumnSet (new string[] { "opportunityproductid", "volumediscountamount","productname" }), 
                Criteria= new Microsoft.Xrm.Sdk.Query.FilterExpression 
                { 
                    Conditions={
                        new Microsoft.Xrm.Sdk.Query.ConditionExpression {
                            AttributeName = "opportunityid",
                            Operator = Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal,
                            Values = { opportunityGuid }
                        }
                    }    
                }
            };

            return service.RetrieveMultiple(query).Entities;
        }

        public static void update(IOrganizationService service, Entity entity){
            service.Update(entity);
        }
    }
}
