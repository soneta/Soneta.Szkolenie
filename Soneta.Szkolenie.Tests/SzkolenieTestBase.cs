using Soneta.Business;
using Soneta.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Szkolenie.Tests
{
    public abstract class SzkolenieTestBase : DbTransactionTestBase
    {
        public override void TestSetup()
        {
            LoadAssembly("Soneta.Szkolenie");
            base.TestSetup();
        }

        protected static IRowBuilder<T> NewRow<T>() where T : Row 
            => new RowBuilder<T>();
    }
}
