using NUnit.Framework;
using Soneta.Test;

namespace Soneta.Szkolenie.Tests {

    [TestFixture]
    public class MaszynaTest : SzkolenieTestBase
    {

        [Test]
        public void NowaMaszynaTest()
        {
            var maszyna = Nowy<Maszyna>()
                .NrBoczny("SP-ABC")
                .Producent("Cessna Ltd.")
                .Model("172p SkyHawk")
                .DataProdukcji("1998-10-12")
                .Build();

            Assert.AreEqual("SP-ABC", maszyna.NrBoczny, "Źle podstawiony nr boczny");
            Assert.AreEqual("Cessna Ltd.", maszyna.Producent, "Źle podstawiony producent");
            Assert.AreEqual("172p SkyHawk", maszyna.Model, "Źle podstawiony model");
            Assert.AreEqual("1998-10-12", maszyna.DataProd, "Źle podstawiona data produkcji");
        }
    }
}