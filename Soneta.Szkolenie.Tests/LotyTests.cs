using NUnit.Framework;
using Soneta.Types;
using Soneta.Test;

namespace Soneta.Szkolenie.Tests
{
    internal class LotyTests : SzkolenieTestBase
    {
        [Test]
        public void NowyLotTest()
        {
            var lot = NewRow<Lot>()
                .KodUslugi("NL")
                .Nazwa("Nowy Lot")
                .LokalizacjaMiejscowosc("Warszawa")
                .Cena(3000)
                .Utwórz();

            Assert.AreEqual("NL", lot.KodUslugi, "Źle podstawiony kod usługi");
            Assert.AreEqual("Nowy Lot", lot.Nazwa, "Źle podstawiona nazwa");
            Assert.AreEqual("Warszawa", lot.LokalizacjaMiejscowosc, "Źle podstawiona miejscowość");
            Assert.AreEqual(new Currency(3000d), lot.Cena, "Źle podstawiona cena");
        }
    }
}
