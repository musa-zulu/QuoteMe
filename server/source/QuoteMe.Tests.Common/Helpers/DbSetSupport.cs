using NSubstitute;
using System.Collections.Generic;
using System.Data.Entity;

namespace QuoteMe.Tests.Common.Helpers
{
    public static class DbSetSupport<T> where T : class, new()
    {
        public static IDbSet<T> CreateDbSetWithAddRemoveSupport(List<T> entityList)
        {
            var dbSet = Substitute.For<IDbSet<T>>();

            dbSet.Add(Arg.Any<T>()).Returns(info =>
            {
                entityList.Add(info.ArgAt<T>(0));
                return info.ArgAt<T>(0);
            });

            dbSet.Remove(Arg.Any<T>()).Returns(info =>
            {
                entityList.Remove(info.ArgAt<T>(0));
                return info.ArgAt<T>(0);
            });

            dbSet.GetEnumerator().Returns(_ => entityList.GetEnumerator());
            return dbSet;
        }
    }
}
