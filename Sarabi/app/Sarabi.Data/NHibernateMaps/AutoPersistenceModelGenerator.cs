using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Sarabi.Core;
using Sarabi.Data.NHibernateMaps.Conventions;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Sarabi.Data.NHibernateMaps
{

    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        #region IAutoPersistenceModelGenerator Members

        public AutoPersistenceModel Generate()
        {
            var mappings = new AutoPersistenceModel();
            mappings.AddEntityAssembly(typeof(Class1).Assembly).Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.Setup(GetSetup());
           
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();

            return mappings;

        }

        #endregion

        private Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
            {
                c.FindIdentity = type => type.Name == "Id";
            };
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.ForeignKeyConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.HasManyConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.HasManyToManyConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.ManyToManyTableNameConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.PrimaryKeyConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.ReferenceConvention>();
                c.Add<Sarabi.Data.NHibernateMaps.Conventions.TableNameConvention>();
            };
        }

        /// <summary>
        /// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
        /// </summary>

        private bool GetAutoMappingFilter(Type t)
        {
            return t.IsSubclassOf(typeof(Entity));
        }
    }
}
