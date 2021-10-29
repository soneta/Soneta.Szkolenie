using NUnit.Framework;
using Soneta.CRM;
using Soneta.Test;
using Soneta.Types;

namespace Soneta.Szkolenie.Tests
{

    [TestFixture]
    public class CenaRabatuLotu : RezerwacjeTestBase
    {
        public override void TestSetup()
        {
            base.TestSetup();

            NewRow<Maszyna>().NrBoczny("SP-CDE").Producent("Cessna").Model("172p SkyHawk1").DataProdukcji("1998-10-11").Utwórz();
            NewRow<Lot>().KodUslugi("NL").Nazwa("Nowy Lot").LokalizacjaMiejscowosc("Warszawa").Cena(3000).Utwórz();

            var kthArray = new IRowBuilder<Kontrahent>[] {
                NowyKontrahent("Nowy1", "Nowy Kontrahent 1"),
                NowyKontrahent("Nowy2", "Nowy Kontrahent 2", "10"),
                NowyKontrahent("Nowy3", "Nowy Kontrahent 3", "30")
            }
            .Utwórz();

            // Wybór kontrahentów na których zostanie ustawiony Rabat 
            Context.Set(kthArray);
        }

        [Test]
        public void CenaRezerwacjiLotuZRabatem_DodacFalse_ObnizycFalse()
        {
            RunUstawRabatWorker("20%", false, false);

            var nowarezerwacja1 = NewRow<Rezerwacja>()
                .NrRezerwacji("1")
                .Data("2021-09-21")
                .Klient("Nowy1")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja2 = NewRow<Rezerwacja>()
                .NrRezerwacji("2")
                .Data("2021-09-21")
                .Klient("Nowy2")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja3 = NewRow<Rezerwacja>()
                .NrRezerwacji("3")
                .Data("2021-09-21")
                .Klient("Nowy3")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            Assert.AreEqual(new Currency(2400d), nowarezerwacja1.CenaLotu);
            Assert.AreEqual(new Currency(2400d), nowarezerwacja2.CenaLotu);
            Assert.AreEqual(new Currency(2100d), nowarezerwacja3.CenaLotu);
        }

        [Test]
        public void CenaRezerwacjiLotuZRabatem_DodacTrue_ObnizycFalse()
        {
            RunUstawRabatWorker("20%", true, false);

            var nowarezerwacja1 = NewRow<Rezerwacja>()
                .NrRezerwacji("1")
                .Data("2021-09-21")
                .Klient("Nowy1")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja2 = NewRow<Rezerwacja>()
                .NrRezerwacji("2")
                .Data("2021-09-21")
                .Klient("Nowy2")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja3 = NewRow<Rezerwacja>()
                .NrRezerwacji("3")
                .Data("2021-09-21")
                .Klient("Nowy3")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            Assert.AreEqual(new Currency(2400d), nowarezerwacja1.CenaLotu);
            Assert.AreEqual(new Currency(2100d), nowarezerwacja2.CenaLotu);
            Assert.AreEqual(new Currency(1500d), nowarezerwacja3.CenaLotu);
        }

        [Test]
        public void CenaRezerwacjiLotuZRabatem_DodacFalse_ObnizycTrue()
        {
            RunUstawRabatWorker("20%", false, true);

            var nowarezerwacja1 = NewRow<Rezerwacja>()
                .NrRezerwacji("1")
                .Data("2021-09-21")
                .Klient("Nowy1")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja2 = NewRow<Rezerwacja>()
                .NrRezerwacji("2")
                .Data("2021-09-21")
                .Klient("Nowy2")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            var nowarezerwacja3 = NewRow<Rezerwacja>()
                .NrRezerwacji("3")
                .Data("2021-09-21")
                .Klient("Nowy3")
                .Maszyna("SP-CDE")
                .Lot("NL")
                .Utwórz();

            Assert.AreEqual(new Currency(2400d), nowarezerwacja1.CenaLotu);
            Assert.AreEqual(new Currency(2400d), nowarezerwacja2.CenaLotu);
            Assert.AreEqual(new Currency(2400d), nowarezerwacja3.CenaLotu);
        }
    }
}