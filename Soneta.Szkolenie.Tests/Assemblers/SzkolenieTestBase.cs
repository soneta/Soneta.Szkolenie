using Soneta.Business;
using Soneta.CRM;
using Soneta.Test;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Szkolenie.Tests
{
    public abstract class SzkolenieTestBase : DbTransactionTestBase
    {
        public override void ClassSetup()
        {
            LoadAssembly("Soneta.Szkolenie");
            base.TestSetup();
        }

        protected static IRowBuilder<T> NewRow<T>() where T : Row 
            => new RowBuilder<T>();

        protected IRowBuilder<Kontrahent> NowyKontrahent(string kod, string nazwa = null, string rabat = "0")
        {
            IRowBuilder<Kontrahent> builder = new RowBuilder<Kontrahent>();
            return builder
                .Enqueue(kth => kth.Kod = kod)
                .Enqueue(kth => kth.Nazwa = string.IsNullOrEmpty(nazwa) ? kod : nazwa)
                .Enqueue(kth => kth.RabatTowaru = Percent.Parse(rabat));
        }

        protected Kontrahent GetKontrahent(string symbol) => Session.GetCRM().Kontrahenci.WgKodu[symbol];
    }
}
