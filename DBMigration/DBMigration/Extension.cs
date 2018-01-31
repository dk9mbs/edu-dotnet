using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp.Crm.DBMigration
{
    public static class EntityExtensions
    {
        public static void SetValue(this Entity entity, string key, object value ) {
            if (entity.Attributes.ContainsKey(key))
                entity.Attributes[key] = value;
            else
                entity.Attributes.Add(key, value);
        }
    }
}
