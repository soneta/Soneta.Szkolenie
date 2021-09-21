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
    public class UstawRabatWorkerTests : SzkolenieTestBase
    {
        public override void TestSetup()
        {
            base.TestSetup();

            var listKontrahenciBuilders = new List<IRowBuilder<Kontrahent>>();
            var ctxList = new List<Kontrahent>();

            IRowBuilder<Kontrahent> builder = null;
            // Dodanie kontrahentów za pomocą utworzonych Assemblerów 
            builder = NowyKontrahent("Nowy1");              listKontrahenciBuilders.Add(builder); ctxList.Add(builder.Build());
            builder = NowyKontrahent("Nowy2", rabat: "10"); listKontrahenciBuilders.Add(builder); ctxList.Add(builder.Build());
            builder = NowyKontrahent("Nowy3", rabat: "30"); listKontrahenciBuilders.Add(builder); ctxList.Add(builder.Build());
            builder = NowyKontrahent("Nowy4");              listKontrahenciBuilders.Add(builder);
            builder = NowyKontrahent("Nowy5", rabat: "10"); listKontrahenciBuilders.Add(builder);
            builder = NowyKontrahent("Nowy6", rabat: "30"); listKontrahenciBuilders.Add(builder);

            listKontrahenciBuilders.ToArray().Utwórz();
            Context.Set(ctxList.ToArray());
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

        private void RunUstawRabatWorker(string rabat, bool dodawac, bool obnizac)
        {
            var worker = Context.CreateObject(null,
                typeof(UstawRabatWorker),
                new UstawRabatWorkerParams(Context)
                {
                    Rabat = Percent.Parse(rabat),
                    DodawacRabaty = dodawac,
                    ObnizacRabaty = obnizac
                }) as UstawRabatWorker;

            if (worker.UstawRabat() is MessageBoxInformation result)
                result.YesHandler();
        }

        private IRowBuilder<Kontrahent> NowyKontrahent(string kod, string nazwa = null, string rabat = "0")
        {
            IRowBuilder<Kontrahent> builder = new RowBuilder<Kontrahent>();
            return builder
                .Enqueue(kth => kth.Kod = kod)
                .Enqueue(kth => kth.Nazwa = string.IsNullOrEmpty(nazwa) ? kod : nazwa)
                .Enqueue(kth => kth.RabatTowaru = Percent.Parse(rabat));
        }

        private Kontrahent GetKontrahent(string symbol) => Session.GetCRM().Kontrahenci.WgKodu[symbol];
    }
}
