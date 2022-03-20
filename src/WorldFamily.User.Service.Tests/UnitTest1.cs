using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WorldFamily.User.Service.Data;
using WorldFamily.User.Service.Dtos;
using WorldFamily.User.Service.Entities;
using WorldFamily.User.Service.Services;

namespace WorldFamily.User.Service.Test
{
    public class Tests
    {
        private IUserService userService { set; get; }

        private static List<Member> FakeMemberList()
        {
            return new List<Member> {
                new Member("S010200", new DateTime(2019, 1, 3), new DateTime(2022, 1, 3, 0, 0, 0, DateTimeKind.Local), "Expired", "0912333550", "Main"),
                new Member("S010201", new DateTime(2019, 1, 3), new DateTime(2022, 7, 4, 0, 0, 0, DateTimeKind.Local), "Normal", "0912123551", "Main"),
                new Member("S010203", new DateTime(2019, 1, 3), new DateTime(2022, 7, 5, 0, 0, 0, DateTimeKind.Local), "Cancel", "0912333551", "Main"),
                new Member("S010204", new DateTime(2021, 3, 1), new DateTime(2022, 3, 1, 0, 0, 0, DateTimeKind.Local), "Expired", "0912333552", "full"),
                new Member("S010205", new DateTime(2021, 3, 1), new DateTime(2022, 3, 2, 0, 0, 0, DateTimeKind.Local), "Expired", "0912333553", "full"),
                new Member("S010208", new DateTime(2022, 1, 1), new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Local), "Normal", "0922210233", "Main"),
                new Member("S010209", new DateTime(2022, 1, 1), new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Local), "Cancel", "0922210234", "Main"),
                new Member("S010210", new DateTime(2022, 1, 1), new DateTime(2023, 1, 3, 0, 0, 0, DateTimeKind.Local), "Normal", "0922210235", "Main")
            };
        }

        private static List<Customer> FakeCustomerList()
        {
            return new List<Customer> {
                new Customer("S010200", "C020200", new DateTime(2019, 1, 3), "Cancel", "0912333550", "Main"),
                new Customer("S010201", "C020201", new DateTime(2019, 1, 3), "Normal", "0912123551", "small"),
                new Customer("S010200", "C020202", new DateTime(2019, 1, 3), "Normal", "0912333550", "full"),
                new Customer("S010203", "C020203", new DateTime(2019, 1, 3), "Cancel", "0912333551", "Main"),
                new Customer("S010204", "C020204", new DateTime(2021, 3, 1), "Normal", "0912333552", "full"),
                new Customer("S010205", "C020205", new DateTime(2021, 3, 1), "Normal", "0912333553", "full"),
                new Customer("S010200", "C020206", new DateTime(2021, 3, 1), "Cancel", "0912333550", "Main"),
                new Customer("S010201", "C020207", new DateTime(2021, 3, 1), "Normal", "0912123551", "small"),
                new Customer("S010208", "C020208", new DateTime(2022, 1, 1), "Normal", "0922210233", "Main"),
                new Customer("S010209", "C020209", new DateTime(2022, 1, 1), "Cancel", "0922210234", "Main"),
                new Customer("S010210", "C020210", new DateTime(2022, 1, 1), "Normal", "0922210235", "Main")
            };
        }

        private static Mock<DbSet<T>> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource("ForwardTestCaseData")]
        public void GetAccountDetailsByPhoneAndRegion(string phone, string region, AccountDetailDto expectAccountDetail)
        {
            var customerDbSet = GetMockDbSet(FakeCustomerList());
            var memberDbSet = GetMockDbSet(FakeMemberList());

            //var options = new Mock<DbContextOptions<WorldFamilyDbContext>>();
            var mockDbContext = new Mock<WorldFamilyDbContext>();
            mockDbContext.Setup(c => c.Customers).Returns(customerDbSet.Object);
            mockDbContext.Setup(c => c.Members).Returns(memberDbSet.Object);

            userService = new UserService(mockDbContext.Object);

            var accountDetail = userService.GetAccountDetailsByPhoneAndRegion(phone, region);
            Assert.AreEqual(JsonSerializer.Serialize(expectAccountDetail), JsonSerializer.Serialize(accountDetail));
        }

        public static IEnumerable ForwardTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                    "0912333550", "TW",
                    new AccountDetailDto("0912333550", "TW", false, "S010200", new DateTime(2022, 1, 2, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020202" })
                );
                yield return new TestCaseData(
                    "0912123551", "TW",
                    new AccountDetailDto("0912123551", "TW", true, "S010201", new DateTime(2022, 7, 3, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020201", "C020207" })
                );
                yield return new TestCaseData(
                    "0912333551", "TW",
                    new AccountDetailDto("0912333551", "TW", false, "S010203", new DateTime(2022, 7, 4, 16, 0, 0, DateTimeKind.Utc), false
                        , new List<string>())
                );
                yield return new TestCaseData(
                    "0912333552", "TW",
                    new AccountDetailDto("0912333552", "TW", false, "S010204", new DateTime(2022, 2, 28, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020204" })
                );
                yield return new TestCaseData(
                    "0912333553", "TW",
                    new AccountDetailDto("0912333553", "TW", false, "S010205", new DateTime(2022, 3, 1, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020205" })
                );
                yield return new TestCaseData(
                    "0922210233", "TW",
                    new AccountDetailDto("0922210233", "TW", true, "S010208", new DateTime(2022, 12, 31, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020208" })
                );
                yield return new TestCaseData(
                    "0922210234", "TW",
                    new AccountDetailDto("0922210234", "TW", false, "S010209", new DateTime(2023, 1, 1, 16, 0, 0, DateTimeKind.Utc), false
                        , new List<string>())
                );
                yield return new TestCaseData(
                    "0922210235", "TW",
                    new AccountDetailDto("0922210235", "TW", true, "S010210", new DateTime(2023, 1, 2, 16, 0, 0, DateTimeKind.Utc), true
                        , new List<string> { "C020210" })
                );
                yield return new TestCaseData(
                    "0922210288", "TW", null
                );
            }
        }
    }
}