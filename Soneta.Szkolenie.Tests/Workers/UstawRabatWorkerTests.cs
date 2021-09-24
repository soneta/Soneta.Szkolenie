using NUnit.Framework;
using Soneta.Szkolenie.UI;
using Soneta.Types;
using Soneta.CRM;
using Soneta.Business.UI;
using Soneta.Szkolenie.Tests;
using System;
using System.Collections.Generic;
using Soneta.Test;

namespace Soneta.Szkolenie.Tests
{
    [TestFixture]
    public class UstawRabatWorkerTests : RezerwacjeTestBase
    {
        public override void TestSetup()
        {
            base.TestSetup();

            var listKontrahenciBuilders = new List<IRowBuilder<Kontrahent>>();

            IRowBuilder<Kontrahent> builder = null;
            // Dodanie kontrahentów za pomocą utworzonych Assemblerów 
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy1"));
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy2", rabat: "10"));
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy3", rabat: "30"));
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy4"));
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy5", rabat: "10"));
            listKontrahenciBuilders.Add(NowyKontrahent("Nowy6", rabat: "30"));

            listKontrahenciBuilders.ToArray().Utwórz();

            Context.Set(new Kontrahent[]
            {
                GetKontrahent("Nowy1"),
                GetKontrahent("Nowy2"),
                GetKontrahent("Nowy3")
            });
        }

        [Test]
        public void UstawRabatWorkerTest_DodacFalse_ObnizacFalse()
        {
            Assert.DoesNotThrow(() => RunUstawRabatWorker("20%", false, false),
                "Wywołanie akcji \"Ustaw rabat\" spowodowało błąd.");

            // Sprawdzenie poprawności działania workera - zmiana ustawinia Rabatu na Kontrahencie 
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy1").RabatTowaru);
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy2").RabatTowaru);
            Assert.AreEqual(Percent.Parse("30%"), GetKontrahent("Nowy3").RabatTowaru);

            // Sprawdzenie poprawności działania workera - brak zmiany Rabatu na Kontrahencie 
            Assert.AreEqual(Percent.Parse("0%"), GetKontrahent("Nowy4").RabatTowaru);
            Assert.AreEqual(Percent.Parse("10%"), GetKontrahent("Nowy5").RabatTowaru);
            Assert.AreEqual(Percent.Parse("30%"), GetKontrahent("Nowy6").RabatTowaru);
        }

        [Test]
        public void UstawRabatWorkerTest_DodacFalse_ObnizycTrue()
        {
            Assert.DoesNotThrow(() => RunUstawRabatWorker("20%", false, true),
                "Wywołanie akcji \"Ustaw rabat\" spowodowało błąd.");

            // Sprawdzenie poprawności działania workera - zmiana ustawinia Rabatu na Kontrahencie
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy1").RabatTowaru);
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy2").RabatTowaru);
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy3").RabatTowaru);

            // Sprawdzenie poprawności działania workera - brak zmiany Rabatu na Kontrahencie
            Assert.AreEqual(Percent.Parse("0%"), GetKontrahent("Nowy4").RabatTowaru);
            Assert.AreEqual(Percent.Parse("10%"), GetKontrahent("Nowy5").RabatTowaru);
            Assert.AreEqual(Percent.Parse("30%"), GetKontrahent("Nowy6").RabatTowaru);
        }

        [Test]
        public void UstawRabatWorkerTest_DodacTrue_ObnizycFalse()
        {
            Assert.DoesNotThrow(() => RunUstawRabatWorker("20%", true, false),
                "Wywołanie akcji \"Ustaw rabat\" spowodowało błąd.");

            // Sprawdzenie poprawności działania workera - zmiana ustawinia Rabatu na Kontrahencie
            Assert.AreEqual(Percent.Parse("20%"), GetKontrahent("Nowy1").RabatTowaru);
            Assert.AreEqual(Percent.Parse("30%"), GetKontrahent("Nowy2").RabatTowaru);
            Assert.AreEqual(Percent.Parse("50%"), GetKontrahent("Nowy3").RabatTowaru);

            // Sprawdzenie poprawności działania workera - brak zmiany Rabatu na Kontrahencie
            Assert.AreEqual(Percent.Parse("0%"), GetKontrahent("Nowy4").RabatTowaru);
            Assert.AreEqual(Percent.Parse("10%"), GetKontrahent("Nowy5").RabatTowaru);
            Assert.AreEqual(Percent.Parse("30%"), GetKontrahent("Nowy6").RabatTowaru);
        }

    }
}
