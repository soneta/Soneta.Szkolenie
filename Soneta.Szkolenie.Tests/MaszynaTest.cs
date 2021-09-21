using NUnit.Framework;
using Soneta.Business;
using Soneta.Test;
using Soneta.Types;

namespace Soneta.Szkolenie.Tests {

    public class MaszynaTest : SzkolenieTestBase
    {
        public override void TestSetup()
        {
            LoadAssembly("Soneta.Szkolenie");
            base.TestSetup();
        }

        [Test]
        public void NowaMaszynaTest()
        {
            var maszynaBld = Nowy<Maszyna>()
                .DataProdukcji("1998-10-12")
                .Producent("Cessna Ltd.")
                .Model("172p SkyHawk")
                .NrBoczny("SP-DEF");

            Maszyna maszyna = null;

            Assert.DoesNotThrow(() => maszyna = maszynaBld.Utwórz());
            Assert.AreEqual("Cessna Ltd.", maszyna.Producent, "Źle podstawiony producent");
            Assert.AreEqual("172p SkyHawk", maszyna.Model, "Źle podstawiony model");
            Assert.AreEqual(Date.Parse("1998-10-12"), maszyna.DataProd, "Źle podstawiona data produkcji");
        }
    }
}