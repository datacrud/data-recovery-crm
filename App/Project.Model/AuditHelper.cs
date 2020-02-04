﻿using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Project.Model
{
    public class AuditHelper
    {
        public string GetIdentityJson(DbEntityEntry entry, Type entityType)
        {
            string identityJson = string.Empty;
            foreach (var field in entityType.GetProperties().Where(o => o.CustomAttributes.FirstOrDefault(oo => oo.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) != null))
            {
                if (identityJson.Length > 0)
                {
                    identityJson += ", ";
                }

                identityJson += $@"""{field.Name}"":{GetFieldValue(field.Name, (entry.State == EntityState.Deleted ? entry.OriginalValues : entry.CurrentValues))}";
            }
            identityJson = $"{{ {identityJson} }}";
            return identityJson;
        }

        private object GetFieldValue(string name, DbPropertyValues values)
        {
            var val = values[name];
            return val == null ? "null" : (IsNumber(val) ? val.ToString() : $@"""{val}""");
        }

        public static Type GetEntityType(DbEntityEntry entry)
        {
            Type entityType = entry.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;
            return entityType;
        }

        public static string GetTableName(ObjectContext context, Type entityType)
        {
            string entityTypeName = entityType.Name;

            EntityContainer container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            string tableName = (from meta in container.BaseEntitySets
                                where meta.ElementType.Name == entityTypeName
                                select meta.Name).First();
            return tableName;
        }

        public static string GetAsJson(DbPropertyValues values)
        {
            string json = string.Empty;
            if (values != null)
            {
                foreach (var propertyName in values.PropertyNames)
                {
                    if (json.Length > 0)
                    {
                        json += ", ";
                    }
                    var val = values[propertyName];
                    json += $@"""{propertyName}"":{(val == null ? "null" : (IsNumber(val) ? val.ToString() : $@"""{val}"""))}";
                }
            }
            return $"{{ {json} }}";
        }

        public static bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}