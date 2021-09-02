using NUnit.Framework;
using Soneta.Szkolenie.UI;
using Soneta.Test;
using Soneta.Types;
using System.Reflection;

namespace Soneta.Szkolenie.Tests
{
    public class UstawRabatWorkerParamsTests : DbTransactionTestBase
    {
        public override void TestSetup()
        {
            // Konieczne jest wczytanie assembly dodatku
            LoadAssembly("Soneta.Szkolenie");
            base.TestSetup();
        }

        [Test]
        public void UstawRabatWorkerParamsTest()
        {
            var parameters = new UstawRabatWorkerParams(Context);

            // Sprawdzamy zainicjowane wartości parametrów
            Assert.AreEqual(false, parameters.DodawacRabaty, "Po zainicjowaniu parametrów check \"Dodawać rabaty\" powinien być wyłączony");
            Assert.AreEqual(false, parameters.ObnizacRabaty, "Po zainicjowaniu parametrów check \"Obniżać rabaty\" powinien być wyłączony");
            Assert.AreEqual(false, parameters.IsReadOnlyObnizacRabaty(), "Po zainicjowaniu parametrów check \"Obniżać rabaty\" powinien być dostępny");

            parameters.Rabat = Percent.Parse("30%");
            // Sprawdzamy wartości parametrów po ustawieniu wysokości rabatu
            Assert.AreEqual(Percent.Parse("30%"), parameters.Rabat, "Po ustawieniu wartości rabatu pole \"Rabat\" powinno zawierać ustawioną wartość");
            Assert.AreEqual(false, parameters.DodawacRabaty, "Po ustawieniu wartości rabatu check \"Dodawać rabaty\" powinien być wyłączony");
            Assert.AreEqual(false, parameters.ObnizacRabaty, "Po ustawieniu wartości rabatu check \"Obniżać rabaty\" powinien być wyłączony");
            Assert.AreEqual(false, parameters.IsReadOnlyObnizacRabaty(), "Po ustwaieniu wartości rabatu check \"Obniżać rabaty\" powinien być dostępny");

            parameters.ObnizacRabaty = true;
            // Sprawdzamy wartości parametrów po ustawieniu obniżania rabatów
            Assert.AreEqual(true, parameters.ObnizacRabaty, "Po ustawieniu obniżania rabatów check \"Obniżać rabaty\" powinien być włączony");

            parameters.DodawacRabaty = true;
            // Sprawdzamy wartości parametrów po ustawieniu dodawania rabatów na TAK
            Assert.AreEqual(true, parameters.DodawacRabaty, "Po ustawieniu dodawania rabatów check \"Dodawać rabaty\" powinien być włączony");
            Assert.AreEqual(false, parameters.ObnizacRabaty, "Po ustawieniu dodawania rabatów check \"Obniżać rabaty\" powinien być wyłączony");
            // Sprawdzamy niedostępność przełączania checka "Obniżać rabaty" po ustawieniu "Dadawać rabaty" na TAK
            Assert.AreEqual(true, parameters.IsReadOnlyObnizacRabaty(), "Po ustawieniu dodawania rabatów check \"Obniżać rabaty\" powinien być niedostępny");

            // Sprawdzamy wartości parametrów po ustawieniu dodawania rabatów na NIE
            parameters.DodawacRabaty = false;
            Assert.AreEqual(false, parameters.DodawacRabaty, "Po wyłączeniu dodawania rabatów check \"Dodawać rabaty\" powinien być wyłączony");
            Assert.AreEqual(true, parameters.ObnizacRabaty, "Po wyłączeniu dodawania rabatów check \"Obniżać rabaty\" powinien być włączony");
            // Sprawdzamy dostępność przełączania checka "Obniżać rabaty" po ustawieniu "Dadawać rabaty" na NIE
            Assert.AreEqual(false, parameters.IsReadOnlyObnizacRabaty(), "Po wyłączeniu dodawania rabatów check \"Obniżać rabaty\" powinien być dostępny");
        }
    }
}
