using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualHotel.Data
{
    public static class InMemoryRepository
    {
        private static readonly List<IdentityUser> AuthorizedPeople;

        static InMemoryRepository()
        {
            AuthorizedPeople = new List<IdentityUser>() {
                new IdentityUser() { AuthorizedPersonType = AuthorizedPersonType.Guest, Email = "john.doe@email.com", Name = "John Doe", Salt="ViwqUjMoUgI=", PasswordHash = "uV/cBQ6KRLJeVc8PWAAeDblCQAoRW4hydDxNrmJlck0=" },
                new IdentityUser() { AuthorizedPersonType = AuthorizedPersonType.KitchenEmployee, Email = "gordon.ramsay@email.com", Name = "Gordon Ramsay", Salt="BbQiQQI3dE0=", PasswordHash = "PSw/B7GhL0i39E7AYa785ap+ZJnbltaALa6QT9JSnsA=" },
                new IdentityUser() { AuthorizedPersonType = AuthorizedPersonType.Security, Email = "derek.boyer@email.com", Name = "Derek Boyer", Salt="32GpIk31JoM=", PasswordHash = "5SrmfJvaruryOfu8MHFznJKypQEXZ2HyUFp2/qSIfME=" },
                new IdentityUser() { AuthorizedPersonType = AuthorizedPersonType.Management, Email = "marry.barra@email.com", Name = "Marry Barra", Salt="h65Gy+H4tFU=", PasswordHash = "Ir7T2lJw9X4qx+Z24PkR6LXRLqrSp1z2QkIgo7Gwlxg=" }
                };
        }

        public static IdentityUser GetUser(string email)
        {
            return AuthorizedPeople.FirstOrDefault(o => o.Email == email);
        }


    }
}
