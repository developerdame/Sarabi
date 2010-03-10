using System;
using System.Configuration;
using log4net;
using Neoworks.Utils.CommandLine;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Sarabi.Data.NHibernateMaps;
using SharpArch.Data.NHibernate;

namespace Sarabi.CommandTool.Commands
{
    public abstract class Command : CommandBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Command));
        public override void Execute()
        {
            Log.DebugFormat("Starting NHibernate session");

            var simpleStorage = new SimpleSessionStorage();
            var cfg = NHibernateSession.Init(
                simpleStorage,
                new string[] { "Sarabi.Data.dll" },
                new AutoPersistenceModelGenerator().Generate(),
                "NHibernate.config");

            var rebuildDb = ConfigurationManager.AppSettings.Get("RebuildDB");
            if (string.IsNullOrEmpty(rebuildDb) == false && Convert.ToBoolean(rebuildDb))
                new SchemaExport(cfg).Create(false, true);

            var factory = NHibernateSession.GetDefaultSessionFactory();
            using (var session = factory.OpenSession())
            using(var transation = session.BeginTransaction())
            {
                Execute(session);    
                transation.Commit();
            }
        }

        protected abstract void Execute(ISession session);
    }
}